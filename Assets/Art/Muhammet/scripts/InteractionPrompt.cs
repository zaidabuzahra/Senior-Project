using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public GameObject logUI; // Reference to the log UI panel
    public TextMeshProUGUI logText; // Reference to the Text UI
    [TextArea(4, 8)] public string message = "This is the log message."; // Bigger text box

    private bool isNear = false; // Track if player is close
    private bool isReading = false; // Track if log is open

    void Start()
    {
        logUI.SetActive(false); // Hide UI at start
        logText.alignment = TextAlignmentOptions.Center; // Center text
        logText.color = Color.black; // Set font color to black
    }

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.F))
        {
            ToggleLog();
        }
    }

    private void ToggleLog()
    {
        isReading = !isReading;
        logUI.SetActive(isReading); // Toggle UI
        logText.text = message; // Set message

        if (isReading)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player
        {
            isNear = true;
            Debug.Log("Press F to interact."); // Console message (optional)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            logUI.SetActive(false); // Hide UI when leaving
            isReading = false;
            Time.timeScale = 1f; // Ensure game resumes if player leaves
        }
    }
}
