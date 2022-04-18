using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    bool isMoving;
    Vector3 target;
    Vector3 start;
    Tile startTile;
    Tile targetTile;
    // Update is called once per frame
    void Update()
    {
        startTile = CreateGrid.getTile(new Vector2(start.x, start.z), startTile);
        targetTile = CreateGrid.getTile(new Vector2(target.x, target.z), startTile);
        if (isMoving && targetTile.getAttribute() != Tile.Attribute.Impassable)
        {
            moving();
        } else if (isMoving) {
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && !isMoving)
        {
            moveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            moveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isMoving)
        {
            moveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
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
