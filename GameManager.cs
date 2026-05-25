using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject gameOverPanel;

    private bool juegoTerminado = false;

    void Start()
    {
        gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void MostrarGameOver()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        StartCoroutine(GameOverRutina());
    }

    IEnumerator GameOverRutina()
    {
        // Esperar animación de muerte
        yield return new WaitForSeconds(2f);

        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}