using Cinemachine.Utility;
using System.Collections;
using System.Net;
using System.Timers;
using UnityEngine;

namespace RunTime
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private GameObject magnetColor;
        [SerializeField] private Material red, blue;
        
        private MagnetPole currentPole = MagnetPole.Red;

        private void OnEnable()
        {
            //InputSignals.Instance.OnInputUseUtilityPressed = UseUtility;
            InputSignals.Instance.OnInputFlipUtilityPressed += FlipUtility;
        }

        private void UseUtility()
        {

        }

        private void FlipUtility()
        {
            currentPole = currentPole == MagnetPole.Red ? MagnetPole.Blue : MagnetPole.Red;
            magnetColor.GetComponent<MeshRenderer>().material = currentPole == MagnetPole.Red ? red : blue;
        }
    }
}