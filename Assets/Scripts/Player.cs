using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float x=500;
    public float y=800;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Vertical");
        y = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, 0), 0.2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y+180, 0), 0.2f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y-90, 0), 0.2f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y+90, 0), 0.2f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Kikk");

        }


    }
    private void FixedUpdate()
    {
        gameObject.GetComponent<Animator>().SetFloat("speed", x, 0.1f, Time.deltaTime);
        gameObject.GetComponent<Animator>().SetFloat("Direction", y, 0.1f, Time.deltaTime);
        if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }
    }
}
