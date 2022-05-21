using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Plugins.SequentedScenes.Iterators
{
    public class DefaultIterator<T> : IIterator<T>
    {
        private const int DefaultIndex = -1;
        
        private readonly List<T> _collection;
        private int _index;

        public DefaultIterator(List<T> collection)
        {
            _collection = collection;
            _index = DefaultIndex;
        }

        public DefaultIterator(List<T> collection, int startIndex)
        {
            _collection = collection;
            _index = startIndex;
        }

        public T Current => _collection[_index];

        object IEnumerator.Current => _collection[_index];

        public int Index => _index;

        public bool MoveNext()
        {
            if (_index < _collection.Count - 1)
            {
                _index++;
                return true;
            }
            
            return false;
        }

        public void Reset() => _index = DefaultIndex;

        public void Dispose() { }
    }
}