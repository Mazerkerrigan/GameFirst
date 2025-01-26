using UnityEngine;

namespace game_one
{
    public class LayerUtils
    {
        public const string BulletLayerName = "Bullet";  // для того чтобы проще было работать с именами
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);

        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName); // маска чтобы искать врагов в радиусе атаки, и сравнивать по маске уже 
        public static readonly int PlayerMask = LayerMask.GetMask(PlayerLayerName);
        public static bool IsBullet(GameObject other) => other.layer == BulletLayer; // => заменяет нам return, короче удобно 

    }
}
