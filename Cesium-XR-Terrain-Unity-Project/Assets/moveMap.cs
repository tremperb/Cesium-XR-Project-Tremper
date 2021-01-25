using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveMap : MonoBehaviour
{
    Vector3 initialPos;
    Vector3 initialScale;
    Vector3 initialRotation;

    public Transform[] bounds;
    public GameObject controls;
    private Slider[] sliders;
    private Slider posx, posz, scale, rotate;
    public float sensitivity = 0f;
    public float scaleSensitivity = 0f;
    //
    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;


    private float xLast, zLast, sLast, rLast;

    // Start is called before the first frame update
    void Start()
    {
        // Get the mesh of the terrain and its vertices
        mesh = this.GetComponentInChildren<MeshFilter>().mesh;
        vertices = mesh.vertices;

        // Set the initial values of the mesh 
        initialPos = this.transform.position;
        initialScale = this.transform.localScale;
        initialRotation = this.transform.eulerAngles;

        // Get the controls to adjust the mesh
        // Ideally would want you to do all this with controller
        // but couldnt fit that into everything with the time constraint
        if(controls != null)
        {
            sliders = controls.GetComponentsInChildren<Slider>();

            // Assign the correct slider to what we want
            foreach(Slider s in sliders)
            {
                switch (s.gameObject.tag)
                {
                    case "posX":
                        posx = s;
                        break;
                    case "posZ":
                        posz = s;
                        break;
                    case "scale":
                        scale = s;
                        break;
                    case "rotate":
                        rotate = s;
                        break;
                    default:
                        Debug.Log("Not recognized");
                        break;
                }
            }
        }

        // Initialize the last position at start it is our starting position
        xLast = posx.value;
        zLast = posz.value;
        sLast = scale.value;
        rLast = rotate.value;
    }

    // Update is called once per frame
    void Update()
    {
        // This check isnt necessarily needed 
        // It simply checks to see if things actually changed 
        // That way this process isn't run every frame especially
        // the mesh vertices
        if (xLast != posx.value || zLast != posz.value || sLast != scale.value || rLast != rotate.value)
        {
            // Set the x and z location of the map
            setDragPos();

            // Set the scale factor
            setNewScale();

            // Set the desired rotation
            setNewRotation();

            // Set the bounds
            // This is where the real work mainly comes into play
            //The material is given a custom shader which essentially discards
            // any vertices which are colored black
            // So to communicate with that we are simply setting any vertex outside 
            //
            /**
             * Set the bounds
             * This is where the real work mainly comes into play. The material is given
             * a custom shader which essentially discard any vertex of said mesh which is colored black.
             * So to work with this anything vertex of the mesh which is out of bounds is set to black
             * and in turn discarded. When back in play the color is set back to white again allowing for the initial material. 
             */
            setMapBounds();
        }

    }

    private void setDragPos()
    {
        this.gameObject.transform.position = new Vector3(initialPos.x + (posx.value * sensitivity), initialPos.y, initialPos.z + (posz.value * sensitivity));
        
    }

    private void setNewScale()
    {
        this.gameObject.transform.localScale = new Vector3(initialScale.x + (scale.value * scaleSensitivity), initialScale.y + (scale.value * scaleSensitivity), initialScale.z + (scale.value * scaleSensitivity));
        
    }

    private void setNewRotation()
    {
        this.gameObject.transform.eulerAngles = new Vector3(initialRotation.x, initialRotation.y + rotate.value, initialRotation.z);
    }

    private void setMapBounds()
    {
        
        colors = new Color[vertices.Length];
        // Check the location of each vertice and take the corresponding action
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 mWorldPoint = transform.TransformPoint(vertices[i]);
            if(mWorldPoint.x < bounds[0].position.x)
            {
                colors[i] = new Color(0f, 0f, 0f, 0f);
            }
            else if(mWorldPoint.x > bounds[1].position.x)
            {
                colors[i] = new Color(0f, 0f, 0f, 0f);
            }
            else if(mWorldPoint.z > bounds[0].position.z)
            {
                colors[i] = new Color(0f, 0f, 0f, 0f);
            }
            else if(mWorldPoint.z < bounds[1].position.z)
            {
                colors[i] = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                colors[i] = new Color(1f, 1f, 1f, 1f);
            }
            
        }

        // Adjust the mesh colors
        mesh.colors = colors;
    }

}
