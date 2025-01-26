using game_one.Movement;
using game_one.Shooting;
using UnityEngine;

namespace game_one
{
    [RequireComponent (typeof (CharacterMovementController),typeof(ShootingController))]
        public class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab; 

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private float _health = 2f;

        private IMovementDirectionSource _movementDirectionSource;

        private CharacterMovementController _characterMovementController;
        private ShootingController _ShootingController;

        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>(); 
            _ShootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            _ShootingController.SetWeapon(_baseWeaponPrefab, _hand);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_ShootingController.HasTarget)
                lookDirection = (_ShootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (_health <= 0f)
            {
                Destroy(gameObject);
            }
        }

            protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>(); 
                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
        }
    }  
}