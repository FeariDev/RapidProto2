using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
 


    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public float price;
        public Button buyButton;
    }

    public ShopItem[] shopItems; // Assign 6 items in Inspector

    private void Start()
    {
        foreach (ShopItem item in shopItems)
        {
            ShopItem capturedItem = item; // avoid closure issue
            item.buyButton.onClick.AddListener(() => BuyItem(capturedItem));
        }
    }

    void BuyItem(ShopItem item)
    {
        if (Player.Instance.money >= item.price)
        {
            Player.Instance.ChangeMoney(-item.price);
            Debug.Log("Bought: " + item.itemName);

            // Disable after buying
            item.buyButton.interactable = false;
        }
        else
        {
            Debug.Log("Not enough money to buy: " + item.itemName);
        }
    }
}


