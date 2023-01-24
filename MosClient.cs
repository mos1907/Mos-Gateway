using Mos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPApp
{
    public class MosClient
    {
        public MosClient()
        {
            UpperPort = 10541;
            LowerPort = 10540;
        }

        /// <summary>
        /// IP of the MOS Client
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Upper Port for Rundown message communication: e.g. <roCreate></roCreate>, <roReq></roReq> etc
        /// </summary>
        public int UpperPort { get; set; }
        /// <summary>
        /// Lower port for updates from the MOS device: object in ready state e.g. <mosObj></mosObj>
        /// </summary>
        public int LowerPort { get; set; }
        public string MosID { get; set; }
        public Queue<mos> MessageQueue = new Queue<mos> { };
    }
}
