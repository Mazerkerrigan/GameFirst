using System;
using game_one;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _followCamereOffset = Vector3.zero; // Отступ от камеры до игрока

        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;

        [SerializeField]
        private PlayerCharacter _player;
        
        protected void Awake()
        {
            if ( _player == null ) 
                throw new NullReferenceException($"Follow camera can't follow null player - {nameof(_player)}");
        }
                
        protected void LateUpdate()
        {
            if (_player != null)
            {
                Vector3 targetRotation = _rotationOffset - _followCamereOffset;

                transform.position = _player.transform.position + _followCamereOffset;
                transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up); // Установка целвого направления
            }
        }
    }
}