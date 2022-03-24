using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    public GameObject projectile;
    public Camera cam;

    private float movementX, movementY;
    private Vector3 mousePos;
    private float money, income;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        income = 1;
        InvokeRepeating("Income", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementX * Time.deltaTime, 0, movementY * Time.deltaTime, Space.World);
        FaceMouse();
    }

    void LateUpdate()
    {
        HealthCheck();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y * speed;
    }

    void OnFire()
    {
        GameObject shot = Instantiate(projectile, transform.position + transform.forward, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            health -= 20;
        }
    }

    void FaceMouse()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, cam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
    }

    void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Income()
    {
        Debug.Log(money);
        money += income;
    }
}
