using UnityEngine;
using System;
using _ToolModules;
using _LoadingModules;

namespace _ComplementaryModules
{
    public class Pause : MonoBehaviour
    {


        #region Variables
        DriverInitMVC driverInitMVC;
        public enum Notification { Exit_Game }
        static Delegate _delegate;
        public delegate void Delegate(string Name, string Message);

        [Header("Asynchronous Modules")]
        public MsgBox msgBoxController;
        public LoadingTool driverLoading;

        public static Pause _singleton;
        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region Mouth
        public void Notifications(Notification notification)
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

        // Subscribe 
        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion



        #region Body

        #endregion



        #region Initialize
        private void Start()
        {
            driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            driverInitMVC.Initialize(Initialize_Module);
        }

        private void Initialize_Module()
        {
            _singleton = GetComponent<Pause>();
        }
        #endregion



        #region OnClick
        public void Onclick_PAUSE()
        {
            driverInitMVC.SetActiveView(true);
            Time.timeScale = 0;
        }

        public void Onclick_PLAY()
        {
            driverInitMVC.SetActiveView(false);
            Time.timeScale = 1;
        }

        public void Onclick_EXIT()
        {
            msgBoxController.driverInitMVC.SetActiveView(true);
            msgBoxController.Hand_GenerateBoxMessage("SALIR?", Receive_Answers);
        }
        #endregion



        #region Receive Answers MsgBox
        public void Receive_Answers(string Name, string Message)
        {
            // Answer: EXIT GAMEPLAY?
            if (Message == MsgBox._Answers.Yes.ToString())
            {
                Time.timeScale = 1; // Normalized timeScale
                LoadingTool._singleton._SetLoad(LoadingTool._SceneList.Menu);
            }
            if (Message == MsgBox._Answers.No.ToString())
            {
                msgBoxController.driverInitMVC.SetActiveView(false);
            }
        }
        #endregion


    }
}
