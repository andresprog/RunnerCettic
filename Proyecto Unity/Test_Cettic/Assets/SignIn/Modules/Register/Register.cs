using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _ToolModules;

namespace SignInModules
{
    public class Register : MonoBehaviour
    {


        #region Variables
        [Header("Components")]
        public RegisterView _registerView;

        private static Register _singleton;
        public static Register _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<Register>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        private bool _fieldEmpty;
        [HideInInspector] public DriverInitMVC _driverInitMVC;
        #endregion


        #region Initialize

        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Inicialize_Module);
        }

        public void Inicialize_Module()
        {
            _fieldEmpty = false;
            _registerView._Initilize_View();
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

        IEnumerator _SendRegister()
        {
            _Check_Empty_Fields_In_RegisterView();

            if (_fieldEmpty == true)
            {
                _registerView._text_Mesagge.text = "Falta uno o mas campos por llenar";
                _fieldEmpty = false;
            }
            else
            {
                _registerView._Anim_Loading(true);

                _registerView._text_Mesagge.text = "";
                WWW connection = new WWW("http://cofradinn.com/cettic/game/register.php?nombre=" + _registerView._inpText_Name.text + "&uss=" + _registerView._inpText_User.text + "&pass=" + _registerView._inpText_Password.text);
                while (!connection.isDone)
                //while(true)
                {
                    //Debug.Log("Estoy Cargando");

                    yield return (connection);
                    if (connection.text == "el usuario ya existe")
                    {
                        _registerView._text_Mesagge.text = "El usuario ya existe!";
                        Debug.Log("el usuario ya existe");
                    }
                    else if (connection.text == "usuario registrado")
                    {
                        Msg._Singleton._EarRequest(Msg._Request.Activate);
                        Debug.Log("Usuario registrado exitosamente!");
                    }
                    else
                    {
                        _registerView._text_Mesagge.text = "Problemas con la conexion";
                        Debug.Log("Problemas en la conexion");
                    }

                }
                _registerView._Anim_Loading(false);

            }
        }


        void _Check_Empty_Fields_In_RegisterView()
        {
            if (_registerView._inpText_Name.text == "")
            {
                _registerView._textField_Name.text = "Falto este campo";
                _fieldEmpty = true;
            }
            if (_registerView._inpText_User.text == "")
            {
                _registerView._textField_User.text = "Falto este campo";
                _fieldEmpty = true;
            }
            if (_registerView._inpText_Password.text == "")
            {
                _registerView._textField_Password.text = "Falto este campo";
                _fieldEmpty = true;
            }

        }

        #endregion



        #region Onclick

        // Btn Send / RegisterView
        public void _Onclick_Send()
        {
            StartCoroutine(_SendRegister());
        }

        public void _Onclick_Return()
        {
            Login._Singleton._driverInitMVC.SetActiveView(true);
            _driverInitMVC.SetActiveView(false);
        }

        #endregion
    }
}
