using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _LoadingModules
{
    public class LoadView : MonoBehaviour
    {

        #region  Propierties
        [Header("Elements")]
        public Slider progressBar;
        public Slider ProgressBar
        {
            get
            {
                return progressBar;
            }

            set
            {
                progressBar = value;
            }
        }

        #endregion

    }
}
