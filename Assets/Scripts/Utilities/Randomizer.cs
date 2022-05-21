using UnityEngine;
using System.Collections.Generic;

public class Randomizer : MonoBehaviour
{
    public static T GetRandomItemWithRemoval<T>(List<T> collection)
    {
        var randomIndex = Random.Range(0, collection.Count);
        var choosenItem = collection[randomIndex];

        collection.Remove(choosenItem);
        return choosenItem;
    }

    public static T GetRandomItem<T>(List<T> collection)
    {
        var randomIndex = Random.Range(0, collection.Count);
        var choosenItem = collection[randomIndex];

        return choosenItem;
    }

    public static T GetRandomItem<T>(T[] collection)
    {
        var randomIndex = Random.Range(0, collection.Length);
        var choosenItem = collection[randomIndex];

        return choosenItem;
    }
}
