using UnityEngine;

namespace game_one.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public float Damage {  get; private set; }

        private Vector3 _direction;
        private float _flySpeed;
        private float _maxFlyDistance;
        private float _currentFlyDistance;

        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float damage) // Нужен нам, так как MonoBehavior объекты мы не можем конструировать при помощи конструктора
        {
            _direction = direction;
            _flySpeed = flySpeed;
            _maxFlyDistance = maxFlyDistance;

            Damage = damage;
        }
        protected void Update()
        {
            var delta = _flySpeed * Time.deltaTime;
            _currentFlyDistance += delta;
            transform.Translate(_direction * delta);

            if (_currentFlyDistance >= _maxFlyDistance)
                Destroy(gameObject); // уничтожаем именно нужный нам gameobject, а не this
        }
    }
}
