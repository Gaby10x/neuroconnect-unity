using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Texto del tutorial")]
    public TextMeshProUGUI textoTutorial;

    [Header("Audio")]
    public AudioSource audioNarrador;

    [Header("Audios iniciales")]
    public AudioClip audioSaludo;
    public AudioClip audioMovimiento;

    [Header("Audios de las cuatro necesidades")]
    public AudioClip audioAyuda;
    public AudioClip audioAgua;
    public AudioClip audioFrio;
    public AudioClip audioDescansarExplicacion;

    [Header("Actividad práctica")]
    public AudioClip audioPedirDescansar;
    public AudioClip audioRespuestaDescansar;
    public AudioClip audioExplicacionFinal;
    public AudioClip audioDespedida;

    [Header("Indicadores de los pictogramas")]
    public GameObject indicadorAyuda;
    public GameObject indicadorAgua;
    public GameObject indicadorFrio;
    public GameObject indicadorDescansar;

    [Header("Configuración")]
    public string escenaPrincipal = "Habitacion_NeuroConnect";
    public float pausaEntreAudios = 0.4f;

    private bool esperandoBotonDescansar;
    private bool tutorialFinalizado;

    private void Start()
    {
        esperandoBotonDescansar = false;
        tutorialFinalizado = false;

        DesactivarIndicadores();

        if (textoTutorial != null)
        {
            textoTutorial.text = "";
        }

        StartCoroutine(IniciarTutorial());
    }

    private IEnumerator IniciarTutorial()
    {
        yield return ReproducirNarracion(
            audioSaludo,
            "Hola, bienvenido a NeuroConnect. Esta aplicación te ayudará a comunicar de forma sencilla lo que necesitas."
        );

        yield return ReproducirNarracion(
            audioMovimiento,
            "Para mirar alrededor, toca la pantalla y desliza suavemente el dedo hacia la dirección que deseas observar."
        );

        ActivarIndicador(indicadorAyuda);

        yield return ReproducirNarracion(
            audioAyuda,
            "El pictograma Necesito ayuda permite comunicar que necesitas la asistencia de otra persona."
        );

        DesactivarIndicador(indicadorAyuda);
        ActivarIndicador(indicadorAgua);

        yield return ReproducirNarracion(
            audioAgua,
            "El pictograma Quiero agua permite comunicar que deseas tomar agua."
        );

        DesactivarIndicador(indicadorAgua);
        ActivarIndicador(indicadorFrio);

        yield return ReproducirNarracion(
            audioFrio,
            "El pictograma Tengo frío permite comunicar que necesitas abrigo o sentirte más cómodo."
        );

        DesactivarIndicador(indicadorFrio);
        ActivarIndicador(indicadorDescansar);

        yield return ReproducirNarracion(
            audioDescansarExplicacion,
            "El pictograma Quiero descansar permite comunicar que deseas descansar o dormir."
        );

        yield return ReproducirNarracion(
            audioPedirDescansar,
            "Ahora vamos a practicar. Desliza el dedo por la pantalla, busca el pictograma Quiero descansar y tócalo una vez."
        );

        esperandoBotonDescansar = true;

        if (textoTutorial != null)
        {
            textoTutorial.text =
                "Busca y toca el pictograma\nQUIERO DESCANSAR";
        }
    }

    public void SeleccionarDescansar()
    {
        if (!esperandoBotonDescansar || tutorialFinalizado)
        {
            return;
        }

        esperandoBotonDescansar = false;
        tutorialFinalizado = true;

        StartCoroutine(FinalizarTutorial());
    }

    private IEnumerator FinalizarTutorial()
    {
        DesactivarIndicador(indicadorDescansar);

        yield return ReproducirNarracion(
            audioRespuestaDescansar,
            "Quiero descansar."
        );

        yield return ReproducirNarracion(
            audioExplicacionFinal,
            "Muy bien. Cuando tocas un pictograma, NeuroConnect comunica en voz alta lo que necesitas."
        );

        yield return ReproducirNarracion(
            audioDespedida,
            "Has terminado el tutorial. Ahora puedes comenzar a utilizar NeuroConnect."
        );

        CargarEscenaPrincipal();
    }

    private IEnumerator ReproducirNarracion(
        AudioClip clip,
        string mensaje
    )
    {
        if (textoTutorial != null)
        {
            textoTutorial.text = mensaje;
        }

        if (audioNarrador != null && clip != null)
        {
            audioNarrador.Stop();
            audioNarrador.clip = clip;
            audioNarrador.Play();

            yield return new WaitForSeconds(
                clip.length + pausaEntreAudios
            );
        }
        else
        {
            // Permite probar el tutorial aunque falte algún audio.
            yield return new WaitForSeconds(3f);
        }
    }

    private void ActivarIndicador(GameObject indicador)
    {
        if (indicador != null)
        {
            indicador.SetActive(true);
        }
    }

    private void DesactivarIndicador(GameObject indicador)
    {
        if (indicador != null)
        {
            indicador.SetActive(false);
        }
    }

    private void DesactivarIndicadores()
    {
        DesactivarIndicador(indicadorAyuda);
        DesactivarIndicador(indicadorAgua);
        DesactivarIndicador(indicadorFrio);
        DesactivarIndicador(indicadorDescansar);
    }

    private void CargarEscenaPrincipal()
    {
        if (string.IsNullOrWhiteSpace(escenaPrincipal))
        {
            Debug.LogError(
                "No se ha indicado el nombre de la escena principal."
            );

            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(escenaPrincipal))
        {
            Debug.LogError(
                "No se puede cargar la escena: " +
                escenaPrincipal +
                ". Comprueba que esté agregada en Build Profiles."
            );

            return;
        }

        SceneManager.LoadScene(escenaPrincipal);
    }
}