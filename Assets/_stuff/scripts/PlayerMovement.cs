    using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   

    public float moveSpeed = 6f;    // yksinkertainen nopeus (units per second)
    public bool useSmoothing = true;
    public float smoothing = 10f;   // suurempi = vähemmän viivettä
    public bool lockYPosition = true;
    private Vector3 velocity = Vector3.zero;
    private float lockedY;

    void Start()
    {
        lockedY = transform.position.y;
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // -1,0,1
        Vector3 target = transform.position + Vector3.right * input * moveSpeed * Time.deltaTime;

        if (lockYPosition) target.y = lockedY;

        if (useSmoothing)
            transform.position = Vector3.Lerp(transform.position, target, Mathf.Clamp01(smoothing * Time.deltaTime));
        else
            transform.position = target;
    }
}


