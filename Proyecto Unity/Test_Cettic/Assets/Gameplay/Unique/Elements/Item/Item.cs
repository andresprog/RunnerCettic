using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GameplayModules
{
    public class Item : MonoBehaviour
    {


        #region Variables

        public static GameObject _collectorOfItems;
        #endregion



        #region Initialize

        private void Start()
        {
        }

        #endregion



        #region Setup

        public static void _Setup(GameObject collectorOfItems)
        {
            _collectorOfItems = collectorOfItems;
        }

        #endregion



        #region Collisions

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == _collectorOfItems.name)
            {
                _MouthNotifications(_Notification.Accumulated_Item);
                AudioManager._Singleton._Play_Audio_Coin();
                Destroy(transform.gameObject);
            }
        }

        #endregion



        #region Mouth

        public delegate void Delegate(string Name, string Message);
        static Delegate _delegate;

        public enum _Notification
        {
            Accumulated_Item
        }

        public void _MouthNotifications(_Notification notification)
        {
            try
            {
                _delegate(transform.name, notification.ToString());
            }
            catch (NullReferenceException)
            {
                //Debug.Log(transform.name + ": Delegado NullReferenceException, Nadie se suscribio");
            }
        }
        //Constructor
        public static void _NotifyMe(Delegate parEar)
        {
            _delegate = parEar;
        }
        #endregion



        #region Running

        private void FixedUpdate()
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y + 2f, transform.rotation.z,0);
        }

        #endregion

    }
}
