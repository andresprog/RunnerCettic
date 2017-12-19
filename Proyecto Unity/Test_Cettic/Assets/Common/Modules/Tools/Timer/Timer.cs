using UnityEngine;

namespace _ToolModules
{
    public class Timer : MonoBehaviour
    {

        #region Ear

        public enum SetPeticion { ToPlay, ToPause, ToStop }
        public void Set(SetPeticion setPeticion)
        {
            switch (setPeticion)
            {
                case SetPeticion.ToPlay: ToPlay(); break;
                case SetPeticion.ToPause: ToPause(); break;
                case SetPeticion.ToStop: ToStop(); break;
                default:; break;
            }
        }

        public enum GetFloat { getTime }
        public float Get(GetFloat getFloat)
        {
            float f;
            switch (getFloat)
            {
                case GetFloat.getTime: f = GetTime(); break;
                default: f = 0f; ; break;
            }
            return f;
        }

        public enum GetString { Estatus }
        public string Get(GetString getString)
        {
            string str;
            switch (getString)
            {
                case GetString.Estatus: str = GetStatus(); break;
                default: str = ""; break;
            }
            return str;
        }
        #endregion


        #region Initialize

        public void Start()
        {
            Set(SetPeticion.ToStop);
        }

        #endregion


        #region Functions

        float Ta;// tiempo acumulado
        float Tx; //tiempo
        float Tt; //Tiempo total
        float delta;
        string estado;


        public void ToPlay()
        {
            if (estado == "Stop")
            {
                estado = "Play";
                delta = Time.time;
            }

            if (estado == "Pause")
            {
                estado = "Play";
                delta = Time.time;
            }
        }

        public void ToPause()
        {
            if (estado == "Play")
            {
                estado = "Pause";
                Tx = Time.time - delta;
                Ta = Ta + Tx;
            }
        }

        public void ToStop()
        {
            delta = 0;
            Tx = 0;
            Ta = 0;
            Tt = 0;

            delta = Time.time;
            estado = "Stop";
        }


        public float GetTime()
        {
            if (estado == "Stop") { Tt = 0f; }
            if (estado == "Play") { Tt = Ta + (Time.time - delta); }
            if (estado == "Pause") { Tt = Ta; }
            return Tt;
        }

        public string GetStatus()
        {
            return estado;
        }

        #endregion


        #region Events

        #endregion

    }

}

