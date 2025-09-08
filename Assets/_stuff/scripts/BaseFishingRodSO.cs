using UnityEngine;

[CreateAssetMenu(fileName = "Base_Fishing_Rod_Settings", menuName = "FishingRods/Base Fishing Rod")]
public class BaseFishingRodSO : ScriptableObject
{
    public float reelingSpeed = 0.8f;
    public float energyDrainMultiplier = 1;
    public bool automaticReel;
}
