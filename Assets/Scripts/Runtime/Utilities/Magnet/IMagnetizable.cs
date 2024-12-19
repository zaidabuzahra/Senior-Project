using System.Numerics;

namespace RunTime
{
    public interface IMagnetizable
    {
        public void Push(Vector3 direction, MagnetPole magnetPole);
        public void Pull(Vector3 direction, MagnetPole magnetPole);
    }
}