using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] GameObject activeObject;
    [SerializeField] RectTransform fillRect;
    public float originalWidth;



    public void ToggleBar(bool toggle)
    {
        activeObject.SetActive(toggle);
    }

    public void SetFillAsPercentage(float percent)
    {
        fillRect.sizeDelta = new Vector2(originalWidth * percent, fillRect.sizeDelta.y);
    }



    void Awake()
    {
        originalWidth = fillRect.sizeDelta.x;
    }
}
