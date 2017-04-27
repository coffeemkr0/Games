using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Runtime.Serialization.Formatters.Binary;

namespace LegendsOfKesmaiSurvival.Services.DataAccesors
{
    /// <summary>
    /// A class that contains base functionality for Data Accessors.
    /// </summary>
    public abstract class DataAccessorBase
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Creates an instance of the Data Accessor that will use the underlying data store specified in the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the underlying data store.
        /// The connection string format should be based on the data source type.</param>
        public DataAccessorBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets an instance of the Data.Entities class.
        /// </summary>
        /// <returns>An instance of the Entities class with a connection to the underlying data store specified in the ConnectionString.</returns>
        protected Data.Entities GetEntities()
        {
            EntityConnectionStringBuilder connectionStringBuilder = new EntityConnectionStringBuilder();
            connectionStringBuilder.Provider = "System.Data.SqlClient";
            connectionStringBuilder.Metadata = @"res://*/LegendsOfKesmaiSurvivalDataModel.csdl|res://*/LegendsOfKesmaiSurvivalDataModel.ssdl|res://*/LegendsOfKesmaiSurvivalDataModel.msl";
            connectionStringBuilder.ProviderConnectionString = this.ConnectionString;

            return new Data.Entities(connectionStringBuilder.ConnectionString);
        }

        /// <summary>
        /// Serialize an object into a byte array.
        /// </summary>
        /// <param name="item">The object to serialize.</param>
        /// <returns>An array of bytes that contains the serialization of the object.</returns>
        protected byte[] BinarySerialize(object item)
        {
            BinaryFormatter binaryFormater = new BinaryFormatter();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                binaryFormater.Serialize(ms, item);
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Deserializes an object from a byte array.
        /// </summary>
        /// <param name="data">The array of bytes that contains the serialization of the object.</param>
        /// <returns>An object that has been deserialized from the data.</returns>
        protected object BinaryDeSerialize(byte[] data)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            object result = formatter.Deserialize(ms);
            ms.Close();
            return result;
        }
    }
}
