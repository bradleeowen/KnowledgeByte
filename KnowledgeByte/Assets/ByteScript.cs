using UnityEngine;

public class FallFromSpace : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 10, 0);
    public Vector3 endPosition = new Vector3(0, 0, 0);
    public float fallDuration = 2f;

    private float elapsed = 0f;
    private bool falling = true;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (falling)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fallDuration);
            t = Mathf.SmoothStep(0, 1, t); // adds smooth easing
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            if (t >= 1f)
                falling = false;
        }
    }
}

    
