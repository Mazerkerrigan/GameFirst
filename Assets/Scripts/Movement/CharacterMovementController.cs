﻿using UnityEngine;
using UnityEngine.UIElements;

namespace game_one.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;
        [SerializeField]
        private float _Nacceleration = 2f;

        public Vector3 MovementDirection {  get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;
        
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
            Vector3 delta;
            if (Input.GetKey(KeyCode.Space))
            {
                delta = MovementDirection * _speed * _Nacceleration * Time.deltaTime;
            }
            else
            { 
                delta = MovementDirection * _speed * Time.deltaTime;

            }
            _characterController.Move(delta);
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
    }
}
