using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{

    public Renderer rend;
    private OrbManager.OrbType elementalType = OrbManager.OrbType.Earth;
    public bool isHovering = false;
    [SerializeField] GameObject player;
    OrbManager orbManager;
    public Sprite orbType;
    public float energyGenerationInterval = 15f;
    private float nextEnergyGenerationTime;
    public bool hasOrb = false;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite orbOnSprite; // Sprite to display when the pylon has an orb
    public Sprite orbOffSprite; // Sprite to display when the pylon does not have an orb

    public void SetPlayerObject(GameObject p)
    {
        player = p;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        orbManager = player.GetComponent<OrbManager>();

        nextEnergyGenerationTime = Time.time + energyGenerationInterval;

        // Get the SpriteRenderer component attached to the pylon
        spriteRenderer.sprite = orbOffSprite;
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        rend.material.color = Color.red;
        isHovering = true;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = Color.white;
        isHovering = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isHovering && hasOrb)
        {
            orbManager.AddOrb(elementalType);
            nextEnergyGenerationTime = Time.time + energyGenerationInterval;
            hasOrb = false;
            UpdatePylonSprite();
        }

        if (Time.time >= nextEnergyGenerationTime)
        {
            // Generate an energy orb of the pylon's elemental type
            hasOrb = true;
            UpdatePylonSprite();
        }


    }


    void UpdatePylonSprite()
    {
        if (hasOrb)
        {
            // Set the sprite to the orb on sprite
            spriteRenderer.sprite = orbOnSprite;
        }
        else
        {
            // Set the sprite to the orb off sprite
            spriteRenderer.sprite = orbOffSprite;
        }
    }



}
