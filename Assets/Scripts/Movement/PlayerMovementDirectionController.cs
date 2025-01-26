﻿using UnityEngine;

namespace game_one.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;
        public Vector3 MovementDirection {  get; private set; }
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

            MovementDirection = direction.normalized; //чтобы направление было имеенно
        }
    }
}