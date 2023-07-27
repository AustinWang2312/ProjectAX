using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstantiator : MonoBehaviour
{
    // Assign these in the inspector
    public GameObject playerPrefab;
    public GameObject hudPrefab;
    public GameObject soundManager;
    public GameObject damageTextPool;
    public GameObject buttonManager;



    private GameObject playerInstance;
    private GameObject hudInstance;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = Instantiate(playerPrefab);
        hudInstance = Instantiate(hudPrefab);

        Instantiate(soundManager);
        Instantiate(damageTextPool);
        Instantiate(buttonManager);

        // Get the Canvas component
        Canvas hudCanvas = hudInstance.GetComponent<Canvas>();
        hudCanvas.worldCamera = Camera.main;

        PlayerHealth playerHealth = playerInstance.GetComponent<PlayerHealth>();
        PlayerController playerController = playerInstance.GetComponent<PlayerController>();
        OrbManager orbManager = playerInstance.GetComponent<OrbManager>();

        PlayerHealthBar healthBar  = hudInstance.transform.Find("Character State Hud/Health Bar").GetComponent<PlayerHealthBar>();
        playerHealth.healthBar = healthBar;
        healthBar.playerHealth = playerHealth;
        playerHealth.healthBar.UpdateHealthBar();


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
