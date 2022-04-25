using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 10;
    public int minCamera = 5;
    public int maxCamera = 15;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, (int)((minCamera + maxCamera) / 2), 0);
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel")*10;

        if ((transform.position.y <= minCamera && ScrollWheelChange! > 0) ||
            (transform.position.y >= maxCamera && ScrollWheelChange! < 0)) //if camera is at edges of zoom
        { }                                                                //don't let it zoom
        else if (ScrollWheelChange != 0)
        {
            transform.Translate(ScrollWheelChange * Vector3.down, Space.World);//Move the main camera
        }



        Vector3 mousePos = Input.mousePosition;

        // up and down
        if (mousePos.y >= Screen.height - 10){
            transform.Translate(speed * Vector3.forward * Time.deltaTime, Space.World);
        }
        else if (mousePos.y <= 10){
            transform.Translate(speed * Vector3.back * Time.deltaTime, Space.World);
        }

        // left and right
        if (mousePos.x >= Screen.width - 10)
        {
            transform.Translate(speed * Vector3.right * Time.deltaTime, Space.World);
        }
        else if (mousePos.x <= 10)
        {
            transform.Translate(speed * Vector3.left * Time.deltaTime, Space.World);
        }



        
    }
}
