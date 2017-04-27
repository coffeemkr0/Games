using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Utility
{
    /// <summary>
    /// Holds configuration settings for the client.
    /// </summary>
    public class Config
    {
        #region Properties
        private string _serverAddress =  "net.tcp://" + System.Environment.MachineName + ":5200/LegendsOfKesmaiSurvival";
        /// <summary>
        /// Gets or sets the address of the server that the client should talk to
        /// </summary>
        public string ServerAddress
        {
            get { return _serverAddress; }
            set { _serverAddress = value; }
        }
        #endregion

        #region Public Methods
        public string ToXml()
        {
            Type type = this.GetType();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);

            System.IO.StringWriter s = new System.IO.StringWriter();
            xs.Serialize(s, this);

            return s.ToString();
        }

        public static Config LoadFromXml(string xml)
        {
            if (xml.Trim() == "") return new Config();

            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Config));

            System.IO.StringReader s = new System.IO.StringReader(xml);

            Config obj = (Config)xs.Deserialize(s);

            return obj;
        }

        public Config Clone()
        {
            return Config.LoadFromXml(this.ToXml());
        }
        #endregion
    }
}
