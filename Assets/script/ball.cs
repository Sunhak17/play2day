using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on this GameObject. Please add a Rigidbody2D component.");
        }
    }
}
