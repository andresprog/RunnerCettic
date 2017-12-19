using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using _ToolModules;
using System;

namespace _GameplayModules
{
    public class PointsBar : MonoBehaviour
    {


        #region Variables


        [HideInInspector] public float _Po;
        [HideInInspector] public float _Pf;
        [HideInInspector] public DriverInitMVC _driverInitMVC;

        public static PointsBar _singleton;
        public static int _numOfObjetives;
        static string _strAttached;
        float _speedAnim;
        bool _running;
        float _delta;
        float _timer;

        //static float barraValue;
        static int _objetivesAchieved;

        private static bool _stopTimer;
        float _timeLevel;
        [Range(5, 60)]
        [HideInInspector]
        public int _limitTimeLevel;
        public Text txtTime;
        public Text txtScore;
        #endregion

        #region Setup

        public void _Setup(int limitTimeLevel)
        {
            _limitTimeLevel = limitTimeLevel;
        }

        #endregion

        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(_Initialize_Module);
        }

        void _Initialize_Module()
        {
            //Debug.Log("Activado HUD2");
            _singleton = GetComponent<PointsBar>();

            _limitTimeLevel = 60;
            _running = false;
            _objetivesAchieved = 0;
            _Po = 0f;
            _Pf = 0;
            _speedAnim = 1f;
            _stopTimer = false;
        }

        #endregion



        #region Ear
        public enum _Request
        {
            Show,
            Hide,
            Start_Time
        }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Show: _driverInitMVC.SetActiveView(true); break;
                case _Request.Hide: _driverInitMVC.SetActiveView(false); break;
                case _Request.Start_Time: Start_Time(); break;
                default:; break;
            }
        }

        void Start_Time()
        {
            _running = true;
            Play_Timer();
        }

        void Play_Timer()
        {
            _delta = Time.timeSinceLevelLoad;
            _timer = 0;
        }
        #endregion


        #region Mouth

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification
        {
            Time_is_up
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



        #region running

        void FixedUpdate()
        {
            if (_running)
            {

                if (((Time.timeSinceLevelLoad - _delta) >= _limitTimeLevel) && (!_stopTimer))
                {
                    //Debug.Log(_stopTimer);
                    _stopTimer = true;
                    _MouthNotifications(_Notification.Time_is_up);
                    txtTime.text = "Tiempo: " + _limitTimeLevel.ToString("F2");
                }

                if (!_stopTimer)
                {
                    _timeLevel = Time.timeSinceLevelLoad;
                    txtTime.text = "Tiempo: " + (Time.timeSinceLevelLoad - _delta).ToString("F2");
                    txtScore.text = "Puntos: " + GameplayManagerNew._points.ToString();
                    // transform.position = new Vector3(posicionCamara.transform.position.x,posicionCamara.transform.position.y, transform.position.z);
                }
            }
        }





        #endregion


    }

}
