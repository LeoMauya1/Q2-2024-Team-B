using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Artifact : MonoBehaviour
{
    public float playerDistance;
    
    public GameObject thisArtifact;

    public Light lightMoment;

    public Sprite artifactLook;

    public List<Sprite> artifactNormalSprites;

    public List<Sprite> artifactHauntedSprites;

    public AudioClip artifactSound;

    public List<AudioClip> artifactSounds;

    public float lightIntensity;

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

    public bool isSound;


    private float targetDistance = 5;

    private GameObject player;

    private PlayerInfo playerInfo;

    public ArtifactGhost ghostes;

    public GameObject ghost;

    public int amountToSpawnGhost = 3;

    public JumpscareController jumpscare;

    public bool hasGrabbed;

    public KeyCode grabArtifact;

    public AudioClip defaultClip;

    public AudioSource audioSource;

    public AudioClip moctopusSound;

    public ParticleSystem particle;

    public AudioSource particleSound;

    public ParticleSystem dirt;

    public AudioSource dirtSound;

    public AudioSource grab;

    public GameObject moctopus;

    public bool isTutorial;

    public GameObject infoText;

    public GameObject possessText;

    public GameObject grabText;

    public GameObject reminderText;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();
        ghost = GameObject.FindGameObjectWithTag("Ghost");
        ghostes = ghost.GetComponent<ArtifactGhost>();
        grab = ghost.GetComponent<AudioSource>();
        List<float> shuffledFloats = switchTimes.OrderBy(x => randyTheRandom.Next()).ToList();
        switchTimeMax = shuffledFloats[0];
        switchTime = switchTimeMax;
        if (isTutorial == false)
        {
            SetArtifactValues();
        }
    }
    public void SetArtifactValues()
    {
        RandomizeHauntedValue();
        if (corruptionCount <= 4)
        {
            RandomizeNormalSprite();
            lightIntensity = 0;
            thisArtifact.GetComponent<Light>().intensity = lightIntensity;
            artifactSound = defaultClip;
            audioSource.clip = artifactSound;
        }
        else if (corruptionCount == 5 || corruptionCount == 6 || corruptionCount == 7 || corruptionCount == 8 )
        {
            RandomizeHauntedSprite();
            isHaunted = true;
            artifactSound = defaultClip;
            audioSource.clip = artifactSound;
        }
        else if (corruptionCount == 9 || corruptionCount == 10)
        {
            RandomizeLightCue();
            RandomizeNormalSprite();
            isHaunted = true;
            artifactSound = defaultClip;
            audioSource.clip = artifactSound;
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
            moctopus.SetActive(true);
            artifactSound = moctopusSound;
            audioSource.clip = moctopusSound;
        }
        else if (corruptionCount == 15)
        {
            RandomizeNormalSprite();
            RandomizeLightCue();
            HasOctopus = true;
            moctopus.SetActive(true);
            isHaunted = true;
            artifactSound = moctopusSound;
            audioSource.clip = moctopusSound;
        }
        else if (corruptionCount == 16)
        {
            RandomizeHauntedSprite();
            HasOctopus = true;
            moctopus.SetActive(true);
            isHaunted = true;
            artifactSound = moctopusSound;
            audioSource.clip = moctopusSound;
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
            artifactSound = defaultClip;
            audioSource.clip = artifactSound;
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
        audioSource.clip = artifactSound;
        audioSource.Play();
    }
    public void RandomizeLightCue()
    {
        lightIntensity = randyTheRandom.Next(5, 150);
        lightMoment.intensity = lightIntensity;
    }
    public void RandomizePrice()
    {
        if (corruptionCount <= 4)
        {
            price = randyTheRandom.Next(minPriceNorm, maxPriceNorm);
        }
        else if (corruptionCount >= 5 && corruptionCount < 15)
        {
            price = randyTheRandom.Next(minPriceHaunt, maxPriceHaunt);
        }
        else if (corruptionCount > 15)
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

    [ContextMenu("PlaySound")]
    public void PlaySound()
    { 
        thisArtifact.GetComponent<AudioSource>().Play();
    }
    

    public void Inking()
    {
        FindObjectOfType<InkSplot>().TurnInkOn();
    }

    /*public void OnTriggerEnter(Collider other)
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
                particle.Play();
                isHaunted = false;
                Debug.Log(isHaunted);
            }
            if (isSound == true)
            {
                audioSource.Stop();
            }
        }
    }
     */
    void Update()
    {
        if (isSwitch == true)
        {
            SwitchSprite();
        }
        lightMoment.intensity = lightIntensity;
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        if (playerDistance <= targetDistance)
        {
            if (isTutorial == true)
            {
                reminderText.SetActive(false);
                possessText.SetActive(false);
                grabText.SetActive(false);
                infoText.SetActive(true);
            }
        }
        if (playerDistance > targetDistance)
        {
            if (isTutorial == true)
            {
                infoText.SetActive(false);
            }
        }
        if (playerDistance <= targetDistance && Input.GetKeyDown(grabArtifact))
        {
            playerInfo.artifactsGrabbed += 1;
            hasGrabbed = true;
            grab.Play();
            if (HasOctopus == true)
            {
                audioSource.Stop();
                jumpscare.SpawnScare();
            }
            if (isHaunted == true)
            {   
                if (playerInfo.artifactsGrabbed == amountToSpawnGhost)
                {
                    FindObjectOfType<EnemyManager>().SpawnGhost();
                    playerInfo.artifactsGrabbed = 0;
                }
                ghostes.isPossessing = true;
                PlayerInfo.possessedNumber += 1;
                playerInfo.possessionClip.volume += 0.1f;
                if (isTutorial == true)
                {
                    possessText.SetActive(true);
                    infoText.SetActive(false);
                }
                if (HasOctopus == false && hasGrabbed == true)
                {
                    Destroy(thisArtifact);
                }
            }
            else if (isHaunted == false)
            {
                if (playerInfo.artifactsGrabbed == amountToSpawnGhost + 2)
                {
                    FindObjectOfType<EnemyManager>().SpawnGhost();
                    playerInfo.artifactsGrabbed = 0;
                }
                SaveDataManager.Instance.daveSata.quotaMoney += price;
                SaveDataManager.Instance.daveSata.spendingMoney += price;
                if (isTutorial == false)
                {
                    FindObjectOfType<ArtifactController>().CreateArtifact();
                }
                if (isTutorial == true)
                {
                    grabText.SetActive(true);
                    infoText.SetActive(false);
                }
                if (HasOctopus == false && hasGrabbed == true)
                {
                    infoText.SetActive(false);
                    Destroy(thisArtifact);
                }
            }
        }
        if (hasGrabbed == true && HasOctopus == true)
        {
            if (jumpscare.jumpscare.jumpscareTime <= 0.15)
            {
                Destroy(jumpscare.spawnedJumpscare);
                player.GetComponent<PlayerMovement>().walkSpeed = jumpscare.storedSpeed;
                Inking();
                Destroy(thisArtifact);
            }
        }

        /*if (isSound == true)
        {
            PlaySound();
        }
       
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(isHaunted);
        }
        */
    }
}
