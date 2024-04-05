using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class GalacticMapGenerator : MonoBehaviour
{
    public ParticleSystem starfieldGenerator;
    public bool ParticleFieldGenerated;
    public bool MapGenerated;
    public GameObject starObject;

    private bool initialGeneration = true;
    public bool triggerGeneration;
    public bool acceptGeneration;

    private void Start()
    {
        if (starfieldGenerator == null)
        {
            starfieldGenerator.GetComponent<ParticleSystem>();
        }
        if (starObject == null)
        {
            Debug.LogError("Missing star object prefab");
        }
    }

    private void Update()
    {
        if (triggerGeneration)  //Generates a new particle field for the player
        {            
            TriggerGeneration();
        }

        if (Input.GetKeyDown(KeyCode.Space))   //Starts generating a static starfield
        {
            StartCoroutine(GenerateStars());
        }
    }

    public void ButtonControlGenerateStars()
    {
        StartCoroutine(GenerateStars());
    }

    void TriggerGeneration()
    {
        if (initialGeneration)
        {
            if (!starfieldGenerator.gameObject.activeInHierarchy)   //Check if active
            {
                starfieldGenerator.gameObject.SetActive(true);
            }
            if (!starfieldGenerator.isPlaying)  //Checks if its already running
            {
                starfieldGenerator.Play();
                Debug.Log("Starfield Generating");
            }
            if (starfieldGenerator.particleCount == starfieldGenerator.main.maxParticles)
            {
                starfieldGenerator.Pause();
                triggerGeneration = false;
            }
            initialGeneration = false;
        }
        else
        {
            starfieldGenerator.Clear();
            if (starfieldGenerator.particleCount == 0)
            {
                starfieldGenerator.Play();
                Debug.Log("Starfield Regenerating");
            }
            if (starfieldGenerator.particleCount == starfieldGenerator.main.maxParticles)
            {
                starfieldGenerator.Pause();
                triggerGeneration = false;
            }

        }



    }

    public IEnumerator GenerateStars()
    {
        //Debug.Log("Generation triggered");
        ParticleSystem.Particle[] starCoordinates = new ParticleSystem.Particle[starfieldGenerator.particleCount];  //This should create a list of the particles, based on the current number active
        //Debug.Log("Star Generator List: " + starfieldGenerator.particleCount + " Star Coordinates: " + starCoordinates.Length);
        starfieldGenerator.GetParticles(starCoordinates);   //Gets the particles in the system
        foreach (ParticleSystem.Particle particleBastard in starCoordinates)    //4e particle in above system
        {
            Instantiate(starObject, particleBastard.position, transform.rotation, transform.parent) ;  //Issue exists with rotation
            //SystemBehaviour colorInheritance = starObject.gameObject.GetComponent<SystemBehaviour>();
            //colorInheritance.starColor = particleBastard.startColor;
        }
        MapGenerated = true;
        //acceptGeneration = false;

        yield return null;
    }

    /*private int playerAssignedLocation;
    private GalacticMapBehavior targetScript;
    IEnumerator AssignPlayerLocation()
    {
        playerAssignedLocation = Random.Range(1, starCoordinates.Length);
        //targetScript = starCoordinates[playerAssignedLocation].;
        if (targetScript != null)
        {
            targetScript.isPlayerLocation = true;
        }
        yield return null;
    }

    IEnumerator ReduceDifficultyNearPlayer()
    {
        yield return null;
    }*/
}
