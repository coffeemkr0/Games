using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters;

namespace LegendsOfKesmaiSurvival.Services.Business.Maps
{
    public class WallImageNameTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
               GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(LegendsOfKesmaiSurvival.Content.ContentManager.GetWalls());
        }
    }

    public class DoorImageNameTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
               GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(LegendsOfKesmaiSurvival.Content.ContentManager.GetDoors());
        }
    }

    public class InsurmountableObjectTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
               GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(LegendsOfKesmaiSurvival.Content.ContentManager.GetUnpassableObjects());
        }
    }

    public class ObjectTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
               GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(LegendsOfKesmaiSurvival.Content.ContentManager.GetObjects());
        }
    }

    /// <summary>
    /// This class is used to help the map editor show a drop down of NPC Types in a property grid.
    /// </summary>
    public class NonPlayableCharacterTypesTypeConverter : EnumConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, 
            //but allow free-form entry
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
               GetStandardValues(ITypeDescriptorContext context)
        {
            List<NonPlayerCharacterTypes> types = new List<NonPlayerCharacterTypes>();
            foreach (NonPlayerCharacterTypes type in Enum.GetValues(typeof(NonPlayerCharacterTypes)))
            {
                types.Add(type);
            }

            return new StandardValuesCollection(types);
        }

        public NonPlayableCharacterTypesTypeConverter()
            : base(typeof(NonPlayerCharacterTypes))
        {

        }
    }
}
