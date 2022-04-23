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
    public Stats stats;
    // Update is called once per frame
    void Update()
    {
        if (isMoving && targetTile.getAttribute() != Tile.Attribute.Impassable)
        {
            moving();
        } else if (isMoving) {
            isMoving = false;
        }

        if (!isMoving && stats.currMovement > 0) {
            if (Input.GetKeyDown(KeyCode.W))
            {
                move("up");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                move("left");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                move("down");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                move("right");
            }
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

    void move(string dir) {
        Vector3 direction = new Vector3(0,0,0);

        switch (dir) 
        {
            case "left":
                direction = new Vector3(-1,0,0);
                break;
            case "down":
                direction = new Vector3(0,0,-1);
                break;
            case "right":
                direction = new Vector3(1,0,0);
                break;
            case "up":
                direction = new Vector3(0,0,1);
                break;
            default:
                Debug.Log($"ERROR! Incorrect Movement Call: {dir}");
                break;
        }

        target = transform.position + direction;
        start = transform.position;
        startTile = CreateGrid.getTile(new Vector2(start.x, start.z), startTile);
        targetTile = CreateGrid.getTile(new Vector2(target.x, target.z), startTile);
        
        if (targetTile.getAttribute() != Tile.Attribute.Impassable)
            stats.currMovement -= 1;

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
