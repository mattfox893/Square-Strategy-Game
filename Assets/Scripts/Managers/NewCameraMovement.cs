using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraMovement : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Vector3 dragDifference;

    public int minCameraZoom = 5;
    public int maxCameraZoom = 15;
    private Camera cam;
    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        transform.position = new Vector3(0, (int)((minCameraZoom + maxCameraZoom) / 2), 0);
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel") * 10;

        if ((transform.position.y <= minCameraZoom && ScrollWheelChange! > 0) ||
            (transform.position.y >= maxCameraZoom && ScrollWheelChange! < 0)) //if camera is at edges of zoom
        { }                                                                //don't let it zoom
        else if (ScrollWheelChange != 0)
        {
            transform.Translate(ScrollWheelChange * Vector3.down, Space.World);//Move the main camera
        }




        if (Input.GetMouseButtonDown(1))
            //dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            dragOrigin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y - 0.5f));


        if (Input.GetMouseButton(1))
        {
            dragDifference = dragOrigin - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y - 0.5f));

            Vector3 temp = cam.transform.position + new Vector3(dragDifference.x, 0, dragDifference.z);
            temp.x = Mathf.Min(Mathf.Max(0, temp.x), GridManager.width);
            temp.z = Mathf.Min(Mathf.Max(0, temp.z), GridManager.length);
            cam.transform.position = temp;

        }
        else
        {
            Vector3 mousePos = Input.mousePosition;

            // up and down
            if (mousePos.y >= Screen.height - 10 && transform.position.z <= GridManager.length)
            {
                transform.Translate(speed * Vector3.forward * Time.deltaTime, Space.World);
            }
            else if (mousePos.y <= 10 && transform.position.z >= 0)
            {
                transform.Translate(speed * Vector3.back * Time.deltaTime, Space.World);
            }

            // left and right
            if (mousePos.x >= Screen.width - 10 && transform.position.x <= GridManager.width)
            {
                transform.Translate(speed * Vector3.right * Time.deltaTime, Space.World);
            }
            else if (mousePos.x <= 10 && transform.position.x >= 0)
            {
                transform.Translate(speed * Vector3.left * Time.deltaTime, Space.World);
            }

        }


    }
}
