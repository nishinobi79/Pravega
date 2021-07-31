using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float mass = 100f;
    public float G = -66.3f;
    public float soi;
    public bool isReleased = false;
    public bool isCaught = false;


    public GameObject spaceship;
    public GameObject orbiter;


    private void Start()
    {
        soi = gameObject.transform.localScale.x;
    }

    private void Update()
    {
        Vector2 direction = spaceship.transform.position - gameObject.transform.position;
        if(direction.magnitude <= soi && isReleased==false && isCaught == false)
        {
            TakeIntoOrbit();
        }

        if (isCaught == true)
        {
            MaintainOrbit();
        }

        if(direction.magnitude > soi + 0.1f)
        {
            isReleased = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReleaseFromOrbit();
        }

    }

    private void TakeIntoOrbit()
    {
        //Debug.Log("Reached orbit");
        spaceship.transform.parent = orbiter.transform;
        Vector3 pos = spaceship.transform.position - gameObject.transform.position;
        pos = pos.normalized * (soi - 0.1f);

        spaceship.transform.position = pos + transform.position;
        spaceship.transform.right = pos;
        isCaught = true;

        spaceship.GetComponent<playerController>().inOrbit = true;
    }
    
    private void MaintainOrbit()
    {
        float w = orbiter.GetComponent<Orbiter>().angularVelcoity;
        //Debug.Log("Maintaining orbit");
        Vector3 pos = spaceship.transform.position - gameObject.transform.position;
        pos = pos.normalized * (soi - 0.1f);

        spaceship.transform.position = pos + transform.position;
        spaceship.transform.right = pos;
        spaceship.transform.up = Vector3.Cross(pos, new Vector3(0f, 0f, -w)).normalized;
    }
    private void ReleaseFromOrbit()
    {
        //Debug.Log("released from orbit");
        isCaught = false;
        isReleased = true;

        spaceship.transform.parent = null;

        spaceship.GetComponent<playerController>().inOrbit = false;

    }
}
