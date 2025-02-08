using game_one.Movement;
using game_one.PickUp;
using game_one.Shooting;
using game_one.Bonus;
using UnityEngine;

namespace game_one
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
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
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_ShootingController.HasTarget)
                lookDirection = (_ShootingController.TargetPosition - transform.position).normalized;     

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _characterMovementController.UpdateMovement(direction, _movementDirectionSource.HasAcceleration, _movementDirectionSource.AccelerationMultiplier);

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
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                pickUp.PickUp(this);

                Destroy(other.gameObject); // уничтожили на поле
            }
            else if (LayerUtils.IsBonusSpeed(other.gameObject))
            {
                var pickUpBonus = other.gameObject.GetComponent<PickUpBonus>();
                pickUpBonus.PickUp(this); // Передаем объект, содержащий бону
                Destroy(other.gameObject); // уничтожили на поле
            }
        }
        public void SetWeapon(Weapon weapon)
        {
            _ShootingController.SetWeapon(weapon, _hand);
        }
        public void ActivateBonus(BonusSpeed bonusSpeed)
        {
            if (bonusSpeed != null)  
                bonusSpeed.TakeBonus(_characterMovementController);          
        }
    }
}