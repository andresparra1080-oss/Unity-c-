using System.Collections;
using UnityEngine;

public class ObjetoDestruible : MonoBehaviour
{
    private int vida = 50;
    private bool destruido = false;

    public void Danarse(int daño)
    {
        vida -= daño;

        Debug.Log("OBJETO VIDA: " + vida);

        if (vida <= 0)
        {
            Destruir();
        }
    }

    public void Destruir()
    {
        if (destruido) return;

        destruido = true;

        Debug.Log("OBJETO DESTRUIDO");

        Destroy(gameObject);
    }
}
