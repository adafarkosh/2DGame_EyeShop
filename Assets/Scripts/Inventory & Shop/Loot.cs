// all scripts from "Inventory & Shop folder" -- based on tutorials https://www.youtube.com/watch?v=rnkbTWvl51c&list=PLSR2vNOypvs7sV_ks7h42F7hZ7DmGJqU6

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;
    public int quantity = 1;

    public static event Action<ItemSO, int> OnItemLooted;

    [Header("Pickup")]
    [SerializeField] private float pickupAnimTime = 0.5f;   // should match LootPickup clip length
    [SerializeField] private float pickupEnableDelay = 0.2f; // for freshly spawned/dropped loot

    [Header("Respawn")]
    [SerializeField] private bool respawns = true;
    [SerializeField] private float respawnSeconds = 5f;

    private bool canBePickedUp = true;
    private bool playerInRange = false;

    private Collider2D col;
    private Vector3 spriteStartLocalPos;
    private Vector3 spriteStartLocalScale;

    [SerializeField] private AudioClip pickupClip; 
    [SerializeField] private AudioClip respawnClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (anim == null) anim = GetComponent<Animator>();
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (sr != null)
        {
            spriteStartLocalPos = sr.transform.localPosition;
            spriteStartLocalScale = sr.transform.localScale;
        }

        UpdateAppearance();
    }

    private void OnValidate()
    {
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        UpdateAppearance();
    }

    public void Initialize(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;

        canBePickedUp = false;
        UpdateAppearance();
        Invoke(nameof(EnablePickup), pickupEnableDelay);
    }

    private void EnablePickup() => canBePickedUp = true;

    private void UpdateAppearance()
    {
        if (itemSO == null || sr == null) return;
        sr.sprite = itemSO.icon;
        gameObject.name = itemSO.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerInRange = false;
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (!canBePickedUp) return;

        // prevents picking up world loot when right-clicking UI slots
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        // RIGHT CLICK to pick up (mouse button 1)
        if (Input.GetMouseButtonDown(0))
        {
            Pickup();
        }
    }

    private void PlayClip(AudioClip clip) //prevents breaking pickup/respawn problem I had
    {
        if (audioSource == null) return;
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }


    private void Pickup() 
    {
        PlayClip(pickupClip);

        canBePickedUp = false;     // block double-pickup
        if (col != null) col.enabled = false;

        anim.Play("LootPickup");
        OnItemLooted?.Invoke(itemSO, quantity);

        if (respawns)
            StartCoroutine(RespawnRoutine());
        else
            Destroy(gameObject, pickupAnimTime);
    }
    /// RESPAWN
    private System.Collections.IEnumerator RespawnRoutine()
    {
        // wait so the pickup animation can play
        yield return new WaitForSeconds(pickupAnimTime);

        // hide visuals + stop triggers
        if (sr != null) sr.enabled = false;
        playerInRange = false;

        // wait respawn time
        yield return new WaitForSeconds(respawnSeconds);

        // play respawn sound when it actually respawns
        PlayClip(respawnClip);

        // reset sprite/anim
        if (sr != null)
        {
            sr.transform.localPosition = spriteStartLocalPos;
            sr.transform.localScale = spriteStartLocalScale;

            UpdateAppearance();
            if (anim != null) anim.Play("Idle", 0, 0f);

            sr.enabled = true;
        }

        if (col != null) col.enabled = true;
        canBePickedUp = true;
    }

}
