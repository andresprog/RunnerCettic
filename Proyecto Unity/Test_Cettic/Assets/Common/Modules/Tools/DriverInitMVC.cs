using UnityEngine;

namespace _ToolModules
{
    public class DriverInitMVC : MonoBehaviour
    {
        // Estas lineas se deben llamar desde el Start()
        //GP2_DriverInitModVisual driver = gameObject.AddComponent<GP2_DriverInitModVisual>();
        //driver.Inicializar(Inicializar_Modulo);

        #region Driver

        public Transform panel;
        private Delegate _delegate;
        public delegate void Delegate();


        public void Initialize(Delegate parDelegate)
        {
            _delegate = parDelegate;
            panel = transform.GetChild(0);                  // My Child number 0
            panel.gameObject.SetActive(true);               // To Active Panel
            _delegate();                                    // init module
            panel.gameObject.SetActive(false);              // Inactive Panel
        }

        public void Initialize()
        {
            panel = transform.GetChild(0);                  // My Child number 0
            panel.gameObject.SetActive(false);              // Disactive Panel
        }


        public void SetActiveView(bool b)
        {
            panel.gameObject.SetActive(b);
        }

        #endregion
    }
}