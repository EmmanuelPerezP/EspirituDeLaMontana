using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

    public Enemy enemy;
    public AudioSource breakbranch;

    // Use this for initialization
    void Start () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            breakbranch.Play();
            enemy.TriggerWatch();
            IEnumerator coroutine;
            coroutine = enemy.TriggerWatch();
            StartCoroutine(coroutine);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
