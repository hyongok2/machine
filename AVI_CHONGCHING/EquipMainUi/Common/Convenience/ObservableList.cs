using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Common.Convenience
{
    public class ObservableList<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        public int Count => list.Count;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private readonly List<T> list = new List<T>();

        public ObservableList()
        {

        }

        public void Add(T obj)
        {
            AddList(obj);
        }

        public void AddList(T item)
        {
            list.Add(item);
            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add, item));
        }

        //public T Dequeue()
        //{
        //    var item = list.Dequeue();
        //    if (CollectionChanged != null)
        //        CollectionChanged(this,
        //            new NotifyCollectionChangedEventArgs(
        //                NotifyCollectionChangedAction.Remove, item));
        //    return item;
        //}

        internal void Clear()
        {
            list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
