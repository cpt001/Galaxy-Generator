using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStateMachine : MonoBehaviour
{
    public enum Stance
    {
        WeaponsFree,    //Will fire on any valid target in range
        SelectiveFire,  //Fires on target if out of cover
        HoldFire,       //Will not fire unless fired upon
        Hunker,         //Will not fire, seeks nearest cover
        StandGuard,     //Will return fire, will not seek cover
        Suppress,       //Will fire regardless of cover or range
    };
    public Stance currentStance;

    public enum CoverStatus
    {
        HighCover,  //High cover reduces any targeters by 50% hit chance
        LowCover,   //Low cover reduces any targeters by 30%
        NoCover,    //No reduction, also triggers cover seek
    }
    public CoverStatus coverStatus; //Cover type can be determined by an empty gameobject at a certain height.
    public GameObject nearestCover;
    public bool unitSelected;
    public float moveSpeed;
    public bool firing;

    public Transform destination;
    public Transform nextNode;


    public int health;      //Main HP
    public int armor;       //Extra health on unit
    public float will;      //Status of trooper, how close they are to breaking under stress
    public float aim;       //Chance to hit target
    public int maxRange;    //How far itll go bang
    public float stamina;   //Tactical option availability

    public GameObject target;
    public int numObjectsObscuring; //Simply counts the number of objects between


    public enum ArmorType
    {
        None,
        Light, 
        Medium,
        Heavy,
    }

    public enum SelectedWeapon
    {
        None,
        Pistol,
        SMG,
        AR,
        Sniper,
        Shotgun
    }

    //hit% = stamina * (aim - (target.covertype + rangetotargetovermax)

    private void Update()
    {
        if (currentStance == Stance.WeaponsFree && Vector3.Distance(transform.position, target.transform.position) <= maxRange)
        {
            StartCoroutine(FiringOnTarget());
        }
        if (currentStance == Stance.SelectiveFire && Vector3.Distance(transform.position, target.transform.position) <= maxRange)
        {
            if(target.GetComponent<BotStateMachine>().coverStatus == CoverStatus.NoCover)
            {
                StartCoroutine(FiringOnTarget());
            }
        }
        if (currentStance == Stance.HoldFire)
        {
            if (target.GetComponent<BotStateMachine>().firing)
            {
                StartCoroutine(FiringOnTarget());
            }
        }
        if (currentStance == Stance.Hunker && coverStatus == CoverStatus.NoCover)
        {
            StartCoroutine(FindNearestCover());
            StartCoroutine(FiringOnTarget());
        }
        if (currentStance == Stance.StandGuard)
        {
            if (target.GetComponent<BotStateMachine>().firing)
            {
                StartCoroutine(FiringOnTarget());
            }
        }
        if (currentStance == Stance.Suppress)
        {
            StartCoroutine(FiringOnTarget());
        }
    }

    IEnumerator FiringOnTarget()
    {
        firing = true;
        yield return null;
    }
    IEnumerator FindNearestCover()
    {
        if (transform == destination)
        {

        }
        yield return null;
    }
}
