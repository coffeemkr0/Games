using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.DataAccesors
{
    class UserDataAccessor : DataAccessorBase
    {
        #region Constructors
        /// <summary>
        /// Creates an instance of the UserDataAccessor class that will use the underlying data store specified in the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the underlying data store.
        /// The connection string format should be based on the data source type.</param>
        public UserDataAccessor(string connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region Public Methods
        public bool ValidateUserCredentials(string username, string password, out Business.UserAccount userAccount)
        {
            //TODO:Encrypt/Decrypt password
            using (Data.Entities entities = GetEntities())
            {
                bool authenticated;
                authenticated = entities.Users.Where(u => u.UserName == username && u.Password == password).SingleOrDefault<LegendsOfKesmaiSurvival.Data.User>() != null;

                if (!authenticated)
                {
                    userAccount = null;
                    return false;
                }
                else
                {
                    userAccount = GetUserAccount(username);
                    return true;
                }
            }
        }

        public Business.UserAccount GetUserAccount(string userName)
        {
            using (Data.Entities entities = GetEntities())
            {
                Data.User userEntity = entities.Users.Where(u => u.UserName == userName).SingleOrDefault<LegendsOfKesmaiSurvival.Data.User>();
                if (userEntity == null) return null;

                return new Business.UserAccount(userEntity.Id, userEntity.UserName, userEntity.NickName);
            }
        }

        public void CreateUserAccount(string userName, string nickName, string password)
        {
            using (Data.Entities entities = GetEntities())
            {
                Data.User userEntity = new Data.User()
                {
                    UserName = userName,
                    NickName = nickName,
                    Password = password,
                };

                entities.Users.AddObject(userEntity);
                entities.SaveChanges();
            }
        }

        public bool UserNameExists(string userName)
        {
            return GetUserAccount(userName) != null;
        }

        public bool NickNameExists(string nickName)
        {
            using (Data.Entities entities = GetEntities())
            {
                Data.User userEntity = entities.Users.Where(u => u.NickName == nickName).SingleOrDefault<LegendsOfKesmaiSurvival.Data.User>();
                return userEntity != null;
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
