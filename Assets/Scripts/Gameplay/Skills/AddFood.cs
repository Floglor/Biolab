using UnityEngine;

namespace Gameplay.Skills
{
    public class AddFood : Skill
    {
        public int AddedFood;
        public override bool TakeEffect(Vector3 clickCoordinates)
        {
            CustomTile customTile = FoodController.Instance.GetFoodTileWorld(clickCoordinates);

            if (customTile == null)
            {
                Debug.Log("AddFood: Tile not found");
                return false;
            }

            customTile.FoodLevel += AddedFood;
            return true;
        }
    }
}