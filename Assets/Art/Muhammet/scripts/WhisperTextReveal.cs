using UnityEngine;
using TMPro;

public class WhisperTextReveal : MonoBehaviour
{
    public TextMeshProUGUI whisperText; // Assign the TMP Text in Inspector
    [TextArea(4, 8)] public string message; // Write the text in the Inspector
    public KeyCode listenKey = KeyCode.Q; // Key to hold for revealing text
    public float revealSpeed = 0.5f; // Speed of revealing the text
    public float fadeOutSpeed = 1f; // Speed of fading out the text

    private bool isPlayerNearby = false;
    private float revealAmount = 0f; // 0 = invisible, 1 = fully visible

    void Start()
    {
        if (whisperText != null)
        {
            whisperText.text = message; // Set text from Inspector
            whisperText.alpha = 0f; // Start fully invisible
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKey(listenKey))
        {
            revealAmount += revealSpeed * Time.deltaTime;
        }
        else
        {
            revealAmount -= fadeOutSpeed * Time.deltaTime;
        }

        revealAmount = Mathf.Clamp(revealAmount, 0f, 1f);

        if (whisperText != null)
        {
            whisperText.alpha = revealAmount; // Adjust text visibility
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
