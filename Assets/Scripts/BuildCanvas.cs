using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    public Transform maincamera;

    void Start()
    {
        maincamera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(maincamera);
        transform.rotation *= Quaternion.Euler(0, 180f, 0);
    }
}
