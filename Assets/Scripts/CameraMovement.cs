using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _mouseSensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotate();
    }

    private void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _player.transform.Rotate(mouseX * new Vector3(0, 1, 0));
        gameObject.transform.Rotate(mouseY * new Vector3(1, 0, 0));
    }
}
