using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotSmall : MonoBehaviour {

    //public void OnTriggerEnter2D(Collider2D other)
    //{

    //}

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.crouched == false)
            {
                player.hidden = false;
            }
            else if (player.crouched == true)
            {
                player.hidden = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.gameObject.GetComponent<Player>();
            player.hidden = false;

        }
    }
}
