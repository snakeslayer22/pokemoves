using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static float maxPlayerHealth = 20;
    public static float PlayerHealth = 20;

    private Animator BodyAnimator;

    private Animator ArmsAnimator;

    [SerializeField] private float speed = 4;
    private Vector2 movement;
    private Rigidbody2D rb;

    public bool ableToMove = true;

    public static Inventory inventory;
    [SerializeField] UI_Inventory uiInventory;

    private Transform arms;
    private Transform holdPoint;
    private SpriteRenderer holdPointSpriteRenderer;

    private Transform MemoryBoxesPanel;
    private Transform MemoryBoxesBoxesBox;
    private bool MemoryBoxesDown = false;
    private Animator MemoryBoxesAnimator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        ArmsAnimator = transform.Find("MC_Arms").GetComponent<Animator>();
        BodyAnimator = transform.Find("MC_Body").GetComponent<Animator>();

        arms = transform.Find("MC_Arms");
        holdPoint = arms.Find("HoldPoint");
        holdPointSpriteRenderer = holdPoint.GetComponent<SpriteRenderer>();

        MemoryBoxesPanel = GameObject.Find("MemoryBoxesPanel").transform;
        MemoryBoxesBoxesBox = GameObject.Find("MemoryBoxesBoxesBox").transform;
        MemoryBoxesAnimator = MemoryBoxesBoxesBox.GetComponent<Animator>();
    }

    private void Awake() 
    {
        IEnumerator coroutine = waitOneFrame();
        StartCoroutine(coroutine);
    }

    private IEnumerator waitOneFrame()
    {
        yield return null;

        inventory = new Inventory(useItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
    }

    private void useItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPosion:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPosion, amount = 1 });
                PlayerHealth += 5;
                HealthBar.SetHealth(0);
                break;
            case Item.ItemType.ManaPosion:
                //do action
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPosion, amount = 1 });
                break;
        }
    }

    public void holdItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sword:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdSwordSpite;
                weponStats(0.55f, 1.5f);
                break;
            case Item.ItemType.Knife:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdKnifeSprite;
                weponStats(0.4f, 1);
                break;
            case Item.ItemType.Bow:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdBowSprite;
                weponStats(69, 1.25f);
                break;
            case Item.ItemType.Mase:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdMaseSprite;
                weponStats(0.6f, 2.5f); //:)
                break;
            case Item.ItemType.Wand:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdWandSprite;
                weponStats(69, 1.75f);
                break;
            case Item.ItemType.Bomb:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdBombSprite;
                weponStats(72, 5);
                break;
            case Item.ItemType.Gun:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdGunSprite;
                weponStats(69, 6);
                break;
            case Item.ItemType.Katana:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdKatanaSprite;
                weponStats(1, 1.25f);
                break;
            case Item.ItemType.QuickKnife:
                holdPointSpriteRenderer.sprite = ItemAssets.Instance.holdQuickKnifeSprite;
                weponStats(0.35f, 0.25f);
                break;
        }
    }

    private void weponStats(float range, float damage)
    {
        Attack.MCAttackRange = range;
        Attack.MCdamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (collider.gameObject.GetComponent<ItemWorld>() == true)
        {
            inventory.checkItemAlreadyInItemList(itemWorld.GetItem());
        }
        if (itemWorld != null)
        {
            if (UI_Inventory.inventoryFull == false)
            {
                //touching item
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
            else if (inventory.itemAlreadyInInventory && itemWorld.GetItem().IsStackable() == true)
            {
                //touching item
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }

            Debug.Log(itemWorld.GetItem().IsStackable());
        }
        
    }

    void Update()
    {

        if (ableToMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown("space"))
            {
                ArmsAnimator.SetBool("Holding", false);
                BodyAnimator.SetBool("Holding", false);
                holdPointSpriteRenderer.sprite = null;
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (!MemoryBoxesDown)
                {
                    MemoryBoxesPanel.localPosition = Vector3.zero;
                    MemoryBoxesAnimator.SetBool("IsDown", true);

                    MemoryBoxesDown = true;
                }
                else
                {
                    IEnumerator coroutine = WaitHalfASecond();
                    StartCoroutine(coroutine);
                    MemoryBoxesAnimator.SetBool("IsDown", false);

                    MemoryBoxesDown = false;
                }
            }
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        BodyAnimator.SetFloat("HorizontalSpeed", movement.x);
        BodyAnimator.SetFloat("VerticalSpeed", movement.y);

        ArmsAnimator.SetFloat("HorizontalSpeed", movement.x);
        ArmsAnimator.SetFloat("VerticalSpeed", movement.y);
    }

    private IEnumerator WaitHalfASecond()
    {
        yield return new WaitForSeconds(0.5f);

        MemoryBoxesPanel.localPosition = new Vector3(0, 450, 0);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
