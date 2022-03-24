using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject projectile;
    public Camera cam;

    private float movementX, movementY;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementX * Time.deltaTime, 0, movementY * Time.deltaTime, Space.World);
        FaceMouse();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y * speed;
    }

    void OnFire()
    {
        GameObject shot = Instantiate(projectile, transform.position + transform.forward, transform.rotation);
    }

    void FaceMouse()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, cam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
    }
}
