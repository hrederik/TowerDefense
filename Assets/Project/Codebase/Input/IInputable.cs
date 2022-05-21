using UnityEngine;
using UnityEngine.Events;

namespace CustomUserInput
{
    public interface IInputable
    {
        event UnityAction<Vector3> Inputting;
        event UnityAction Started;
        event UnityAction Stopped;
    }
}