using UnityEngine;

public class TreasureItem : MonoBehaviour
{
    public float value;
    public enum Type
    {
        Good,
        Bad
    }
    public Type type;
    public bool isCaught;



    public void ToggleFreedom(bool toggle)
    {
        isCaught = !toggle;
    }
}
