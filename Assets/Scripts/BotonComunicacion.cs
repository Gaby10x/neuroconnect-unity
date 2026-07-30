using UnityEngine;
using TMPro;
using System.Collections;

public class BotonComunicacion : MonoBehaviour
{
    [Header("Mensaje que se mostrará")]
    public string mensaje;

    [Header("Texto principal en pantalla")]
    public TextMeshProUGUI textoPrincipal;

    [Header("Tiempo que el mensaje estará visible")]
    public float tiempoVisible = 2f;

    [Header("Audio del mensaje")]
    public AudioClip audioMensaje;

    private AudioSource audioSource;
    private Coroutine rutinaMensaje;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();

        if (textoPrincipal != null)
        {
            textoPrincipal.text = "";
        }
    }

    void OnMouseDown()
    {
        MostrarMensajeTemporal();
        ReproducirAudio();

        Debug.Log("Botón presionado: " + mensaje);
    }

    void MostrarMensajeTemporal()
    {
        if (textoPrincipal != null)
        {
            if (rutinaMensaje != null)
            {
                StopCoroutine(rutinaMensaje);
            }

            rutinaMensaje = StartCoroutine(MostrarYLimpiarMensaje());
        }
    }

    IEnumerator MostrarYLimpiarMensaje()
    {
        textoPrincipal.text = mensaje;

        yield return new WaitForSeconds(tiempoVisible);

        textoPrincipal.text = "";
    }

    void ReproducirAudio()
    {
        if (audioSource != null && audioMensaje != null)
        {
            audioSource.Stop();
            audioSource.clip = audioMensaje;
            audioSource.Play();
        }
    }
}