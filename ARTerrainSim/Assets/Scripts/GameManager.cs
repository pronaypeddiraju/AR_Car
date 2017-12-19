using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private bool trackingView = true;

    public ParticleSystem bubbles;
    public MyTrackableEventHandler m_EventHandler;

    public GameObject wayPointCar;
    public Collider carBaseCollider;

    [SerializeField]
    private Camera ARCam;
    [SerializeField]
    private Camera Cam;

	// Use this for initialization
	void Start () {
        Cam.depth = 1;
        Instance = this;
        bubbles.Pause();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //Called using trigger for applying brake before obstacles
    public void CarBrakeApply()
    {
        wayPointCar.GetComponent<CarAIControl>().collided = true;
    }

    //Called using trigger to apply accelaration after crossing obstacles
    public void CarAccApply()
    {
        wayPointCar.GetComponent<CarAIControl>().collided = false;
    }

    //Changing the camera view 
    public void ChangeCams()
    {
        if(trackingView)
        {
            Cam.depth = 3;
            ARCam.cullingMask = (1 << LayerMask.NameToLayer("ImageTarget"));
        }
        else
        {
            Cam.depth = 1;
            ARCam.cullingMask = (1 << LayerMask.NameToLayer("ImageTarget") | 1 << LayerMask.NameToLayer("Default"));
        }
        trackingView = !trackingView;

    }


    
}
