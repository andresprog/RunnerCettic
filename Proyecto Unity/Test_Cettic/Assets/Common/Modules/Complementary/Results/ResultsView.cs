using UnityEngine;

namespace _ComplementaryModules
{
    public class ResultsView : MonoBehaviour
    {
        public AnimationClip _anim_Enter;
        private static ResultsView _singleton;
        private Animator _myAnimator;

        public static ResultsView _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<ResultsView>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        public void Initialize_View()
        {
            _myAnimator = GetComponent<Animator>();
        }




        public void _Anim_Enter()
        {
            _myAnimator.SetTrigger("ToEnter");
        }

        public void _Anim_Exit()
        {
            _myAnimator.SetTrigger("ToExit");
        }



    }
}
