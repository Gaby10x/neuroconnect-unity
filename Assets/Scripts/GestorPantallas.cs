using TMPro;
using UnityEngine;

public class GestorPantallas : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelDetalle;

    [Header("Textos del panel de detalle")]
    [SerializeField] private TMP_Text tituloEnfermedad;
    [SerializeField] private TMP_Text textoDescripcion;
    [SerializeField] private TMP_Text textoCuidados;
    [SerializeField] private TMP_Text textoAlerta;

    [Header("Narración")]
    [SerializeField] private AudioSource audioNarrador;
    [SerializeField] private AudioClip audioDiabetes;
    [SerializeField] private AudioClip audioHipertension;
    [SerializeField] private AudioClip audioArtritis;
    [SerializeField] private AudioClip audioAlzheimer;

    private AudioClip audioActual;

    private void Start()
    {
        MostrarMenu();
    }

    public void MostrarMenu()
    {
        DetenerNarracion();

        panelMenu.SetActive(true);
        panelDetalle.SetActive(false);
    }

    private void CargarEnfermedad(
        string titulo,
        string descripcion,
        string cuidados,
        string alerta,
        AudioClip narracion)
    {
        tituloEnfermedad.text = titulo;
        textoDescripcion.text = descripcion;
        textoCuidados.text = cuidados;
        textoAlerta.text = alerta;

        audioActual = narracion;

        panelMenu.SetActive(false);
        panelDetalle.SetActive(true);

        ReproducirNarracion();
    }

    public void ReproducirNarracion()
    {
        if (audioNarrador == null)
        {
            Debug.LogWarning("No se asignó el AudioSource del narrador.");
            return;
        }

        if (audioActual == null)
        {
            Debug.LogWarning("No se asignó el audio de esta enfermedad.");
            return;
        }

        audioNarrador.Stop();
        audioNarrador.clip = audioActual;
        audioNarrador.Play();
    }

    public void DetenerNarracion()
    {
        if (audioNarrador != null)
        {
            audioNarrador.Stop();
        }
    }

    public void MostrarDiabetes()
    {
        CargarEnfermedad(
            "DIABETES",
            "La diabetes ocurre cuando el nivel de azúcar en la sangre se mantiene elevado.",
            "Cumple tus controles médicos, toma los medicamentos indicados y mantén una alimentación equilibrada.",
            "Busca atención médica si presentas confusión, debilidad intensa o cambios importantes en tu estado de salud.",
            audioDiabetes
        );
    }

    public void MostrarHipertension()
    {
        CargarEnfermedad(
            "HIPERTENSIÓN",
            "La hipertensión ocurre cuando la presión de la sangre se mantiene demasiado elevada.",
            "Controla tu presión, toma los medicamentos indicados, reduce el consumo de sal y sigue las recomendaciones médicas.",
            "Busca atención urgente si presentas dolor de pecho, dificultad para respirar, confusión o dolor de cabeza muy intenso.",
            audioHipertension
        );
    }

    public void MostrarArtritis()
    {
        CargarEnfermedad(
            "ARTRITIS",
            "La artritis afecta las articulaciones y puede producir dolor, rigidez o inflamación.",
            "Sigue el tratamiento indicado y realiza únicamente la actividad física recomendada por un profesional.",
            "Consulta si el dolor o la inflamación aumentan, aparece fiebre o tienes mucha dificultad para mover una articulación.",
            audioArtritis
        );
    }

    public void MostrarAlzheimer()
    {
        CargarEnfermedad(
            "ALZHÉIMER",
            "El alzhéimer afecta progresivamente la memoria, el pensamiento y las actividades diarias.",
            "Mantén controles médicos, rutinas sencillas y el acompañamiento de familiares o cuidadores.",
            "Busca orientación médica cuando los problemas de memoria o comportamiento dificulten las actividades diarias.",
            audioAlzheimer
        );
    }
}