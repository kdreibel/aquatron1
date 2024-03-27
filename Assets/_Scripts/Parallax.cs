using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
  See https://www.youtube.com/watch?v=zit45k6CUMk for explanation
  Use the script as follows:
  * Add an image to be used as the background, as a child of the camera.
  * Add the script to the image.
  * Add two duplicate children to the image, move one to the right and one to the left.

*/

public class Parallax : MonoBehaviour
{
    private float length, startpos;

    [Tooltip("The main camera (should be the parent)")]
    public GameObject cam;

    [Range(0, 1)]
    [Tooltip("0 moves at full speed, 1 means a fixed background.")]
    public float parallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Determine if we need to wrap
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        
        float dist = (cam.transform.position.x * parallaxEffect);
        // NOTE: The youtube example was an x parallax only, whereas my usage
        // needs an x y parallax.  Thus, include height.
        float height = cam.transform.position.y * parallaxEffect;
        
        // vary position based on the camera
        transform.position = new Vector3(startpos + dist, height, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
        
    }
}
