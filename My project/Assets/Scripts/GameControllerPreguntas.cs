using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using models;
using modell;
using System;
using System.IO;
using TMPro;
using System.Linq;

public class GameControllerPreguntas : MonoBehaviour
{
    List<PreguntasMultiples> listaPM;
    List<PreguntasVerdaderoFalso> listaPVF;
    List<PreguntasAbiertas> listaPA;

    List<PreguntasMultiples> listaPMFacil;
    List<PreguntasMultiples> listaPMDificil;

    List<PreguntasVerdaderoFalso> listaPVFFacil;
    List<PreguntasVerdaderoFalso> listaPVFDificil;

    List<PreguntasAbiertas> listaPAFacil;
    List<PreguntasAbiertas> listaPADificil;

    string preguntaLeida;

    public AudioClip sonidoCorrecto, sonidoIncorrecto;
    public AudioSource audioManager;

    public TextMeshProUGUI txtPregunta;
    public TextMeshProUGUI txtRespuesta1;
    public TextMeshProUGUI txtRespuesta2;
    public TextMeshProUGUI txtRespuesta3;
    public TextMeshProUGUI txtRespuesta4;
    

    public TextMeshProUGUI txtPreguntaVF;
    public TextMeshProUGUI txtPreguntaPA;
    public TextMeshProUGUI txtRespuesta1VF;
    public TextMeshProUGUI txtRespuesta2VF;

    public TextMeshProUGUI txtContadorMalas;
    public TextMeshProUGUI txtContadorBuenas;
    public TextMeshProUGUI txtContadorBuenasF;
    public TextMeshProUGUI txtContadorMalasF;



    public TextMeshProUGUI txtRepsuestaPA;


    public GameObject panelPreguntasPM;
    public GameObject panelPreguntasVF;
    public GameObject panelPreguntasPA;
    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;
    public GameObject panelrespuestaPA;
    public GameObject panelAcompañan;
    public GameObject panelinicial;
    public GameObject panelfacilPreguntas;
    public GameObject paneldificilPreguntas;
    public GameObject paneltimer;
    public GameObject panelFinal; 

    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    public GameObject botonDesistir;
    public GameObject botonEsperanza;

    public Timer tempor;

    string respuesta;

    System.Random rnd;

    int contadorMalas = 0;
    int contadorBuenas = 0;
    int contadorVidas = 0;

    int contadorDificles = 1;

    

    string tipoPregunta;




    // Start is called before the first frame update
    void Start()
    {
        listaPM = new List<PreguntasMultiples>();
        listaPVF = new List<PreguntasVerdaderoFalso>();
        listaPA = new List<PreguntasAbiertas>();

        listaPMFacil = new List<PreguntasMultiples>();
        listaPMDificil = new List<PreguntasMultiples>();

        listaPVFFacil = new List<PreguntasVerdaderoFalso>();
        listaPVFDificil = new List<PreguntasVerdaderoFalso>();

        listaPAFacil = new List<PreguntasAbiertas>();
        listaPADificil = new List<PreguntasAbiertas>();

        rnd = new System.Random();
        panelAcompañan.SetActive(true);



        LeerPreguntasMultiples();
        leerPreguntasVerdaderoFalse();
        leerPreguntasAbiertas();

        panelAcompañan.SetActive(false);
        panelinicial.SetActive(true);
        panelFinal.SetActive(false);
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelrespuestaPA.SetActive(false);
        panelPreguntasPA.SetActive(false);
        panelPreguntasPM.SetActive(false);
        panelPreguntasVF.SetActive(false);
        panelfacilPreguntas.SetActive(false);
        paneldificilPreguntas.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void iniciar()
    {
        panelinicial.SetActive(false);
        paneltimer.SetActive(true);
        mostrarPreguntaBoton();
    }

    public void ReiniciarJuego()
    {

        ReiniciarContadores();

        listaPMFacil.Clear();
        listaPMDificil.Clear();
        listaPVFFacil.Clear();
        listaPVFDificil.Clear();
        listaPAFacil.Clear();
        listaPADificil.Clear();

        LeerPreguntasMultiples();
        leerPreguntasVerdaderoFalse();
        leerPreguntasAbiertas();

        panelFinal.SetActive(false);
        panelinicial.SetActive(true);
        panelAcompañan.SetActive(false);
        panelPreguntasPM.SetActive(false);
        panelPreguntasVF.SetActive(false);
        panelPreguntasPA.SetActive(false);

        contadorVidas = 0;
        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);


    }


    public void mostrarPreguntaBoton()
    {
        panelFinal.SetActive(false);
        panelAcompañan.SetActive(true);
        contadorVidas = 0;

        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);



        List<string> tiposPreguntas = new List<string>();

        if (listaPMFacil.Count > 0) tiposPreguntas.Add("PMFacil");
        if (listaPVFFacil.Count > 0) tiposPreguntas.Add("PVFFacil");
        if (listaPAFacil.Count > 0) tiposPreguntas.Add("PAFacil");

 
        if (tiposPreguntas.Count == 0)
        {
            if (contadorDificles == 1)
            {
                paneldificilPreguntas.SetActive(true);
                contadorDificles--;

            } 
            
            if (listaPMDificil.Count > 0) tiposPreguntas.Add("PMDificil");
            if (listaPVFDificil.Count > 0) tiposPreguntas.Add("PVFDificil");
            if (listaPADificil.Count > 0) tiposPreguntas.Add("PADificil");
        }

  
        if (tiposPreguntas.Count > 0)
        {
        
            string tipoSeleccionado = tiposPreguntas[rnd.Next(0, tiposPreguntas.Count)];

            switch (tipoSeleccionado)
            {
                case "PMFacil":
                    int numPM = rnd.Next(0, listaPMFacil.Count);
                    mostrarPreguntaDesdeLista(listaPMFacil[numPM]);
                    listaPMFacil.RemoveAt(numPM);
                    panelPreguntasPM.SetActive(true);
                    panelPreguntasVF.SetActive(false);
                    panelPreguntasPA.SetActive(false);
                    tipoPregunta = "Multiple";
                    break;

                case "PVFFacil":
                    int numVF = rnd.Next(0, listaPVFFacil.Count);
                    txtRespuesta1VF.text = "true";
                    txtRespuesta2VF.text = "false";
                    txtPreguntaVF.text = listaPVFFacil[numVF].Pregunta;
                    respuesta = listaPVFFacil[numVF].RespuestaCorrecta;
                    listaPVFFacil.RemoveAt(numVF);
                    panelPreguntasPM.SetActive(false);
                    panelPreguntasVF.SetActive(true);
                    panelPreguntasPA.SetActive(false);
                    tipoPregunta = "VF";
                    break;

                case "PAFacil":
                    int numPA = rnd.Next(0, listaPAFacil.Count);
                    txtPreguntaPA.text = listaPAFacil[numPA].Pregunta;
                    respuesta = listaPAFacil[numPA].RespuestaCorrecta;
                    listaPAFacil.RemoveAt(numPA);
                    panelPreguntasPM.SetActive(false);
                    panelPreguntasVF.SetActive(false);
                    panelPreguntasPA.SetActive(true);
                    tipoPregunta = "Abierta";
                    break;

                case "PMDificil":
                    int numPMD = rnd.Next(0, listaPMDificil.Count);
                    mostrarPreguntaDesdeLista(listaPMDificil[numPMD]);
                    listaPMDificil.RemoveAt(numPMD);
                    panelPreguntasPM.SetActive(true);
                    panelPreguntasVF.SetActive(false);
                    panelPreguntasPA.SetActive(false);
                    tipoPregunta = "Multiple";
                    break;

                case "PVFDificil":
                    int numVFD = rnd.Next(0, listaPVFDificil.Count);
                    txtRespuesta1VF.text = "true";
                    txtRespuesta2VF.text = "false";
                    txtPreguntaVF.text = listaPVFDificil[numVFD].Pregunta;
                    respuesta = listaPVFDificil[numVFD].RespuestaCorrecta;
                    listaPVFDificil.RemoveAt(numVFD);
                    panelPreguntasPM.SetActive(false);
                    panelPreguntasVF.SetActive(true);
                    panelPreguntasPA.SetActive(false);
                    tipoPregunta = "VF";
                    break;

                case "PADificil":
                    int numPAD = rnd.Next(0, listaPADificil.Count);
                    txtPreguntaPA.text = listaPADificil[numPAD].Pregunta;
                    respuesta = listaPADificil[numPAD].RespuestaCorrecta;
                    listaPADificil.RemoveAt(numPAD);
                    panelPreguntasPM.SetActive(false);
                    panelPreguntasVF.SetActive(false);
                    panelPreguntasPA.SetActive(true);
                    tipoPregunta = "Abierta";
                    break;
            }
        }
        else
        {
            Debug.Log("No hay más preguntas disponibles.");
           
            

        }


    }

    public void mostrarPanelFinal()
    {

        txtContadorBuenasF.text = contadorBuenas.ToString();
        txtContadorMalasF.text = contadorMalas.ToString();
        panelFinal.SetActive(true);
        panelAcompañan.SetActive(false);
       


    }

    public void ReiniciarContadores()
    {
        contadorBuenas = 0;
        contadorMalas = 0;
        txtContadorBuenas.text = contadorBuenas.ToString();
        txtContadorMalas.text = contadorMalas.ToString();
        txtContadorBuenasF.text = contadorBuenas.ToString();
        txtContadorMalasF.text = contadorMalas.ToString();
    }



    #region mostrarPreguntas


    private void mostrarPreguntaDesdeLista(PreguntasMultiples pregunta)
    {
        txtPregunta.text = pregunta.Pregunta;
        txtRespuesta1.text = pregunta.Respuesta1;
        txtRespuesta2.text = pregunta.Respuesta2;
        txtRespuesta3.text = pregunta.Respuesta3;
        txtRespuesta4.text = pregunta.Respuesta4;
        respuesta = pregunta.RespuestaCorrecta;
    }




    public void validarRespuesta(TextMeshProUGUI answer)
    {
        if (tipoPregunta.Equals("Abierta"))
        {
            audioManager.PlayOneShot(sonidoCorrecto);
            panelCorrecto.SetActive(true);

        }
        else
        {
            if (string.Equals(answer.text, respuesta, StringComparison.OrdinalIgnoreCase))
            {
                panelCorrecto.SetActive(true);
                contadorBuenas++;
                Debug.Log("Respuesta Correcta");
                txtContadorBuenas.text = contadorBuenas.ToString();
                txtContadorBuenasF.text = txtContadorBuenas.text;
                audioManager.PlayOneShot(sonidoCorrecto);
            }
            else
            {
                botonDesistir.SetActive(false);
                botonEsperanza.SetActive(true);
                panelIncorrecto.SetActive(true);
                contadorMalas++;
                Debug.Log("Respuesta Incorrecta");
                txtContadorMalas.text = contadorMalas.ToString();
                txtContadorMalasF.text = txtContadorMalas.text;
                contadorVidas++;
                if (contadorVidas == 1)
                {
                    corazon1.SetActive(false);
                }
                else if (contadorVidas == 2)
                {
                    corazon2.SetActive(false);
                    
                }
                else if (contadorVidas == 3)
                {
                    botonDesistir.SetActive(true);
                }

                audioManager.PlayOneShot(sonidoIncorrecto);

            }
        }

    }

    #region respuestas Multiples

    #endregion

  
    public void LeerPreguntasMultiples()
    {
        try
        {
            StreamReader sr1 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasM.txt");
            while ((preguntaLeida = sr1.ReadLine()) != null)
            {
                string[] lineaPartida = preguntaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta = lineaPartida[5];
                string versiculo = lineaPartida[6];
                string dificultad = lineaPartida[7];

                PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2, respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);

                if (dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
                {
                    listaPMFacil.Add(objPM);
                }
                else if (dificultad.Equals("Dificil",StringComparison.OrdinalIgnoreCase))
                {
                    listaPMDificil.Add(objPM);
                }



                listaPM.Add(objPM);
            }
            Debug.Log("Tamaño de lista " + listaPM.Count);
            Debug.Log("Tamaño de lista Facil " + listaPMFacil.Count);
            Debug.Log("Tamaño de lista Dificil " + listaPMDificil.Count);



        }
        catch (Exception e)
        {
            Debug.Log("Error " + e.ToString());

        }

    }

    public void leerPreguntasVerdaderoFalse()
    {
        try
        {
            StreamReader sr2 = new StreamReader("Assets/Resources/Files/preguntasFalso_Verdadero.txt");
            while ((preguntaLeida = sr2.ReadLine()) != null)
            {
                string[] lineaPartida = preguntaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuestaCorrecta = lineaPartida[1];
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3];

                PreguntasVerdaderoFalso objPVF = new PreguntasVerdaderoFalso(pregunta, respuestaCorrecta, versiculo, dificultad);

                if (dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
                {
                    listaPVFFacil.Add(objPVF);
                }
                else if (dificultad.Equals("Dificil", StringComparison.OrdinalIgnoreCase))
                {
                    listaPVFDificil.Add(objPVF);
                }

                listaPVF.Add(objPVF);
            }
            Debug.Log("Tamaño de lista VF " + listaPVF.Count);
            Debug.Log("Tamaño de lista Facil VF " + listaPVFFacil.Count);
            Debug.Log("Tamaño de lista Dificil VF" + listaPVFDificil.Count);
        }
        catch (Exception e)
        {
            Debug.Log("Error " + e.ToString());
        }
    }

    public void leerPreguntasAbiertas()
    {

        try
        {
            StreamReader sr3 = new StreamReader("Assets/Resources/Files/ArchivoPreguntasAbiertas.txt");
            while ((preguntaLeida = sr3.ReadLine()) != null)
            {
                string[] lineaPartida = preguntaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuestaCorrecta = lineaPartida[1];  
                string versiculo = lineaPartida[2];
                string dificultad = lineaPartida[3];
                PreguntasAbiertas objPA = new PreguntasAbiertas(pregunta, respuestaCorrecta, versiculo, dificultad);

                if (dificultad.Equals("Facil", StringComparison.OrdinalIgnoreCase))
                {
                    listaPAFacil.Add(objPA);
                }
                else if (dificultad.Equals("Dificil", StringComparison.OrdinalIgnoreCase))
                {
                    listaPADificil.Add(objPA);
                }
                listaPA.Add(objPA);
            }
            Debug.Log("Tamaño de lista " + listaPA.Count);
            Debug.Log("Tamaño de lista Facil  PA" + listaPAFacil.Count);
            Debug.Log("Tamaño de lista Dificil PA" + listaPADificil.Count);
        }
        catch (Exception e)
        {
            Debug.Log("Error " + e.ToString());
        }
    }

    #endregion
}
