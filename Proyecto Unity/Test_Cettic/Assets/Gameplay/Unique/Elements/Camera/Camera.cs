using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GameplayModules
{
    public class Camera : MonoBehaviour
    {
        public Transform _target;
        Vector3 _startPosition;

        void Start()
        {
            _startPosition = transform.position;
        }

        void Update()
        {
            transform.position = _target.transform.position + _startPosition;
        }
    }
}
