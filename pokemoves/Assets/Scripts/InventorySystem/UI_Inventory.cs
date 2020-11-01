using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour{

    public Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerMovement player;

    private Transform Player;
    private Animator ArmsAnimator;
    private Animator BodyAnimator;

    public static bool inventoryFull;

    public Transform shootTarget;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        Player = GameObject.FindWithTag("Player").transform;
        ArmsAnimator = Player.Find("MC_Arms").GetComponent<Animator>();
        BodyAnimator = Player.Find("MC_Body").GetComponent<Animator>();

        shootTarget = GameObject.Find("ShootPoint").transform;
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        //delete existing inventory items
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 125f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UseItem(item);
                if (item.IsHoldable())
                {
                    ArmsAnimator.SetBool("Holding", true);
                    BodyAnimator.SetBool("Holding", true);
                    player.holdItem(item);
                }

                if (item.ranged())
                {
                    shootTarget.GetComponent<SpriteRenderer>().sprite = ItemAssets.Instance.lookTarget;
                }
                else
                {
                    shootTarget.GetComponent<SpriteRenderer>().sprite = null;
                }
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("sprite").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if(x > 5)
            {
                inventoryFull = true;
            }
            else
            {
                inventoryFull = false;
            }
        }
    }
}
