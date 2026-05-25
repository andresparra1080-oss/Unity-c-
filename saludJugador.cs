using UnityEngine;

public class SaludJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"Jugador recibe {cantidad} de daño. Vida restante: {vidaActual}");
        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto");
    }
}