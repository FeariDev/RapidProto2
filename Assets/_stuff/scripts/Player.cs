using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public PlayerMovement playerMovement;
    public PlayerDirectionController playerDirectionController;
    public float money { get; private set; }



    public void ChangeMoney(float value)
    {
        money += value;
    }
   

    public void ToggleMoevement(bool enabled)
    {
        playerMovement.enabled = enabled;
        playerDirectionController.enabled = enabled;
    }
}
