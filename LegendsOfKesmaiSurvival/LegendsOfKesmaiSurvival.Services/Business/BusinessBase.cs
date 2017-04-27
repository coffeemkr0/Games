using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business
{
    /// <summary>
    /// A class that other business objects can inherit from to get common members that most business objects have.
    /// This class can greatly reduce a lot of duplicated code all over the project.
    /// </summary>
    [Serializable()]
    public class BusinessBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets a numeric Id for the business object.
        /// Most business objects get an Id to help identify them in a list of other business objects or
        /// to help with data store operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a name for the business object.
        /// Most business objects get a name that can be used for a variety of purposes such as the name of an NPC or the name of a weapon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description of the business object.
        /// Most business objects need a description that can be shown with the Name to help describe it.
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Constructors
        public BusinessBase()
        {

        }

        public BusinessBase(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
        #endregion
    }
}
