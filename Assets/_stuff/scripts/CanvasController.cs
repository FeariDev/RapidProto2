using UnityEditorInternal;
using UnityEngine;
using TMPro;
public class CanvasController : MonoBehaviour
{

    public GameObject ShopPanel;
    public GameObject Press_EPanel;
    public PlayerMovement PlayerMovement;
    public PlayerDirectionController PlayerDirectionController;
    public TMP_Text MoneyText;

    public void Update()
    { 
        if (Player.Instance != null)
        {
            MoneyText.text = Player.Instance.money.ToString() ;
        }
    }

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
