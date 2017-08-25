using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathBarrior : MonoBehaviour {

    public GameObject player;
    private Vector3 resetPosition;

    // Use this for initialization
    void Start () {
        resetPosition = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.transform.position = resetPosition;
    }
}
