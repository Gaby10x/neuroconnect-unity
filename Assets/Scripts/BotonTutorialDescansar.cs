using UnityEngine;

public class BotonTutorialDescansar : MonoBehaviour
{
    [Header("Administrador del tutorial")]
    public TutorialManager tutorialManager;

    private void OnMouseDown()
    {
        if (tutorialManager != null)
        {
            tutorialManager.SeleccionarDescansar();
        }
    }
}