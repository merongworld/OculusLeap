using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

namespace WidgetShowcase
{
    public class ArmHUDMGR : MonoBehaviour
    {
        [Header("Movement")]
        private int targetHandID = -1;
        public float verticalFilterTime = 0f; // Optimal tracking data
        public float horizontalFilterTime = 0f; // Reduced tracking data
        private SmoothedVector3 smoothedPosition;
        private SmoothedQuaternion smoothedRotation;

        [Header("References")]
        public CapsuleHand hand;
        public GameObject ArmHUDgeom;
        public Transform ArmHUDbaseLookAtGRP;

        [SerializeField]
        private List<ButtonDemoToggle> m_SliderToggleButtons;

        public Text StatusPanelDateTimeText;
        public Text StatusPanelTimeText;

        void Awake()
        {
            smoothedPosition = new SmoothedVector3();
            smoothedRotation = new SmoothedQuaternion();
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {        
            if (hand.GetLeapHand().IsLeft == true)
            {
                AlignArmHUD(hand);
            }
        }

        void AlignArmHUD(CapsuleHand hand)
        {
            ArmHUDgeom.SetActive(true);
            if (targetHandID != hand.GetInstanceID())
            {
                smoothedPosition.reset = true;
                smoothedRotation.reset = true;
                targetHandID = hand.GetInstanceID();

                float sqrCos = hand.GetLeapHand().Arm.Direction.y;
                sqrCos *= sqrCos;
                float delay = sqrCos * verticalFilterTime + (1f - sqrCos) * horizontalFilterTime;
                smoothedPosition.delay = delay;
                smoothedRotation.delay = delay; ;
                Vector3 tts = UnityVectorExtension.ToVector3(hand.GetLeapHand().Arm.Center);
                //Debug.Log("Tts : " + tts);
                //Vector3 localArmCenter = transform.parent.InverseTransformPoint(tts);
                Vector3 localArmCenter = tts;
                //Debug.Log(localArmCenter);
                //Quaternion localArmRotation = Quaternion.Inverse(transform.parent.rotation) * UnityQuaternionExtension.ToQuaternion(hand.GetLeapHand().Arm.Rotation);
                Quaternion localArmRotation = UnityQuaternionExtension.ToQuaternion(hand.GetLeapHand().Arm.Rotation);
                Debug.Log(localArmRotation.y);
                localArmRotation.y -= 0.5f;
                transform.localPosition = smoothedPosition.Update(localArmCenter, Time.deltaTime);
                transform.localRotation = smoothedRotation.Update(localArmRotation, Time.deltaTime);
                ArmHUDgeom.transform.position = localArmCenter;
                //Debug.Log(ArmHUDgeom.transform.localPosition);
            }
            targetHandID = -1;
        }
        /*
        public void UpdateArmHUDGUIvalues()
        {
            if (TimeAndLocationHandler.Instance)
            {
                SettingsPanelDateTimeText.text = TimeAndLocationHandler.Instance.DateAndTime.ToLongDateString();
                StatusPanelDateTimeText.text = TimeAndLocationHandler.Instance.DateAndTime.ToLongDateString();
                StatusPanelTimeText.text = TimeAndLocationHandler.Instance.DateAndTime.ToShortTimeString() + " GMT";
            }
            AsterismnBrightnessText.text = Convert.ToInt32((asterismBrightnessDataBinder.GetCurrentData() * 100f)).ToString() + "%";
            StarNameBrightnessText.text = Convert.ToInt32((starNameBrightnessDataBinder.GetCurrentData() * 100f)).ToString() + "%";
            MilkyWayIntensityText.text = Convert.ToInt32((milkyWayIntensityDataBinder.GetCurrentData() * 100f)).ToString() + "%";
            LuminanceFilterText.text = Convert.ToInt32((luminanceFilterDataBinder.GetCurrentData() * 100f)).ToString() + "%";
            StarSaturationText.text = Convert.ToInt32((starSaturationDataBinder.GetCurrentData() * 100f)).ToString() + "%";
            DepthControlDataBinderText.text = Convert.ToInt32((depthControlDataBinder.GetCurrentData() * 100f)).ToString() + "%";
        }
        */
    }
}