using UnityEngine;
using UnityEngine.EventSystems;

public class ClubLogoHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animation Settings")]
    public Animator animator;
    public string hoverAnimationTrigger = "Hover";
    public string normalAnimationTrigger = "Normal";

    [Header("Scale Animation (Optional)")]
    public bool useScaleAnimation = true;
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);
    public float animationSpeed = 0.2f;

    private Vector3 originalScale;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;

        // Auto-find Animator if not assigned
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (useScaleAnimation && isHovering)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, hoverScale, animationSpeed);
        }
        else if (useScaleAnimation && !isHovering)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, animationSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        // Trigger animator if available
        if (animator != null && !string.IsNullOrEmpty(hoverAnimationTrigger))
        {
            animator.SetTrigger(hoverAnimationTrigger);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        // Trigger animator if available
        if (animator != null && !string.IsNullOrEmpty(normalAnimationTrigger))
        {
            animator.SetTrigger(normalAnimationTrigger);
        }
    }
}
