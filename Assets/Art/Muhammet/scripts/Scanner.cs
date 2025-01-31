using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange = 5f; // How far the scanner can detect
    public LayerMask scanableLayer; // Layer for scannable objects
    public KeyCode scanKey = KeyCode.E; // Key to scan

    void Update()
    {
        if (Input.GetKeyDown(scanKey))
        {
            ScanForObjects();
        }
    }

    private void ScanForObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRange, scanableLayer);

        foreach (Collider hit in hits)
        {
            ScannableObject scannable = hit.GetComponent<ScannableObject>();
            if (scannable != null)
            {
                scannable.ScanObject();
            }
        }
    }
}
