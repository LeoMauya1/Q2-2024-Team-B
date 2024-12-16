using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEAR : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.forward = rb.velocity;
    }


    private void Start()
    {
        StartCoroutine(destroy());
    }



    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
