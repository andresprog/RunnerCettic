using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _ComplementaryModules
{
    public class CountdownView : MonoBehaviour
    {

        public GameObject objNum1;
        public GameObject objNum2;
        public GameObject objNum3;





        private void Start()
        {
            // Metodo a llamar para iniciar la animacion
            // Anim_Iniciar_CuentaRegresiva();
        }


        public void Anim_Init_Countdown()
        {
            GetComponent<Animator>().SetTrigger("Iniciar");
        }




        public void Anim_Num1_Enfasis()
        {
            objNum1.GetComponent<AnimNumber>().Anim_NumEnfasis();
        }

        public void Anim_Num2_Enfasis()
        {
            objNum2.GetComponent<AnimNumber>().Anim_NumEnfasis();

        }

        public void Anim_Num3_Enfasis()
        {
            objNum3.GetComponent<AnimNumber>().Anim_NumEnfasis();
        }



    }

}