using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomizationSelector : MonoBehaviour
{
    [System.Serializable]
    public class CustomizationItem
    {
        public string itemName;
        public Sprite itemSprite;
    }

    [Header("Settings")]
    public CustomizationItem[] items; // All items (players, balls, or stadiums)
    public string itemType = "Player"; // "Player", "Ball", or "Stadium"

    [Header("UI References")]
    public Image displayImage;
    public TextMeshProUGUI itemNameText;

    private int currentIndex = 0;

    void Start()
    {
        if (items.Length > 0)
        {
            UpdateDisplay();
        }
    }

    public void NextItem()
    {
        if (items.Length == 0) return;

        currentIndex++;
        if (currentIndex >= items.Length)
            currentIndex = 0;

        UpdateDisplay();
    }

    public void PreviousItem()
    {
        if (items.Length == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = items.Length - 1;

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        CustomizationItem current = items[currentIndex];

        if (displayImage != null)
            displayImage.sprite = current.itemSprite;

        if (itemNameText != null)
            itemNameText.text = current.itemName;
    }

    public string GetSelectedItem()
    {
        if (items.Length > 0)
            return items[currentIndex].itemName;
        return "";
    }

    public Sprite GetSelectedSprite()
    {
        if (items.Length > 0)
            return items[currentIndex].itemSprite;
        return null;
    }
}
