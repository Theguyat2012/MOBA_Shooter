using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    public Camera camera;
    public GameObject projectile;
    public UnityEvent mysteryBoxAction;

    private Rigidbody rb;
    private float movementX, movementY;
    private Vector3 mousePos;
    private float money, income;
    private bool inRangeMysteryBox;
    private Vector3 offset;
    private Camera cam;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        
        if (view.IsMine)
        {
            rb = gameObject.GetComponent<Rigidbody>();
            money = 100;
            income = 1;
            InvokeRepeating("Income", 10f, 10f);
            inRangeMysteryBox = false;
            cam = Instantiate(camera, transform.position + camera.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            rb.velocity = new Vector3 (movementX * Time.deltaTime, 0, movementY * Time.deltaTime);
            FaceMouse();
            MysteryBox();
            cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (view.IsMine)
        {
            HealthCheck();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y * speed;
    }

    void OnFire()
    {
        if (view.IsMine)
        {
            PhotonNetwork.Instantiate("Projectile", transform.position + transform.forward, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            health -= 20;
        }

        if (other.tag == "Mystery Box")
        {
            inRangeMysteryBox = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mystery Box")
        {
            inRangeMysteryBox = false;
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
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void Income()
    {
        money += income;
    }

    void MysteryBox()
    {
        if (inRangeMysteryBox) {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (money >= 50.0f)
                {
                    money -= 50.0f;
                    mysteryBoxAction.Invoke();
                } else 
                {
                    Debug.Log("Not enough money");
                }
            }
        }
    }
}
