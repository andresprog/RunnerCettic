using System;
using System.Collections;
using UnityEngine;
using _ToolModules;

namespace _ComplementaryModules
{
    public class Result : MonoBehaviour
    {


        #region Variables
        // Inspector
        [Header("Audio")]
        public AudioSource aud_EfectAbi_WinBackground;
        public AudioSource aud_EfectAbi_Win;
        public AudioSource aud_EfectAbi_LoseBackground;
        public AudioSource aud_EfectAbi_Lose;
        public AudioSource aud_Star;
        public GameObject obj_Star;

        [HideInInspector] public DriverInitMVC _driverInitMVC;
        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;
        public GameObject obj_Panel_Win;
        public GameObject obj_Panel_Lose;

        public static Result _singleton;
        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region Ear
        public enum _Request { Show_Win, Show_Lose, Activate, Deactivate }
        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Show_Win: Win(); break;
                case _Request.Show_Lose: Lose(); break;

                case _Request.Activate: ; break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;
                default:; break;
            }
        }

        #region Implementation

        private void Win()
        {
            StartCoroutine(Pre_Wait_Win());
        }

        private void Lose()
        {
            StartCoroutine(Pre_Wait_Lose());
        }

        public IEnumerator Pre_Wait_Lose()
        {
            aud_EfectAbi_Lose.Play();
            aud_EfectAbi_LoseBackground.Play();
            yield return new WaitForSeconds(1.5f);
            _driverInitMVC.SetActiveView(true);
            obj_Panel_Win.SetActive(false);
            obj_Panel_Lose.SetActive(true);
            yield return new WaitForSeconds(2f);
            To_Finish();
        }
        public IEnumerator Pre_Wait_Win()
        {
            obj_Star.SetActive(false); // ocultando estrella
            aud_EfectAbi_Win.Play();
            aud_EfectAbi_WinBackground.Play();
            float f = aud_EfectAbi_WinBackground.clip.length;
            yield return new WaitForSeconds(1.5f);
            _driverInitMVC.SetActiveView(true);
            obj_Panel_Win.SetActive(true);
            obj_Panel_Lose.SetActive(false);
            yield return new WaitForSeconds(f - 1.5f);
            Anim_Star();
            yield return new WaitForSeconds(1.5f);
            To_Finish();
        }

        public void Anim_Star()
        {
            aud_Star.Play();
            obj_Star.SetActive(true);
        }

        // 2 - Apareciendo
        public void To_Finish()
        {
            Notifications(_Notification.Finished_Result);
        }
        #endregion


        #endregion



        #region Mouth
        public enum _Notification { Finished_Result }
        // Enivando Notificaciones
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
            _singleton = GetComponent<Result>();

            obj_Panel_Win = transform.GetChild(0).transform.GetChild(0).gameObject;
            obj_Panel_Lose = transform.GetChild(0).transform.GetChild(1).gameObject;
        }
        #endregion






    }

}
