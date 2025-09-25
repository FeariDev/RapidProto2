using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public BaseFishingRodSO fishingRod1Settings;
    public BaseFishingRodSO fishingRod2Settings;
    public static int energyDrinks;
    public static int fishingRod1;
    public static int fishingRod2;

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