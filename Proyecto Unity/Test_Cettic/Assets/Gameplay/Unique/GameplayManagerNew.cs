using _ComplementaryModules;
using _ToolModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _CentralModules;
using UnityStandardAssets.Characters.ThirdPerson;

namespace _GameplayModules
{
    public class GameplayManagerNew : MonoBehaviour
    {


        #region Variables

        private static GameplayManagerNew _singleton;
        public static GameplayManagerNew _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<GameplayManagerNew>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        public static int _points;

        [Header("Elements")]
        public GameObject _character;

        // Inspector
        [Header("Level Configuration")]
        [Header("Parameters Of The Game")]
        [Tooltip("True: Para habilitar edición por inspector, False: Para deshabilitarla")]
        public bool _configInInspectorEnable;
        [Range(0, 100)]
        public int _itemValue;
        [Range(0, 5)]
        public int _characterSpeed;
        [Tooltip("duration of the game (seconds)")]
        [Range(30,1000)]
        public int _levelTime;
        
        [Header("Read-Only Parameters")]
        [SerializeField] private int _r_itemValue;
        [SerializeField] private int _r_characterSpeed;
        [SerializeField] private int _r_levelTime;
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
            Item._NotifyMe(_EarNotifications);
            _configInInspectorEnable = false;
            // character collector of items
            Item._Setup(_character);
            Transporter._Setup(_character);
        }
        #endregion



        #region Setup

        public void _Setup()
        {
            // Alternando el tipo de configuracion de las variables de configuracion del juego
            if (!_configInInspectorEnable)
            {
                // Recuperando datos de la base de datos local
                _r_itemValue = DataManager._Singleton._MySQL_itemValue;
                Debug.Log("_r_itemValue: " + _r_itemValue);
                _r_characterSpeed = DataManager._Singleton._MySQL_characterSpeed;
                _r_levelTime = DataManager._Singleton._MySQL_levelTime;
                _itemValue = 0;
                _characterSpeed = 0;
                _levelTime = 0;
            }
            else
            {
                Debug.Log("_r_itemValue: " + _r_itemValue);
                _r_itemValue = _itemValue;
                _r_characterSpeed = _characterSpeed;
                _r_levelTime = _levelTime;
            }

            Debug.Log("Cuantos");
            PointsBar._singleton._Setup(_r_levelTime);
        }
        #endregion




        #region Ear

        public enum _Request { Activate, Deactivate }

        public void _EarRequest(_Request request)
        {
            switch (request)
            {
                case _Request.Activate: _driverInitMVC.SetActiveView(true); break;
                case _Request.Deactivate: _driverInitMVC.SetActiveView(false); break;

                default:; break;
            }
        }

        public void _EarNotifications(string name, string notification)
        {
            if (notification == Item._Notification.Accumulated_Item.ToString())
            {
                _points = _points + _r_itemValue;
                Debug.Log(notification);
                Debug.Log(_points);
                Debug.Log(_r_itemValue);
            }
        }

        #endregion


        #region Running
        private void FixedUpdate()
        {
            // si se estan modificando lo sparametros desde el inspector actualizar en tiempo real
            if (_configInInspectorEnable)
            {
                _r_itemValue = _itemValue;
                PointsBar._singleton._Setup(_levelTime);
                ThirdPersonCharacter._Singleton.m_MoveSpeedMultiplier = _characterSpeed;
            }
        }

        #endregion

    }
}
