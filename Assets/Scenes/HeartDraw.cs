using UnityEngine;

public class HeartDraw : MonoBehaviour
{

// Create a new array of type GameObject to store spheres
    private GameObject [] spheres;

    // An array to hold the different start / end positions of each sphere to be able to lerp with them 
    private Vector3 [] startPos;
    private Vector3 [] endPos; 

    float lerpFraction;
    float time;

// An int to represent the count of how many spheres we wat to draw
    private int sphereCount = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Must initialize the arrays to a size as sphereCount determines
        spheres = new GameObject[sphereCount];
        startPos = new Vector3[sphereCount];
        endPos = new Vector3[sphereCount];


        // Upon starting, we want to initialize our GameObject Array - we can use a for loop to instantiate it - the loop will go as long as the sphereCount determines
        for (int i = 0; i < sphereCount; i++){

            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
            // Doing this assigns the new spheres position, which is of type Vector3, to a new vector3 - the sphere is initialized, and given a set of x,y,z coordinates
            // We are using functions to determine this heart, compared to the circle which was a formula - there is no radius in this case, and we need i to help determine where to plot each sphere
            // We gain access to each sphere through our sphere array, create its new position, and set that equal into a position array for each sphere
           startPos[i] = spheres[i].transform.position = new Vector3(Mathf.Sqrt(2)*Mathf.Pow(Mathf.Sin(i), 3), Mathf.Pow(-Mathf.Cos(i), 3) - Mathf.Pow(Mathf.Cos(i), 2) + 2 * Mathf.Cos(i), 5f);
           
           // To set the end position, we want to also create a new Vector3, to tell each sphere where in the xyz space it needs to reside
           // Each endPos will be randomly generated
           endPos[i] = new Vector3(5 , 10 , 15 );
           

            
        }

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // A for loop that goes over each sphere, and gives it the info to proceed with the lerp functionality - each sphere's position is updated with the lerp function
        // where  astart and end point are given for the system to move the spheres between the two points over time
        for (int i = 0 ; i < sphereCount; i++){
             lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
            spheres[i].transform.position = Vector3.Lerp(startPos[i], endPos[i], lerpFraction);
            
            // Each iteration of a sphere, randomly change where its end range will be the next time around
            endPos[i] = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), Random.Range (-4f, 7f));

            // Each call, we also want to get a sphere's renderer component to be able to change its color
            // Value gets updated each loop iteration to get the renderer component of a sphere
            // Normally with something like a rigidbody we get the component at start time as we need that property to take effect ASAP/frame 1 - here we 
            // can call renderer in update as we only need access to it as it is running - furthermore, renderers are built in components all primitives have by default
            Renderer sphereProperties = spheres[i].GetComponent<Renderer>();
            
            // Each time we go to change the color, we can have the color completly change instead of graually fading in and out
            float hue = Random.Range(0f, 1f);
            float saturation = Random.Range(0.5f, 1f);
            float value = Random.Range(0.75f, 1f);
            
            // Calculate the color for a sphere - use the above random values, and convert HSV to RGB
            Color color = Color.HSVToRGB(hue, saturation, value);

// Get the sphereProperties variable, and access the material color property of it, and set the color to the above calculated color
            sphereProperties.material.color = color; 
        }
    }
    
}
 