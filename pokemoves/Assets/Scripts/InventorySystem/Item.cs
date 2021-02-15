using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType
    {
        Sword,
        HealthPosion,
        ManaPosion,
        Coin,
        MedKit,
        Knife,
        Katana,
        Mase,
        Wand,
        Bow,
        Gun,
        Bomb,
        QuickKnife,
        Arrow,
        magic,
        bullet,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.holdSwordSpite;
            case ItemType.HealthPosion: return ItemAssets.Instance.healthPosionSprite;
            case ItemType.ManaPosion: return ItemAssets.Instance.manaPosionSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
            case ItemType.MedKit: return ItemAssets.Instance.medKitSprite;
            case ItemType.Knife: return ItemAssets.Instance.holdKnifeSprite;
            case ItemType.Katana: return ItemAssets.Instance.holdKatanaSprite;
            case ItemType.Mase: return ItemAssets.Instance.holdMaseSprite;
            case ItemType.Wand: return ItemAssets.Instance.holdWandSprite;
            case ItemType.Bow: return ItemAssets.Instance.holdBowSprite;
            case ItemType.Gun: return ItemAssets.Instance.holdGunSprite;
            case ItemType.Bomb: return ItemAssets.Instance.holdBombSprite;
            case ItemType.QuickKnife: return ItemAssets.Instance.holdQuickKnifeSprite;
            case ItemType.Arrow: return ItemAssets.Instance.arrowSprite;
            case ItemType.magic: return ItemAssets.Instance.magicSprite;
            case ItemType.bullet: return ItemAssets.Instance.bulletSprite;
        }
    }

    //public Sprite GetHeldSprite()
    //{
    //    switch (heldItem)
    //    {
    //        default:
    //        case HeldItem.Sword: return ItemAssets.Instance.holdSwordSpite;
    //    }
    //}

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPosion:
            case ItemType.ManaPosion:
            case ItemType.Arrow:
            case ItemType.magic:
            case ItemType.bullet:
                return true;
            case ItemType.Sword:
            case ItemType.MedKit:
            case ItemType.Bow:
            case ItemType.Knife:
            case ItemType.Katana:
            case ItemType.Bomb:
            case ItemType.Gun:
            case ItemType.QuickKnife:
            case ItemType.Wand:
            case ItemType.Mase:
                return false;
        }
    }

    public bool IsHoldable()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPosion:
            case ItemType.ManaPosion:
            case ItemType.Coin:
            case ItemType.MedKit:
            case ItemType.Arrow:
            case ItemType.magic:
            case ItemType.bullet:
                return false;
            case ItemType.Sword:
            case ItemType.Bow:
            case ItemType.Knife:
            case ItemType.Katana:
            case ItemType.Bomb:
            case ItemType.Gun:
            case ItemType.QuickKnife:
            case ItemType.Wand:
            case ItemType.Mase:
                return true;
        }
    }

    public bool ranged()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPosion:
            case ItemType.ManaPosion:
            case ItemType.Coin:
            case ItemType.MedKit:
            case ItemType.Arrow:
            case ItemType.magic:
            case ItemType.bullet:
            case ItemType.Mase:
            case ItemType.QuickKnife:
            case ItemType.Sword:
            case ItemType.Knife:
            case ItemType.Katana:
                return false;
            case ItemType.Bow:
            case ItemType.Bomb:
            case ItemType.Gun:
            case ItemType.Wand:
                return true;
        }
    }
}
