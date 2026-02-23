using UnityEngine;
// partially from tutorial https://www.youtube.com/watch?v=0JXVT28KCIg 

/// PUBLIC METHOD other scripts can call to read that destination Transform =so each door knows where it sends you
public class DoorTeleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
   
    public Transform GetDestination()
    {
        return destination;
    }
}
