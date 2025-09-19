using UnityEngine;

public class FishingRodController : Singleton<FishingRodController>
{
    public BaseFishingRodSO fishingRodSettings;

    [Header("General")]
    public float lureSinkSpeedEmpty;
    public float lureSinkSpeedFull;
    public float lureMoveSpeedEmpty;
    public float lureMoveSpeedFull;
    public FishingLure lure;

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

    [Header("Energy")]
    public float maxEnergy;
    public float currentEnergy;
    public float energyDrain;
    public float energyGain;
    public EnergyBar energyBar;

    [Header("Camera Follow")]
    public Camera playerCamera;
    public float followSpeed;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Transform lureIdlePos;
    public Transform lureObj;

    public float reelingTapEase = 0.3f;
    public float timeSinceReeled;



    #region Fishing methods

    void SetFishingState(FishingState newState)
    {
        fishingState = newState;
    }
    void SetLureState(LureState newState)
    {
        lureState = newState;
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
        if (fishingRodSettings.automaticReel)
        {
            if (Input.GetKey(KeyCode.W))
            {
                SetFishingState(FishingState.Reeling);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                timeSinceReeled = 0;
                SetFishingState(FishingState.Reeling);
            }
        }
    }
    void UpdateState_Reeling()
    {
        if (fishingRodSettings.automaticReel)
        {
            if (!Input.GetKey(KeyCode.W))
            {
                SetFishingState(FishingState.Sinking);
            }
        }
        else
        {
            timeSinceReeled += Time.deltaTime;

            if (timeSinceReeled >= reelingTapEase)
            {
                SetFishingState(FishingState.Sinking);
            }
        }

        if (lureObj.position.y >= lureIdlePos.position.y)
        {
            FinishFishing();
        }
    }

    void UpdateFishing()
    {
        switch (fishingState)
        {
            case FishingState.Idle:
                UpdateState_Idle();
                return;
            case FishingState.Sinking:
                UpdateState_Sinking();
                return;
            case FishingState.Reeling:
                UpdateState_Reeling();
                return;
        }
    }

    #endregion



    #region Energy methods

    void UpdateEnergy()
    {
        if (fishingState == FishingState.Idle) return;

        if (currentEnergy <= 0 && lureState == LureState.Full) lure.FreeCurrentTreasureItem();

        UpdateEnergyBarVisibility();
        UpdateEnergyValue();
        
        energyBar.SetFillAsPercentage(currentEnergy / maxEnergy);
    }
    void UpdateEnergyBarVisibility()
    {
        switch (lureState)
        {
            case LureState.Empty:
                energyBar.ToggleBar(false);
                break;
            case LureState.Full:
                energyBar.ToggleBar(true);
                break;
        }
    }
    void UpdateEnergyValue()
    {
        float energyChange = 0;

        switch (lureState)
        {
            case LureState.Empty:
                energyChange += energyGain * Time.deltaTime;
                break;
            case LureState.Full:
                switch (fishingState)
                {
                    case FishingState.Sinking:
                        energyChange += energyGain * Time.deltaTime;
                        break;
                    case FishingState.Reeling:
                        energyChange -= energyDrain * fishingRodSettings.energyDrainMultiplier * Time.deltaTime;
                        break;
                }
                break;
        }

        currentEnergy = Mathf.Clamp(currentEnergy + energyChange, 0, maxEnergy);
    }

    #endregion



    #region Lure methods

    void MoveLure(Vector2 movement)
    {
        lureObj.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime;
    }

    void ThrowLure()
    {
        lureObj.position = lureIdlePos.position;

        SetFishingState(FishingState.Sinking);

        lure.OnItemCatch += CatchTreasureOnLure;
        lure.OnItemFree += FreeTreasureOnLure;
    }

    void ResetLure()
    {
        lure.OnItemCatch -= CatchTreasureOnLure;
        lure.OnItemFree -= FreeTreasureOnLure;

        SetLureState(LureState.Empty);

        lureObj.position = lureIdlePos.position;
    }

    void FinishFishing()
    {
        ResetLure();
        if(lure.currentItem != null) Player.Instance.ChangeMoney(lure.currentItem.value);
        lure.DestroyCurrentTreasureItem();
        currentEnergy = maxEnergy;
        UpdateEnergyBarVisibility();

        SetFishingState(FishingState.Idle);
    }

    void CatchTreasureOnLure(TreasureItem item)
    {
        if (item.type == TreasureItem.Type.Bad)
        {
            Player.Instance.ChangeMoney(lure.currentItem.value);
            lure.DestroyCurrentTreasureItem();
            return;
        }

        SetLureState(LureState.Full);
    }
    void FreeTreasureOnLure()
    {
        SetLureState(LureState.Empty);
    }



    void UpdateLure()
    {
        if (fishingState == FishingState.Idle) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float moveSpeedX = 0;
        float moveSpeedY = 0;
        float sinkSpeed = 0;

        switch (lureState)
        {
            case LureState.Empty:
                moveSpeedX = lureMoveSpeedEmpty * horizontalInput;
                sinkSpeed = lureSinkSpeedEmpty;
                break;
            case LureState.Full:
                moveSpeedX = lureMoveSpeedFull * horizontalInput;
                sinkSpeed = lureSinkSpeedFull;
                break;
        }

        switch (fishingState)
        {
            case FishingState.Sinking:
                moveSpeedY = -sinkSpeed;
                break;
            case FishingState.Reeling:
                moveSpeedY = fishingRodSettings.reelingSpeed;
                break;
        }

        Vector2 movement = new Vector2();
        movement.x = moveSpeedX;
        movement.y = moveSpeedY;
        
        MoveLure(movement);
    }

    #endregion



    void CameraFollow()
    {
        Vector3 followPos = playerCamera.transform.position;

        followPos.y = Mathf.Lerp(playerCamera.transform.position.y, lureObj.position.y, followSpeed * Time.deltaTime);

        playerCamera.transform.position = followPos;
    }



    void UpdateFishingRodSprite()
    {
        spriteRenderer.sprite = fishingRodSettings.fishingRodSprite;
    }



    #region Unity lifecycle

    void Update()
    {
        UpdateFishingRodSprite();
        CameraFollow();

        if (Input.GetKey(KeyCode.R))
        {
            SetLureState(LureState.Full);
        }
        if (Input.GetKey(KeyCode.E))
        {
            SetLureState(LureState.Empty);
        }

        UpdateLure();
        UpdateEnergy();
        UpdateFishing();
    }
    
    #endregion
}
