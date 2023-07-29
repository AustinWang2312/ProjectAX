using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{

    public Renderer rend;
    public OrbManager.OrbType elementalType;
    public bool isHovering = false;
    [SerializeField] GameObject player;
    OrbManager orbManager;
    public float energyGenerationInterval = 15f;
    private float nextEnergyGenerationTime;
    public bool hasOrb = false;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite orbOnSprite; // Sprite to display when the pylon has an orb
    public Sprite orbOffSprite; // Sprite to display when the pylon does not have an orb

    public void SetPlayerObject(GameObject p)
    {
        player = p;
        orbManager = player.GetComponent<OrbManager>();
    }

    public void SetOrbType(OrbManager.OrbType type, Sprite onSprite, Sprite offSprite)
    {
        elementalType = type;
        orbOnSprite = onSprite;
        orbOffSprite = offSprite;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        if(player)
        {
            orbManager = player.GetComponent<OrbManager>();
        }
        

        nextEnergyGenerationTime = Time.time + energyGenerationInterval;

        // Get the SpriteRenderer component attached to the pylon
        spriteRenderer.sprite = orbOffSprite;


     }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cursor"))
        {
            rend.material.color = Color.red;
            isHovering = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cursor"))
        {
            rend.material.color = Color.white;
            isHovering = false;
        }
    }



    void Update()
    {
        if(!orbManager)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player)
            {
                this.orbManager = player.GetComponent<OrbManager>();
            }

            
        }


  



        if (Input.GetKeyDown(KeyCode.Space) && isHovering)
        {
            if(!hasOrb)
            {
                SoundManager.instance.PlaySound("Deny Orb");
                return;
            }
            if (orbManager.AddOrb(elementalType))
            {
                nextEnergyGenerationTime = Time.time + energyGenerationInterval;
                hasOrb = false;
                UpdatePylonSprite();
                SoundManager.instance.PlaySound("Absorb Orb");
            }
            else
            {
                SoundManager.instance.PlaySound("Deny Orb");
            }
            
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
