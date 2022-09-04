using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public GameObject agent;
    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.name == "Ball") {
            agent.GetComponent<BallAgent>().HitFloor();
        }
    }
}
