using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    bool moving;
    Vector3 target;
    Vector3 start;
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(start, transform.position) > 1f)
            {
                transform.position = target;
                moving = false;
                return;
            }
            transform.position += (target - start) * moveSpeed * Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            target = transform.position + Vector3.forward;
            start = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            target = transform.position + Vector3.left;
            start = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            target = transform.position + Vector3.back;
            start = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            target = transform.position + Vector3.right;
            start = transform.position;
            moving = true;
        }
    }
}
