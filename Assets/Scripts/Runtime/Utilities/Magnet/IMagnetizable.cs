using UnityEngine;

namespace RunTime
{
    public interface IMagnetizable
    {
        public void Interact(Vector3 direction, MagnetPole magnetPole);
        public void HighlightTarget();
        public void GrayoutTarget();
    }
}