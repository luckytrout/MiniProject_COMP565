using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    //global variables
    PrimitiveType selectedShape;
    Material selectedTexture;
    GameObject shape;
    Material[] availableMaterials;
    HoverScript hoverScript;

    [SerializeField] Material rockMaterial;
    [SerializeField] Material metalMaterial;
    [SerializeField] Material brickMaterial;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {

        // look for ScriptManager and get HoverScript that is attached
        // need this for specific interactions
        hoverScript = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<HoverScript>();

        Debug.Log("Hello World!");

        //set default shape to Cube
        changeToCube();

        //set default texture to Texture1
        changeToTexture1();
    }

    // Update is called once per frame
    void Update()
    {

        //do Input.GetMouseButton(0) for fun
        if (Input.GetMouseButtonUp(0))
        {
            //2D to 3D coordinates
            #region Screen To World

            //hitInfo is information gathered from a raycast
            RaycastHit hitInfo = new RaycastHit();

            //hit is false when referenced object is not hit, otherwise true
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("You hit: " + hitInfo.transform.name);

                //generates a primitive geometric shape (based on selected shape)
                shape = GameObject.CreatePrimitive(selectedShape);

                //changes material of generated shape to the selected texture
                shape.GetComponent<MeshRenderer>().material = selectedTexture;

                //adds a box-shaped collider/hitbox to generated shape
                shape.AddComponent<BoxCollider>();
                shape.GetComponent<BoxCollider>().isTrigger = true;
                
                //create name to distinguish it
                //cube.name = string.Format("MyCube[0]", index.ToString()); //original code
                shape.name = string.Format("Shape[" + index + "]", index.ToString());
                index++;

                //adds a rigidbody to generated shape so that it can be affected by physics
                //shape.AddComponent<Rigidbody>();

                if (hitInfo.transform.name.Equals("Base"))
                {

                    //position the generated shape based on where the mouse was clicked (for the base)
                    shape.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
                }
                else
                {
                    //else if it hits a gameObject that's not the base
                    if (hitInfo.normal == new Vector3(0, 0, 1)) // z+
                    {
                        shape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + (0.5f));
                    }
                    #region HIDE
                    if (hitInfo.normal == new Vector3(1, 0, 0)) // x+
                    {
                        shape.transform.position = new Vector3(hitInfo.point.x + (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 1, 0)) // y+
                    {
                        shape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + (0.5f), hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 0, -1)) // z-
                    {
                        shape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - (0.5f));
                    }
                    if (hitInfo.normal == new Vector3(-1, 0, 0)) // x-
                    {
                        shape.transform.position = new Vector3(hitInfo.point.x - (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, -1, 0)) // y-
                    {
                        shape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - (0.5f), hitInfo.transform.position.z);
                    }
                    #endregion
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            #endregion
        }
    }


    // methods to change the geometry of the generated shape
    // changeHoverShape is used to correct the previewed, transparent shape
    public void changeToSphere()
    {
       selectedShape = PrimitiveType.Sphere;
       hoverScript.changeHoverShape(2);
    }

    public void changeToCapsule()
    {
        selectedShape = PrimitiveType.Capsule;
        hoverScript.changeHoverShape(3);
    }

    public void changeToCube()
    {
        selectedShape = PrimitiveType.Cube;
        hoverScript.changeHoverShape(1);
    }


    //methods to change the texture/material of the generated shape
    public void changeToTexture1()
    {
        selectedTexture = rockMaterial;
    }

    public void changeToTexture2()
    {
        selectedTexture = brickMaterial;
    }

    public void changeToTexture3()
    {
        selectedTexture = metalMaterial;
    }

}
