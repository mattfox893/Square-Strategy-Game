using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
