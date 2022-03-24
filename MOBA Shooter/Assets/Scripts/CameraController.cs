using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, 20.0f + player.transform.position.y, player.transform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
