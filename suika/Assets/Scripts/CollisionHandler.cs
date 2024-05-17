using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (gameObject.tag == other.gameObject.tag)
        {
            CollisionMediator.instance.HandleCollision(gameObject, other.gameObject);
        }
    }
}
