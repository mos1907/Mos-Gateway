using Mos.Entities;
using Mos.Utilities;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml;
using System.CodeDom;
using System.Windows.Forms;
using Message = SimpleTCP.Message;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Net.Sockets;


namespace TCPApp
{
    enum MessageType
    {
        REPLY,
        UPDATE
    }

    public class MosServerException : Exception
    {
        public MosServerException() : base() { }
        public MosServerException(string message) : base(message) { }
        public MosServerException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected MosServerException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
    public class MosServer
    {
        Form1 former = new Form1();
        public MosServer()
        {

        }
        public MosServer(int port, string ncsID, List<MosClient> mosClients)
        {
            Port = port;
            NcsID = ncsID;
            MosClients = mosClients;
        }

        public int Port { get; set; }
        public string NcsID { get; set; }
        public List<MosClient> MosClients { get; set; }

        public void Start()
        {
            try
            {
               
                ConnectMosClients();
                StartServer();
            }

            catch (Exception ex)
            {
                throw new MosServerException("error starting ncs server!", ex);
            }
        }

        public void SendMosCommand(mos mosCommand)
        {
            Port = 10541;
            NcsID = former.NcsName;
            MosClients = new List<MosClient>() {
                    new MosClient{  IP=former.serverip, UpperPort=10541, MosID=former.mosid} };

            foreach (var mosClient in MosClients)
            {
                mosClient.MessageQueue.Enqueue(mosCommand);
                SimpleTcpClient client = null;
                if (mosClient.MessageQueue.Count > 0)
                {
                    if (client == null)
                    {
                        client = new SimpleTcpClient();
                        client.StringEncoder = Encoding.BigEndianUnicode;
                        client.Connect(mosClient.IP, mosClient.UpperPort);
                    }
                    if (client.TcpClient.Connected)
                    {
                        var mosObj = mosClient.MessageQueue.Peek().SerializeObject();
                        var message = client.WriteLineAndGetReply(mosObj, TimeSpan.FromMilliseconds(1000));
                        RaiseEvents(message, MessageType.REPLY);
                        mosClient.MessageQueue.Dequeue();
                    }
                }
            }
        }

        public void SendAck(string RoID)
        {
            SendMosCommandACK(new mos()
            {
                ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.mosID, ItemsChoiceType3.ncsID, ItemsChoiceType3.roAck },
                Items = new object[] { former.mosid, former.NcsName, new roAck() { roID = RoID, roStatus = "OK" } }
            });
        }
        int messageID;
        public void SendroReq(string RoID)
        {
            messageID++;
            SendMosCommandNoReply(new mos()
            {
                ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.mosID, ItemsChoiceType3.ncsID, ItemsChoiceType3.messageID, ItemsChoiceType3.roReq },
                Items = new object[] { former.mosid, former.NcsName, messageID, new roReq { roID = RoID } }
            });
        }
        public void SendRoReqAll()
        {
            messageID++;
            SendMosCommandNoReply(new mos()
            {
                ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.mosID, ItemsChoiceType3.ncsID, ItemsChoiceType3.messageID, ItemsChoiceType3.roReqAll },
                Items = new object[] { former.mosid, former.NcsName, messageID, new roReqAll { } }
            });
        }

        public void SendMosCommandNoReply(mos mosCommand)
        {
            Port = 10541;
            NcsID = former.NcsName;
            MosClients = new List<MosClient>() {
                    new MosClient{  IP=former.serverip, UpperPort=10541, MosID=former.mosid} };
            foreach (var mosClient in MosClients)
            {
                mosClient.MessageQueue.Enqueue(mosCommand);
                SimpleTcpClient client = null;
                if (mosClient.MessageQueue.Count > 0)
                {
                    if (client == null)
                    {
                        client = new SimpleTcpClient();
                        client.StringEncoder = Encoding.BigEndianUnicode;
                        client.Connect(mosClient.IP, mosClient.UpperPort);
                    }
                    if (client.TcpClient.Connected)
                    {                        
                        var mosObj = mosClient.MessageQueue.Peek().SerializeObject();                        
                        client.WriteLine(mosObj);
                        client.DataReceived += Client_DataReceived;
                        client.DelimiterDataReceived+= Client_DelimiterDataReceived;
                        mosClient.MessageQueue.Dequeue();
                    }
                }
            }
        }

        public void SendMosCommandACK(mos mosCommand)
        {
            Port = 10541;
            NcsID = former.NcsName;
            MosClients = new List<MosClient>() {
                    new MosClient{  IP=former.serverip, UpperPort=10541, MosID=former.mosid} };
            foreach (var mosClient2 in MosClients)
            {
                mosClient2.MessageQueue.Enqueue(mosCommand);
                SimpleTcpClient client2 = null;
                if (mosClient2.MessageQueue.Count > 0)
                {
                    if (client2 == null)
                    {
                        client2 = new SimpleTcpClient();
                        client2.StringEncoder = Encoding.BigEndianUnicode;
                        client2.Connect(mosClient2.IP, mosClient2.UpperPort);
                    }
                    if (client2.TcpClient.Connected)
                    {
                        var mosObj = mosClient2.MessageQueue.Peek().SerializeObject();
                        client2.WriteLine(mosObj);
                        mosClient2.MessageQueue.Dequeue();
                    }
                }
            }
        }
        public event EventHandler<MosClient> clientConnected;
        public event EventHandler<MosServer> lowerPortsServiceStarted;
        public event EventHandler<mos> ReplyReceivedfromClient;
        public event EventHandler<mos> UpdateReceivedfromClient;
        public event EventHandler<roAck> RoAckReceived;
        public event EventHandler<heartbeat> HeartbeatReceived;
        public event EventHandler<mosObj> MosObjReceived;
        public event EventHandler<roReqAll> RoReqAllReceived;
        public event EventHandler<roCreate> RoCreateReceived;
        public event EventHandler<roList> RoListReceived;
        public event EventHandler<roReplace> RoReplaceReceived;
        public event EventHandler<roDelete> RoDeleteReceived;
        //Story Events Handler ---->>>
        public event EventHandler<roStoryAppend> RoStoryAppendReceived;
        public event EventHandler<roStoryInsert> RoStoryInsertReceived;
        public event EventHandler<roStoryReplace> RoStoryReplaceReceived;
        public event EventHandler<roStoryMove> RoStoryMoveReceived;
        public event EventHandler<roStorySwap> RoStorySwapReceived;
        public event EventHandler<roStoryDelete> RoStoryDeleteReceived;
        public event EventHandler<roStoryMoveMultiple> RoStoryMoveMultipleReceived;
        public event EventHandler<roStorySend> RoStorySendReceived;
        public event EventHandler<roItemInsert> RoItemInsertReceived;
        public event EventHandler<roItemReplace> RoItemReplaceReceived;
        public event EventHandler<roItemMoveMultiple> RoItemMoveMultipleReceived;
        public event EventHandler<roItemDelete> RoItemDeleteReceived;
        public event EventHandler<roElementAction> RoElementActionReceived;
        // Story Events Handler ---|||
        // Item Events Handler
        public event EventHandler<mos> MosReceived;
        private void ConnectMosClients()
        {
            SimpleTcpClient client = null;
            string errorMessage = default(string);
            if (MosClients == null) return;
            if (MosClients.Count < 1) throw new MosServerException("no MOS client exist");
            foreach (var mosClient in MosClients)
            {
                #region Dequeue task
                Task.Run(() =>
                {
                    while (true)
                    {
                        try
                        {
                            if (mosClient.MessageQueue.Count > 0)
                            {

                                if (client == null)
                                {
                                    client = new SimpleTcpClient();
                                    client.StringEncoder = Encoding.BigEndianUnicode;
                                    client.DataReceived += Client_DataReceived;
                                    client.DelimiterDataReceived += Client_DelimiterDataReceived;
                                    client.Connect(mosClient.IP, mosClient.UpperPort);
                                }
                                else if (!client.TcpClient.Connected)
                                {
                                    client.Connect(mosClient.IP, mosClient.UpperPort);
                                }
                                else
                                {                                   
                                    var mosObj = mosClient.MessageQueue.Peek().SerializeObject();
                                    var message = client.WriteLineAndGetReply(mosObj, TimeSpan.FromMilliseconds(5000));                                   
                                    RaiseEvents(message, MessageType.REPLY);
                                    mosClient.MessageQueue.Dequeue();
                                    //log.Info(string.Format("MESSAGE SENT TO MOS DEVICE {0}\n {1}", host, mosObj));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (errorMessage != ex.Message)
                            {
                                errorMessage = ex.Message;
                                throw new MosServerException(string.Format("error connecting mos client {0}", mosClient.IP), ex);
                            }
                        }
                        Thread.Sleep(10);
                    }
                });
                #endregion

                #region heartbeat Task
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (mosClient.MessageQueue.Count < 1) // if queue is empty
                        {
                            mosClient.MessageQueue.Enqueue(
                            new mos()
                            {
                                ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.mosID, ItemsChoiceType3.ncsID, ItemsChoiceType3.heartbeat },
                                Items = new object[] { mosClient.MosID, former.NcsName, new heartbeat() { time = DateTime.Now.ToString() } }
                            });
                        }
                        Thread.Sleep(5000);
                    }
                });
                #endregion heartbeat Task
            }
        }

        private void Client_DelimiterDataReceived(object sender, Message e)
        {
            RaiseEvents(e, MessageType.REPLY);
        }

        private void Client_DataReceived(object sender, Message e)
        {
            RaiseEvents(e, MessageType.REPLY);
        }

        private void Server_DelimiterDataReceived(object sender, Message e)
        {
            RaiseEvents(e, MessageType.UPDATE);
        }

        private void Server_DataReceived(object sender, Message e)
        {
            e.Reply(Encoding.BigEndianUnicode.GetBytes(DateTime.Now.ToString()));
            RaiseEvents(e, MessageType.UPDATE);
        }

        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            throw new NotImplementedException();
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            //clientConnected?.Invoke(sender,)
        }

        SimpleTcpServer _server = null;
        private void StartServer()
        {
            
            try
            {
                if (Port == default(int)) throw new MosServerException("port not specified");
                if (NcsID == default(string)) throw new MosServerException("ncs id not specified");
                _server = new SimpleTcpServer();
                Task.Run(() =>
                {
                    _server.ClientConnected += Server_ClientConnected;
                    _server.ClientDisconnected += Server_ClientDisconnected; ;
                    _server.DataReceived += Server_DataReceived; ;
                    _server.DelimiterDataReceived += Server_DelimiterDataReceived; ;
                    _server.StringEncoder = Encoding.BigEndianUnicode;
                    _server.AutoTrimStrings = true;
                    _server.Start(Port);
                });
            }
            catch (Exception ex)
            {
                throw new MosServerException("cannot listen to port " + Port, ex);
            }
        }
        int say;
       
        private void RaiseEvents(Message message, MessageType type)
        {
            if(message != null)
            {
                var client = GetMosClientFromTcpClient(message.TcpClient);
                if (client == null) return;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(message.MessageString);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                dynamic jsonObj = JsonConvert.DeserializeObject(jsonText);
                dynamic o = JObject.Parse(jsonText);
                var mosObject = Mos.Utilities.Extensions.DeserializeFromString<mos>(message.MessageString);
                if (mosObject != null)
                {
                    if (mosObject.Items.Length > 2)
                    {
                        object mosInnerObject = mosObject.Items[2];
                        string typex = mosObject.Items[2].ToString();                      
                        switch (typex)
                        {
                            case "Mos.Entities.roAck":
                                RoAckReceived?.Invoke(client, (roAck)mosInnerObject);
                                break;
                            case "Mos.Entities.heartbeat":
                                HeartbeatReceived?.Invoke(client, (heartbeat)mosInnerObject);
                                break;
                            case "Mos.Entities.roCreate":
                                RoCreateReceived?.Invoke(client, (roCreate)mosInnerObject);
                                string RoCreateID = o.mos.roCreate.roID;
                                File.WriteAllText(@"" + former.rundwonpath + "\\" + RoCreateID + ".json", jsonText);
                                SendAck(RoCreateID);
                                break;
                            case "Mos.Entities.roList":
                                RoListReceived?.Invoke(client, (roList)mosInnerObject);
                                string rolistID = o.mos.roList.roID;
                                File.WriteAllText(@"" + former.rundwonpath + "\\" + rolistID + ".json", jsonText);
                                break;
                            case "Mos.Entities.roReplace":
                                RoReplaceReceived?.Invoke(client, (roReplace)mosInnerObject);
                                string roReplaceID = o.mos.roReplace.roID;
                                File.WriteAllText(@"" + former.rundwonpath + "\\" + roReplaceID + ".json", jsonText);
                                SendAck(roReplaceID);
                                break;
                            case "Mos.Entities.roDelete":
                                string roIDDelete = o.mos.roDelete.roID;
                                RoDeleteReceived?.Invoke(client, (roDelete)mosInnerObject);
                                SendAck(roIDDelete);
                                break;
                            case "Mos.Entities.roStoryAppend":
                                Thread.Sleep(100);
                                string roIdfromAppend = o.mos.roStoryAppend.roID;
                                RoStoryAppendReceived?.Invoke(client, (roStoryAppend)mosInnerObject);
                                SendAck(roIdfromAppend);
                                SendroReq(roIdfromAppend);
                                break;
                            case "Mos.Entities.roStoryDelete":
                                string roIdFromstoryDelete = o.mos.roStoryDelete.roID;
                                RoStoryDeleteReceived?.Invoke(client, (roStoryDelete)mosInnerObject);
                                SendAck(roIdFromstoryDelete);
                                SendroReq(roIdFromstoryDelete);
                                break;
                            case "Mos.Entities.roStoryInsert":
                                Thread.Sleep(100);
                                string roIdStoryInsert = o.mos.roStoryInsert.roID;
                                RoStoryInsertReceived?.Invoke(client, (roStoryInsert)mosInnerObject);
                                SendAck(roIdStoryInsert);
                                SendroReq(roIdStoryInsert);
                                break;
                            case "Mos.Entities.roStoryMove":
                                string roIdstorymove = o.mos.roStoryMove.roID;
                                RoStoryMoveReceived?.Invoke(client, (roStoryMove)mosInnerObject);
                                SendAck(roIdstorymove);
                                SendroReq(roIdstorymove);
                                break;
                            case "Mos.Entities.roStoryMoveMultiple":
                                string roIdStoryMoveMultiple = o.mos.roStoryMoveMultiple.roID;
                                RoStoryMoveMultipleReceived?.Invoke(client, (roStoryMoveMultiple)mosInnerObject);
                                SendAck(roIdStoryMoveMultiple);
                                SendroReq(roIdStoryMoveMultiple);
                                break;
                            case "Mos.Entities.roStoryReplace":
                                string roIdstoryReplace = o.mos.roStoryReplace.roID;
                                RoStoryReplaceReceived?.Invoke(client, (roStoryReplace)mosInnerObject);
                                SendAck(roIdstoryReplace);
                                Thread.Sleep(100);
                                SendroReq(roIdstoryReplace);
                                break;
                            case "Mos.Entities.roReqAll":
                                RoReqAllReceived?.Invoke(client, (roReqAll)mosInnerObject);
                                break;
                            case "Mos.Entities.mosObj":
                                MosObjReceived?.Invoke(client, (mosObj)mosInnerObject);
                                break;
                        }
                        if (type == MessageType.REPLY)
                            ReplyReceivedfromClient?.Invoke(client, (mos)mosObject);
                        if (type == MessageType.UPDATE)
                            UpdateReceivedfromClient?.Invoke(client, (mos)mosObject);
                        MosReceived?.Invoke(client, mosObject);
                    }
                }
            }         
        }
        private MosClient GetMosClientFromTcpClient(System.Net.Sockets.TcpClient client)
        {
            var address = IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
            var ip = ((IPEndPoint)client.Client.RemoteEndPoint).Port;

            return MosClients.Find(x => x.IP == address.ToString());
        }
    }
}
