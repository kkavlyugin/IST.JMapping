using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IST.JMapping
{
    public abstract class SupportIEntityCollection<T> : IEnumerable<T> where T : IEntity
    {
        private readonly List<T> _list = new List<T>();
        protected int ItemsCount { get { return _list.Count; } }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _list)
                yield return item;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        protected bool AddItem(T item)
        {
            if (_list.Exists(ByName(item.Name)))
                return false;
            _list.Add(item); //_list.ElementAt(1);
            return true;
        }

        protected List<T> GetItem()
        {
            return _list;
        }

        protected bool ExistsItem(string name)
        {
            return _list.Exists(ByName(name));
        }
        protected void Remove(T item)
        {
            _list.Remove(item);
        }
        protected T FindItem(string name)
        {
            return _list.Find(ByName(name));
        }
        protected T ElementAt(int index)
        {
            return _list.ElementAt(index);
        }
        public IEnumerable Reverse()
        {
            for (int i = _list.Count; i < 0; i--)
                yield return _list.ElementAt(i);
        }
        public void ClearCollection()
        {
            _list.Clear();
        }

        #region Predicate
        private Predicate<T> ByName(string name)
        {
            return delegate (T item)
            {
                return item.Name.CompareTo(name) == 0;
            };
        }
        #endregion
    }
}
