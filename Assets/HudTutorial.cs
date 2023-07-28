using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTutorial : MonoBehaviour
{
    public GameObject obj1; // Drag the text object in here in the editor
    public GameObject obj2; // Drag the text object in here in the editor
    public GameObject obj3; // Drag the text object in here in the editor


    // This method gets called when another object enters this one's trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // If it is, show the tutorial text
            obj1.SetActive(true);
            obj2.SetActive(true);
            obj3.SetActive(true);
        }
    }

    // This method gets called when another object leaves this one's trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object that left the trigger is the player
        if (other.CompareTag("Player"))
        {
            // If it is, hide the tutorial text
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(false);
        }
    }
}
