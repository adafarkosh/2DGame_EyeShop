using UnityEngine;
// tutorial https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Cursor.SetCursor.html

public class CursorClick : MonoBehaviour
{
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D clickCursor;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private static CursorClick instance;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject); // <- keeps it for both scenes
    }


    private void Start()
    {
        Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Cursor.SetCursor(clickCursor, hotSpot, cursorMode);

        if (Input.GetMouseButtonUp(0))
            Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
    }
}
