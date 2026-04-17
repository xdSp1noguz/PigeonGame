using System.Collections;
using System.Collections.Generic; // Исправлено Sestem на System
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.5f; // Исправлено на sensitivity
    public float smoothing = 1.5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;

    private void Start()
    {
        // Прячем курсор и блокируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    
    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    
    void ModifyInput()
    {
        // Исправлены названия переменных
        xMousePos *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f / smoothing);
    }

    
    void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;
        
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}