using System.Collections.Generic;
using UnityEngine.Events;

namespace Plugins.SequentedScenes.Iterators
{
    public interface IIterator<T> : IEnumerator<T>
    {
        int Index { get; }
    }
}