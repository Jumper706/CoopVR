using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireDelay;
    public bool reloading;
    public GameObject arrow;
    public AudioClip twang;

	// Use this for initialization
	void Start ()
    {
        reloading = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!reloading)
            StartCoroutine(fire());
	}

    private IEnumerator fire()
    {
        AudioSource.PlayClipAtPoint(twang, transform.position);
        Instantiate(arrow, transform.position, transform.rotation);
        reloading = true;
        yield return new WaitForSeconds(fireDelay);
        reloading = false;
    }
}
