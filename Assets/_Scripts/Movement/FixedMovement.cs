
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FixedMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _movementX;
    private float _movementY;
    private FacingDirection _facingDirection = FacingDirection.Right;

    public float maxVerticalVelocity = 10;

    public float maxHorizontalVelocity = 10;

    public GameObject ChangeDirectionEffect;
    
    public enum FacingDirection
    {
        Left,
        Right,
    }
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movementX, _movementY);
        Quaternion q = _rb.gameObject.transform.rotation;
        if (_rb.velocity.x > 0 && _facingDirection == FacingDirection.Left)
        {
            _rb.SetRotation(new Quaternion(q.x, q.y, 0, q.w));
            _facingDirection = FacingDirection.Right;
            OnChangedLeftRightDirection();
        }
        else if (_rb.velocity.x < 0 && _facingDirection == FacingDirection.Right)
        {
            _rb.SetRotation(new Quaternion(q.x, q.y, 180, q.w));
            _facingDirection = FacingDirection.Left;
            OnChangedLeftRightDirection();
        }
    }

    private void OnChangedLeftRightDirection()
    {
        // Instantiate a Hit6-burst prefab at the player's position
        if (ChangeDirectionEffect != null)
        {
            Instantiate(ChangeDirectionEffect, transform.position, Quaternion.identity);
        }
        
        // log a debug message
        Debug.Log("Changed direction to " + _facingDirection);
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();


        _movementX = ExactVelocityOrZero(movementVector.x, maxHorizontalVelocity);
        _movementY = ExactVelocityOrZero(movementVector.y, maxVerticalVelocity);
    }

    private static float ExactVelocityOrZero(float y, float maximum)
    {
        return y > 0 ? maximum : (y < 0 ? -maximum : 0);
    }
}
