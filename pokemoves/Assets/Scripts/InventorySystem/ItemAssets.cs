using System;
using System.Collections;
using UnityEngine;

public class ItemAssets : MonoBehaviour{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite healthPosionSprite;
    public Sprite manaPosionSprite;
    public Sprite coinSprite;
    public Sprite medKitSprite;


    public Sprite holdSwordSpite;
    public Sprite holdKnifeSprite;
    public Sprite holdKatanaSprite;
    public Sprite holdMaseSprite;
    public Sprite holdWandSprite;
    public Sprite holdBowSprite;
    public Sprite holdGunSprite;
    public Sprite holdBombSprite;
    public Sprite holdQuickKnifeSprite;


    public Sprite lookTarget;

    public Sprite arrowSprite;
    public Sprite magicSprite;
    public Sprite bulletSprite;
}
