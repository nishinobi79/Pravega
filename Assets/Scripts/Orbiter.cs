using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public float angularVelcoity = 50f;
    public float dirRadius;

    public GameObject spaceShip;

    private float planetsoi;
    private bool wchanged = false;
    
    void Update()
    {
        planetsoi = transform.parent.gameObject.GetComponent<PlanetGravity>().soi;
        dirRadius = planetsoi + 0.2f;

        if ((spaceShip.transform.position - transform.position).magnitude <= dirRadius && (spaceShip.transform.position - transform.position).magnitude > planetsoi && wchanged == false)
        {
            AdjustAngularVelocity();
        }

        if((spaceShip.transform.position - transform.position).magnitude <= planetsoi)
        {
            RevolveAroundPlanet();
        }

        if((spaceShip.transform.position - transform.position).magnitude > dirRadius)
        {
            wchanged = false;
        }
        
    }

    private void AdjustAngularVelocity()
    {
        Vector3 a = spaceShip.transform.up;
        Vector3 b = spaceShip.transform.position - transform.position;

        Vector3 c = Vector3.Cross(a, b).normalized;
        
        if (c.z > 0)
        {
            if(angularVelcoity > 0f)
            {
                angularVelcoity *= -1f;
            }
        }
        else
        {
            if (angularVelcoity < 0f)
            {
                angularVelcoity *= -1f;
            }
        }

        wchanged = true;
    }

    private void RevolveAroundPlanet()
    {
        transform.Rotate(new Vector3(0f, 0f, angularVelcoity) * Time.deltaTime);
        /*if(angularVelcoity < 0 && gameObject.transform.childCount == 1)
        {
            Vector3 face = spaceShip.transform.up;
            spaceShip.transform.up = -1 * face;
        }*/
    }
}
