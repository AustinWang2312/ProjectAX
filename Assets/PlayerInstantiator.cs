using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInstantiator : MonoBehaviour
{
    // Assign these in the inspector
    public GameObject playerPrefab;
    public GameObject hudPrefab;
    public GameObject soundManager;
    public GameObject damageTextPool;
    public GameObject buttonManager;
    //public Transform cursor;



    private GameObject playerInstance;
    private GameObject hudInstance;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = Instantiate(playerPrefab);
        hudInstance = Instantiate(hudPrefab);

        //Instantiate Singleton Manger Objects
        Instantiate(soundManager);
        Instantiate(damageTextPool);
        Instantiate(buttonManager);
        //Instantiate(cursor);

        // Get the Hud Canvas component
        Canvas hudCanvas = hudInstance.GetComponent<Canvas>();
        hudCanvas.worldCamera = Camera.main;


        //Get character state hud
        Transform charStateHud = hudInstance.transform.Find("Character State Hud");

        //Get Player related components
        PlayerHealth playerHealth = playerInstance.GetComponent<PlayerHealth>();
        PlayerController playerController = playerInstance.GetComponent<PlayerController>();
        OrbManager orbManager = playerInstance.GetComponent<OrbManager>();
        orbManager.cursorPoint = GameObject.Find("Cursor").transform;

        
        //Setup Healthbar
        PlayerHealthBar healthBar  = charStateHud.Find("Health Bar").GetComponent<PlayerHealthBar>();
        playerHealth.healthBar = healthBar;
        healthBar.playerHealth = playerHealth;
        playerHealth.healthBar.UpdateHealthBar();


        //Get orb huds
        Transform orbHud = charStateHud.Find("Orbs Hud");
        Transform backupOrbHud = charStateHud.Find("Backup Orbs Hud");

        Debug.Log(orbHud);

        //Set up Orbs
        for (int i = 0; i < 3; i++)
        {
            string orbName = "Orb " + (i+1);
            Image orbImage = orbHud.Find(orbName).GetComponent<Image>();
            Debug.Log("orbImage", orbImage);
            Image backupOrbImage = backupOrbHud.Find(orbName).GetComponent<Image>();
            orbManager.orbIcons[i] = orbImage;
            orbManager.backupOrbIcons[i] = backupOrbImage;

        }


        //Set up Spell Hud
        for (int i = 0; i < 9; i++)
        {
            string descriptorName = "Spell Descriptor " + "(" + i + ")";
            Transform descriptor = hudInstance.transform.Find(descriptorName);

            TextMeshProUGUI spellName = descriptor.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            orbManager.spellNames[i] = spellName;

            for (int j = 0; j < 3; j++)
            {
                string orbName = "Orb " + (j + 1);
                Image orbImage = descriptor.Find(orbName).GetComponent<Image>();
                orbManager.orbImageSets[i].orbImages[j] = orbImage;
            }

        }




    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
