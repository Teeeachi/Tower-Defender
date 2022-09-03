using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    public void GetInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localPosition += transform.forward*speed*Time.deltaTime;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
    }
}
