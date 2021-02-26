using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   [RequireComponent(typeof(Rigidbody2D))]
    public class Collectible : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("Collectible");
                Destroy(this.gameObject);
            }
        }
    }

