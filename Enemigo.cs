using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Referencias")]
    public Transform jugador;
    Animator anim;

    [Header("Disparo")]
    public GameObject poder;

    [Header("Ataque")]
    public float rangoAtaque = 10f;
    public float enfriamientoAtaque = 2f;
    public int dañoAtaque = 10;

    private NavMeshAgent agente;

    private bool atacando = false;
    private bool muerto = false;

    private int vida = 10;

    private float siguienteAtaque;
    public AudioSource laser;
    public AudioSource muerte;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (jugador == null)
        {
            GameObject j = GameObject.FindGameObjectWithTag("Player");

            if (j != null)
            {
                jugador = j.transform;
            }
            else
            {
                Debug.LogError("No se encontró el jugador");
            }
        }

        if (agente != null)
        {
            agente.stoppingDistance = rangoAtaque * 0.8f;
        }
    }

    void Update()
    {
        if (muerto) return;

        if (jugador == null) return;

        float distancia =
            Vector3.Distance(transform.position, jugador.position);

        // MOVERSE
        if (!atacando && agente != null && agente.isOnNavMesh)
        {
            agente.SetDestination(jugador.position);
        }

        // ATACAR
        if (distancia <= rangoAtaque &&
            Time.time >= siguienteAtaque)
        {
            siguienteAtaque = Time.time + enfriamientoAtaque;

            Atacar();
        }
    }

    public void Atacar()
    {
        if (atacando) return;

        atacando = true;

        Debug.Log("ENEMIGO ATACANDO");

        anim.SetBool("isSendingMagic", true);

        StartCoroutine(ReiniciarAtaque());
    }

    IEnumerator ReiniciarAtaque()
    {
        yield return new WaitForSeconds(0.5f);

        // DISPARO
        if (poder != null)
        {
            DisparoEnemigo disparo =
                poder.GetComponent<DisparoEnemigo>();

            if (disparo != null)
            {
                disparo.enemigo = gameObject;

                disparo.Shoot();

                laser.Play();
            }
        }

        yield return new WaitForSeconds(1f);

        anim.SetBool("isSendingMagic", false);

        atacando = false;
    }
    public void Danarse(int daño)
    {
        vida -= daño;

        Debug.Log("VIDA: " + vida);

        anim.Play("Danarse");

        if (vida < 0)
        {
            Morir();

        }
    }
    public void Morir()
    {
        if (muerto) return;

        muerto = true;

        atacando = false;

        if (agente != null)
        {
            agente.isStopped = true;
        }

        anim.SetTrigger("Morir");
        muerte.Play();

        Debug.Log("ENEMIGO MUERTO");


        Destroy(gameObject, 5f);
    }
}