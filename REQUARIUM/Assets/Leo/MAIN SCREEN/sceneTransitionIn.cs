using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransitionIn : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TransitionIn());
    }


    private IEnumerator TransitionIn()
    {
        animator.SetBool("sceneTransitionOut", true);
        yield return new WaitForSeconds(3.1f);
        animator.SetBool("sceneTransitionOut", false);
        animator.SetBool("idle", true);
    }


}
