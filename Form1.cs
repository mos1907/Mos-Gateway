using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mos;
using Mos.Entities;
using Ini;
using System.Net.Sockets;
using System.Xml;
using SimpleTCP;
using System.Xml.Serialization;
using System.Net;

namespace TCPApp
{
    public partial class Form1 : Form
    {

        IniFile ini = new IniFile(Environment.CurrentDirectory + "\\Config.ini");
        public string NcsName;
        public string mosid;
        public string serverip;
        public string rundwonpath;
        public Form1()
        {
            InitializeComponent();
            var maxThread = 90;
            ThreadPool.SetMaxThreads(maxThread, maxThread * 2);
            NcsName = ini.IniReadValue("Settings", "NCSNAME");
            mosid = ini.IniReadValue("Settings", "MOSID");
            serverip = ini.IniReadValue("Settings", "MOSSERVERIP");
            rundwonpath = ini.IniReadValue("Settings", "PATH");          
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => StartPort1());
        }
        public void StartPort1()
        {

            try
            {
                MosServer ncsServer = new MosServer()
                {
                    Port = 10541,
                    NcsID = NcsName,
                    MosClients = new List<MosClient>() {
                    new MosClient{  IP=serverip, UpperPort=10541, MosID=mosid}
                }
                };
                ncsServer.MosReceived += NcsServer_MosReceived;
                ncsServer.RoCreateReceived += NcsServer_RoCreateReceived;
                ncsServer.RoDeleteReceived += NcsServer_RoDeleteReceived;
                ncsServer.RoListReceived += NcsServer_RoListReceived;
                ncsServer.RoReplaceReceived += NcsServer_RoReplaceReceived;
                ncsServer.Start();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString(), "Red");
            }
        }

       

        private void NcsServer_RoDeleteReceived(object sender, roDelete e)
        {
            try
            {
                var path = @"" + rundwonpath + "\\" + e.roID + ".json";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                WriteLog("Rundown Deleted From  " + path + "", "Red");
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString(), "Red");
            }
        }int counterheartbeat = 0;
        private void NcsServer_MosReceived(object sender, mos e)
        {
            if (e.Items[2].ToString() == "Mos.Entities.heartbeat")
            {
                if(counterheartbeat == 0)
                {
                    WriteLog("Message received from " + ((MosClient)sender).IP + " " + e.Items[2].ToString(), "Heart");
                    counterheartbeat = 1;
                }
                else
                {
                    counterheartbeat = 0;
                }              
            }
            else
            {
                WriteLog("Message received from " + ((MosClient)sender).IP + " " + e.Items[2].ToString(), "Normal");
            }
        
        }
        private void NcsServer_RoCreateReceived(object sender, roCreate e)
        {
            var path = @"" + rundwonpath + "\\" + e.roID + ".json";
            WriteLog("New Rundown Downloaded to " + path + "", "Green");
        }

        private void NcsServer_RoListReceived(object sender, roList e)
        {
            var path = @"" + rundwonpath + "\\" + e.roID + ".json";
            WriteLog("Rundown Updated List Downloaded to " + path + "", "Green");
        }
        private void NcsServer_RoReplaceReceived(object sender, roReplace e)
        {
            var path = @"" + rundwonpath + "\\" + e.roID + ".json";
            WriteLog("Rundown Rpelaced List Downloaded to " + path + "", "Green");
        }
        public void WriteLog(string info, string color)
        {
            if (color == "Green")
            {
                this.Invoke((MethodInvoker)delegate { TxtLog.SelectionColor = Color.Green; TxtLog.AppendText(DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + " -----> " + info + "\r\n"); TxtLog.ScrollToCaret(); TxtLog.Update(); });
            }
            else if (color == "Normal")
            {
                this.Invoke((MethodInvoker)delegate { TxtLog.ForeColor = Color.SlateGray; TxtLog.AppendText(DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + " -----> " + info + "\r\n"); TxtLog.ScrollToCaret(); TxtLog.Update(); });
            }
            else if (color == "Red")
            {
                this.Invoke((MethodInvoker)delegate { TxtLog.SelectionColor = Color.Red; TxtLog.AppendText(DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + " -----> " + info + "\r\n"); TxtLog.ScrollToCaret(); TxtLog.Update(); });
            }
            else if (color == "Heart")
            {
                this.Invoke((MethodInvoker)delegate { TxtHeartBeat.SelectionColor = Color.SlateGray; TxtHeartBeat.AppendText(DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + " -----> " + info + "\r\n"); TxtHeartBeat.ScrollToCaret(); TxtHeartBeat.Update(); });
            }
        }
        int msgID;
        private void RoAck_Click(object sender, EventArgs e)
        {           
            try
            {
                MosServer ncsServer = new MosServer();
                ncsServer.SendMosCommand(new mos()
                {
                    ItemsElementName = new ItemsChoiceType3[] { ItemsChoiceType3.mosID, ItemsChoiceType3.ncsID, ItemsChoiceType3.messageID, ItemsChoiceType3.mosObj },
                    Items = new object[] { "CINEGY", "TINEWS", "" + msgID + "", new mosObj { objID = "", objSlug = "", mosAbstract = "", objGroup = "", objType = objType.VIDEO, objTB = 50, 
                    objRev = 1, objDur = 500, status = "NEW", objAir = objAir.READY, objPaths = new objPaths { objProxyPath =  new objProxyPath[] { new objProxyPath {techDescription="" ,
                    Value=""} } } , createdBy="Mmam_PlaceHolder_Creator", changed = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"), changedBy = "Mmam_PlaceHolder_Creator", created = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"),
                     description = new description{}, } }
                }); 
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString(), "Normal");
            }
        }

        private void SettingsMenu_Click(object sender, EventArgs e)
        {
            Settings f2 = new Settings();
            f2.ShowDialog();
        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            Help h2 = new Help();
            h2.ShowDialog();
        }

        private void TxtHeartBeat_TextChanged(object sender, EventArgs e)
        {
            var lineCount = TxtHeartBeat.Lines.Count();
            if(lineCount == 25)
            {               
                TxtHeartBeat.Clear(); 
            }
            Console.WriteLine(lineCount.ToString()); 
        }
    }
}
