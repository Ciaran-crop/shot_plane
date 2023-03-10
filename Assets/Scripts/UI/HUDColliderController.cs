using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDColliderController : MonoBehaviour
{
    PlayerController playerController;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController == null) playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ChangeUIShow(false);
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        playerController.SetUIBefore();
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerController == null) playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ChangeUIShow(true);
        }
    }
}
