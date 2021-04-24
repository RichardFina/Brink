using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    //offset to determine if paralex scrolling should happen
    //two bools to check if the camera has background elements of to the side
    public int offsetX = 2;
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    //used if object is not tileable
    public bool reverseScale = false;
    //the width of our element
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform mytransform;

    private void Awake()
    {
        cam = Camera.main;
        mytransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRender = GetComponent<SpriteRenderer>();
        spriteWidth = sRender.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //does the camera need buddies
        if(hasALeftBuddy == false || hasARightBuddy == false)
        {
            //calculate the distance from center of camera to edge of screen (World coordinates)
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where camera can see edge of sprite
            float edgeVisibleRight = (mytransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisibleLeft = (mytransform.position.x - spriteWidth / 2) + camHorizontalExtent;
            
            //check if we can see the edge of the element and then calling MakeNewBuddy 
            if(cam.transform.position.x >= edgeVisibleRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if(cam.transform.position.x <= edgeVisibleLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    //Function that creates a buddy on the side required
    void MakeNewBuddy(int rightOrleft)
    {
        //calculate new position for our new buddy
        Vector3 newPos = new Vector3(mytransform.position.x + (spriteWidth*2) * rightOrleft, mytransform.position.y, mytransform.position.z);
        Transform newBuddy = Instantiate(mytransform, newPos, mytransform.rotation) as Transform;
        
        //if not tileable, reverse the x size of our object to hide seams
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = mytransform.parent;
        if(rightOrleft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
