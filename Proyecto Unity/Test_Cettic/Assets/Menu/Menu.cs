using UnityEngine;
using _ToolModules;
using _CentralModules;
using _LoadingModules;

namespace _Menu
{
    public class Menu : MonoBehaviour
    {

        #region Variables
        [Header("Scripts")]
        public static Menu _singleton;
        [Header("Tools")]
        public MsgBox _msgBox;
        public LoadingTool _driverLoading;

        private DriverInitMVC _driverInitMVC;

        [HideInInspector]
        public enum Vowels { A, E, I, O, U }
        #endregion


        #region Initialize
        private void Start()
        {
            _driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            _driverInitMVC.Initialize(Initialize_Module);
            _driverInitMVC.SetActiveView(true);
        }

        void Initialize_Module()
        {
            _singleton = GetComponent<Menu>();
        }
        #endregion


        #region Running

        public void Exit()
        {
            _msgBox.driverInitMVC.SetActiveView(true);
            _msgBox.Hand_GenerateBoxMessage("SALIR?", AnswerMesaggeBox);
        }

        public void AnswerMesaggeBox(string Name, string Mesagge)
        {
            if (Mesagge == "Yes")
            {
                Traveler._Singleton._strUser = "";
                LoadingTool._singleton._SetLoad(LoadingTool._SceneList.SignIn);
            }
            if (Mesagge == "No")
            {
                _msgBox.driverInitMVC.SetActiveView(false);
            }
        }

        public void Play()
        {
            LoadingTool._singleton._SetLoad(LoadingTool._SceneList.Gameplay);
        }
        #endregion

    }

}
