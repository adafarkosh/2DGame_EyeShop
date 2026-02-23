using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraZone : MonoBehaviour
{
    public bool inCameraZone; // assigning rooms to camera zones
    public string currentRoomTag;

    private void Start()
    {
        inCameraZone = false;
        currentRoomTag = ""; //clears room tag
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"ENTERED: {other.name} tag={other.tag}");

        //Debug.Log($"[ZONE ENTER] hit={other.name} tag={other.tag} layer={LayerMask.LayerToName(other.gameObject.layer)} playerY={transform.position.y}");


        if (other.CompareTag("Room1")) //Checks if the trigger object has tag “Room1”.
        {
            inCameraZone = true; // Marks that you’re in a camera zone and records which room.
            currentRoomTag = "Room1";
        }
        else if (other.CompareTag("Room2")) //Same for Room2
        {
            inCameraZone = true;
            currentRoomTag = "Room2";
        }

        //Debug.Log($"[ROOM STATE] inCameraZone={inCameraZone} currentRoomTag='{currentRoomTag}'");

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log($"[ZONE ENTER] hit={other.name} tag={other.tag} layer={LayerMask.LayerToName(other.gameObject.layer)} playerY={transform.position.y}");

        if (other.CompareTag("Room1") && currentRoomTag == "Room1") //wo checks: you’re leaving a trigger tagged Room1 and your stored current room is Room1. This prevents weird cases where you might have overlapping triggers and you don’t want to clear the state incorrectly.
        {
            inCameraZone = false;
            currentRoomTag = ""; // Leave a zone - clear a tag.
        }
        else if (other.CompareTag("Room2") && currentRoomTag == "Room2")
        {
            inCameraZone = false;
            currentRoomTag = "";
        }
        //Debug.Log($"[ROOM STATE] inCameraZone={inCameraZone} currentRoomTag='{currentRoomTag}'");



    }

}
