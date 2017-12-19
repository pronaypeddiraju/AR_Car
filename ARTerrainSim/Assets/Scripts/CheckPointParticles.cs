using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointParticles : MonoBehaviour {

    [SerializeField]
    private ParticleSystem bubbles;

	// Use this for initialization
	void Start () {
        bubbles.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ColliderBottom")
        {
            bubbles.Emit(10);
        }
    }
}
