using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using _ToolModules;
using _CentralModules;

namespace _LoadingModules
{
    public class Load : MonoBehaviour
    {

        // Translations - Traducciones
        // greater - mayor
        // lesser - menor
        // Slave = Esclavo
        // Ear = Oido o Oreja
        // Mouth = Boca
        // Request = Peticion, requerimiento, solicitud


        #region Variables
        [Header("Scripts")]
        public LoadView loadView;
        private AsyncOperation asyncOperation;

        [Header("Progress Bar")]
        [Range(0f, 0.01f)]
        public float velFictitious;
        #endregion



        #region Hand  
        [HideInInspector] public string r_HandLevel;
        #endregion



        #region Initialize
        void Start()
        {
            // initialization with cofradinn driver 
            DriverInitMVC driverInitMVC = gameObject.AddComponent<DriverInitMVC>();
            driverInitMVC.Initialize(Initialize_Module);
            driverInitMVC.SetActiveView(true);
        }

        private void Awake()
        {
            Set_Load_Scene(Traveler._Singleton.strSceneName);            
        }

        private void Initialize_Module()
        {
            velFictitious = 0.65f;
            // First Load
            loadView = transform.GetComponent<LoadView>();
        }
        #endregion



        #region Running

        public void Set_Load_Scene(string levelName)
        {
            StartCoroutine(Load_Level_Slider(levelName));
        }

        IEnumerator Load_Level_Slider(string levelName)
        {
            asyncOperation = SceneManager.LoadSceneAsync(levelName);

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress > 0.95f) // if the real progress is greater than 85f
                {
                    loadView.ProgressBar.value = asyncOperation.progress;
                }
                else // else apply the fictitious progress
                {
                    if (asyncOperation.progress < 0.94f)
                    {
                        try
                        {
                            loadView.ProgressBar.value = loadView.ProgressBar.value + velFictitious;
                        }
                        catch(NullReferenceException)
                        {

                        }
                    }
                }

                yield return null;
                // Debug.Log("Carga Completa");
            }
        }

        #endregion



    }
}
