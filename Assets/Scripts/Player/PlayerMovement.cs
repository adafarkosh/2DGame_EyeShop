// parts are from the tutorials https://www.youtube.com/watch?v=0-c3ErDzrh8 = speed part and smootheness/stop; https://youtu.be/5KLV6QpSAdI?si=oMRWWaHeeekuUUCS point and click// 
// prevent player to go beyong a screen https://www.youtube.com/watch?v=qnr42UXQ0kc&t=120s
using UnityEngine;
using UnityEngine.Audio;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player;

    [SerializeField] private float playerSpeed;
    private Vector2 desiredPosition; // where we're moving 

    [SerializeField] private SpriteRenderer playerSprite; //* player will store a reference to the player’s SpriteRenderer component.
    public Animator playerAnimation;
    private AudioSource playerWalking;

    [SerializeField] private LayerMask stopperMask;
    private bool blockedByStopper;

    float directionX = 0f;

    void Start()
    {
        desiredPosition = player.position; // player begins at 0.0 as i set it (=on a "scene" player position) + prevents from random walking at the beginning of the game
        playerWalking = GetComponent<AudioSource>();

    }

    void Update()
    {
/// SETTING A CLICK, moving in FixedUpdatae
        if (Input.GetMouseButtonDown(0)) //=doesnt create movement, only sets desired location
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            desiredPosition = new Vector2(mouseWorldPosition.x, player.position.y);
        }

/// WALKING ANIMATION 

        bool isWalking = !blockedByStopper && Mathf.Abs(player.linearVelocity.x) > 0.001f;
        playerAnimation.SetBool("isWalking", isWalking);


        if (directionX < 0)
        {
            playerSprite.flipX = false;
        }
        else if (directionX > 0)
        {
            playerSprite.flipX = true;
        }

    }

    private void FixedUpdate() // for Rigidbody, velocity & other physics= it runs once per physics step (=no jitter as I had in void Update())
    {
        float distanceToDesiredLocation = desiredPosition.x - player.position.x;
        float stopNearThisFrame = playerSpeed * Time.fixedDeltaTime; //physics steps run on fixed(!) time interval, when fps is high, deltaTime is tiny --> fixed

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
            directionX = 0f;
            player.linearVelocity = new Vector2(0f, player.linearVelocity.y);
        }

        player.linearVelocity = new Vector2(directionX * playerSpeed, player.linearVelocity.y);

        /// WALKING sound
        if (Mathf.Abs(player.linearVelocity.x) > 0.01f)
        //* IF WITHOUT MATHF ABS than it will only be true when moving right. When you walk left, -2.5 > 0.01 is false, so your footsteps won’t play.
        //* Mathf.Abs(x) turns “how fast and in what direction” into “how fast (ignoring direction)”.
        // could be if (vx > 0.01f || vx < -0.01f) Play();
        {
            if (!playerWalking.isPlaying) playerWalking.Play();
        }
        else
        {
            if (playerWalking.isPlaying) playerWalking.Stop();
        }

    }

    /// METHOD FOR STOPPERs - walls, counter
    private void OnCollisionEnter2D(Collision2D stopperCollision)
    {
        if (((1 << stopperCollision.gameObject.layer) & stopperMask.value) != 0)
        {
            blockedByStopper = true;
            player.linearVelocity = new Vector2(0f, player.linearVelocity.y);
            desiredPosition = new Vector2(player.position.x, desiredPosition.y);
        }
    }

    private void OnCollisionExit2D(Collision2D stopperCollision)
    {
        if (((1 << stopperCollision.gameObject.layer) & stopperMask.value) != 0)
        {
            blockedByStopper = false;
        }
    }

}


