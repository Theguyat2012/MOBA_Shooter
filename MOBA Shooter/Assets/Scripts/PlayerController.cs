using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    // Player Components
    private Rigidbody rb;
    private PhotonView view;

    // Player Stats
    public float speed;
    public int health;
    public float money, income;

    // Player Objects
    public Camera camera;
    public GameObject projectile;
    public UnityEvent mysteryBoxAction;

    // Player UI
    public Canvas hud;
    private TextMeshProUGUI textHealth;
    private TextMeshProUGUI textMoney;

    // Player Equipment
    public int currentWeapon;
    public GameObject primary;
    public GameObject secondary;
    public GameObject melee;

    // Player Controls
    private float movementX, movementY;
    private Camera cam;
    private Vector3 mousePos;
    private bool inRangeMysteryBox;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        
        if (view.IsMine)
        {
            Debug.Log(view);
            rb = gameObject.GetComponent<Rigidbody>();
            cam = Instantiate(camera, transform.position + camera.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            hud = Instantiate(hud);
            textHealth = hud.transform.Find("Health").GetComponent<TextMeshProUGUI>();
            textMoney = hud.transform.Find("Money").GetComponent<TextMeshProUGUI>();

            money = 100;
            income = 1;
            InvokeRepeating("Income", 10f, 10f);
            inRangeMysteryBox = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            rb.velocity = new Vector3 (movementX * Time.deltaTime, 0, movementY * Time.deltaTime);
            FaceMouse();
            HudUpdate();
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
        if (view.IsMine)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x * speed;
            movementY = movementVector.y * speed;
        }
    }

    void OnFire()
    {
        if (view.IsMine)
        {
            PhotonNetwork.Instantiate(projectile.name, transform.position + transform.forward, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            health -= 20;
            if (view.IsMine)
            {
                // other.gameObject.GetComponent<PhotonView>().RequestOwnership();
                // other.gameObject.GetComponent<PhotonView>().TransferOwnership(view.CreatorActorNr);
                PhotonNetwork.Destroy(other.gameObject);
            }
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

    void HudUpdate()
    {
        textHealth.text = "Health: " + health.ToString();
        textMoney.text = "Money: " + money.ToString() + " + " + income.ToString();
    }

    void HealthCheck()
    {
        if (view)
        {
            if (view.IsMine)
            {
                if (health <= 0)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
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
