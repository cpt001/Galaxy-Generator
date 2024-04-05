using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGen : MonoBehaviour
{
    [Header("Distance from star")]
    //[Range()]   //mercury orbits at distance .387au (4878km), temp estimated 800f, no atmo -- venus orbit 0.7au, temp 880f, thick atmo, earth orbit 1.0au, temp -100 to 140f
    public float orbitalDistanceShort;  //Highest boiling temp is 5657 with tungsten. 
    public float orbitalDistanceLong;
    public float currentOrbitalDistance;
    public float temp;  //Temperature of world is orbitdist * x / sunTemp
    public float orbitalSpeed;
    [Header("World Stats")]
    [Range(0, 12)]
    public int worldSize;   //Determines possible thickness of atmosphere
    public float spinSpeed; //Day night speed
    public bool tectonicallyActive;
    public float tectonicFrequency; //How often tectonic events occur
    public bool hasAtmosphere;  //Can the planet support an atmo? -- determined by temperature, speed speed, and size -- radius 16, spin rate 6, rim speed 10 -> gravity .6g
    public enum AtmosphereType  //Thicker atmos can innoculate a temperature modifier
    {

    }
    public float atmoDepth;
    public float atmo2Depth;
    public float oceanDepth;
    public bool goldilocks;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
