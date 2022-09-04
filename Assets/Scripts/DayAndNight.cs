using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    /*   int fadeOutTime;
       public GameObject Light;
       public float speed = 10f;
       public float targetRotation = 0;
       private void fadeOutTheSun()
       {
           //transform.rotation = Quaternion.Lerp(Quaternion.Euler(60, 0, 0), Quaternion.Euler(190, 0, 0), Time.deltaTime*100f);
           transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(targetRotation, transform.rotation.y, transform.rotation.z), Time.deltaTime * speed);
       }

       private void fadeInTheSun()
       {
           Light.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(60, 0, 0), Time.deltaTime * 10f);
       }

       private void Start()
       {
           transform.rotation = Quaternion.Euler(targetRotation, transform.rotation.y, transform.rotation.z);
       }
       private void Update()
       {
           fadeOutTheSun();
           targetRotation += Time.deltaTime*speed;
       }*/
    int fadeOutTime;
    public GameObject Light;
    public float speed = 10f;
    public float targetRotation = 0;
    private void fadeOutTheSun()
    {
        // transform.color = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation, transform.rotation.y, transform.rotation.z), Time.deltaTime * speed);
        GetComponent<Light>().color = Color.blue;

    }
}

    

 


