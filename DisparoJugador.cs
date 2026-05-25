using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public GameObject jugador;
    public Transform particulas;
    public AudioSource audioImpacto; // Asignar en Inspector
    private ParticleSystem systemaParticulas;

    public float tiempoEntreDisparos = 1f;
    public float rango = 100f;
    public int daño = 25;

    float timer;

    Ray shootRay;
    RaycastHit shootHit;

    int shootableMask;

    LineRenderer gunLine;
    Light gunLight;

    float effectsDisplayTime = 0.2f;

    void Start()
    {
       systemaParticulas = particulas.GetComponent<ParticleSystem>();

        systemaParticulas.Stop();
        
        // Si no está asignado en Inspector, intentar obtenerlo
        if (audioImpacto == null)
        {
            audioImpacto = GetComponent<AudioSource>();
        }
    }

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunLine = GetComponent<LineRenderer>();

        gunLight = GetComponent<Light>();

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tiempoEntreDisparos * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void Shoot()
    {
        Debug.Log("JUGADOR DISPARANDO");

        Vector3 ubicacion = new Vector3(
            jugador.transform.position.x,
            jugador.transform.position.y + 1.1f,
            jugador.transform.position.z
        );

        timer = 0f;

        gunLine.enabled = true;
        gunLight.enabled = true;

        shootRay.origin = ubicacion;
        shootRay.direction = jugador.transform.forward;

        gunLine.SetPosition(0, ubicacion);
   

        // Usar raycast que detecte múltiples impactos
        RaycastHit[] hits = Physics.RaycastAll(shootRay, rango);
        
        bool enemigoEncontrado = false;
        
        // Buscar el primer impacto que sea enemigo
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.isTrigger) continue; // Ignorar triggers
            
            Debug.Log("HIT SOMETHING: " + hit.collider.name);
            Debug.Log("Impacto con: " + hit.collider.name);
            Debug.Log("OBJETO GOLPEADO: " + hit.collider.gameObject.name);

            // Reproducir partículas en el punto de impacto
            if (systemaParticulas != null)
            {
                particulas.position = hit.point;
                particulas.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                systemaParticulas.Play();
            }

            // Buscar componente Enemigo
            Enemigo enemigo = hit.collider.GetComponent<Enemigo>();
            
            if (enemigo == null)
            {
                enemigo = hit.collider.transform.root.GetComponent<Enemigo>();
            }
            
            if (enemigo != null)
            {
                Debug.Log("ENEMIGO ENCONTRADO");
                enemigo.Danarse(25);
                
                // Incrementar contador del jugador
                if (jugador != null)
                {
                    Jugador_control jugadorControl = jugador.GetComponent<Jugador_control>();
                    if (jugadorControl != null)
                    {
                        jugadorControl.IncrementarContadorEne();
                    }
                }
                
                enemigoEncontrado = true;
                gunLine.SetPosition(1, hit.point);
                break;
            }
            else
            {
                // Reproducir sonido de impacto si no es enemigo
                if (audioImpacto != null)
                {
                    audioImpacto.Play();
                    Debug.Log("SONIDO DE IMPACTO REPRODUCIDO");
                }

                // Buscar objeto destructible con tag "Destruir"
                if (hit.collider.CompareTag("Destruir"))
                {
                    ObjetoDestruible destruible = hit.collider.GetComponent<ObjetoDestruible>();
                    
                    if (destruible == null)
                    {
                        destruible = hit.collider.transform.root.GetComponent<ObjetoDestruible>();
                    }
                    
                    if (destruible != null)
                    {
                        Debug.Log("OBJETO DESTRUCTIBLE ENCONTRADO");
                        destruible.Danarse(25);
                                    if (jugador != null)
                            {
                                Jugador_control jugadorControl = jugador.GetComponent<Jugador_control>();
                                if (jugadorControl != null)
                                {
                                    jugadorControl.IncrementarContadorDes();
                                }
                            }
                    }
                }

                enemigoEncontrado = true;
                gunLine.SetPosition(1, hit.point);
                break;
            }
        }
        
        // Si no encuentra enemigo, mostrar último impacto
        if (!enemigoEncontrado && hits.Length > 0)
        {
            gunLine.SetPosition(1, hits[hits.Length - 1].point);
        }
        else if (!enemigoEncontrado)
        {
            Debug.Log("No impacto");
            gunLine.SetPosition(1,
                shootRay.origin + shootRay.direction * rango);
        }
    }

    void DisableEffects()
    {
        gunLine.enabled = false;

        gunLight.enabled = false;
    }
}