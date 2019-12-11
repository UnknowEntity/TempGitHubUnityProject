using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    private    bool isClick=false;
    private Ray cameraToMouse;
    public GameObject CameraView;
    public GameObject terant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseToScreen=transform.position;
        Debug.Log("Temp commit");
        if (Input.GetMouseButtonDown(0))
        {
            isClick=true;
            cameraToMouse=Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(cameraToMouse);
        }
        // if (isClick)
        // {
        //     mouseToScreen=mousePosition;
        // }
        
        Plane groundPlane = new Plane(Vector3.up,Vector3.zero);
        float enter = 100.0f;
        if (isClick)
            {
                
                groundPlane.Raycast(cameraToMouse, out enter);
                float speed=10;
                Debug.Log("Plane Raycast hit at distance: " + enter);
                var pos = cameraToMouse.GetPoint(enter);
                //terant.GetComponent<Terrain>().SampleHeight
                Vector3 cameraNewPosition=new Vector3(pos.x+5,terant.GetComponent<Terrain>().SampleHeight(pos),pos.z-5);
                float step =  speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, pos, step);
                CameraView.transform.position=Vector3.MoveTowards(CameraView.transform.position, cameraNewPosition, step);
                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, pos) < 0.001f)
                {
                    // Swap the position of the cylinder.
                    pos *= -1.0f;
                    cameraNewPosition*=-1.0f;
                    isClick=false;
                }
                if (Vector3.Distance(CameraView.transform.position, cameraNewPosition) < 0.001f)
                {
                    // Swap the position of the cylinder.
                    cameraNewPosition*=-1.0f;
                }
            }
        //transform.position.y=0;
    }
}
