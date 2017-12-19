using System;
using UnityEngine;
using UnityEngine.UI;
using _ToolModules;

namespace _ComplementaryModules
{
    public class Objetives : MonoBehaviour
    {


        #region Variables
        // Inspector
        [Header("Audio")]
        public AudioSource aud_ObjetivesSong;
        [Header("Elements")]
        public Text Couple; // Cabra
        public Text Stage; // Cabra

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;
        public enum _Request { Activate, Deactivate }
        public enum _Notification { Next }
        [HideInInspector] public DriverInitMVC _driverInitMVC;

        public static Objetives _singleton;
        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region Ear
        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Activate: _driverInitMVC.SetActiveView(true); ; break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); ; break;
                default:; break;
            }
        }
        #endregion



        #region Mouth
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

        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion



        #region Initialize

        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initilize_Module);
        }

        void Initilize_Module()
        {
            _singleton = GetComponent<Objetives>();
        }
        #endregion



        #region Hand
        public void _Hand_ShowObjetives()
        {
            aud_ObjetivesSong.Play();
            // Deberia pasarse el parametro para evitar la dependencia
            Stage.text = "OBJETIVOS"; //+ (currentStage + 1).ToString();
            // Deprecated
            // Couple.text = "x" + PointsBar._numOfObjetives; //CACHOOOOOOOOOOO
            // Stage.text = "NIVEL " + (GameplayManager.etapaActual + 1).ToString(); //CACHOOOOOOOOOOO
        }
        #endregion



        #region OnClick
        public void OnClick_NEXT()
        {
            aud_ObjetivesSong.Stop();
            _MouthNotifications(_Notification.Next);
            _driverInitMVC.SetActiveView(false);
        }
        #endregion

    }

}
