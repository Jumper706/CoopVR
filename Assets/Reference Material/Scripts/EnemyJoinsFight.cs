using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJoinsFight : MonoBehaviour
{

    private GameObject target;
    private bool moving;
    public float moveSpeed;
    public float moveTime;
    private GameObject weapon;
    public AudioClip spawnNoise;

    // Use this for initialization
    void Start ()
    {
        moving = true;
        transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.x = 0.0f;
        rotationVector.z = 0.0f;
        transform.rotation = Quaternion.Euler(rotationVector);
        StartCoroutine(approach());
        AudioSource.PlayClipAtPoint(spawnNoise, transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (moving)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private IEnumerator approach()
    {
        yield return new WaitForSeconds(moveTime);
        moving = false;
        //start shooting
        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon")
                child.gameObject.GetComponent<Shoot>().reloading = false;
        }
    }
}
