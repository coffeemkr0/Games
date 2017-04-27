using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace LegendsOfKesmaiSurvival.Services.Business
{
    //A general note about some stuff in this class.  This class is meant to be thread safe.
    //Basically what it boils down to is that when an object either signs up for an event or removes its delegate to handle an event,
    //the list of delegates that are signed up for the event is locked while the delegate is added or removed.
    //The [MethodImpl(MethodImplOptions.Synchronized)] attribute simply acts as a lock around anything in the method it is applied to.
    //This is the same as wrapping everything in the method with a lock on a sync object.
    //The Delegate.Combine and Delegate.Remove are calls to add and remove the delegate that is signing/de-signing up for an event.

    /// <summary>
    /// A list of BusinessBase objects with events to notify when items are Added,Edited,Removed,Inserted,Moved or when the list is Cleared
    /// This class is thread safe.
    /// </summary>
    [Serializable()]
    public class BusinessBaseList<T> : List<T> where T:BusinessBase
    {
        #region ListCleared
        public delegate void ListClearedEventHandler(object sender);
        private ListClearedEventHandler _listCleared;
        public event ListClearedEventHandler ListCleared
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _listCleared = (ListClearedEventHandler)Delegate.Combine(_listCleared, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _listCleared = (ListClearedEventHandler)Delegate.Remove(_listCleared, value);
            }
        }

        protected virtual void OnListCleared(object sender)
        {
            if (_listCleared != null)
            {
                _listCleared(sender);
            }
        }

        public new void Clear()
        {
            base.Clear();
            OnListCleared(this);
        }
        #endregion

        #region ItemAdded
        public delegate void ItemAddedEventHandler(object sender, T item);
        private ItemAddedEventHandler _itemAdded;
        public event ItemAddedEventHandler ItemAdded
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _itemAdded = (ItemAddedEventHandler)Delegate.Combine(_itemAdded, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _itemAdded = (ItemAddedEventHandler)Delegate.Remove(_itemAdded, value);
            }
        }

        protected virtual void OnItemAdded(object sender, T item)
        {
            if (_itemAdded != null)
            {
                _itemAdded(sender, item);
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnItemAdded(item, item);
        }

        public new void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }
        #endregion

        #region ItemEdited
        public delegate void ItemEditedEventHandler(object sender, string changeInfo);
        private ItemEditedEventHandler _itemEdited;
        public event ItemEditedEventHandler ItemEdited
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _itemEdited = (ItemEditedEventHandler)Delegate.Combine(_itemEdited, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _itemEdited = (ItemEditedEventHandler)Delegate.Remove(_itemEdited, value);
            }
        }
        protected virtual void OnItemEdited(object sender, string changeInfo)
        {
            if (_itemEdited != null)
            {
                _itemEdited(sender, changeInfo);
            }
        }
        #endregion

        #region ItemRemoved
        public delegate void ItemRemovedEventHandler(object sender, T item);
        private ItemRemovedEventHandler _itemRemoved;
        public event ItemRemovedEventHandler ItemRemoved
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _itemRemoved = (ItemRemovedEventHandler)Delegate.Combine(_itemRemoved, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _itemRemoved = (ItemRemovedEventHandler)Delegate.Remove(_itemRemoved, value);
            }
        }
        protected virtual void OnItemRemoved(object sender, T item)
        {
            if (_itemRemoved != null)
            {
                _itemRemoved(sender, item);
            }
        }
        public new void Remove(T item)
        {
            base.Remove(item);
            OnItemRemoved(this, item);
        }
        public new void RemoveAt(int index)
        {
            T item = this[index];
            base.RemoveAt(index);
            OnItemRemoved(this, item);
        }
        #endregion

        #region ItemInserted
        public delegate void ItemInsertedEventHandler(object sender, int index, T item);
        private ItemInsertedEventHandler _itemInserted;
        public event ItemInsertedEventHandler ItemInserted
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _itemInserted = (ItemInsertedEventHandler)Delegate.Combine(_itemInserted, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _itemInserted = (ItemInsertedEventHandler)Delegate.Remove(_itemInserted, value);
            }
        }
        protected virtual void OnItemInserted(object sender, int index, T item)
        {
            if (_itemInserted != null)
            {
                _itemInserted(this, index, item);
            }
        }
        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            OnItemInserted(item, index, item);
        }
        #endregion

        #region ItemMoved
        public delegate void ItemMovedEventHandler(string direction, T item);
        private ItemMovedEventHandler _itemMoved;
        public event ItemMovedEventHandler ItemMoved
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _itemMoved = (ItemMovedEventHandler)Delegate.Combine(_itemMoved, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _itemMoved = (ItemMovedEventHandler)Delegate.Remove(_itemMoved, value);
            }
        }
        protected virtual void OnItemMoved(string direction, T item)
        {
            if (_itemMoved != null)
            {
                _itemMoved(direction, item);
            }
        }

        /// <summary>
        /// Moves the item at the given index up one place in the list.  This decreases it's index by one.
        /// </summary>
        /// <param name="index"></param>
        public void MoveItemUpOneAt(int index)
        {
            if (index == 0) return;
            if (index > base.Count - 1) return;
            T item = base[index];
            base.RemoveAt(index);
            base.Insert(index - 1, item);
            OnItemMoved("Up", item);
        }

        /// <summary>
        /// Moves the item at the given index down one place in the list.  This increases it's index by one.
        /// </summary>
        /// <param name="index"></param>
        public void MoveItemDownOneAt(int index)
        {
            if (index >= base.Count - 1) return;
            T item = base[index];
            base.RemoveAt(index);
            base.Insert(index + 1, item);
            OnItemMoved("Down", item);
        }

        public void MoveItem(int currentIndex, int newIndex)
        {
            if (currentIndex < 0 || currentIndex > base.Count - 1) return;
            if(currentIndex == newIndex) return;

            T item = base[currentIndex];
            base.RemoveAt(currentIndex);
            base.Insert(newIndex, item);

            if (currentIndex < newIndex)
            {
                OnItemMoved("Down", item);
            }
            else
            {
                OnItemMoved("Up", item);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Does a deep clone of the list
        /// </summary>
        /// <returns></returns>
        public BusinessBaseList<T> Clone()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
            {
                binFormater.Serialize(memStream, this);
                //Reset the posotion to initial so that it can read the bytes
                memStream.Position = 0;
                return binFormater.Deserialize(memStream) as BusinessBaseList<T>;
            }
        }
        #endregion
    }
}
