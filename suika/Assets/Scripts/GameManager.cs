using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableCircles;
    [SerializeField] GameObject[] allCircles;

    public float MaxTimer = 1f;
    public float currentTimer = 0f;
    bool objectCreated = false;
    
    ObjectController spawnedObject;

    private void Start() {
        //alerted by mediator everytime a collision is seen
        CollisionMediator.instance.collision += UpgradeObject;
    }

    public void Update()
    {
        if(currentTimer > 0)
            currentTimer -= Time.deltaTime;
        else
        {
            if(!objectCreated)
                SpawnNewObject();
        }
    }

    void SpawnNewObject()
    {
        int objectIndex = Random.Range(0, spawnableCircles.Length);
        GameObject newObject = Instantiate(spawnableCircles[objectIndex], new Vector3(0, 3.5f, 0), Quaternion.identity);
        spawnedObject = newObject.GetComponent<ObjectController>();
        objectCreated = true;

        //subscribing to event, so when the circle is dropped, gamemanager is notified but objectcontroller
        //does not know about gamemanager
        spawnedObject.DroppedObject += StartTimer;
    }

    void UpgradeObject(GameObject object1, GameObject object2)
    {
        int objectIndex = object1.GetComponent<ObjectController>().myIndex;
        if(objectIndex == allCircles.Length - 1)
            return;
        
        Debug.Log(objectIndex);

        //get median position
        float newX = (object1.transform.position.x + object2.transform.position.x) / 2;
        float newY = (object1.transform.position.y + object2.transform.position.y) / 2;
        //destroy both
        Destroy(object1);
        Destroy(object2);
        //spawn new object at the median position
        GameObject newObject = Instantiate(allCircles[objectIndex+1], new Vector3(newX, newY, 0), Quaternion.identity);
        spawnedObject = newObject.GetComponent<ObjectController>();
        spawnedObject.DropCircle();
    }   

    void StartTimer()
    {
        currentTimer = MaxTimer;
        objectCreated = false;
    }
}
