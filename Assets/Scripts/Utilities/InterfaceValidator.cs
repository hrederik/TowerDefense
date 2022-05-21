using UnityEngine;

public class InterfaceValidator : MonoBehaviour
{
    public static void Validate<T>(MonoBehaviour checkable)
    {
        if (checkable is T == false)
        {
            Debug.LogError($"{checkable.name} needs to implement {typeof(T).Name}");
        }
    }
}