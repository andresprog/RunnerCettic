using UnityEngine;

namespace _CentralModules
{
    public class Traveler : MonoBehaviour
    {
        private static Traveler _singleton;
        public static Traveler _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<Traveler>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        //public int numGameVowel = 0;
        public string strSceneName = "SignIn";

        public string _strUser = "";
        public string _strName = "";

        #region Don't Destroy
        public static Traveler traveler;

        void Awake()
        {
            if (traveler == null)
            {
                traveler = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (traveler != this)
                {
                    Destroy(gameObject);
                }
            }
        }
        #endregion

    }
}