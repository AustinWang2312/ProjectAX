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

    private LayerMask layerMask;

    private Collider2D myCollider;
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

        myCollider = GetComponent<Collider2D>();
        layerMask = LayerMask.GetMask("PylonLayer");
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


        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouseWorldPoint);
        // Cast a ray from camera to mouse position
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPoint, Vector2.zero, Mathf.Infinity, layerMask);
        //Debug.Log(hit.transform);
        // Check if the ray hits your object
        if (hit.collider == myCollider)
        {
            //Debug.Log("Mouse is over the object");
            rend.material.color = Color.red;
            isHovering = true;
        }
        else
        {
            rend.material.color = Color.white;
            isHovering = false;
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
