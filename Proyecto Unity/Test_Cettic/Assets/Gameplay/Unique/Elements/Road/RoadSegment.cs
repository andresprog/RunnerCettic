using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{


    #region Collisions

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ThirdPersonController")
        {
            _MouthNotifications(_Notification.Finished_Section);
        }
    }

    #endregion


    #region Mouth

    public delegate void Delegate(string Name, string Message);
    static Delegate _delegate;

    public enum _Notification
    {
        Finished_Section
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


}
