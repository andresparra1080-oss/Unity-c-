using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public GameObject enemigo;

    public float tiempoEntreDisparos = 1f;
    public float rango = 100f;
    public int daño = 10;

    float timer;

    Ray shootRay;
    RaycastHit shootHit;

    int shootableMask;

    LineRenderer gunLine;
    Light gunLight;

    float effectsDisplayTime = 0.2f;


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
        Debug.Log("ENEMIGO DISPARANDO");

        Vector3 ubicacion = new Vector3(
            enemigo.transform.position.x,
            enemigo.transform.position.y + 1.1f,
            enemigo.transform.position.z
        );

        timer = 0f;

        gunLine.enabled = true;
        gunLight.enabled = true;

        shootRay.origin = ubicacion;
        shootRay.direction = enemigo.transform.forward;

        gunLine.SetPosition(0, ubicacion);

        if (Physics.Raycast(shootRay, out shootHit, rango))
        {
            Debug.Log("Impacto con: " + shootHit.collider.name);

            Jugador_control jugador =
                shootHit.collider.GetComponentInParent<Jugador_control>();

            if (jugador != null)
            {
                Debug.Log("JUGADOR ENCONTRADO");

                jugador.Danarse(10);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else
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