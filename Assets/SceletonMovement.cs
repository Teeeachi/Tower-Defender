using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceletonMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float runspeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, runspeed * Time.deltaTime);
    }
}
