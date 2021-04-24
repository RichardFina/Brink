using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    //Use array to store elements to paralex
    public Transform[] backgroundElements;
    //use array to store paralex scales for distance of element
    private float[] parallexScale;
    //amount of smoothing as objects move. Set abouve >0 
    public float smoothing = 1f;

    //reference to the main camera's transform
    private Transform cam;
    //store position of camera in previous frame
    private Vector3 previousCamPos;

    //is called before Start()
    private void Awake()
    {
        //set cam reference
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //store the previous frame 
        previousCamPos = cam.position;

        parallexScale = new float[backgroundElements.Length];
        //loop through each background and assign to corresponding parallexScale
        for (int i = 0; i < backgroundElements.Length; i++)
        {
            parallexScale[i] = backgroundElements[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for each background element
        for(int i = 0; i < backgroundElements.Length; i++)
        {
            //the parallex is the opposite of the camera movement from the previous frame multiplied by scale
            float parallax = (previousCamPos.x - cam.position.x) * parallexScale[i];

            //set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgroundElements[i].position.x - parallax;

            //create a target position which is the background current pos with it's target position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundElements[i].position.y, backgroundElements[i].position.z);

            //fade between current position and the target position using lerp
            backgroundElements[i].position = Vector3.Lerp(backgroundElements[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
