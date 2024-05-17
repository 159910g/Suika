using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionMediator : MonoBehaviour
{
    public static CollisionMediator instance { get; set; }

    private void Awake()
    {
        instance = this;
    }

    //keeping the data stored efficiently
    //using tuples allows for storing pairs of gameobjects
    private HashSet<Tuple<GameObject, GameObject>> collidedObjects = new HashSet<Tuple<GameObject, GameObject>>();

    //delegate
    public delegate void CollisionEventHandler(GameObject object1, GameObject object2);

    //the event itself
    public event CollisionEventHandler collision;

    public void HandleCollision(GameObject object1, GameObject object2) 
    {
        //check if collision has already been handled
        var collisionPair = new Tuple<GameObject, GameObject>(object1, object2);
        var CollisionPairFlipped = new Tuple<GameObject, GameObject>(object2, object1);
        if (!collidedObjects.Contains(collisionPair) && !collidedObjects.Contains(CollisionPairFlipped))
        {
            // Trigger the event
            collision?.Invoke(object1, object2);

            // Add the collision pair to the set
            collidedObjects.Add(collisionPair);
        }
        //collidedObjects.Clear();
    }
}
