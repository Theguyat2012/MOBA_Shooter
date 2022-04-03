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
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        // Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        if (view)
        {
            if (view.IsMine)
            {
                rb.velocity = transform.up * speed;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (view)
        {
            if (view.IsMine)
            {
                if (other.tag == "Indestructible")
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
