using UnityEngine;

namespace _ToolModules
{
    public class MsgBox : MonoBehaviour
    {


        #region Variables
        [HideInInspector]
        public DriverInitMVC driverInitMVC;
        private LoadingTool driverLoading;
        public enum _Answers { Yes, No }
        public delegate void Delegate(string Name, string Message);
        public static Delegate _delegate;

        [Header("Scripts")]
        public MsgBoxView msgBoxView;
        #endregion



        #region Setup
        // Initial setup (Configuracion inicial)
        #endregion



        #region  Hand
        public void Hand_GenerateBoxMessage(string parQuestion, Delegate parEarNotification)
        {
            msgBoxView.txtTitle.text = parQuestion;
            _delegate = parEarNotification;
        }
        #endregion



        #region Initialize
        private void Start()
        {
            driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            driverInitMVC.Initialize();
        }
        #endregion



        #region Mouth
        public void To_Answer(string Message)
        {
            _delegate(transform.name, Message);
        }
        #endregion


    }

}
