using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Utility
{
    class Global
    {
        #region Properties
        //TODO:Need to be able to configure the connection string
        private static string _connectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=LegendsOfKesmaiSurvival;Integrated Security=True;Persist Security Info=True";
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        #endregion

        #region Public Methods
        public static bool IsClientAuthenticated(Guid key)
        {
            return GetClientUser(key) != null;
        }

        public static Business.UserAccount GetClientUser(Guid key)
        {
            //TODO:This seems inefficient.  Need to find a better way to see if a client has been authenticated rather than
            //iterating a list of keys on every method.
            //This also means that every method from the client has to pass additional data in the form of a Guid
            if (!Authentication.AuthenticationService.AuthenticatedClients.ContainsKey(key))
            {
                return null;
            }

            return Authentication.AuthenticationService.AuthenticatedClients[key].UserAccount;
        }
        #endregion

        #region Data Accessor Helpers
        public static DataAccesors.UserDataAccessor GetUserDataAccessor()
        {
            return new DataAccesors.UserDataAccessor(ConnectionString);
        }
        #endregion
    }
}
