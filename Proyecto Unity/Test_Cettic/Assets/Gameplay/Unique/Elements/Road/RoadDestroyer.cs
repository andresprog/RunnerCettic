using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GameplayModules
{
    public class RoadDestroyer : MonoBehaviour
    {

        #region Collisions

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }

        #endregion

    }
}
