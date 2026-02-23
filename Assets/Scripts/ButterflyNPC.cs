using UnityEngine;


public class ButterflyNPC : MonoBehaviour
{
    public Rigidbody2D butterfly;

    [SerializeField] private float butterflySpeed;

    [SerializeField] private Transform pointA; // left point
    [SerializeField] private Transform pointB; // right point

    private Vector2 desiredPosition; // where we're moving

    [SerializeField] private SpriteRenderer butterflySprite;

    float directionX = 0f;

    void Start()
    {
        desiredPosition = butterfly.position;

        // start by going to the right point
        if (pointA != null)
            desiredPosition = pointA.position;

        // default butterfly facing = left, so:
        // facing left => flipX = false
        butterflySprite.flipX = false;
    }


    private void FixedUpdate()
    {
        float distanceToDesiredLocation = desiredPosition.x - butterfly.position.x;
        float stopNearThisFrame = butterflySpeed * Time.fixedDeltaTime;

        if (distanceToDesiredLocation > stopNearThisFrame)
        {
            directionX = 1f; // move right
        }
        else if (distanceToDesiredLocation < -stopNearThisFrame)
        {
            directionX = -1f; // move left
        }
        else
        {
            // arrived: stop, flip, and swap target
            directionX = 0f;
            butterfly.linearVelocity = new Vector2(0f, butterfly.linearVelocity.y);

            if (pointA != null && pointB != null)
            {
                // if we were heading to B, switch to A (and face left)
                if (Mathf.Abs(desiredPosition.x - (float)pointB.position.x) < 0.001f)
                {
                    desiredPosition = pointA.position;
                    butterflySprite.flipX = false; // face left (default)
                }
                // otherwise switch to B (and face right)
                else
                {
                    desiredPosition = pointB.position;
                    butterflySprite.flipX = true; // face right
                }
            }
        }

        butterfly.linearVelocity = new Vector2(directionX * butterflySpeed, butterfly.linearVelocity.y);
    }
}
