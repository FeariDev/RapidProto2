using UnityEngine;

public class Player : Singleton<Player>
{
    public float money { get; private set; }



    public void ChangeMoney(float value)
    {
        money += value;
    }
}
