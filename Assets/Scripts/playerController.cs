using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public bool inOrbit = false;
    private float speed = 10f;

    private void Update()
    {
        if (inOrbit == false)
        {
            Vector3 move = transform.up.normalized;
            move = move * speed * Time.deltaTime;
            transform.position += move;
        }
    }
}
