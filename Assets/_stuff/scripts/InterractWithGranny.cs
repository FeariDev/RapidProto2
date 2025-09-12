using UnityEngine;

public class InterractWithGranny : MonoBehaviour
{
    public CanvasController CanvasController;
    public PlayerMovement PlayerMovement;
    public PlayerDirectionController PlayerDirectionController;
    private bool isInsideTrigger = false;

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            CanvasController.ShowShop();

            Player.Instance.ToggleMoevement(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isInsideTrigger = true;
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
            Debug.Log("Player left trigger zone.");
        }
    }
}
