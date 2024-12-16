using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEAR : MonoBehaviour
{





    private void Update()
    {
        StartCoroutine(destroy());
    }



    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
