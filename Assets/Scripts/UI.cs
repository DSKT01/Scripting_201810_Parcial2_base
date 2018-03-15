using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    [SerializeField]
    Text texto1, texto2;
    [SerializeField]
    ControladorDePartida puntos;
    [SerializeField]
    Player vidas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        texto1.text = "Puntos: " + puntos.Puntos.ToString() + " / Vidas: " + vidas.vida;
        texto2.text = puntos.Puntos.ToString()+ " Puntos Totales";
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
