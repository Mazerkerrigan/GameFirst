using UnityEngine;

namespace game_one.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        public float Speed = 1f;

        [SerializeField]
        private float _maxRadiansDelta = 10f;
        public Vector3 MovementDirection {  get; set; }
        public Vector3 LookDirection { get; set; }      

        private CharacterController _characterController;
        public Vector3 Delta { get; private set; }
        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();            
        }
            
        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
            { 
                Rotate();
            }
        }
        protected void Translate()
        {           
            _characterController.Move(Delta);
        }
        protected void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection = LookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon) 
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
        public void UpdateMovement(Vector3 movementDirection, bool isSprinting, float accelerationMultiplier)
        {
            float speedMultiplier;
            if (isSprinting == true)
                speedMultiplier = accelerationMultiplier;
            else
                speedMultiplier = 1f;

            Delta = Speed * speedMultiplier * Time.deltaTime * movementDirection;
        }
    }
}
