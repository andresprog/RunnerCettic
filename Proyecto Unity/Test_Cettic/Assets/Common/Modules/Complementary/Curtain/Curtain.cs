using System;
using UnityEngine;
using _ToolModules;

namespace _ComplementaryModules
{
    public class Curtain : MonoBehaviour
    {

        #region Variables

        [HideInInspector] public DriverInitMVC _driverInitMVC;
        public static Curtain _singleton;
        public CurtainView curtainView;

        _Congig _animation = _Congig.Anim_1_Alpha;

        #endregion


        #region Setup
        // Initial setup (Configuracion inicial)

        public enum _Congig
        {
            Anim_1_Alpha = 1,
            Anim_2_Twist = 2
        }

        public void _Setup(_Congig animation = _Congig.Anim_1_Alpha)
        {
            _animation = animation;
        }
        #endregion


        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Inicialize_Module);
        }

        public void Inicialize_Module()
        {
            _singleton = GetComponent<Curtain>();
        }
        #endregion


        #region Ear

        public enum _Request
        {
            Raise_Curtain,
            Lower_Curtain,
            Activate,
            Deactivate
        }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Raise_Curtain: SetRaiseCurtain(); break;
                case _Request.Lower_Curtain: SetLowerCurtain(); break;
                case _Request.Activate: _driverInitMVC.SetActiveView(true); break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;
                default: break;
            }
        }

        #region Implementation

        private void SetRaiseCurtain()
        {
            if (_animation == _Congig.Anim_1_Alpha)
            {
                curtainView.StartCoroutine(curtainView.Raise_Curtain_Alpha());
            }
            if (_animation == _Congig.Anim_2_Twist)
            {
                curtainView.StartCoroutine(curtainView.Raise_Curtain_Twist());
            }
        }

        private void SetLowerCurtain()
        {
            if (_animation == _Congig.Anim_1_Alpha)
            {
                curtainView.StartCoroutine(curtainView.Lower_Curtain_Alpha());
            }
            if (_animation == _Congig.Anim_2_Twist)
            {
                curtainView.StartCoroutine(curtainView.Lower_Curtain_Twist());
            }
        }

        #endregion

        #endregion


        #region Mouth

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification
        {
            Raised_Curtain,
            Lowered_Curtain
        }

        public void _MouthNotifications(_Notification notification)
        {
            try
            {
                _delegate(transform.name, notification.ToString());
            }
            catch (NullReferenceException)
            {
                //Debug.Log(transform.name + ": Delegado NullReferenceException, Nadie se suscribio");
            }
        }
        //Constructor
        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion


        #region Hand
        #endregion

    }

}
