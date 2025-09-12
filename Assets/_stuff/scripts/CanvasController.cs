using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public GameObject ShopPanel;
    public GameObject Press_EPanel;




    public void ShowShop()
    {
        ShopPanel.SetActive(true);
        Press_EPanel.SetActive(false);
    }

    public void HideShop()
    {
        ShopPanel.SetActive(false);
        Press_EPanel.SetActive(true);
    }
}
