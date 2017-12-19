using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _ComplementaryModules
{
    public class DataManagerView : MonoBehaviour
    {

        private Animator _myAnimator;       // mi animator controller
        public GameObject _panelLoading;    // panel cargando (para la espera)
        public Text _text_Loading;          // texto de la ventana de carga
        public GameObject _panelReady;      // panel con mensaje de terminado

        // inicializacion de la vista
        public void Initialize_View()
        {
            _myAnimator = GetComponent<Animator>();
            _panelLoading.SetActive(false);
            _panelReady.SetActive(false);
        }

        // mostrar panel de cargando con su mensaje
        public void _Show_WindowLoading(bool setActive, string textLoad)
        {
            if (setActive)
            {
                _panelLoading.SetActive(true);          // activando la vista del panel cargando
                _text_Loading.text = textLoad;          // asignando texto con mensaje
                _myAnimator.SetBool("Loading", true);   // activando animacion de imagen giratoria
            }
            else
            {
                _myAnimator.SetBool("Loading", false);  // desactivando animacion de imagen giratoria
                _panelLoading.SetActive(false);         // desactivando la vista del panel cargando
            }
        }

        // mostrar panel de proceso terminado
        public void _Show_WindowSaveReady(bool b)
        {
            _panelReady.SetActive(b);
        }


        // Continuar con el siguiente estado del juego
        public void _Onclick_Continue()
        {
            DataManager._Singleton._Onclick_Continue();
        }



    }
}
