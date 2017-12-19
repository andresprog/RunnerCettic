using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {

        #region Variables

        private GameObject _character;
        [Header("Config")]
        [Range(0, 30)]
        public float _speedH;
        public static bool _RunOff;

        private static ThirdPersonUserControl _singleton;
        public static ThirdPersonUserControl _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<ThirdPersonUserControl>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        #endregion



        #region Initialize

        private void Initialize_Module()
        {
            _RunOff = true;
            _character = gameObject;
        }

        #endregion



        #region Ear

        public enum _Request
        {
            RunOn,
            RunOff
        }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.RunOn: _RunOff = false; break;
                case _Request.RunOff: _RunOff = true; break;

                default:; break;
            }
        }


        #endregion





        #region Asset

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

            Initialize_Module();
            crouch = false;
        }


        private void Update()
        {
            if (!_RunOff)
            {
                if (!m_Jump)
                {
                    m_Jump = Input.GetKey(KeyCode.Space);
                }
            }
        }

        bool crouch;
        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                //m_Move = v*m_CamForward + h*m_Cam.right;
                m_Move = 1 * m_CamForward; // + h * m_Cam.right;

                if (!_RunOff)
                {
                    Vector3 _vH = new Vector3(h, 0, 0).normalized;
                    _character.transform.Translate((_speedH / 100) * _vH);
                }
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif
            if (_RunOff)
            {
                m_Move = new Vector3(0, 0, 0);
            }
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);

            m_Jump = false;


        }

        #endregion

    }
}
