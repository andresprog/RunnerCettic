using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _ToolModules;

namespace SignInModules
{
    public class Msg : MonoBehaviour
    {

        #region Variables
        private static Msg _singleton;
        public static Msg _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<Msg>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        #endregion


        #region Initialize
        [HideInInspector] public DriverInitMVC _driverInitMVC;

        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Inicialize_Module);
        }

        public void Inicialize_Module()
        {
        }



        #endregion


        #region Ear

        public enum _Request
        {
            Activate,
            Deactivate
        }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Activate: _driverInitMVC.SetActiveView(true); break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;
                default:; break;
            }
        }


        #endregion


        #region Mouth

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification
        {
            Continue

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


        #region Onclick

        public void Onclick_Go_To_Login ()
        {
            _MouthNotifications(_Notification.Continue);

        }
        #endregion
    }
}
