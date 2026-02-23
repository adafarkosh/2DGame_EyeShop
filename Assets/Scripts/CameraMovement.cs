using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /* [SerializeField] private float stepSize; */  // move this many units per press
    private float desiredDestY;

    private Camera cam;
    [SerializeField] private BoxCollider2D camBoxCollider;


    [SerializeField] private float room1Y;
    [SerializeField] private float room2Y;

    [SerializeField] private PlayerCameraZone playerZone;
    private string lastRoomTag = "";


    void Start()
    {
        desiredDestY = transform.position.y; 

        cam = GetComponent<Camera>(); //*Camera is a component in Inspector (for player "Transform" is a component)

    }


    void Update()
    {
        if (Input.GetKey("escape")) 
        {
            Application.Quit();
            Debug.Log("Quit!");
        }
        //// auto-follow player room
        if (playerZone.currentRoomTag != lastRoomTag)
        {
            if (playerZone.currentRoomTag == "Room1")
            {
                desiredDestY = room1Y;
            }

            else if (playerZone.currentRoomTag == "Room2")
            {
                desiredDestY = room2Y;
            }

            lastRoomTag = playerZone.currentRoomTag;
        }


        Vector3 currentPosition = transform.position; //* FOR CAMERA -- always Vector3

        /// BOUND BOX

        Bounds b = camBoxCollider.bounds; //gives me a Bounds object =a 3D “box” in world space that wraps the collider
                                          //* Bounds by default contains: b.min = the lowest corner(x, y, z); b.max = the highest corner(x, y, z); b.center, b.size, etc.

        float camHalfHeight = cam.orthographicSize;
        desiredDestY = Mathf.Clamp(desiredDestY, b.min.y + camHalfHeight, b.max.y - camHalfHeight);

        transform.position = new Vector3(currentPosition.x, desiredDestY, currentPosition.z); //giving "currentPosition" new coordinates

        float minY = b.min.y + camHalfHeight;
        float maxY = b.max.y - camHalfHeight;


    }

}
    