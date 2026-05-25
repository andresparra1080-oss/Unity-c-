using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador_control : MonoBehaviour
{
    public GameObject poder;

    Animator anim;

    public int vida = 100;

    public Text textoContador;

    private bool muerto = false;

    private bool atacando = false;
    private AudioSource laser;
    private int contador=0;
    public ProgressBar Pb;
    
    private float yInicial;
    private float distanciaMaximaFall = 20f; // Metros de diferencia para reiniciar

    void Start()
    {
        anim = GetComponent<Animator>();
        laser = GetComponent<AudioSource>();
        Pb.BarValue = vida;
        textoContador.text = "Puntaje: " + contador.ToString();
        
        yInicial = transform.position.y;
    }

    void Update()
    {
        if (muerto) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Animar();
        }
        
        // Verificar si cayó más de 20 metros
        float yActual = transform.position.y;
        if (yInicial - yActual > distanciaMaximaFall)
        {
            Debug.Log("¡JUGADOR CAYÓ! Reiniciando escena...");
            StartCoroutine(ReiniciarEscena());
        }
    }

    public void Animar()
    {
        if (atacando) return;

        StartCoroutine(Reiniciar());
    }

    public IEnumerator Reiniciar()
    {
        atacando = true;

        anim.SetBool("isSendingMagic", true);

        yield return new WaitForSecondsRealtime(1.0f);

        if (poder != null)
        {
            poder.transform.position = transform.position;
            poder.transform.rotation = transform.rotation;

            DisparoJugador disparo =
                poder.GetComponent<DisparoJugador>();

            if (disparo != null)
            {
                disparo.jugador = gameObject;

                disparo.Shoot();
                laser.Play();
            }
            else
            {
                Debug.LogError(
                    "El objeto poder no tiene DisparoJugador");
            }
        }
        else
        {
            Debug.LogError("Poder no asignado");
        }

        anim.SetBool("isSendingMagic", false);

        atacando = false;
    }

    public void Danarse(int daño)
    {
        vida -= daño;
        Pb.BarValue = vida;

        Debug.Log("VIDA: " + vida);

        anim.Play("Danarse");


        if (vida <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        if (muerto) return;

        muerto = true;


        anim.SetTrigger("Morir");
     

        Debug.Log("JUGADOR MUERTO");

    }

    public void IncrementarContadorEne()
    {
        contador=contador + 2;
        
        if (textoContador != null)
        {
            textoContador.text = "Puntaje: " + contador.ToString();
        }
        
        Debug.Log("Puntaje: " + contador);
    }
    public void IncrementarContadorDes()
    {
        contador++;
        
        if (textoContador != null)
        {
            textoContador.text = "Puntaje: " + contador.ToString();
        }
        
        Debug.Log("Puntaje: " + contador);
    }
    
    public IEnumerator ReiniciarEscena()
    {
        muerto = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}