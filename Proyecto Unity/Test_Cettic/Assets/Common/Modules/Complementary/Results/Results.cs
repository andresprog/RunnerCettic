using System.Collections;
using UnityEngine;
using System;
using _ToolModules;
using UnityEngine.UI;


namespace _ComplementaryModules
{
    public class Results : MonoBehaviour
    {

        #region varibales
        // Inspector
        [Header("Audio")]
        public AudioSource aud_star;
        public AudioSource aud_Results;
        [Header("Elements")]
        public GameObject panelButtons;
        public Text _text_Score;
        [Header("Components")]
        public ResultsView _resultsView;

        // Others
        public enum _Request { Show_Results, Activate, Deactivate }
        public enum _Notification
        {
            Restart,
            Go_To_The_Menu
        }

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;
        [HideInInspector] public DriverInitMVC _driverInitMVC;
        [HideInInspector] public GameObject score;
        int numStars;
        float Pf;
        float f;
        public static Results _singleton;
        #endregion



        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initialize_Module);
        }

        private void Initialize_Module()
        {
            _singleton = GetComponent<Results>();
            ResultsView._Singleton.Initialize_View();
        }
        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region Ear
        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Show_Results: Show_Results(); break;
                case _Request.Activate: _driverInitMVC.SetActiveView(true); break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;
                default:; break;
            }
        }


        #region Implementation

        void Show_Results()
        {
            StartCoroutine(AnimPuntajes());
        }

        public IEnumerator AnimPuntajes(int puntos = 0)
        {
            ResultsView._Singleton._Anim_Enter();
            aud_Results.Play();
            yield return new WaitForSeconds(0.5f);
        }

        #endregion

        #endregion



        #region Hand
        public void _Hand_points(float points)
        {
            _text_Score.text = points.ToString();
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
        //Constructor
        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion




        #region OnClick
        public void Onclick_CONTINUE()
        {
            _MouthNotifications(_Notification.Go_To_The_Menu);
        }

        public void Onclick_RESTART()
        {
            _MouthNotifications(_Notification.Restart);
        }
        #endregion

    }

}
