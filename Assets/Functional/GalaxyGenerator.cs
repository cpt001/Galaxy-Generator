using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyGenerator : MonoBehaviour
{
    /// <summary>
    /// This class controls the generation of each node throughout the galaxy. 
    /// It iterates partway through a circle, generating a node at each point designated.
    /// </summary>
    //public int numPoints; //Number of points to generate
    public float numArmsGenerated;
    private float dst;
    private float angle;
    private float distanceBetweenNodes = 0.001f;
    [Tooltip("Remember to make this private when all variables have been established")]
    public float turnFraction;  //Increase to increase spiral, decrease to create more ends
    [Range(1, 10)]  //1 is a spiral galaxy
    private GenerationController genControl;

    public bool canGenerate = true;

    public GameObject localSystemGenerator;
    public Transform centralTransform;

    private void Awake()
    {
        genControl = gameObject.GetComponent<GenerationController>();
        centralTransform = gameObject.transform;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (canGenerate)
        {   //if cangen needs to be changed to generate button. 
            //it needs to check num arms vs desired num arms & num nodes vs desired num nodes.
            //after, it does trigger gen
            ChangeArms();
            TriggerGeneration();
        }
    }


    public void ChangeArms()    //Something something irrational numbers        -- process found - need to find the nearest fraction (ie - 1/4 = .25), then make it less precise, such as .245
    {
        if (genControl.NumberArms == 1f)
        {
            turnFraction = 0.99f;   //.979
        }
        if (genControl.NumberArms == 2f)
        {
            //turnFraction = Random.Range(0.50401f, 0.50999f);    //maybe allow player control?
            turnFraction = 0.509f;
        }
        if (genControl.NumberArms == 3f)
        {
            turnFraction = 0.3300575f; //correct, still loose
        }
        if (genControl.NumberArms == 4f)
        {
            turnFraction = 0.2450275f;   //Correct (04/04/2021) - Numbers were getting too precise. generating at .2500001 was generating at 1/4 with a tiny bit of movement. generating at .245 gives more space between node gens
        }
        if (genControl.NumberArms == 5f)
        {
            turnFraction = .195001f;    //correct
        }
        if (genControl.NumberArms == 6f)
        {
            turnFraction = 0.1700575f;  //correct
        }
        if (genControl.NumberArms == 7f)
        {
            turnFraction = 0.14001f;   //correct
        }
        if (genControl.NumberArms == 8f)
        {
            turnFraction = 0.1230575f;   //correct
        }
        if (genControl.NumberArms == 9f)
        {
            turnFraction = 0.113001f;    //correct
        }
        if (genControl.NumberArms == 10f)
        {
            turnFraction = .0990001f; //t
        }
        if (genControl.NumberArms == 11f)
        {
            turnFraction = 0.09000546f; //correct
        }

        numArmsGenerated = genControl.NumberArms;
    }

    void TriggerGeneration()
    {
        //How this works: Nodes are generated based on a certain distance around a circle. 
        //.99 turn fraction will move .99ths around the circle before generating another node.
        for (int i = 0; i < genControl.numNodes; i++) //Num you want
        {
            float dst = i + distanceBetweenNodes / (genControl.numNodes - 1f);   //Distance goes 0 -> 1 over course of loop
            float angle = 2f * Mathf.PI * turnFraction * i;  //Turns a fraction of circle based on distance

            float x = dst * Mathf.Cos(angle);
            float z = dst * Mathf.Sin(angle);

            Instantiate(localSystemGenerator, new Vector3(x, 0, z), Quaternion.identity, centralTransform);
            if (i == genControl.numNodes - 1)
            {
                canGenerate = false;
            }
        }
    }
}
