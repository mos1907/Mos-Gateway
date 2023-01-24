using Mos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TCPApp
{
    [XmlRoot("mos")]
    public class RoreqCommands
    {
        string _mosID, _ncsID;
        long _messageID;
        croID _roID;

        public string mosID
        {
            get { return _mosID; }
            set { _mosID = value; }
        }

        public string ncsID
        {
            get { return _ncsID; }
            set { _ncsID = value; }
        }

        public long messageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }
        public croID roID        {
            get { return _roID; }
            set { _roID = value; }
        }
    }

    [XmlRoot("roID")]
    public class croID
    {
        private string roIDField;

        /// <remarks/>
        public string roID
        {
            get
            {
                return this.roIDField;
            }
            set
            {
                this.roIDField = value;
            }
        }
    }
}
