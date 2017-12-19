using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GameplayModules
{
    public class Transporter : MonoBehaviour
    {

        public static Transform _character; // personaje Etham

        #region Setup

        // Configuracion inicial
        public static void _Setup(GameObject character)
        {
            _character = character.transform; // obteniendo personaje desde modulo centrales
        }
        #endregion



        #region Running

        void FixedUpdate()
        {
            // siguiendo al personaje solo en su cordenada "Z" (es decir moviendose solo hacia adelante)
            transform.position = new Vector3(transform.position.x, transform.position.y, _character.transform.position.z);
        }
        #endregion



    }
}
