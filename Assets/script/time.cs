using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    public float gameTime = 90f;
    private float currentTime;
    private TextMeshProUGUI timeText;

    void Start()
    {
        currentTime = gameTime;
        timeText = GetComponent<TextMeshProUGUI>(); // Get TextMeshPro component
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                currentTime = 0;
        }
        
        if (timeText != null)
            timeText.text = Mathf.Ceil(currentTime).ToString();
    }

    public float GetTimeRemaining()
    {
        return currentTime;
    }
}