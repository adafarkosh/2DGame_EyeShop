using UnityEngine;

public class PlayerDoorTeleporter : MonoBehaviour //* HERE IT'S ALREADY ATTACHED TO THE PLAYER
{
    [SerializeField] private float teleportCooldown = 0.5f;
    private float nextTeleportTime = 0f;

    public Rigidbody2D player; //* it’s the recommended way to move physics objects NOT transform.position



    /// Called when the player enters a trigger collider (door must have Is Trigger on).
    private void OnTriggerEnter2D(Collider2D collision) //* TRIGGER ENTER
    {

        if (Time.time < nextTeleportTime) return;

        if (!collision.CompareTag("Teleporter")) return;

        var door = collision.GetComponent<DoorTeleporter>();
        if (door == null) return;

        nextTeleportTime = Time.time + teleportCooldown;

        Vector3 dest = door.GetDestination().position;

        player.position = dest;
        player.linearVelocity = Vector2.zero;

        //       Debug.Log($"TELEPORT ENTER: {collision.name} time={Time.time} next={nextTeleportTime}");

    }


}
