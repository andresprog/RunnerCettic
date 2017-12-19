using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _ToolModules;

namespace _ComplementaryModules
{
    public class Countdown : MonoBehaviour
    {


        #region Variables
        [Header("Audio")]
        public AudioSource aud_Efect_Countdown;
        [Header("Elements")]
        public Text TextCount;

        private float delta;
        private float timer;
        float timeout;
        [HideInInspector] public DriverInitMVC _driverInitMVC;
        public static Countdown _singleton;
        CountdownView countdownView;
        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification { Finalized_Countdown }
        public enum _Request
        {
            Start_Countdown_1s,
            Start_Countdown_2s,
            Start_Countdown_3s,
            Start_Countdown_4s,
            Start_Countdown_5s,
            Start_Countdown_6s,
            Start_Countdown_7s,
            Start_Countdown_8s,

            Activate,
            Deactivate
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
                case _Request.Start_Countdown_1s: Start_Countdown(1); break;
                case _Request.Start_Countdown_2s: Start_Countdown(2); break;
                case _Request.Start_Countdown_3s: Start_Countdown(3); break;
                case _Request.Start_Countdown_4s: Start_Countdown(4); break;
                case _Request.Start_Countdown_5s: Start_Countdown(5); break;
                case _Request.Start_Countdown_6s: Start_Countdown(6); break;
                case _Request.Start_Countdown_7s: Start_Countdown(7); break;
                case _Request.Start_Countdown_8s: Start_Countdown(8); break;

                case _Request.Activate: _driverInitMVC.SetActiveView(true); break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;
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
            _driverInitMVC.Initialize(Initialize_Module);
        }

        private void Initialize_Module()
        {
            _singleton = GetComponent<Countdown>();
            // obteniendo clase de la vista
            countdownView = transform.GetChild(0).GetComponent<CountdownView>();
            TextCount = GameObject.FindGameObjectWithTag("GP1/Memoria/Countdown/TextCount").GetComponent<Text>();
            TextCount.gameObject.SetActive(false);
            timeout = 0;
        }

        private void OnEnable()
        {
            _singleton = transform.gameObject.GetComponent<Countdown>();
        }
        #endregion



        #region Body

        private void Start_Countdown(int n)
        {
            // iniciar animacion de cuenta regresiva
            countdownView.Anim_Init_Countdown();

            aud_Efect_Countdown.Play();
            timeout = n;
            _singleton.StartCoroutine(_singleton.Counter());
        }

        // 2 - Countdown
        private IEnumerator Counter()
        {
            TextCount.gameObject.SetActive(true);
            Reset_Timer();
            while (timer < timeout)
            {
                //Debug.Log("Aqui estoyyyyyy!!!!");
                timer = Time.time - delta;
                TextCount.text = Math.Round((timeout - timer), 0).ToString();
                yield return null;
            }

            // Avisar que terminamos de contar
            _MouthNotifications(_Notification.Finalized_Countdown);
            //_driverInitMVC.SetActive(false); // desactivando mi panel
        }


        private void Reset_Timer()
        {
            delta = Time.time; // reset delta
            timer = 0f; // reset timer
        }

        #endregion




    }

}
