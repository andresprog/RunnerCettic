using UnityEngine;
using _ToolModules;
using _ComplementaryModules;
using _CentralModules;
using UnityStandardAssets.Characters.ThirdPerson;

namespace _GameplayModules
{
    public class SceneSequencer : MonoBehaviour
    {

        #region Variables
        // Driver init
        static bool _levelWon;

        [Header("Audio")]
        public AudioSource _aud_GameplaySong;

        // singleton
        public static SceneSequencer _singleton;
        public static SceneSequencer _Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = FindObjectOfType<SceneSequencer>();
                }
                return _singleton;
            }

            set
            {
                //_singleton = value;
            }
        }

        // Referenciando los modulos para controlar su secuencia
        [Header("Gameplay Modules")]
        public PointsBar _pointsBar; // Modulo de barra de puntos y tiempo
        public HudLives _timeBar;    // Modulo de barra de vidas

        [Header("Complementary Modules")]
        public Results _results;        // modulo de resultados
        public Pause _pause;            // modulo de pausa
        public Objetives _objetives;    // modulo de pausa
        public Countdown _countdown;    // modulo de cuenta regresiva
        public Curtain _curtain;        // modulo de telon (imagen oscura de la estrella)

        [Header("Tool Modules")]
        public MsgBox _msgBox;          // modulo de mensaje emergente
        public PanelBlock _panelBlock;  // modulo para inhabilitar el acceso a toda la pantalla

        static _State _state;           // Estado actual de la partida
        string GoTo;

        // Estados de la secuencia en la escena del gameplay
        public enum _State
        {
            G1_PREPARING_BOARD,
            G2_RAISING_CURTAIN,
            G3_SHOWING_OBJETIVES,
            G4_SHOWING_COUNTDOWN,
            G5_RUNNING_GAMEPLAY,
            G6_EXTRACTING_STATISTICS,
            G7_SHOWING_RESULT,
            G8_LOWERING_CURTAIN,
            G7_VALIDATING_NEXT_LEVEL,
            G7_SHOWING_RESULTS
        }
        #endregion



        // configuracion inicial del modulo
        #region Setup
        private void _Setup()
        {
            _Subscribe_Notifications();
            _curtain._Setup(Curtain._Congig.Anim_2_Twist);
        }
        #endregion




        // Suscribiendose para recibir notificaciones de eventos en todos estos modulos
        #region Subscriptions
        void _Subscribe_Notifications()
        {
            Curtain._singleton._NotifyMe(_EarNotifications);
            Objetives._singleton._NotifyMe(_EarNotifications);
            Countdown._singleton._NotifyMe(_EarNotifications);
            PanelBlock._singleton._NotifyMe(_EarNotifications);
            HudLives._Singleton._NotifyMe(_EarNotifications);
            Results._singleton._NotifyMe(_EarNotifications);
            PointsBar._singleton._NotifyMe(_EarNotifications);
            Pause._singleton._NotifyMe(_EarNotifications);
            DataManager._Singleton._NotifyMe(_EarNotifications);
        }
        #endregion





        #region Initialize
        private void Start()
        {
            _Initialize_Module();
        }


        void _Initialize_Module()
        {
            _levelWon = false;
            _Setup();
            _G1_Prepare_Board();
        }
        #endregion



        #region Ear
        // Oido de notificaciones para recibir eventos de terceros 
        public void _EarNotifications(string Name, string Notification)
        {
            // Notification: Exit Game
            if (Notification == Pause.Notification.Exit_Game.ToString())
            {
                _Show_Message_Exit_Game(); // mostrar mensaje de salir del juego
            }
            else
            {
                // enviando notificaciones especificas segun el estado actual
                switch (_state)
                {
                    case _State.G1_PREPARING_BOARD: EarG1(Notification); break;
                    case _State.G2_RAISING_CURTAIN: EarG2(Notification); break;
                    case _State.G3_SHOWING_OBJETIVES: EarG3(Notification); break;
                    case _State.G4_SHOWING_COUNTDOWN: EarG4(Notification); break;
                    case _State.G5_RUNNING_GAMEPLAY: EarG5(Notification); break;
                    case _State.G6_EXTRACTING_STATISTICS: EarG6(Notification); break;
                    case _State.G7_SHOWING_RESULTS: EarG7(Notification); break;
                    case _State.G8_LOWERING_CURTAIN: EarG8(Notification); break;
                    default:; break;

                }
            }

        }

        #endregion




        #region Sequence
        // estado 1
        void _G1_Prepare_Board()
        {
            _state = _State.G1_PREPARING_BOARD;
            Curtain._singleton._EarRequest(Curtain._Request.Activate);
            PanelBlock._singleton._EarRequest(PanelBlock._Request.Activate);
            DataManager._Singleton._Hand_Read_Game_Config_MySQL();

        }
        void EarG1(string notification)
        {
            if (notification == DataManager._Notification.Load_Successful.ToString())
            {
                Debug.Log("Aqui1");
                GameplayManagerNew._Singleton._Setup();
                _G2_Raise_Curtain();
            }
            if (notification == DataManager._Notification.Load_ConnectError.ToString())
            {
                Debug.Log("Aqui2");
                GameplayManagerNew._Singleton._Setup();
                _G2_Raise_Curtain();
            }
        }


        void _G2_Raise_Curtain()
        {
            Debug.Log("Aqui3");

            _state = _State.G2_RAISING_CURTAIN;
            Curtain._singleton._EarRequest(Curtain._Request.Raise_Curtain);
            Objetives._singleton._EarRequest(Objetives._Request.Activate);
            Objetives._singleton._Hand_ShowObjetives();
            //Curtain._singleton._EarRequest(Curtain._Request.Raise_Curtain_2s);
        }
        void EarG2(string notification)
        {
            if (notification == Curtain._Notification.Raised_Curtain.ToString())
            {
                _G3_Show_Objetives();
            }
        }


        void _G3_Show_Objetives()
        {
            Debug.Log("Aqui3");

            _state = _State.G3_SHOWING_OBJETIVES;
            PanelBlock._singleton._EarRequest(PanelBlock._Request.Deactivate);
            Curtain._singleton._EarRequest(Curtain._Request.Deactivate);
        }
        void EarG3(string notification)
        {
            if (notification == Objetives._Notification.Next.ToString())
            {
                _G4_Show_Countdown();
            }
        }



        void _G4_Show_Countdown()
        {
            _state = _State.G4_SHOWING_COUNTDOWN;
            //PanelBlock._singleton.Ear(PanelBlock._Request.Activate);
            PointsBar._singleton._EarRequest(PointsBar._Request.Show);
            HudLives._Singleton._EarRequest(HudLives._Request.Show_Timer);
            Countdown._singleton._EarRequest(Countdown._Request.Activate);
            Countdown._singleton._EarRequest(Countdown._Request.Start_Countdown_2s);
        }
        void EarG4(string notification)
        {
            if (notification == Countdown._Notification.Finalized_Countdown.ToString())
            {
                ThirdPersonUserControl._Singleton._EarRequest(ThirdPersonUserControl._Request.RunOn);
                PointsBar._singleton._EarRequest(PointsBar._Request.Start_Time);
                _G5_Run_Gameplay();
            }
        }



        void _G5_Run_Gameplay()
        {
            _state = _State.G5_RUNNING_GAMEPLAY;
            _aud_GameplaySong.Play();
            Countdown._singleton._EarRequest(Countdown._Request.Deactivate);
            PanelBlock._singleton._EarRequest(PanelBlock._Request.Deactivate);
            //GameplayManager._singleton._EarRequest(GameplayManager._Request.Start_Stage);
        }
        void EarG5(string notification)
        {
            if (notification == HudLives._Notification.Lives_Finished.ToString())
            {
                _levelWon = false;
                _R1_Show_Results();
            }
            if (notification == PointsBar._Notification.Time_is_up.ToString())
            {
                _levelWon = true;
                ThirdPersonUserControl._Singleton._EarRequest(ThirdPersonUserControl._Request.RunOff);
                _R1_Show_Results();
            }
        }





        void _R1_Show_Results()
        {
            _state = _State.G7_SHOWING_RESULTS;
            Results._singleton._EarRequest(Results._Request.Activate);
            Results._singleton._Hand_points(GameplayManagerNew._points);
        }
        void EarG7(string notification)
        {
            if (notification == Results._Notification.Go_To_The_Menu.ToString())
            {
                GoTo = "Menu";
                Results._singleton._EarRequest(Results._Request.Deactivate);
                _G6_Extract_Statistics();
            }
            if (notification == Results._Notification.Restart.ToString())
            {
                GoTo = "Restart";
                Results._singleton._EarRequest(Results._Request.Deactivate);
                _G6_Extract_Statistics();
            }
        }




        void _G6_Extract_Statistics()
        {
            _state = _State.G6_EXTRACTING_STATISTICS;
            DataManager._Singleton._EarRequest(DataManager._Request.Activate);
            DataManager._Singleton._Hand_Save_Sattistics_MySQL
                (
                GameplayManagerNew._points,
                Traveler._Singleton._strUser,
                DataManager._Singleton._MySQL_levelTime,
                DataManager._Singleton._MySQL_itemValue
                );
        }
        void EarG6(string notification)
        {
            if (notification == DataManager._Notification.Save_Successful.ToString())
            {
                _G8_Lower_Curtain_Menu();
            }
            if (notification == DataManager._Notification.Save_UserError.ToString())
            {
                _G8_Lower_Curtain_Menu();
            }
        }





        void _G8_Lower_Curtain_Menu()
        {
            _state = _State.G8_LOWERING_CURTAIN;
            Curtain._singleton._EarRequest(Curtain._Request.Activate);
            Curtain._singleton._EarRequest(Curtain._Request.Lower_Curtain);
        }
        void EarG8(string notification)
        {
            if (notification == Curtain._Notification.Lowered_Curtain.ToString())
            {
                if (GoTo == "Menu")
                {
                    LoadingTool._singleton._SetLoad(LoadingTool._SceneList.Menu);
                }
                if (GoTo == "Restart")
                {
                    LoadingTool._singleton._SetLoad(LoadingTool._SceneList.Gameplay);
                }
            }
        }


        #endregion



        #region Pause
        void _Show_Message_Exit_Game()
        {
            _msgBox.driverInitMVC.SetActiveView(true);
        }
        #endregion


    }

}
