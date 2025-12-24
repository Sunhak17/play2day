using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WelcomeMenuCarousel : MonoBehaviour
{
    [System.Serializable]
    public class MenuItem
    {
        public string name;
        public GameObject menuObject;
        public string sceneName;
        public Vector3 leftPosition;
        public Vector3 centerPosition;
        public Vector3 rightPosition;
        public Vector3 leftScale = Vector3.one;
        public Vector3 centerScale = Vector3.one * 1.2f;
        public Vector3 rightScale = Vector3.one;
    }

    [Header("Menu Items")]
    public List<MenuItem> menuItems = new List<MenuItem>();

    [Header("Navigation Buttons")]
    public Button leftButton;
    public Button rightButton;
    public Button centerButton;

    [Header("Animation Settings")]
    public float transitionDuration = 0.5f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private int currentCenterIndex = 1; // Start with middle item (PLAY) in center
    private bool isTransitioning = false;

    void Start()
    {
        // Setup button listeners
        if (leftButton != null)
            leftButton.onClick.AddListener(OnLeftButtonClick);

        if (rightButton != null)
            rightButton.onClick.AddListener(OnRightButtonClick);

        if (centerButton != null)
            centerButton.onClick.AddListener(OnCenterButtonClick);

        // Initialize positions
        UpdateMenuPositions(false);
    }

    public void OnLeftButtonClick()
    {
        if (isTransitioning) return;

        // Move carousel left (items move right visually)
        currentCenterIndex--;
        if (currentCenterIndex < 0)
            currentCenterIndex = menuItems.Count - 1;

        StartCoroutine(AnimateCarousel());
    }

    public void OnRightButtonClick()
    {
        if (isTransitioning) return;

        // Move carousel right (items move left visually)
        currentCenterIndex++;
        if (currentCenterIndex >= menuItems.Count)
            currentCenterIndex = 0;

        StartCoroutine(AnimateCarousel());
    }

    public void OnCenterButtonClick()
    {
        if (isTransitioning) return;

        MenuItem centerItem = menuItems[currentCenterIndex];
        
        if (!string.IsNullOrEmpty(centerItem.sceneName))
        {
            Debug.Log($"Loading scene: {centerItem.sceneName}");
            SceneManager.LoadScene(centerItem.sceneName);
        }
        else
        {
            Debug.LogWarning($"No scene assigned for {centerItem.name}");
        }
    }

    void UpdateMenuPositions(bool animate)
    {
        if (menuItems.Count < 1) return;

        // Show all items and position them in a circular manner
        for (int i = 0; i < menuItems.Count; i++)
        {
            MenuItem item = menuItems[i];
            if (item.menuObject == null) continue;

            // Calculate the distance from current center item
            int distance = (i - currentCenterIndex + menuItems.Count) % menuItems.Count;
            
            Vector3 targetPos;
            Vector3 targetScale;

            // Position items based on their distance from center
            if (distance == 0)
            {
                // Center item
                targetPos = item.centerPosition;
                targetScale = item.centerScale;
            }
            else if (distance == 1 || distance == menuItems.Count - 1)
            {
                // Adjacent items (left or right)
                if (distance == 1)
                {
                    // Right side
                    targetPos = item.rightPosition;
                    targetScale = item.rightScale;
                }
                else
                {
                    // Left side
                    targetPos = item.leftPosition;
                    targetScale = item.leftScale;
                }
            }
            else
            {
                // Hide items that are far away
                item.menuObject.SetActive(false);
                continue;
            }

            item.menuObject.SetActive(true);

            if (animate)
            {
                // Animation will be handled by coroutine
            }
            else
            {
                item.menuObject.transform.localPosition = targetPos;
                item.menuObject.transform.localScale = targetScale;
            }
        }
    }

    IEnumerator AnimateCarousel()
    {
        isTransitioning = true;

        // Disable buttons during transition
        if (leftButton != null) leftButton.interactable = false;
        if (rightButton != null) rightButton.interactable = false;
        if (centerButton != null) centerButton.interactable = false;

        // Store starting positions and scales for all items
        Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();
        Dictionary<int, Vector3> startScales = new Dictionary<int, Vector3>();

        foreach (var item in menuItems)
        {
            if (item.menuObject != null && item.menuObject.activeInHierarchy)
            {
                int index = menuItems.IndexOf(item);
                startPositions[index] = item.menuObject.transform.localPosition;
                startScales[index] = item.menuObject.transform.localScale;
            }
        }

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = transitionCurve.Evaluate(elapsed / transitionDuration);

            // Animate all items
            for (int i = 0; i < menuItems.Count; i++)
            {
                MenuItem item = menuItems[i];
                if (item.menuObject == null) continue;

                // Calculate the distance from current center item
                int distance = (i - currentCenterIndex + menuItems.Count) % menuItems.Count;
                
                Vector3 targetPos;
                Vector3 targetScale;

                // Determine target position based on distance
                if (distance == 0)
                {
                    targetPos = item.centerPosition;
                    targetScale = item.centerScale;
                }
                else if (distance == 1)
                {
                    targetPos = item.rightPosition;
                    targetScale = item.rightScale;
                }
                else if (distance == menuItems.Count - 1)
                {
                    targetPos = item.leftPosition;
                    targetScale = item.leftScale;
                }
                else
                {
                    continue;
                }

                // Animate position and scale
                if (startPositions.ContainsKey(i))
                {
                    item.menuObject.transform.localPosition = Vector3.Lerp(
                        startPositions[i],
                        targetPos,
                        t
                    );
                    item.menuObject.transform.localScale = Vector3.Lerp(
                        startScales[i],
                        targetScale,
                        t
                    );
                }
            }

            yield return null;
        }

        // Ensure final positions are exact
        UpdateMenuPositions(false);

        // Re-enable buttons
        if (leftButton != null) leftButton.interactable = true;
        if (rightButton != null) rightButton.interactable = true;
        if (centerButton != null) centerButton.interactable = true;

        isTransitioning = false;
    }

    // Helper method to setup positions automatically based on screen
    public void AutoSetupPositions()
    {
        if (menuItems.Count < 3) return;

        float spacing = 400f; // Adjust based on your screen size

        for (int i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].leftPosition = new Vector3(-spacing, 0, 0);
            menuItems[i].centerPosition = new Vector3(0, 0, 0);
            menuItems[i].rightPosition = new Vector3(spacing, 0, 0);
        }
    }

#if UNITY_EDITOR
    // Helper to visualize positions in editor
    void OnValidate()
    {
        if (Application.isPlaying) return;
        
        // Auto-assign names if empty
        if (menuItems.Count >= 3)
        {
            if (string.IsNullOrEmpty(menuItems[0].name))
                menuItems[0].name = "Settings";
            if (string.IsNullOrEmpty(menuItems[1].name))
                menuItems[1].name = "Play";
            if (string.IsNullOrEmpty(menuItems[2].name))
                menuItems[2].name = "About Us";
        }
    }
#endif
}
