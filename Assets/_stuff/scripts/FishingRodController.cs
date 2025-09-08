using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    public BaseFishingRodSO fishingRodSettings;

    [Header("General")]
    public float maxEnergy;
    public float currentEnergy;
    public float lureSinkSpeedEmpty;
    public float lureSinkSpeedFull;
    public float lureMoveSpeedEmpty;
    public float lureMoveSpeedFull;

    public enum FishingState
    {
        Idle,
        Sinking,
        Reeling
    }
    public FishingState fishingState;
    public enum LureState
    {
        Empty,
        Full
    }
    public LureState lureState;

    [Header("References")]
    public Transform lureIdlePos;
    public Transform lure;



    #region Helper methods

    void SetFishingState(FishingState newState)
    {
        fishingState = newState;
    }
    void SetLureState(LureState newState)
    {
        lureState = newState;
    }



    void MoveLure(float speed)
    {
        if (Input.GetKey(KeyCode.A))
        {
            lure.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            lure.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }



    void ThrowLure()
    {
        lure.position = lureIdlePos.position;

        SetFishingState(FishingState.Sinking);
        SetLureState(LureState.Empty);
    }
    void SinkLure(float speed)
    {
        lure.position -= new Vector3(0, speed, 0) * Time.deltaTime;
    }
    void ReelLure(float speed)
    {
        lure.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }



    void UpdateState_Idle()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowLure();
        }
    }
    void UpdateState_Sinking()
    {
        if (Input.GetKey(KeyCode.W))
        {
            SetFishingState(FishingState.Reeling);
        }

        switch (lureState)
        {
            case LureState.Empty:
                MoveLure(lureMoveSpeedEmpty);
                SinkLure(lureSinkSpeedEmpty);
                break;
            case LureState.Full:
                MoveLure(lureMoveSpeedFull);
                SinkLure(lureSinkSpeedFull);
                break;
        }
    }
    void UpdateState_Reeling()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SetFishingState(FishingState.Sinking);
        }

        switch (lureState)
        {
            case LureState.Empty:
                MoveLure(lureMoveSpeedEmpty);
                ReelLure(fishingRodSettings.reelingSpeed);
                break;
            case LureState.Full:
                MoveLure(lureMoveSpeedFull);
                ReelLure(fishingRodSettings.reelingSpeed);
                break;
        }
    }

    void UpdateFishing()
    {
        switch (fishingState)
        {
            case FishingState.Idle:
                UpdateState_Idle();
                break;
            case FishingState.Sinking:
                UpdateState_Sinking();
                break;
            case FishingState.Reeling:
                UpdateState_Reeling();
                break;
        }
    }

    #endregion



    #region Unity lifecycle

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SetLureState(LureState.Full);
        }
        if (Input.GetKey(KeyCode.E))
        {
            SetLureState(LureState.Empty);
        }

        UpdateFishing();
    }
    
    #endregion
}
