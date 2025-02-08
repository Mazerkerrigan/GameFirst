using game_one.Bonus;
using UnityEngine;

namespace game_one.PickUp
{
    public class PickUpBonus : PickUpItem
    {
        [SerializeField]
        public BonusSpeed _bonusPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);                            
            character.ActivateBonus(_bonusPrefab); 
        }
   
    }
}