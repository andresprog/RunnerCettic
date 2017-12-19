using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _GameplayModules
{
    public class Road : MonoBehaviour
    {


        #region Variables
        [Header("Prefabs")]
        public GameObject _prefabSection;
        public GameObject _prefabItem;
        public GameObject _prefabObstacle_1;
        public GameObject _prefabObstacle_2;
        public GameObject _prefabObstacle_3;
        public GameObject _prefabObstacle_4;
        public GameObject _prefabObstacle_5;

        [Header("Elements")]
        public GameObject _refSection;
        public GameObject _items;
        public GameObject _Obstacles;
        [Header("Points of reference")]
        [Tooltip("reference points for the generation of elements")]
        public GameObject _point0;
        public GameObject _point1;
        public GameObject _point2;
        public GameObject _point3;
        public GameObject _point4;
        public GameObject _point5;

        [Header("Config")]
        public int _initialSections;

        Quaternion _startRotation;
        Vector3 _startPosition;
        GameObject _section;
        float _newZ;
        #endregion



        #region Initialize

        void Start()
        {
            _newZ = 0;
            _startRotation = _refSection.transform.rotation;
            _startPosition = _refSection.transform.position;

            _Instantiating_Initial_Sections();

            // subscribe to receive notifications from the section
            RoadSegment._NotifyMe(_EarNotifications);
        }

        public void _Instantiating_Initial_Sections()
        {
            for (int i = 0; i < _initialSections; i++)
            {
                _Instantiate_Section();
            }
        }
        #endregion



        #region Ear

        public void _EarNotifications(string name, string notification)
        {
            if (notification == RoadSegment._Notification.Finished_Section.ToString())
            {
                _Instantiate_Section();
            }
        }

        #endregion



        #region Sequence

        public void _Instantiate_Section()
        {
            // Adicionando distancia
            _newZ = _newZ + 19f;
            Vector3 _newPosition = new Vector3(_startPosition.x, _startPosition.y, _newZ);
            _section = Instantiate(_prefabSection, _newPosition, _startRotation);
            // Hacer nueva instancia hijo de la pista
            _section.transform.parent = transform;

            _Instantiate_Items();
        }

        Vector3 _v;
        private void _Instantiate_Items()
        {
            // Calculando el punto de generacion de forma aleatoria
            int refPoint = Random.Range(0, 6);
            // calculando la cantidad de items consecutivos
            int numberOfItems = Random.Range(0, 6);
            // obteniendo el obj equivalente al punto de referencia
            GameObject objRefPoint = _Select_Ref_Point(ref refPoint);

            // instanciando
            for (int i = 0; i < numberOfItems; i++)
            {
                _v = new Vector3( 0, 0, i);
                GameObject instance = Instantiate(_prefabItem, objRefPoint.transform.position + _v, objRefPoint.transform.rotation);
                instance.transform.parent = _items.transform;
            }

            // random para elegir la cantidad de osbtaculos
            int numberOfObstacles = Random.Range(0, 3);

            for (int i = 0; i < numberOfObstacles; i++)
            {
                Instatiate_Obstacle();
            }
            

        }


        void Instatiate_Obstacle()
        {
            // Instanciando obstaculo
            // calculando una posicion de 1 a 6 unidades detrás de las monedas para instanciar obstaculo 
            _v = new Vector3(0, 0, 5 + Random.Range(1, 6));
            int num = Random.Range(0, 6);
            // obteniendo el obj equivalente al punto de referencia
            GameObject obj_Obstacle = _Select_Obstacle(ref num);
            // Calculando el punto de generacion de forma aleatoria
            int refPoint = Random.Range(0,3);
            // obteniendo el obj equivalente al punto de referencia
            GameObject objRefPoint = _Select_Ref_Point(ref refPoint);
            // Instanciar obstaculo
            GameObject obj = Instantiate(obj_Obstacle, objRefPoint.transform.position + _v, objRefPoint.transform.rotation);
            obj.transform.parent = _Obstacles.transform;
        }

        private GameObject _Select_Ref_Point(ref int n)
        {
            GameObject point;

            switch (n)
            {
                case 0: point = _point0; break;
                case 1: point = _point1; break;
                case 2: point = _point2; break;
                case 3: point = _point3; break;
                case 4: point = _point4; break;
                case 5: point = _point5; break;

                default: point = _point0; break;
            }

            return point; 
        }

        private GameObject _Select_Obstacle(ref int n)
        {
            GameObject obstacle;

            switch (n)
            {
                case 0: obstacle = _prefabObstacle_1; break;
                case 1: obstacle = _prefabObstacle_2; break;
                case 2: obstacle = _prefabObstacle_3; break;
                case 3: obstacle = _prefabObstacle_4; break;
                case 4: obstacle = _prefabObstacle_5; break;

                default: obstacle = _prefabObstacle_1; break;
            }

            return obstacle;
        }
        #endregion



    }
}
