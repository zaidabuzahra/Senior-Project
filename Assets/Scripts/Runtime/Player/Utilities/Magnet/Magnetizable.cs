using UnityEngine;

namespace RunTime
{
    public abstract class Magnetizable : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected GameObject highlightObject;
        [SerializeField] protected float forceStrength;
        [SerializeField] protected MagnetPole pole;

        public abstract void Interact(Vector3 direction, MagnetPole magnetPole);
        public virtual void HighlightTarget() 
        {
            highlightObject.SetActive(true);
        }

        public virtual void GrayoutTarget()
        {
            highlightObject.SetActive(false);
        }
    }
}