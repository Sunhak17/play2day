using UnityEngine;
using System.Collections;

public class PlayerKickAnimation : MonoBehaviour
{
    [Header("Kick Animation Settings")]
    public Transform legOrShoe; // Drag your leg/shoe GameObject here
    public float kickRotationAngle = 45f; // How much to rotate when kicking
    public float kickDuration = 0.3f; // How long the kick animation takes
    
    [Header("Alternative: Whole Player Animation")]
    public bool animateWholePlayer = false;
    public Vector3 kickScale = new Vector3(1.2f, 1f, 1f); // Stretch player forward
    
    private bool isKicking = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    void Start()
    {
        // Store original values
        if (legOrShoe != null)
        {
            originalPosition = legOrShoe.localPosition;
            originalRotation = legOrShoe.localRotation;
        }
        originalScale = transform.localScale;
    }

    public void PlayKickAnimation()
    {
        if (!isKicking)
        {
            StartCoroutine(KickAnimationCoroutine());
        }
    }

    IEnumerator KickAnimationCoroutine()
    {
        isKicking = true;
        float elapsed = 0f;
        float halfDuration = kickDuration / 2f;

        if (animateWholePlayer)
        {
            // Animate the whole player
            // Forward motion (first half)
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / halfDuration;
                transform.localScale = Vector3.Lerp(originalScale, kickScale, progress);
                yield return null;
            }

            // Return motion (second half)
            elapsed = 0f;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / halfDuration;
                transform.localScale = Vector3.Lerp(kickScale, originalScale, progress);
                yield return null;
            }

            transform.localScale = originalScale;
        }
        else if (legOrShoe != null)
        {
            // Animate specific leg/shoe
            Quaternion kickRotation = originalRotation * Quaternion.Euler(0, 0, kickRotationAngle);
            Vector3 kickPosition = originalPosition + new Vector3(0.5f, 0, 0); // Move forward

            // Forward kick (first half)
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / halfDuration;
                legOrShoe.localRotation = Quaternion.Lerp(originalRotation, kickRotation, progress);
                legOrShoe.localPosition = Vector3.Lerp(originalPosition, kickPosition, progress);
                yield return null;
            }

            // Return to normal (second half)
            elapsed = 0f;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / halfDuration;
                legOrShoe.localRotation = Quaternion.Lerp(kickRotation, originalRotation, progress);
                legOrShoe.localPosition = Vector3.Lerp(kickPosition, originalPosition, progress);
                yield return null;
            }

            // Ensure exact return to original
            legOrShoe.localRotation = originalRotation;
            legOrShoe.localPosition = originalPosition;
        }

        isKicking = false;
    }

    public bool IsKicking()
    {
        return isKicking;
    }
}
