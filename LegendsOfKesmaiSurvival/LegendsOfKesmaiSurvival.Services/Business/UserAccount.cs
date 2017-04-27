using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business
{
    public class UserAccount
    {
        /// <summary>
        /// Gets or sets the unique numeric Id of the user account
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name (login) for the account
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the nickname that will be shown to other users in the lobby etc.
        /// </summary>
        public string Nickname { get; set; }

        public UserAccount()
        {

        }

        public UserAccount(int id, string userName, string nickName)
        {
            this.Id = id;
            this.UserName = userName;
            this.Nickname = nickName;
        }
    }
}
