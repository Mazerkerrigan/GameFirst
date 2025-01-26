using UnityEngine;

namespace game_one.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;

        private Weapon _weapon; // оружие которое мы сейчас держим

        private Collider[] _colliders = new Collider[3]; // !! делаем для того чтобы враг мог видеть не только себя случайно, а еще и нас

        private float _nextShotTimerSec; // следующий выстрел (секунды)
        private GameObject _target;

        protected void Update()
        {
            _target = getTarget();

            _nextShotTimerSec -= Time.deltaTime; // отнимаем время с предыдушего выстрела
            if ( _nextShotTimerSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);
               
                _nextShotTimerSec = _weapon.ShootFrequencySec; // обновляем таймер
            }
        }
        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            _weapon = Instantiate(weaponPrefab, hand); // создаем оружие в руке 
            _weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        
        private GameObject getTarget()
        {
            GameObject target = null;

            var position = _weapon.transform.position; // позиция оружия в текущий момент времени
            var radius = _weapon.ShootRadius;
            // Выбираем маску в зависимости от слоя текущего объекта
            int mask;
            if (gameObject.layer == LayerMask.NameToLayer(LayerUtils.EnemyLayerName))
            {
                mask = LayerUtils.PlayerMask; // Для игрока ищем врагов
            }
            else
            {
                mask = LayerUtils.EnemyMask; // Для врагов ищем игрока
            }
            //var mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask); // создает сферу вокруг указанной позиции, указанного радиуса
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)  // найденный объект не мы, чтобы сам в себя не стрельнул
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }    
            }

            return target;
        }
    }

}