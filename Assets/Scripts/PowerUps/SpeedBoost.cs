using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{

        AudioSource pickupAudio;
        BoxCollider2D trigger;
        public AudioClip collisionClip;

    private void Start()
    {
        pickupAudio = GetComponent<AudioSource>();
        trigger = GetComponent<BoxCollider2D>();

        if (pickupAudio)
        {
            pickupAudio.clip = collisionClip;
            pickupAudio.loop = false;
        }
    }

    private void Update()
   {
    if (!pickupAudio.isPlaying && !trigger.enabled)
    {
        Destroy(gameObject);
    }
   }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("POWERUP GET!");
            collision.GetComponent<PlayerMovement>().SpeedChange();
            pickupAudio.Play();
            trigger.enabled = false;
        }
    }
}
