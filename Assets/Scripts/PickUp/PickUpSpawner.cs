using UnityEditor;
using UnityEngine;

namespace game_one.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        [SerializeField]
        private float _spawnIntervalSecMax = 10f;
        [SerializeField]
        private float _spawnIntervalSecMin = 2f;

        private float _currentSpawnTimerSeconds;
        private int _currentCount;
        private float _randomTime;

        private void Start()
        {
            _randomTime = Random.Range(_spawnIntervalSecMin, _spawnIntervalSecMax);
        }

        void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;
                                
                if (_currentSpawnTimerSeconds > _randomTime)
                {
                    _randomTime = Random.Range(_spawnIntervalSecMin, _spawnIntervalSecMax);
                    _currentSpawnTimerSeconds = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }
        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color; // указывается цвет 
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}