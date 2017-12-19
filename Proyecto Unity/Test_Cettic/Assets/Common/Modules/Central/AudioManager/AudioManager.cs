using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource _aud_Coin;

    private static AudioManager _singleton;
    public static AudioManager _Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<AudioManager>();
            }
            return _singleton;
        }

        set
        {
            //_singleton = value;
        }
    }

    public void _Play_Audio_Coin()
    {
        _aud_Coin.Play();
    }
	

}
