using UnityEngine;

public class Heart : MonoBehaviour
{
    // Create a new array of type GameObject to store spheres
    private GameObject[] spheres;

    // Arrays to hold the different start / end positions of each sphere to be able to lerp with them 
    private Vector3[] startPos;
    private Vector3[] endPos;

    // A public variable a programmer can change to determine how fast they want the heart to osciallate between the 0-1 point / heart to spread out and back 
    public float speed;

    // A variable to determine the value we pass into our sin function to help us get our t value for lerp
    private float sinVal;

    // A variable that does the calculations, and holds the t value for lerp between 0 and 1
    private float lerpTVal;

    // An public int to represent the count of how many spheres we wat to draw - programmer chooses how many spheres they want
    public int sphereCount;

    // A public variable the programmer can use to determine how large they want their shape to be - multiplies xyz coordinates to spread coordinates out, making shape bigger
    public float shapeSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Must initialize the arrays to a size as sphereCount determines
        spheres = new GameObject[sphereCount];
        startPos = new Vector3[sphereCount];
        endPos = new Vector3[sphereCount];


        // Upon starting, we want to initialize our GameObject Array - we can use a for loop to instantiate it - the loop will go as long as the sphereCount determines
        for (int i = 0; i < sphereCount; i++)
        {

            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Doing this assigns the new spheres position, which is of type Vector3, to a new vector3 - the sphere is initialized, and given a set of x,y,z coordinates
            // We are using functions to determine this heart, compared to the circle which was a formula - there is no radius in this case, and we need i to help determine where to plot each sphere
            // We gain access to each sphere through our sphere array, create its new position, and set that equal into a position array for each sphere
            // Multiplying by a number at the end pushes the xyz coordinates out, spacing the shape out/making it larger
            startPos[i] = spheres[i].transform.position = new Vector3(Mathf.Sqrt(2) * Mathf.Pow(Mathf.Sin(i), 3), Mathf.Pow(-Mathf.Cos(i), 3) - Mathf.Pow(Mathf.Cos(i), 2) + 2 * Mathf.Cos(i), 5f) * shapeSize;

            // To set the end position, we want to also create a new Vector3, to tell each sphere where in the xyz space it needs to reside
            // Each endPos will be randomly generated
            endPos[i] = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), Random.Range(-4f, 7f));



        }

    }

    // Update is called once per frame
    void Update()
    {
        // This calculation is what we can normally use to move an object - here we use it to determine a value we can pass into our sin function, such that it will smoothly
        // generate values between 0 and 1 to move the spheres 
        sinVal = sinVal + speed * Time.deltaTime;

        // Because the t value for lerp needs to be from 0 to 1, multiplying by 0.5 and then adding 0.5 ensures that if, on the sin function, we are at its lowest point
        // at -1, we can turn that number into a 0, so the t value will be accurate and between 0 and 1 - this function still will keep the max at 1, as 1 * .5 is .5, +.5 is 1
        lerpTVal = (Mathf.Sin(sinVal) * 0.5f) + 0.5f;

        // A for loop that goes over each sphere, and gives it the info to proceed with the lerp functionality - each sphere's position is updated with the lerp function
        // where a start and end point are given for the system to move the spheres between the two points over time
        for (int i = 0; i < sphereCount; i++)
        {
            spheres[i].transform.position = Vector3.Lerp(startPos[i], endPos[i], lerpTVal);

            // Each iteration of a sphere, randomly change where its end range will be the next time around
            // This makes it extremly sporadic, as while the shape is on its way to its end position through the update calls, it each call gets a new end position
            // FEEL FREE TO UNCOMMENT THIS LINE FOR A CRAZY EXPERIENCE!!!!! (* STROBE WARNING *)
            // endPos[i] = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), Random.Range (-4f, 7f));

            // Each call, we also want to get a sphere's renderer component to be able to change its color
            // Value gets updated each loop iteration to get the renderer component of a sphere
            // Normally with something like a rigidbody we get the component at start time as we need that property to take effect ASAP/frame 1 - here we 
            // can call renderer in update as we only need access to it as it is running - furthermore, renderers are built in components all primitives have by default
            Renderer sphereProperties = spheres[i].GetComponent<Renderer>();

            // Each time we go to change the color, we can have the color completly change and be flashy instead of graually fading in and out
            // I have decided to stick with bright, fun colors, therefore, I kept the hue range fully open, the value range to the top half, and the saturation at max
            float hue = Random.Range(0f, 1f);
            float value = Random.Range(0.5f, 1f);

            // Calculate the color for a sphere - use the above random values, and convert HSV to RGB
            Color color = Color.HSVToRGB(hue, 1f, value);

            // Get the sphereProperties variable (which has the renderer component of a sphere), and access the material color property of it, 
            // and set the color to the above calculated color
            sphereProperties.material.color = color;
        }
    }

}
