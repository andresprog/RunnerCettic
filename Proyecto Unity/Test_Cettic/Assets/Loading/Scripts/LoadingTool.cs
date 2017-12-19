using UnityEngine;
using UnityEngine.SceneManagement;
using _CentralModules;
using _LoadingModules;

namespace _ToolModules
{
    public class LoadingTool : MonoBehaviour
    {
        public enum _SceneList
        {
            Loading,
            SignIn,
            Menu,
            Gameplay
        }


        public static LoadingTool _singleton;



        private void Start()
        {
            _singleton = GetComponent<LoadingTool>();
            
        }

        public void _SetLoad(_SceneList sceneName)
        {
            // Debug.Log("Level: " + level);
            Traveler._Singleton.strSceneName = sceneName.ToString();
            SceneManager.LoadScene(_SceneList.Loading.ToString());
        }
        public void _SetLoad(string sceneName)
        {
            // Debug.Log("Level: " + level);
            Traveler._Singleton.strSceneName = sceneName.ToString();
            SceneManager.LoadScene(_SceneList.Loading.ToString());
        }
    }
}
