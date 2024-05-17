using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    Camera camera;
    Rigidbody2D rb;
    public int myIndex;
    
    //delegate
    public delegate void DroppedObjectEventHandler();

    //the event itself
    public event DroppedObjectEventHandler DroppedObject;

    bool dropped = false;

    public void Update()
    {
        if(camera == null)
            camera = FindObjectOfType<Camera>();
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();

        if(Input.GetMouseButton(0) && !dropped)
            TrackMouse();

        if(Input.GetMouseButtonUp(0))
            DropCircle();
    }
    
    //get mouse down to have circles follow mouse
    void TrackMouse()
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //Debug.Log(gameObject.transform.position);
        // Define the bounds
        float minX = -2.3f;
        float maxX = 2.3f;

        // Clamp the mouse position within the bounds
        float clampedMouseX = Mathf.Clamp(mouseX, minX, maxX);

        // Set the circle's position
        gameObject.transform.position = new Vector2(clampedMouseX, gameObject.transform.position.y);
    }

    //get mouse up to drop circle
    public void DropCircle()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
            
        rb.constraints = RigidbodyConstraints2D.None;
        dropped = true;
        //call the event
        DroppedObject?.Invoke();

    }
}
