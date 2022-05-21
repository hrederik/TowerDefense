using UnityEngine;

namespace Game.Movement
{
    public interface IMoveable
    {
        float Speed { get; set; }
        void Move(Vector3 direction);
        void ResetSpeed();
    }
}