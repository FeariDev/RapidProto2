using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public PlayerMovement playerMovement;
    public PlayerDirectionController playerDirectionController;
    public PlayerInventory playerInventory;
    public float money { get; private set; }

    [Header("UI")]
    public TMP_Text moneyText;



    public void ChangeMoney(float value)
    {
        money += value;

        moneyText.text = money.ToString();
    }
   

    public void ToggleMoevement(bool enabled)
    {
        playerMovement.enabled = enabled;
        playerDirectionController.enabled = enabled;
    }
    [Button("Add Money")]
    void AddMoney()
    {
        ChangeMoney(10);
    }
}
