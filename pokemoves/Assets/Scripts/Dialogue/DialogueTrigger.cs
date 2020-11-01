using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour{

    public Dialogue dialogue;

    public bool clickE = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!clickE && col.tag == "dialogue")
        {
            TriggerDialogue();
            FindObjectOfType<PlayerMovement>().ableToMove = false;
            clickE = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (clickE && Input.GetKeyDown(KeyCode.E) && collision.tag == "dialogue")
        {
            TriggerDialogue();
            FindObjectOfType<PlayerMovement>().ableToMove = false;
        }
    }
}
