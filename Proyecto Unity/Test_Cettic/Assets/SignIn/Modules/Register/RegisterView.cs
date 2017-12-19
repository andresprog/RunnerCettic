using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SignInModules
{
    public class RegisterView : MonoBehaviour
    {
        #region Variables

        // Register
        public InputField _inpText_Name;
        public InputField _inpText_User;
        public InputField _inpText_Password;

        public Text _textField_Name;
        public Text _textField_User;
        public Text _textField_Password;

        public Text _text_Mesagge;
        public Image _img_Loading;

        public Button _Btn_Return;
        public Button _Btn_Send;

        private Animator _myAnimator;
        #endregion


        #region Initialize

        public void _Initilize_View()
        {
            _myAnimator = GetComponent<Animator>();
        }
        #endregion


        #region Animations

        public void _Anim_Loading(bool b)
        {
            if (b)
            {
                _inpText_Name.interactable = false;
                _inpText_User.interactable = false;
                _inpText_Password.interactable = false;
                _Btn_Return.interactable = false;
                _Btn_Send.interactable = false;
                _img_Loading.gameObject.SetActive(b);
                _myAnimator.SetBool("Loading", b);
            }
            else
            {
                _inpText_Name.interactable = true;
                _inpText_User.interactable = true;
                _inpText_Password.interactable = true;
                _Btn_Return.interactable = true;
                _Btn_Send.interactable = true;
                _myAnimator.SetBool("Loading", b);
                _img_Loading.gameObject.SetActive(b);
            }

        }
        #endregion
    }
}
