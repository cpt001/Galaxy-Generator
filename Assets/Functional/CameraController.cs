using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //Virtual camera; zoom only
    [SerializeField] private CinemachineVirtualCamera vCam;
    //[SerializeField] private CinemachineFreeLook fCam;
    //[SerializeField] private CinemachineFreeLook.Orbit[] originalOrbits;
    CinemachineComponentBase componentBase;
    private float cameraDistance;
    [SerializeField] private float sensitivity = 10;
    public SystemBehaviour currentFocus;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.StartListening("NewSystemFocus", SetNewFocus);

        /*for (int i = 0; i < fCam.m_Orbits.Length; i++)
        {
            originalOrbits[i].m_Height = fCam.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = fCam.m_Orbits[i].m_Radius;
        }*/
    }

    // Update is called once per frame
    private void Update()
    {
        if (componentBase == null)
        {
            componentBase = vCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }

        //Scroll zoom in and out
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Debug.Log("Mouse scroll detected");
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= cameraDistance;
            }
        }



        /*if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;

            for (int i = 0; i < fCam.m_Orbits.Length; i++)
            {
                fCam.m_Orbits[i].m_Height = originalOrbits[i].m_Height * cameraDistance;
                fCam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * cameraDistance;
            }
        }*/
    }

    void SetNewFocus()
    {
        if (currentFocus)
        {
            vCam.LookAt = currentFocus.transform;
            vCam.Follow = currentFocus.transform;
            //fCam.Follow = currentFocus.transform;
            //fCam.LookAt = currentFocus.transform;
        }
    }

    public void RecenterFocusOnGalaxy()
    {
        currentFocus = null;

        vCam.LookAt = GameObject.Find("CenterOfGalaxy").transform;
        vCam.Follow = GameObject.Find("CenterOfGalaxy").transform;
    }

}
