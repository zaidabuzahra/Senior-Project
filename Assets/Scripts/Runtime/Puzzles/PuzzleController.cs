using UnityEngine;

namespace RunTime
{
    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private ObjectInPlace[] conditions;
        [SerializeField] private ResolvedEvent[] resolvedEvents;

        public ApplyShader shader;
        public void Notify()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].Check())
                {
                    return;
                }
            }
            Debug.LogWarning("CONGRATS");
            for (int i = 0; i < resolvedEvents.Length; i++)
            {
                resolvedEvents[i].Resolved();
            }
            shader.StartColorChange();
        }
    }
}