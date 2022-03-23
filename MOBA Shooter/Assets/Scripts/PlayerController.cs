using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject projectile;

    private float movementX, movementY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementX * Time.deltaTime, 0, movementY * Time.deltaTime);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y * speed;
    }

    void OnFire()
    {
        GameObject shot = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.6f), transform.rotation);
    }
}
