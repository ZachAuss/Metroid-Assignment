using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("POWERUP GET!");
            collision.GetComponent<PlayerMovement>().SpeedChange();
            Destroy(this.gameObject);
        }
    }
}
