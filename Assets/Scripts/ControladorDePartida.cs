using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControladorDePartida: MonoBehaviour  {


    float puntos = 0;

    static ControladorDePartida _intance;

    public float Puntos
    {
        get
        {
            return puntos;
        }

        set
        {
            puntos = value;
        }
    }

    private void Awake()
    {
        if (_intance == null)
        {
            _intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
