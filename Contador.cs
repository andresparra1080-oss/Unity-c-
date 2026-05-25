using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public int contador;
    public int hitsNeeded = 3;
    public Material damagedMaterial;
    private Material originalMaterial;
    private Renderer rend;

    void Start()
    {
        contador = 0;
        rend = GetComponent<Renderer>();
        if (rend != null) originalMaterial = rend.material;
    }

    public void Hit()
    {
        contador++;
        if (contador >= hitsNeeded)
        {
            Destroy(gameObject);
        }
        else if (damagedMaterial != null && rend != null)
        {
            rend.material = damagedMaterial; // show cracking effect after first hit
        }
    }
}

