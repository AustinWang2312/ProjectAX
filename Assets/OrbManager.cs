using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrbManager : MonoBehaviour
{
    public int maxOrbs = 3;
    private int currentOrbs = 0;
    private OrbType[] carriedOrbs;
    public Image[] orbIcons;
    public Sprite emptyOrb;
    public Sprite earthOrb;
    public Sprite waterOrb;
    public Sprite fireOrb;

    const int FORGE = 1;
    const int CAST = 0;


    public enum OrbType
    {
        Empty,
        Earth,
        Water,
        Fire
    }

    // Start is called before the first frame update
    void Start()
    {
        carriedOrbs = new OrbType[maxOrbs];
        for (int i = 0; i < carriedOrbs.Length; i++)
        {
            carriedOrbs[i] = OrbType.Empty;
        }
        carriedOrbs[0] = OrbType.Earth;
        carriedOrbs[1] = OrbType.Water;
        carriedOrbs[2] = OrbType.Fire;
        currentOrbs = 3;

        UpdateOrbHUD();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(CAST) || Input.GetMouseButtonDown(FORGE)) // Check for left mouse button click
        {
            if (currentOrbs == 3)
            {
                int method = Input.GetMouseButtonDown(CAST) ? CAST : FORGE; // 0 for left click, 1 for right click
                Invoke(method);
            }
        }
        
    }

    private void Invoke(int method)
    {
        // Perform the action based on the unique combination
        if (carriedOrbs[0] == OrbType.Earth && carriedOrbs[1] == OrbType.Water && carriedOrbs[2] == OrbType.Fire)
        {
            // Action for Earth-Water-Fire combination
            if (method == CAST)
            {
                Debug.Log("Performing action for Earth-Water-Fire combination on left click!");
            }
            else if (method == FORGE)
            {
                Debug.Log("Performing action for Earth-Water-Fire combination on right click!");
            }
        }
        else if (carriedOrbs[0] == OrbType.Earth && carriedOrbs[1] == OrbType.Fire && carriedOrbs[2] == OrbType.Water)
        {
            // Action for Earth-Fire-Water combination
            if (method == CAST)
            {
                Debug.Log("Performing action for Earth-Fire-Water combination on left click!");
            }
            else if (method == FORGE)
            {
                Debug.Log("Performing action for Earth-Fire-Water combination on right click!");
            }
        }
        else if (carriedOrbs[0] == OrbType.Water && carriedOrbs[1] == OrbType.Earth && carriedOrbs[2] == OrbType.Fire)
        {
            // Action for Water-Earth-Fire combination
            if (method == CAST)
            {
                Debug.Log("Performing action for Water-Earth-Fire combination on left click!");
            }
            else if (method == FORGE)
            {
                Debug.Log("Performing action for Water-Earth-Fire combination on right click!");
            }
        }
        // Add more conditions for other permutations

        // Deplete all orbs back to the empty orb
        for (int i = 0; i < carriedOrbs.Length; i++)
        {
            carriedOrbs[i] = OrbType.Empty;
        }

        currentOrbs = 0;

        UpdateOrbHUD();
    }

    



    private void UpdateOrbHUD()
    {
        for (int i = 0; i < orbIcons.Length; i++)
        {
            switch (carriedOrbs[i])
            {
                case OrbType.Empty:
                    orbIcons[i].sprite = emptyOrb;
                    break;

                case OrbType.Earth:
                    orbIcons[i].sprite = earthOrb;
                    break;
                case OrbType.Water:
                    orbIcons[i].sprite = waterOrb;
                    break;
                case OrbType.Fire:
                    orbIcons[i].sprite = fireOrb;
                    break;
            }
        }
    }
}
