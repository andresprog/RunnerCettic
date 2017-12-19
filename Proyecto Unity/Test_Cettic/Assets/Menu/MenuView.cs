using UnityEngine;

namespace _Menu
{
    public class MenuView : MonoBehaviour
    {

        public void OnClick_PLAY_GAME()
        {
            Menu._singleton.Play();
        }

        public void OnClick_EXIT_GAME()
        {
            Menu._singleton.Exit();
        }

    }
}