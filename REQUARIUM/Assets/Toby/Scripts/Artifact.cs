using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    public GameObject thisArtifact;
   
    public Light lightMoment;
   
    public Sprite artifactLook;
   
    public List<Sprite> artifactNormalSprites;
   
    public List<Sprite> artifactHauntedSprites;
   
    public AudioClip artifactSound;
   
    public List<AudioClip> artifactSounds;
   
    public float lightIntensity;
   
    public List<float> lightIntensities;
   
    public List<float> switchTimes;
   
    public float switchTime;
   
    private float switchTimeMax;

    public bool HasOctopus;
   
    public int price;
   
    public int maxPriceNorm;
   
    public int minPriceNorm;
   
    public int maxPriceHaunt; 
   
    public int minPriceHaunt;
   
    public int corruptionCount;
   
    public static System.Random randyTheRandom = new();
   
    public bool isHaunted;
   
    private bool isSwitch;
    
    private bool isSound;
    
    private float playerDistance;
    
    private float targetDistance = 5;
   
    private GameObject player;
    
    private PlayerInfo playerInfo;
   
    private GameObject ghost;
    
    public Ghost ghostes;

    private int amountToSpawnGhost = 3;
    
    public GameObject inkSplot;

    public GameObject mocktopus;

    public float jumpscareTime;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
        ghost = GameObject.FindGameObjectWithTag("Ghost");
        ghostes = ghost.GetComponent<Ghost>();
        ghostes.isArtifact = true;
        inkSplot = GameObject.FindGameObjectWithTag("Ink");
        mocktopus = GameObject.FindGameObjectWithTag("Moctopus");
        List<float> shuffledFloats = switchTimes.OrderBy(x => randyTheRandom.Next()).ToList();
        switchTimeMax = shuffledFloats[0];
        switchTime = switchTimeMax;
        SetArtifactValues();
    }
    public void SetArtifactValues()
    {
        RandomizeHauntedValue();
        if (corruptionCount <= 4)
        {
            RandomizeNormalSprite();
            lightIntensity = 0;
            thisArtifact.GetComponent<Light>().intensity = lightIntensity;
        }
        else if (corruptionCount == 5 || corruptionCount == 6 || corruptionCount == 7)
        {
            RandomizeHauntedSprite();
            isHaunted = true;
        }
        else if (corruptionCount == 8 || corruptionCount == 9 || corruptionCount == 10)
        {
            RandomizeLightCue();
            RandomizeNormalSprite();
            isHaunted = true;
        }
        else if (corruptionCount == 11 || corruptionCount == 12 || corruptionCount == 13)
        {
            RandomizeNormalSprite();
            RandomizeSound();
            isHaunted = true;
            isSound = true; 
        }
        else if (corruptionCount == 14)
        {
            RandomizeNormalSprite();
            HasOctopus = true;
        }
        else if (corruptionCount == 15)
        {
            RandomizeNormalSprite();
            RandomizeLightCue();
            HasOctopus = true;
            isHaunted = true;
        }
        else if (corruptionCount == 16)
        {
            RandomizeHauntedSprite();
            HasOctopus = true;
            isHaunted = true;
        }
        else if (corruptionCount == 17)
        {
            RandomizeHauntedSprite();
            RandomizeSound();
            RandomizeLightCue();
            isHaunted = true;
            isSound = true;
        }
        else if (corruptionCount == 18)
        {
            isHaunted = true;
            isSwitch = true;
        }
        RandomizePrice();
    }
    public void RandomizeHauntedValue()
    {
        corruptionCount = randyTheRandom.Next(0, 18);
    }
    public void RandomizeNormalSprite()
    {
        List<Sprite> shuffledSprites = artifactNormalSprites.OrderBy(x => randyTheRandom.Next()).ToList();
        artifactLook = shuffledSprites[0];
        thisArtifact.GetComponent<SpriteRenderer>().sprite = artifactLook;
    }
    public void RandomizeHauntedSprite()
    {
        List<Sprite> shuffledSprites = artifactHauntedSprites.OrderBy(x => randyTheRandom.Next()).ToList();
        artifactLook = shuffledSprites[0];
        thisArtifact.GetComponent<SpriteRenderer>().sprite = artifactLook;
    }
    public void RandomizeSound()
    {
        List<AudioClip> shuffledSounds = artifactSounds.OrderBy(x => randyTheRandom.Next()).ToList();
        artifactSound = shuffledSounds[0];
        thisArtifact.GetComponent<AudioSource>().clip = artifactSound;
    }
    public void RandomizeLightCue()
    {
        List<float> shuffledLights = lightIntensities.OrderBy(x => randyTheRandom.Next()).ToList();
        lightIntensity = shuffledLights[0];
        thisArtifact.GetComponent<Light>().intensity = lightIntensity;
    }
    public void RandomizePrice()
    {
        if (corruptionCount <= 4)
        {
            price = randyTheRandom.Next(minPriceNorm, maxPriceNorm);
        }
        else if (corruptionCount >= 5 && corruptionCount < 11)
        {
            price = randyTheRandom.Next(minPriceHaunt, maxPriceHaunt);
        }
        else if (corruptionCount > 16)
        {
            price = randyTheRandom.Next(1000, 3000);
        }
    }
    public void SwitchSprite()
    {
        if (switchTime <= 0)
        {
            RandomizeNormalSprite();
            switchTime = switchTimeMax;
        }
        else
        {
            switchTime -= Time.deltaTime;
        }
    }
    public void PlaySound()
    {
        if (switchTime <= 0)
        {
            thisArtifact.GetComponent<AudioSource>().Play();
            switchTime = switchTimeMax;
        }
        else
        {
            switchTime -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cruci-Fish"))
        {
            if (CRUCIFISH.isHaunted == true)
            {
                price = price / 2;
            }
            if (isHaunted == false)
            {
                Destroy(thisArtifact);
            }
            else if (isHaunted == true)
            {
                isHaunted = false;
                Debug.Log(isHaunted);
            }
        }
    }
    void Update()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        if (playerDistance <= targetDistance && Input.GetKeyDown(KeyCode.E))
        {
            if (HasOctopus == true)
            {
                mocktopus.GetComponent<Image>().gameObject.SetActive(true);
                jumpscareTime -= Time.deltaTime;
                if (jumpscareTime <= 0)
                {
                    inkSplot.GetComponent<Image>().gameObject.SetActive(true);
                }
            }
            if (isHaunted == true)
            {
                playerInfo.artifactsGrabbed += 1;
                ghostes.isPossessing = true;
                playerInfo.possessedNumber += 1;
                ghostes.flashlight = GameObject.FindGameObjectWithTag("Light");
                ghostes.state = Ghost.States.Possessing;
                SaveDataManager.Instance.daveSata.quotaMoney += price / 2;
                SaveDataManager.Instance.daveSata.spendingMoney += price / 2;
                if (playerInfo.artifactsGrabbed == amountToSpawnGhost && SaveDataManager.Instance.daveSata.isNewGame == false)
                 {
                     FindObjectOfType<EnemyManager>().SpawnGhost();
                     amountToSpawnGhost = amountToSpawnGhost + 3;
                 }
                Destroy(thisArtifact);
            }
            else if (isHaunted == false)
            {
                SaveDataManager.Instance.daveSata.quotaMoney += price;
                SaveDataManager.Instance.daveSata.spendingMoney += price;
                playerInfo.artifactsGrabbed += 1;
                if (playerInfo.artifactsGrabbed == amountToSpawnGhost && SaveDataManager.Instance.daveSata.isNewGame == false)
                {
                    FindObjectOfType<EnemyManager>().SpawnGhost();
                    amountToSpawnGhost = amountToSpawnGhost + 3;
                }
                FindObjectOfType<ArtifactController>().CreateArtifact();
                Destroy(thisArtifact);
            }
        }
        if (isSwitch == true)
        {
            SwitchSprite();
        }
        if (isSound == true)
        {
            PlaySound();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(isHaunted);
        }
    }
}
