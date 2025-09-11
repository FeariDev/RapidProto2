using System;
using UnityEngine;

public class FishingLure : MonoBehaviour
{
    public TreasureItem currentItem;

    public event Action<TreasureItem> OnItemCatch;
    public event Action OnItemFree;



    public void CatchTreasureItem(ref TreasureItem item)
    {
        currentItem = item;

        currentItem.ToggleFreedom(false);

        OnItemCatch?.Invoke(currentItem);
    }
    public void FreeCurrentTreasureItem()
    {
        currentItem.ToggleFreedom(true);

        currentItem = null;

        OnItemFree?.Invoke();
    }
    public void DestroyCurrentTreasureItem()
    {
        if (currentItem == null) return;

        Destroy(currentItem.gameObject);
    }



    #region Unity lifecycle

    void Update()
    {
        if (currentItem != null) currentItem.transform.position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TreasureItem treasureItem;

        if (treasureItem = other.GetComponent<TreasureItem>())
        {
            CatchTreasureItem(ref treasureItem);
        }
    }
    
    #endregion
}
