using UnityEngine;
using System.Collections.Generic;

public class TendrilMaterialChanger : MonoBehaviour
{
    public List<Renderer> tendrilRenderers; // Drag and drop tendrils here in Inspector
    public Material newMaterial; // The new material to apply
    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>();

    void Start()
    {
        // Store the original materials
        foreach (Renderer rend in tendrilRenderers)
        {
            if (rend != null)
            {
                originalMaterials[rend] = rend.material;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detect player entering trigger
        {
            ChangeMaterial(newMaterial);
        }
    }

   

    private void ChangeMaterial(Material mat)
    {
        foreach (Renderer rend in tendrilRenderers)
        {
            if (rend != null && mat != null)
            {
                rend.material = mat; // Change material
            }
        }
    }

    private void RestoreOriginalMaterials()
    {
        foreach (Renderer rend in tendrilRenderers)
        {
            if (rend != null && originalMaterials.ContainsKey(rend))
            {
                rend.material = originalMaterials[rend]; // Restore original
            }
        }
    }
}
