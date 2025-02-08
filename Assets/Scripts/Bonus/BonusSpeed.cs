using game_one.Movement;
using UnityEngine;

namespace game_one.Bonus
{
    public class BonusSpeed : MonoBehaviour
    {

        [SerializeField]
        private float _n = 2f;

        [SerializeField]
        private float _bonusTime = 2f;

        private float _initialSpeed;

        private CharacterMovementController _characterMovementController;
        private bool _isBonusActive = false;
        
        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            _isBonusActive = false;
        }
        public void TakeBonus(CharacterMovementController speed)
        {
            if (_isBonusActive == false)
            {
                _isBonusActive = true;
                _characterMovementController = speed;
                _initialSpeed = _characterMovementController.Speed;
                _characterMovementController.Speed = _initialSpeed * _n;                
            }
            CancelInvoke(nameof(ResetSpeed));
            Invoke(nameof(ResetSpeed), _bonusTime);
        }
        private void ResetSpeed()
        {                   
            _characterMovementController.Speed = _initialSpeed;
            _isBonusActive = false;
        }
    }
}