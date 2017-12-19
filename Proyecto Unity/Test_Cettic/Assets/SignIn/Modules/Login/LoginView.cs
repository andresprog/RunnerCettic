using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SignInModules
{
    public class LoginView : MonoBehaviour /*,IVista*/
    {

        #region Variables

        // Login
        public InputField _inpText_User;
        public InputField _inpText_Password;

        public Text _textField_User;
        public Text _textField_Password;

        public Text _text_Message;
        public Image _img_Loading;

        public Button _Btn_Login;
        public Button _Btn_Register;

        private Animator _myAnimator;


        #endregion


        #region Initialize

        public void Initilize_View()
        {
            _myAnimator = GetComponent<Animator>();
        }
        #endregion


        #region Animations

        public void _Anim_Loading(bool b)
        {
            if (b)
            {
                _inpText_User.interactable = false;
                _inpText_Password.interactable = false;
                _Btn_Login.interactable = false;
                _Btn_Register.interactable = false;
                _img_Loading.gameObject.SetActive(b);
                _myAnimator.SetBool("Loading", b);
            }
            else
            {
                _inpText_User.interactable = true;
                _inpText_Password.interactable = true;
                _Btn_Login.interactable = true;
                _Btn_Register.interactable = true;
                _myAnimator.SetBool("Loading", b);
                _img_Loading.gameObject.SetActive(b);
            }
  
        }
        #endregion

    }
}
