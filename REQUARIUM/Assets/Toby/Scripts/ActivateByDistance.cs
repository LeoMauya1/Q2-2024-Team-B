using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByDistance : MonoBehaviour
{
    public GameObject player;
    public PlayerInfo playerInfo;

    public float playerDistance;

    private float targetDistance = 5;

    public KeyCode activateThing;
    public bool thingActivated;

    public GameObject thingToActivate;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        if (playerDistance <= targetDistance && Input.GetKeyDown(activateThing) && thingActivated == false)
        {
            thingToActivate.gameObject.SetActive(true);
            Time.timeScale = 0;
            thingActivated = true;
        }
        else if (thingActivated == true && Input.GetKeyDown(activateThing))
        {
            thingToActivate.gameObject.SetActive(false);
            Time.timeScale = 1;
            thingActivated = false;
        }
    }
}
