using UnityEngine;

namespace game_one.Movement
{
    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection {  get; private set; }
        public bool HasAcceleration => false;
        public float AccelerationMultiplier => 1f;

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }

    }
}