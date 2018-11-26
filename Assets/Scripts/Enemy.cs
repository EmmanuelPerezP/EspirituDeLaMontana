using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Player player;
    public bool watching = false;
    public AudioSource WatchingSound;
    public AudioSource NotWatchingSound;
    public AudioSource EnemyNoticing;
    public AudioSource Howl;
    public AudioSource deathSound;


    Animator animator;
    private IEnumerator coroutine;


    void Start()
    {
        animator = GetComponent<Animator>();
        coroutine = CallTriggerWatch();
        StartCoroutine(coroutine);

    }

    IEnumerator CallTriggerWatch()
    {
        while (true)
        {
            // we check if the enemy is not watching already (like from a branch trigger)
            if (!watching)
            {
                // this routine is activated every 12-23 seconds
                yield return new WaitForSeconds(Random.Range(4, 10));
                animator.SetTrigger("ToAlert");
                EnemyNoticing.Play();
                yield return new WaitForSeconds(3f);
                ChangeWatchState();
                yield return new WaitForSeconds(Random.Range(5, 10));
                animator.SetTrigger("FromAlert");
                ChangeWatchState();
            }
        }
    }

    public IEnumerator TriggerWatch()
    {
        // if the monster is not watching it means the trap activated and its going to watch
        animator.SetTrigger("ToAlert");
        EnemyNoticing.Play();
        yield return new WaitForSeconds(3f);
        ChangeWatchState();
        yield return new WaitForSeconds(Random.Range(5, 10));
        animator.SetTrigger("FromAlert");
        ChangeWatchState();
    }

    // this method flips the watched state and handles the sound
    void ChangeWatchState()
    {
        watching = !watching;
        if (watching)
        {
            Howl.Play();
        }

    }

    // Update is called once per frame
    void Update () {
        if (watching && !player.hidden)
        {
            player.Dead();
            // deathSound.Play();
        }
		
	}
}
