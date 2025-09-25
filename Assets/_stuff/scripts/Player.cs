using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public PlayerMovement playerMovement;
    public PlayerDirectionController playerDirectionController;
    public PlayerInventory playerInventory;
    public static float money { get; private set; }

    [Header("UI")]
    public TMP_Text moneyText;



    public void ChangeMoney(float value)
    {
        money += value;
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
    [Button("Load Pool")]
    void LoadPoolScene()
    {
        SceneManager.LoadScene("Pool_Area");
    }
    [Button("Load Entry")]
    void LoadEntryScene()
    {
        SceneManager.LoadScene("Entry");
    }



    void UpdateFishingController()
    {
        if (SceneManager.GetActiveScene().name != "Pool_Area") return;

        if (PlayerInventory.fishingRod2 > 0)
        {
            FishingRodController.Instance.fishingRodSettings = playerInventory.fishingRod2Settings;
        }
        else if (PlayerInventory.fishingRod1 > 0)
        {
            FishingRodController.Instance.fishingRodSettings = playerInventory.fishingRod1Settings;
        }

    }



    void Update()
    {
        moneyText.text = money.ToString();

        UpdateFishingController();
    }
}
