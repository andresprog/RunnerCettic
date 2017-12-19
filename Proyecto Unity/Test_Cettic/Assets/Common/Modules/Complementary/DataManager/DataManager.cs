using _ToolModules;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _ComplementaryModules
{
    public class DataManager : MonoBehaviour
    {


        #region Variables
        private static DataManager _singleton;
        public static DataManager _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<DataManager>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        public DataManagerView _dataManagerView;

        [HideInInspector] public int _MySQL_characterSpeed;
        [HideInInspector] public int _MySQL_levelTime;
        [HideInInspector] public int _MySQL_itemValue;
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
            _dataManagerView.Initialize_View();
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


        #region Hand

        public void _Hand_Read_Game_Config_MySQL()
        {
            StartCoroutine(Read_Game_Config_MySQL());
        }

        IEnumerator Read_Game_Config_MySQL()
        {
            _driverInitMVC.SetActiveView(true);
            _dataManagerView._Show_WindowLoading(true, "Cargando...");

            string webPage = "http://cofradinn.com/cettic/game/read_congif_game.php?";
            WWW conection = new WWW(webPage);
            yield return (conection);
            if (conection.text == "mal")
            {
                Debug.Log("Problemas en la conexion");
                _MouthNotifications(_Notification.Load_ConnectError);
            }
            else
            {
                string[] data = conection.text.Split('|');
                if (data.Length != 3)
                {
                    Debug.Log("Error en la conexion");
                    _MouthNotifications(_Notification.Load_ConnectError);
                }
                else
                {
                    _MySQL_characterSpeed = Int16.Parse(data[0]);
                    _MySQL_levelTime = Int16.Parse(data[1]);
                    _MySQL_itemValue = Int16.Parse(data[2].Remove(data[2].Length - 4));
                    Debug.Log(_MySQL_characterSpeed);
                    Debug.Log(_MySQL_levelTime);
                    Debug.Log(_MySQL_itemValue);
                    //Debug.Log("dato:" + data[2]); // esta recibiendo "N<br>"
                    _MouthNotifications(_Notification.Load_Successful);
                }
            }

            _dataManagerView._Show_WindowLoading(false, "");
            _driverInitMVC.SetActiveView(false);

        }

        public void _Hand_Save_Sattistics_MySQL(int parPoints, string parUser, int parTime, int parItemValue)
        {
            Debug.Log("Entré1");

            _singleton.StartCoroutine(_singleton.Save_Sattistics_MySQL(parPoints, parUser, parTime, parItemValue));
        }

        IEnumerator Save_Sattistics_MySQL(int points, string user, int time, int itemValue)
        {
            Debug.Log("Entré2");


            _dataManagerView._Show_WindowLoading(true, "Guardando Progreso...");

            WWW connection = new WWW("http://cofradinn.com/cettic/game/save_statistics.php?points" + points + "&uss=" + user + "&timeS=" + time + "&item=" + itemValue);
            while (!connection.isDone)
            //while(true)
            {
                //Debug.Log("Estoy Cargando");

                yield return (connection);
                if (connection.text == "partida guardada")
                {
                    Debug.Log("Partida guardada");
                    _MouthNotifications(_Notification.Save_Successful);
                }
                else if (connection.text == "usuario no existe")
                {
                    Debug.Log("Usuario no existe");
                    _MouthNotifications(_Notification.Save_UserError);
                }
            }

            _dataManagerView._Show_WindowLoading(false, "");
            _dataManagerView._Show_WindowSaveReady(true);
        }

        #endregion



        #region Onclick

        public void _Onclick_Continue()
        {
            _MouthNotifications(_Notification.Save_Successful);
        }

        #endregion



        #region Mouth

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification
        {
            Save_Successful,
            Save_UserError,
            Load_Successful,
            Load_ConnectError
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
        //Constructor
        public void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion







        //private static int characterSpeed;
        //private static int timeLevel;
        //private static int itemValue;

        //public static int _CharacterSpeed
        //{
        //    get
        //    {
        //        return PlayerPrefs.GetInt("CharacterSpeed");
        //    }

        //    set
        //    {
        //        PlayerPrefs.SetInt("CharacterSpeed", value);
        //    }
        //}

        //public static int _TimeLevel
        //{
        //    get
        //    {
        //        return PlayerPrefs.GetInt("TimeLevel");
        //    }

        //    set
        //    {
        //        PlayerPrefs.SetInt("TimeLevel", value);
        //    }
        //}

        //public static int _ItemValue
        //{
        //    get
        //    {
        //        return PlayerPrefs.GetInt("ItemValue");
        //    }

        //    set
        //    {
        //        PlayerPrefs.SetInt("ItemValue", value);
        //    }
        //}


    }
}
