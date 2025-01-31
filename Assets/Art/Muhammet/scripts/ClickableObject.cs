using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    private ScannableObject scannable;

    void Start()
    {
        scannable = GetComponent<ScannableObject>();
    }

    void OnMouseDown()
    {
        if (scannable != null)
        {
            scannable.ShowInfo();
        }
    }
}
