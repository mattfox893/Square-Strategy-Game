using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    bool isMoving;
    Vector3 target;
    Vector3 start;
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moving();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
    }

    void moving() {
        if (Vector3.Distance(start, transform.position) > 1f)
        {
            transform.position = target;
            isMoving = false;
            return;
        }
        transform.position += (target - start) * moveSpeed * Time.deltaTime;
        return;
    }

    void moveRight() {
        target = transform.position + Vector3.right;
        start = transform.position;
        isMoving = true;
    }

    void moveLeft() {
        target = transform.position + Vector3.left;
        start = transform.position;
        isMoving = true;
    }

    void moveUp() {
        target = transform.position + Vector3.forward;
        start = transform.position;
        isMoving = true;
    }

    void moveDown() {
        target = transform.position + Vector3.back;
        start = transform.position;
        isMoving = true;
    }
}
