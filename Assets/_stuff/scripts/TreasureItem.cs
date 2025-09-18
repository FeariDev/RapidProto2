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
    public float moveSpeed;
    public int moveDir;
    public float spawnChance;



    public void ToggleFreedom(bool toggle)
    {
        isCaught = !toggle;
    }

    public void FreeItem()
    {
        isCaught = false;
    }
    public void CatchItem()
    {
        isCaught = true;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }



    #region Unity lifecycle

    void Start()
    {
        if (transform.position.x > 0)
        {
            moveDir = -1;
        }
        else if (transform.position.x < 0)
        {
            moveDir = 1;
        }
    }

    void Update()
    {
        if (isCaught) return;

        transform.position += new Vector3(moveSpeed * moveDir, 0, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TreasureDestroyer"))
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
