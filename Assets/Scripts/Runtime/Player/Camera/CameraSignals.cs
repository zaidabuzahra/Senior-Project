using RunTime.Cam;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraEnum> OnSwitchCamera = delegate { };
    }
}
