using UnityEngine;

public class Player : Singleton<Player>
{
    public float money;



    public void ChangeMoney(float value)
    {
        money += value;
    }
}
