using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MapEditor.Controls
{
  /// <summary>
  /// Windows Forms extender control that resolves the
  /// data refresh issue with data bound detail controls
  /// </summary>
  [DesignerCategory("")]
  [ProvideProperty("ReadValuesOnChange", typeof(BindingSource))]
    [ToolboxItem(true), ToolboxBitmap(typeof(BindingSourceRefresh), "ibml.JobSetup.UI.Controls.BindingSourceRefresh")]
  public class BindingSourceRefresh : Component, IExtenderProvider
  {

    private Dictionary<BindingSource, bool> _sources = new Dictionary<BindingSource, bool>();

    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    /// <param name="container">Container of the control.</param>
    public BindingSourceRefresh(IContainer container)
    {
      container.Add(this);
    }

    /// <summary>
    /// Gets a value indicating whether the extender control
    /// can extend the specified control.
    /// </summary>
    /// <param name="extendee">The control to be extended.</param>
    /// <remarks>
    /// This control only extends <see cref="BindingSource"/> controls.
    /// </remarks>
    public bool CanExtend(object extendee)
    {
      if (extendee is BindingSource)
        return true;
      return false;
    }

    /// <summary>
    /// Gets the value of the custom ReadValuesOnChange extender
    /// property added to extended controls.
    /// </summary>
    /// <param name="source">Control being extended.</param>
    public bool GetReadValuesOnChange(BindingSource source)
    {
      if (_sources.ContainsKey(source))
        return _sources[source];
      return false;
    }

    /// <summary>
    /// Sets the value of the custom ReadValuesOnChange extender
    /// property added to extended controls.
    /// </summary>
    /// <param name="source">Control being extended.</param>
    /// <param name="value">New value of property.</param>
    /// <remarks></remarks>
    public void SetReadValuesOnChange(
      BindingSource source, bool value)
    {
      if (_sources.ContainsKey(source))
        _sources[source] = value;
      else
        _sources.Add(source, value);
      if (value)
      {
        // hook
        source.BindingComplete += 
          new BindingCompleteEventHandler(Source_BindingComplete);
      }
      else
      {
        // unhook
        source.BindingComplete -= 
          new BindingCompleteEventHandler(Source_BindingComplete);
      }
    }

    private void Source_BindingComplete(
      object sender, BindingCompleteEventArgs e)
    {
      e.Binding.ReadValue();
    }
  }
}
