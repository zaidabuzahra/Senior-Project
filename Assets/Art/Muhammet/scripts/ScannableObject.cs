using UnityEngine;
using TMPro;

public class ScannableObject : MonoBehaviour
{
    public string objectName; // Name of the object
    [TextArea(4, 8)] public string objectInfo; // Text information after scanning
    public GameObject infoPanel; // Reference to UI panel (assign in Inspector)
    public TextMeshProUGUI infoText; // Reference to TMP UI Text (assign in Inspector)

    private bool isScanned = false; // Check if the object is scanned
    private bool isPanelOpen = false; // Track if the panel is open

    void Update()
    {
        // Close panel when pressing "Esc"
        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInfoPanel();
        }
    }

    public void ScanObject()
    {
        if (!isScanned)
        {
            isScanned = true;
            Debug.Log($"{objectName} scanned!");
        }
    }

    public void ShowInfo()
    {
        if (isScanned)
        {
            OpenInfoPanel();
        }
    }

    private void OpenInfoPanel()
    {
        isPanelOpen = true;
        infoPanel.SetActive(true);
        infoText.text = $"<b>{objectName}</b>\n{objectInfo}";
    }

    private void CloseInfoPanel()
    {
        isPanelOpen = false;
        infoPanel.SetActive(false);
    }
}
