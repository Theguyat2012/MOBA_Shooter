using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private Rigidbody rb;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Indestructible")
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
