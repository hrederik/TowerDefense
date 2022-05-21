using System.Collections.Generic;
using Plugins.SequentedScenes.Iterators;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.SequentedScenes
{
    [CreateAssetMenu(fileName = "Sequence", menuName = "SequentedScenes/Sequence")]
    public class ScenesSequence : ScriptableObject
    {
        [SerializeField] private SequenceType _type;
        [SerializeField] private List<SequenceItem> _items;
        private IIterator<SequenceItem> _iterator;

        public event UnityAction Ended;

        public int ScenesCount => _items.Count;

        public void Init()
        {
            _iterator = IteratorBuilder.GetIterator(_type, _items);
        }

        public void Init(int startIndex)
        {
            _iterator = IteratorBuilder.GetIterator(_type, _items, startIndex);
        }
        
        public bool GetNextScene(out SequenceItem next)
        {
            var hasNextScene = _iterator.MoveNext();
            next = null;

            if (hasNextScene)
                next = _iterator.Current;
            else
                Ended?.Invoke();

            return hasNextScene;
        }
    }
}