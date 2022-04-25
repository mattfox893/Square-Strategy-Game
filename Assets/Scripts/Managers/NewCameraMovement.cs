using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraMovement : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Vector3 dragDifference;

    private bool drag = false;

    public int minCamera = 5;
    public int maxCamera = 15;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel") * 10;

        if ((transform.position.y <= minCamera && ScrollWheelChange! > 0) ||
            (transform.position.y >= maxCamera && ScrollWheelChange! < 0)) //if camera is at edges of zoom
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

            cam.transform.position += new Vector3(dragDifference.x, 0, dragDifference.z);
        }

    }
}
