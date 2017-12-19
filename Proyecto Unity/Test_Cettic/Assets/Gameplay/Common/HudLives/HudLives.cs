using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _ToolModules;


namespace _GameplayModules
{
    public class HudLives : MonoBehaviour
    {


        #region Variables
   
        [HideInInspector] public DriverInitMVC _driverInitMVC;
        private int _totalNumLives;         // 0 - 3 ej

        private static HudLives _singleton;
        public static HudLives _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<HudLives>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        List<GameObject> _listOfLives;

        //Inspector
        // new lifes num
        [Header("Elements")]
        public Text _txtLives;
        public GameObject _obj_PanelLives;

        #endregion



        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initialize_Module);
        }

        void Initialize_Module()
        {
            _singleton = GetComponent<HudLives>();
            _Setup();
        }


        HudLives singleton;
        private void OnEnable()
        {
            singleton = transform.gameObject.GetComponent<HudLives>();
        }
        #endregion



        #region Ear

        public enum _Request
        {
            Play_Timer,
            Pause_Timer,
            Increase_Live,
            Discount_Live,
            Show_Timer,
            Hide_Timer
        }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Discount_Live: _Discount_Live(); break;
                case _Request.Show_Timer: _driverInitMVC.SetActiveView(true); break;
                case _Request.Hide_Timer: _driverInitMVC.SetActiveView(false); break;
                default: Debug.Log(request.ToString() + ": This options does not Available"); break;
            }
        }


        #region Implementation

        public void _Discount_Live()
        {
            if (_totalNumLives > 1)
            {
                _totalNumLives--;
                _SetLives(_totalNumLives);
            }
            else
            {
                _totalNumLives--;
                _SetLives(_totalNumLives);
                _MouthNotifications(_Notification.Lives_Finished);
                //Debug.LogError("Las vidas ya se agotaron");
            }

        }

        #endregion

        #endregion



        #region Mouth

        public enum _Notification
        {
            Lives_Finished,
            Increased_Live,
            Discounted_Live,
        }

        public void _MouthNotifications(_Notification notification)
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

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public void _NotifyMe(Delegate parBuzon)
        {
            _delegate = parBuzon;
        }
        #endregion



        #region Setup

        public enum _ConfigLives
        {
            Lives_1 = 1,
            Lives_2 = 2,
            Lives_3 = 3,
            Lives_4 = 4,
            Lives_5 = 5,
            Lives_6 = 6,
            Lives_7 = 7,
            Lives_8 = 8,
            Lives_9 = 9,
            Lives_10 = 10
        }
        // Configuracion inicial
        public void _Setup(_ConfigLives parLives = _ConfigLives.Lives_3, int time = 30)
        {
            _driverInitMVC.SetActiveView(true);

            _totalNumLives = (int)parLives;

            // inicializando texto vidas
            _txtLives.text = _totalNumLives.ToString();

            _InstantiatorOfImg();

            _driverInitMVC.SetActiveView(false);

        }
        #endregion



        #region Tools
        // Modificador de Escena
        void _SetLives(int parLives)
        {
            // txt vidas
            _txtLives.text = parLives.ToString();
            // img corazones
            _listOfLives[parLives].SetActive(false);
        }


        void _InstantiatorOfImg()
        {
            _listOfLives = new List<GameObject>();

            _driverInitMVC.SetActiveView(true);

            foreach (Transform child in _obj_PanelLives.transform)
            {
                Destroy(child.gameObject);
            }


            //Debug.Log("totalNumVidas: " + totalNumVidas);
            for (int i = 0; i < _totalNumLives; i++)
            {
                _listOfLives.Add(Instantiate(Resources.Load("Prefabs/PrefabVida") as GameObject));
                _listOfLives[i].transform.SetParent(_obj_PanelLives.transform);
                _listOfLives[i].transform.localScale = new Vector3(1f, 1f, 1f);

            }

        }
        #endregion




        #region Properties

        public int NumVidasActual
        {
            get
            {
                return _totalNumLives;
            }

            set
            {
                Debug.LogError("La variable: NumVidas, no puede ser modificada manualmente por un modulo externo");
            }
        }
        #endregion





  


    }

}
