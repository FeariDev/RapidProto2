using System;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    public Sprite outOfOrderSprite;


    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public float price;
        public Button buyButton;
        public bool isSoldOut;
    }

    public ShopItem[] shopItems; // Assign 6 items in Inspector
    public static bool[] soldOutItems;

    private void Start()
    {
        ResizeSoldOutList();

        int index = 0;
        foreach (ShopItem item in shopItems)
        {
            ShopItem capturedItem = item; // avoid closure issue
            item.isSoldOut = soldOutItems[index];
            item.buyButton.onClick.AddListener(() => BuyItem(capturedItem));

            if (item.isSoldOut)
            {
                item.buyButton.GetComponent<Image>().sprite = outOfOrderSprite;
                item.buyButton.interactable = false;
            }

            index++;
        }
    }

    void BuyItem(ShopItem item)
    {
        if (Player.money >= item.price)
        {
            Player.Instance.ChangeMoney(-item.price);
            Debug.Log("Bought: " + item.itemName);
            PlayerInventory inventory = Player.Instance.playerInventory;
            if (item.itemName == "Small Energy Drink")
            {
                inventory.AddItem(PlayerInventory.Items.EnergyDrink1, 1);
                soldOutItems[0] = true;
            }
            else if (item.itemName == "Strong Rod")
            {
                inventory.AddItem(PlayerInventory.Items.FishingRod1, 1);
                soldOutItems[4] = true;
            }
            else if (item.itemName == "Automatic Rod")
            {
                inventory.AddItem(PlayerInventory.Items.FishingRod2, 1);
                soldOutItems[3] = true;
            }
            item.buyButton.GetComponent<Image>().sprite = outOfOrderSprite;
            item.buyButton.interactable = false;
        }
        else
        {
            Debug.Log("Not enough money to buy: " + item.itemName);
        }
    }

    [Button]
    void ResizeSoldOutList()
    {
        Array.Resize(ref soldOutItems, shopItems.Length);
        foreach (bool b in soldOutItems)
        {
            Debug.Log(b);
        }
    }
}


