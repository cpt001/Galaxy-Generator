using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SystemBehaviour : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for all the data on an individual star gameobject
    /// 
    /// 
    /// 
    /// Random Generate everything first
    /// Determine Player Location
    /// Look for nearest 5 linked systems
    /// Reduce range of possible encounters
    /// </summary>
    /// 

    public bool isPlayerLocation;

    public enum Resource
    {
        None,
        Iron,
        Copper,
        Gold,
        Quartz,
        PlantFiber,
        Zinc,
        GassyRock,
        Silver,
        Resin,

    }
    public Resource resourceAvailable = Resource.None;
    public enum StarType
    {
        Null,
        ClassO, //UV Star -- very rare -- massive stars -- very hot -- may damage ships in system, even with shields and through armor
        ClassB, //Blue Luminous -- silicon mentioned -- unusual properties of faster rotations around, maybe introduces issues with ship control?
        ClassA, //White/BlueWhite -- ionized metals -- ~1 in 160
        ClassG, //Yellow -- neutral metals -- ~1 in 13 -- our sun is classed a g star
        ClassK, //Orange -- best chance of life -- neutral metals
        ClassM, //Red Dwarf - 76% of main sequence stars are m -- lower neutral metals, oxide visible
        ClassL, //Brown Dwarf - dark red in color -- low gravity of star -- alkali metals prominent
        ClassC, //Carbon star - nearly dead start -- high amounts of carbon from burned material present in atmosphere -- usually giants or super giants
        Neutron,//Dead star - small and cold -- larger it is, more neutrino flux carried
        BlackHole, //Dead star - an explosion so massive that a hole in space has replaced it -- event horizon is lethal
        Pulsar, //Rotating star, super magnetized -- lethal to ships caught in magnetization
        Quasar, //Supermassive black hole, with large accretion disk

        Nebula,
        Nova,   //14
    }
    public StarType typeOfStar;
    [Range(0.05f, 25)]   //% per tick of freighter being filled
    public float resourceQuality;
    [Range(0.05f, 2.0f)]
    public float starSize;
    public GameObject starVisualObject;

    [Range(0, 15)]  //% of ire raised toward player
    public float enemyIre;
    [Range(0, 150)] //Number of ships generated
    public int enemyPresence;


    //public float timeSinceActivityReport;   //Resets to 0 when player has present units in system
    public bool playerPresent;

    public int numSystemsConnected; //How many systems this one is linked to.
    private Transform StarfieldContainer;   //Hierarchy organization

    private SystemFinder sysFinder;
    public Color starColor;

    [Range(0, 15)]
    public int numOrbitingBodies;

    private Scene sceneOnNode;  //A scene can NEVER be null. This was causing a bug with non-generation
    private string sceneName;

    private CameraController cameraController;
    private TextMeshProUGUI GUIInfo;

    private void Awake()
    {
        sysFinder = GetComponent<SystemFinder>();
        typeOfStar = (StarType)Random.Range(1, 14);
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        GUIInfo = GameObject.Find("SystemInformation").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        numOrbitingBodies = Random.Range(0, 15);

        SystemSetup();
        FindSystems();
        starVisualObject = transform.GetChild(0).gameObject;
        starVisualObject.GetComponent<MeshRenderer>().material.color = starColor;
        name = typeOfStar.ToString() + " @" + transform.position;
        //Debug.Log("Scene on Start 2: " + sceneOnNode);
    }

    private void FindSystems()
    {
        sysFinder.FindSystems();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("StarObject"))
        {
            Destroy(collision.gameObject);
        }
    }

    void SystemSetup()
    {
        switch (typeOfStar)
        {
            case StarType.Null:
                {
                    resourceAvailable = 0;
                    resourceQuality = 0;
                    enemyIre = 0;
                    enemyPresence = 0;
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(0f, 255f, 10f);   //Debug green
                    Debug.Log("Star @" + transform.name + " is null");
                    break;
                }
            case StarType.ClassO:   //Super massive, super hot, temp - 25000 - 50000k (89540f)
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(12.55f, 8.63f, 35.69f);   //UV
                    break;
                }
            case StarType.ClassB:   //Fast rotation, silicon?, temp - 10000k to 25000k (44540f)
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(0.0f, 87.5f, 100.0f);   //Blue Luminous
                    break;
                }
            case StarType.ClassA:   //ionized metals, 1/160 chance, temp - 7400k to 10000k
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(80.0f, 97.3f, 100.0f);   //White/Bluewhite
                    break;
                }
            case StarType.ClassG:   //neutral metals, 1/13, SOL, temp - 5000 to 6000k, hab zone - 0.9 to 1.2 AU
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = Color.yellow;   //Yellow
                    break;
                }
            case StarType.ClassK:   //Neutral metals, Best chance of life, temp - 3500 to 5000k, hab zone - 0.7 to 1.0 AU
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(100.0f, 76.1f, 7.8f);   //Orange
                    break;
                }
            case StarType.ClassM:   //oxide, lower neutral metals, 76% chance that star is class M, temp - 3000k, hab zone - 0.3 au to 0.6 au
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = Color.red;   //Red
                    break;
                }
            case StarType.ClassL:   //Low gravity, alkali metals prominent, temp - 1500 to 2500k, hab zone - .007 to .044 AU (1050km - 6700)
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(69.8f, 22.7f, 0.0f);   //Brown
                    break;
                }
            case StarType.ClassC:   //High carbon atmosphere, high alkaline metals, temp 31.15 kelvin to 3000k
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    starColor = new Color(34.1f, 11.0f, 0.0f);   //Carbon (Dark Brown)
                    break;
                }
            case StarType.Neutron:
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = 0;
                    starColor = Color.grey;   //Dark blue
                    break;
                }
            case StarType.BlackHole:
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = 0;
                    starColor = Color.black;
                    break;
                }
            case StarType.Quasar:
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = 0;
                    starColor = Color.grey;   //Red Orange
                    break;
                }
            case StarType.Pulsar:
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = Random.Range(0, 15);
                    starColor = Color.grey;   //Purple
                    break;
                }
            case StarType.Nebula:
                {
                    //Nebulae need to be a strange exception. They technically have no interactions, and instead act as stellar masses. They add resources to affected systems
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = 0;
                    starColor = Color.grey;   //Pink
                    break;
                }
            case StarType.Nova:
                {
                    resourceAvailable = (Resource)Random.Range(0, 9);
                    resourceQuality = Random.Range(0.05f, 25.0f);
                    enemyIre = Random.Range(0.0f, 15.0f);
                    enemyPresence = Random.Range(0, 150);
                    numSystemsConnected = Random.Range(1, 6);
                    numOrbitingBodies = 0;
                    starColor = Color.grey;   //RGB
                    break;
                }
        }
    }

    /// <summary>
    /// Maybe this generation on click is a bad way to go.
    /// It may be worthwhile exploring a set of prefab scenes that can be modified and saved as new scenes instead.
    /// 
    /// Another potential fix is the need for players to 'scout' a system, forcing them to perform a generation before allowing entry to the systen? Testing would be required to implement.
    /// 
    /// Running this through the editor scenemanagement seems to be acting against what im trying to accomplish?
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("System Clicked: " + gameObject.name);    //Working
        cameraController.currentFocus = this;
        EventsManager.TriggerEvent("NewSystemFocus");
        if (sceneOnNode.IsValid() == false)   //Changed from sON == null, to current. Scene can never be null in Unity.
        {
            Debug.Log("Star Generating");
            sceneOnNode = SceneManager.CreateScene("StarID: " + typeOfStar + transform.position);   //This seems to be ignoring the string information
            sceneName = sceneOnNode.name;
            Debug.Log("Scene on Node: " + sceneOnNode);
            StartCoroutine(LoadScene());
        }
        else
        {
            StartCoroutine(LoadScene());
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("System found: " + gameObject.name);
        transform.localScale = new Vector3(2, 2, 2);
        GUIInfo.text = "Star: " + typeOfStar + " | Orbiting Bodies: " + numOrbitingBodies + " | Resource: " + resourceAvailable;
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public IEnumerator LoadScene()  //THE NAME IS NULL. this has been causing the load issue the whole fucking time; im swapping from sceneOnNode to a new sceneName
    {
        Debug.Log("Loading Scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }
}
