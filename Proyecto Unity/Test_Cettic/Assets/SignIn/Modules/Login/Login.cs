using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _ToolModules;
using _CentralModules;

namespace SignInModules
{
    public class Login : MonoBehaviour/*, IControlador*/
    {


        #region Variables

        private static Login _singleton;
        public static Login _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<Login>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        public LoginView _loginView;
        private bool _fieldEmpty = false;
        public bool SesionIniciada = false;
        private int attempts = 0;
        #endregion



        #region Initialize
        [HideInInspector] public DriverInitMVC _driverInitMVC;

        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initialize_Module);
        }

        public void Initialize_Module()
        {
            _loginView.Initilize_View();
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



        #region Running

        IEnumerator _Login()
        {
            _Check_Empty_Fields_In_LoginView();

            if (_fieldEmpty == true)
            {
                _loginView._text_Message.text = "Falta uno o más campos por llenar";
                _fieldEmpty = false;
            }
            else
            {
                _loginView._Anim_Loading(true);
                // Data de Usuario
                Traveler._Singleton._strUser = _loginView._inpText_User.text;

                WWW connection = new WWW("http://cofradinn.com/cettic/game/login.php?uss=" + _loginView._inpText_User.text + "&pass=" + _loginView._inpText_Password.text);
                while (!connection.isDone)
                {
                    // Debug.Log("Estoy Cargando");
                    yield return (connection);

                    if (connection.text == "bien")
                    {
                        Debug.Log("Sesion Iniciada Exitosamente");
                        LoadingTool._singleton._SetLoad(LoadingTool._SceneList.Menu);
                    }
                    else if (connection.text == "mal")
                    {
                        _loginView._text_Message.text = "Usuario o Contraseña incorrectos";
                        Debug.Log("Usuario o Contraseña incorrectos");
                        attempts++;
                        if (attempts > 2)
                        {
                            Debug.Log("Aviso: Casi Supera el numero de intentos");
                            _loginView._text_Message.text = "Aviso: Casi Supera el numero de intentos";
                        }
                    }
                    else
                    {
                        Debug.Log("Problemas en la conexion");
                    }

                }
                _loginView._Anim_Loading(false);
            }
        }


        void _Check_Empty_Fields_In_LoginView()
        {
            if (_loginView._inpText_User.text == "")
            {
                _loginView._textField_User.text = "Falto este campo";
                _fieldEmpty = true;
            }
            if (_loginView._inpText_Password.text == "")
            {
                _loginView._textField_Password.text = "Falto este campo";
                _fieldEmpty = true;
            }
        }

        #endregion



        #region Onclick

        public void _Onclick_Login()
        {
            StartCoroutine(_Login());
        }

        public void _Onclick_Register()
        {
            Register._Singleton._driverInitMVC.SetActiveView(true);
            _driverInitMVC.SetActiveView(false);
        }

        #endregion

    }
}
