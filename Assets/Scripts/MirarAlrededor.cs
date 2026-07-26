using UnityEngine;

public class MirarAlrededor : MonoBehaviour
{
    public float sensibilidad = 2f;
    public float limiteVertical = 35f;

    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Start()
    {
        Vector3 rotacionInicial = transform.localEulerAngles;
        rotacionY = rotacionInicial.y;
        rotacionX = rotacionInicial.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        rotacionY += mouseX;
        rotacionX -= mouseY;

        rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);

        transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}