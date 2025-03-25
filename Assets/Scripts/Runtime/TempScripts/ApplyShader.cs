using UnityEngine;

namespace RunTime
{
    public class ApplyShader : MonoBehaviour
    {
        public Material terrainMaterial;  // Reference to the terrain's material
        public float changeSpeed = 1.0f;  // Speed at which the saturation wave moves
        private float timeValue = 0f;     // Time value used to control the shader
        private float currentValue = 0f;
        private bool startColorChange = false;


        private void Awake()
        {
            terrainMaterial.SetFloat("_TimeValue", 0f);
        }

        void Update()
        {
            if (startColorChange)
            {
                // Increase the time value over time to spread the saturation effect
                timeValue += Time.deltaTime * changeSpeed;
                currentValue = Mathf.Lerp(0, 255, timeValue);
                // Set the updated time value in the shader
                terrainMaterial.SetFloat("_TimeValue", currentValue);
            }
            if (timeValue > 255f)
            {
                Destroy(this);
            }
        }

        public void StartColorChange()
        {
            startColorChange = true;
        }
    }
}
