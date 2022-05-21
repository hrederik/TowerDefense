using System.Collections.Generic;

namespace Plugins.SequentedScenes.Iterators
{
    public static class IteratorBuilder
    {
        public static IIterator<T> GetIterator<T>(SequenceType sequenceType, List<T> collection) =>
            sequenceType switch
            {
                SequenceType.Endless => new EndlessIterator<T>(collection),
                _ => new DefaultIterator<T>(collection)
            };

        public static IIterator<T> GetIterator<T>(SequenceType sequenceType, List<T> collection, int startIndex) =>
            sequenceType switch
            {
                SequenceType.Endless => new EndlessIterator<T>(collection, startIndex),
                _ => new DefaultIterator<T>(collection, startIndex)
            };
    }
}