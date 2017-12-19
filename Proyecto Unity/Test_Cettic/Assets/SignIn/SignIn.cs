using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SignInModules
{
    public class SignIn : MonoBehaviour
    {
        private static SignIn _singleton;
        public static SignIn _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<SignIn>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }


        void Start()
        {
            Login._Singleton._EarRequest(Login._Request.Activate);
            Msg._Singleton._NotifyMe(_EarNotifications);
        }


        #region Ear

        public void _EarNotifications(string name, string notification)
        {
            if (notification == Msg._Notification.Continue.ToString())
            {
                Login._Singleton._EarRequest(Login._Request.Activate);
                Register._Singleton._EarRequest(Register._Request.Deactivate);
                Msg._Singleton._EarRequest(Msg._Request.Deactivate);
            }
        }
        #endregion



        #region Onclick

        public void Onclick_Exit()
        {
            Application.Quit();
        }

        #endregion
    }
}
