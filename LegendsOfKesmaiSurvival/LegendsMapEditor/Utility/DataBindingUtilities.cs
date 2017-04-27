using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MapEditor.Utility
{
    public static class DataBindingUtilities
    {
        public static void SuspendTwoWayBinding(BindingManagerBase bindingManager)
        {
            if (bindingManager == null)
            {
                throw new ArgumentNullException("bindingManager");
            }
            foreach (Binding b in bindingManager.Bindings)
            {
                b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
            }
        }

        public static void UpdateDataBoundObject(BindingManagerBase bindingManager)
        {
            if (bindingManager == null)
            {
                throw new ArgumentNullException("bindingManager");
            }
            foreach (Binding b in bindingManager.Bindings)
            {
                b.WriteValue();
            }
        }
    }
}
