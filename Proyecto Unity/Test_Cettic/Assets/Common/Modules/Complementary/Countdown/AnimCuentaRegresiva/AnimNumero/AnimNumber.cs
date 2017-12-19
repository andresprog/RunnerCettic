using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _ComplementaryModules
{
    public class AnimNumber : MonoBehaviour
    {
        public void Anim_NumEnfasis()
        {
            GetComponent<Animator>().SetTrigger("Contar");
        }
    }
}