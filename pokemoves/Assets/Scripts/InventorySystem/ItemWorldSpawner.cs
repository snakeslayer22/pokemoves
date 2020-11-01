using System;
using System.Collections;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour{

    public Item item;

    private void Awake()
    {
        IEnumerator coroutine = waitOneFrame();
        StartCoroutine(coroutine);
    }

    private IEnumerator waitOneFrame()
    {
        yield return null;

        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
