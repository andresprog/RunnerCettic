using System;
using UnityEngine;
using _ToolModules;


namespace _ComplementaryModules
{
    public class PanelBlock : MonoBehaviour
    {

        #region Variables

        [HideInInspector]
        public DriverInitMVC _driverInitMVC;

        public enum _Request { Activate, Deactivate }
        public enum _Notification { Tap }

        static Delegate _delegate;
        public delegate void Delegate(string Name, string Message);

        Transform _panel;
        public static PanelBlock _singleton;

        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region Mouth
        public void Notifications(_Notification notification)
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

        // Para el constructor "para recibir el buzon de otros modulos y enviarles mensajes"
        //Constructor
        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion




        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initialize_Module);
        }
        private void Initialize_Module()
        {
            _singleton = GetComponent<PanelBlock>();
        }
        #endregion



        #region Ear
        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Activate: Transparent_Panel(true); break;
                case _Request.Deactivate: Transparent_Panel(false); break;
                default:; break;
            }
        }

        void Transparent_Panel(bool parActive)
        {
            _driverInitMVC.SetActiveView(parActive);
        }
        #endregion




        #region OnClick
        public void Onclick_TAP()
        {
            Debug.Log("Tap");
            Notifications(_Notification.Tap);
        }
        #endregion


    }

}
