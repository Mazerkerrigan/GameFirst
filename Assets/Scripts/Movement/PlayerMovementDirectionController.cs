using game_one.Bonus;
using UnityEngine;

namespace game_one.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;
        public Vector3 MovementDirection {  get; private set; }
        public bool HasAcceleration { get; private set; }

        [SerializeField] 
        private float _n = 2f;
        public float AccelerationMultiplier => _n;
        void Awake()
        {
            _camera = UnityEngine.Camera.main;                      
        }
        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0 , vertical);
            direction = _camera.transform.rotation * direction; 
            direction.y = 0;
            
            MovementDirection = direction.normalized; //чтобы направление было именно
            HasAcceleration = Input.GetKey(KeyCode.LeftShift);
        } 
    }    
}