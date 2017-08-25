using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {

    public AudioSource boom;
    public Transform playerPosition;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = playerPosition.position;
        //transform.localEulerAngles = new Vector3(transform.rotation.x, playerPosition.rotation.y, transform.rotation.z);
        //Debug.Log(playerPosition.eulerAngles.y);
        transform.rotation = Quaternion.Euler(transform.rotation.x, playerPosition.eulerAngles.y, transform.rotation.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            Debug.Log("Dead!");

            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (var i = 0; i < enemies.Length; i++)
                Destroy(enemies[i]);

            SpawnEnemies controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnEnemies>();
            controller.gameStarted = false;
            controller.spawnMore = false;
            controller.spawnDelay = 4.5f;
            boom.Play();
        }
    }
}
