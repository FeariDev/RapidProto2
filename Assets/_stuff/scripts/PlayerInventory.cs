using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int energyDrinks;
    public int fishingRod1;
    public int fishingRod2;

    public enum Items
    {
        EnergyDrink1,
        FishingRod1,
        FishingRod2
    }



    public void AddItem(Items item, int amount)
    {
        switch (item)
        {
            case Items.EnergyDrink1:
                energyDrinks += amount;
                break;
            case Items.FishingRod1:
                fishingRod1 += amount;
                break;
            case Items.FishingRod2:
                fishingRod2 += amount;
                break;
        }
    }
}