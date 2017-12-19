using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _ComplementaryModules
{
    public class CurtainView : MonoBehaviour
    {

        #region Variables
        private float _timer;
        private float _delta;
        private float _timeout;
        private float _alpha;

        // Inspector
        [Header("Elements")]
        public Image _Img_Curtain;
        public Image _Img_Logo;
        [Header("Components")]
        public Animator _myAnimator;
        public AnimationClip _anim_Twist;
        public AnimationClip _anim_Alpha;
        #endregion


        #region Initialize
        public void Initialize_View()
        {
            _Img_Curtain.gameObject.SetActive(true);
            _Img_Logo.gameObject.SetActive(false);
        }
        #endregion


        #region Animation Triggers

        public IEnumerator Raise_Curtain_Twist()
        {
            _Img_Curtain.gameObject.SetActive(false);
            _Img_Logo.gameObject.SetActive(true);

            float t = _anim_Twist.length/2;
            _myAnimator.SetTrigger("RaiseCurtainTwist");
            yield return new WaitForSeconds(t);

            Curtain._singleton._MouthNotifications(Curtain._Notification.Raised_Curtain);
            _Img_Logo.gameObject.SetActive(false);
        }

        public IEnumerator Lower_Curtain_Twist()
        {
            _Img_Curtain.gameObject.SetActive(false);
            _Img_Logo.gameObject.SetActive(true);

            float t = _anim_Twist.length/2;
            _myAnimator.SetTrigger("LowerCurtainTwist");
            yield return new WaitForSeconds(t);

            Curtain._singleton._MouthNotifications(Curtain._Notification.Lowered_Curtain);
        }

        public IEnumerator Raise_Curtain_Alpha()
        {
            _Img_Curtain.gameObject.SetActive(true);
            _Img_Logo.gameObject.SetActive(false);

            float t = _anim_Alpha.length;
            _myAnimator.SetTrigger("RaiseCurtainAlpha");
            yield return new WaitForSeconds(t);

            Curtain._singleton._MouthNotifications(Curtain._Notification.Raised_Curtain);
            _Img_Curtain.gameObject.SetActive(false);
        }

        public IEnumerator Lower_Curtain_Alpha()
        {
            _Img_Curtain.gameObject.SetActive(true);
            _Img_Logo.gameObject.SetActive(false);

            float t = _anim_Alpha.length;
            _myAnimator.SetTrigger("LowerCurtainAlpha");
            yield return new WaitForSeconds(t);

            Curtain._singleton._MouthNotifications(Curtain._Notification.Lowered_Curtain);
        }

        #endregion


    }
}
