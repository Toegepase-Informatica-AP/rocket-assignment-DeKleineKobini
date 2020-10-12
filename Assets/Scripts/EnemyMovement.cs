using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementDirection { X, Y, Z }

    public MovementDirection direction = MovementDirection.Y;
    public float step = 1f;
    public bool invertMovement = false;
    public float lowerBoundary = 0;
    public float upperBoundary = 10;

    private GameController gameController;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        switch (direction)
        {
            case MovementDirection.X:
                movement = new Vector3(step, 0, 0);
                break;
            case MovementDirection.Y:
                movement = new Vector3(0, step, 0);
                break;
            case MovementDirection.Z:
                movement = new Vector3(0, 0, step);
                break;
            default:
                movement = Vector3.zero;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();

        if (invertMovement)
            transform.position -= movement * Time.deltaTime;
        else
            transform.position += movement * Time.deltaTime;
    }

    private void CheckPosition()
    {
        float position = 0;

        switch (direction)
        {
            case MovementDirection.X:
                position = transform.position.x;
                break;
            case MovementDirection.Y:
                position = transform.position.y;
                break;
            case MovementDirection.Z:
                position = transform.position.z;
                break;
            default:
                break;
        }

        if (invertMovement && position <= lowerBoundary)
            invertMovement = false;
        else if (!invertMovement && position >= upperBoundary)
            invertMovement = true;
    }

}
