using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public GameObject ShopPanel;
    public GameObject Press_EPanel;
    public PlayerMovement PlayerMovement;
    public PlayerDirectionController PlayerDirectionController;




    public void ShowShop()
    {
        ShopPanel.SetActive(true);
        Press_EPanel.SetActive(false);
    }

    public void HideShop()
    {
        ShopPanel.SetActive(false);
        Press_EPanel.SetActive(true);
        Player.Instance.ToggleMoevement(true);
    }



}
