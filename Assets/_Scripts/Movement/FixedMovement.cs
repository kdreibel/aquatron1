
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FixedMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;

    public float maxVerticalVelocity = 10;

    public float maxHorizontalVelocity = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movementX, movementY);
        Quaternion q = rb.gameObject.transform.rotation;
        if (rb.velocity.x > 0)
        {
            rb.SetRotation(new Quaternion(q.x, q.y, 0, q.w));
        }
        else
        {
            rb.SetRotation(new Quaternion(q.x, q.y, 180, q.w));

        }
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();


        movementX = ExactVelocityOrZero(movementVector.x, maxHorizontalVelocity);
        movementY = ExactVelocityOrZero(movementVector.y, maxVerticalVelocity);
    }

    private static float ExactVelocityOrZero(float y, float maximum)
    {
        return y > 0 ? maximum : (y < 0 ? -maximum : 0);
    }
}
