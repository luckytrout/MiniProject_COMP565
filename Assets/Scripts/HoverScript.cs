using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{

    //global variables
    MainScript mainScript;

    //hoverShape is the transparent, preview shape that follows the cursor
    [SerializeField] GameObject hoverShape;

    // primitive meshes for changing GameObject shapes
    [SerializeField] Mesh sphereMesh;
    [SerializeField] Mesh cubeMesh;
    [SerializeField] Mesh capsuleMesh;

    [SerializeField] Material greenColor;
    [SerializeField] Material yellowColor;

    
    // Start is called before the first frame update
    void Start()
    {
        // look for ScriptManager and get MainScript that is attached
        // need this in order to access the script's variables later
        mainScript = GameObject.FindGameObjectWithTag("ScriptManager").GetComponent<MainScript>();

        // set default shape of hoverShape to cube (option = 1)
        changeHoverShape(1);

        // set default color of hoverShape to yellow (option = 1)
        changeHoverColor(1);
    }

    // Update is called once per frame
    void Update()
    {
            //Same code as in MainScript, but runs every frame
            //2D to 3D coordinates
            #region Screen To World

            //hitInfo is information gathered from a raycast
            RaycastHit hitInfo = new RaycastHit();

            //hit is false when referenced object is not hit, otherwise true
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.name.Equals("Base"))
                {
                    //the color of the hoverShape is yellow to preview a freeform placement
                    changeHoverColor(1);

                    //position the generated shape based on where the mouse was clicked (for the base)
                    hoverShape.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
                }
                else
                {
                    //else if it hits a gameObject that's not the base

                    // the color of the hoverShape is green to represent a fixed placement on a generated shape
                    changeHoverColor(2);

                    if (hitInfo.normal == new Vector3(0, 0, 1)) // z+
                    {
                        
                        hoverShape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z + (0.5f));
                    }
                    #region HIDE
                    if (hitInfo.normal == new Vector3(1, 0, 0)) // x+
                    {
                        hoverShape.transform.position = new Vector3(hitInfo.point.x + (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 1, 0)) // y+
                    {
                        hoverShape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y + (0.5f), hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, 0, -1)) // z-
                    {
                        hoverShape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.point.z - (0.5f));
                    }
                    if (hitInfo.normal == new Vector3(-1, 0, 0)) // x-
                    {
                        hoverShape.transform.position = new Vector3(hitInfo.point.x - (0.5f), hitInfo.transform.position.y, hitInfo.transform.position.z);
                    }
                    if (hitInfo.normal == new Vector3(0, -1, 0)) // y-
                    {
                        hoverShape.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.point.y - (0.5f), hitInfo.transform.position.z);
                    }
                    #endregion
                }
            }
            else
            {
                hoverShape.transform.position = new Vector3(0, 20, 0);
                Debug.Log("No hit");
            }

        #endregion
    }


    // method to change shape/mesh of hoverShape
    public void changeHoverShape(int option = 1)
    {
        switch(option){
            case 1:
                hoverShape.GetComponent<MeshFilter>().mesh = cubeMesh;
                break;
            case 2:
                hoverShape.GetComponent<MeshFilter>().mesh = sphereMesh;
                break;
            case 3:
                hoverShape.GetComponent<MeshFilter>().mesh = capsuleMesh;
                break;
        }
    }

    public void changeHoverColor(int option = 1)
    {
        switch(option){
            case 1:
                hoverShape.GetComponent<MeshRenderer>().material = yellowColor;
                break;
            case 2:
                hoverShape.GetComponent<MeshRenderer>().material = greenColor;
                break;
        }
    }
}
