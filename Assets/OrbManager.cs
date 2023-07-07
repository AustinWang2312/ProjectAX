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

    private Dictionary<string, System.Action> combinationActions;

    public enum OrbType
    {
        Earth,
        Water,
        Fire,
        Empty
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Orbs carried
        carriedOrbs = new OrbType[maxOrbs];
        for (int i = 0; i < carriedOrbs.Length; i++)
        {
            carriedOrbs[i] = OrbType.Empty;
        }
        carriedOrbs[0] = OrbType.Earth;
        carriedOrbs[1] = OrbType.Earth;
        carriedOrbs[2] = OrbType.Earth;
        currentOrbs = 3;


        //Initialize Combination hash map
        combinationActions = new Dictionary<string, System.Action>();
        for (int orb1 = 0; orb1 < 3; orb1++)
        {
            for (int orb2 = 0; orb2 < 3; orb2++)
            {
                for (int orb3 = 0; orb3 < 3; orb3++)
                {
                    for (int click = 0; click < 2; click++)
                    {
                        string combinationKey = $"{orb1}{orb2}{orb3}{click}";
                        Debug.Log(combinationKey);
    
                        System.Action action = GetCombinationAction(orb1, orb2, orb3, click);
                        combinationActions.Add(combinationKey, action);
                    }
                }
            }
    }

        UpdateOrbHUD();
        
    }


    private string GetOrbTypeName(int orbValue)
    {
        switch (orbValue)
        {
            case 0:
                return "Earth";
            case 1:
                return "Water";
            case 2:
                return "Fire";
            default:
                return "Empty";
        }
    }


    private System.Action GetCombinationAction(int orb1, int orb2, int orb3, int click)
    {
        string stringOrb1 = ((OrbType) orb1).ToString();
        string stringOrb2 = ((OrbType) orb2).ToString();
        string stringOrb3 = ((OrbType) orb3).ToString();
        string methodName = $"CombinationAction_"+stringOrb1+stringOrb2+stringOrb3+click;
        Debug.Log(methodName);
        System.Action action = null;

        // Use reflection to retrieve the method dynamically
        System.Reflection.MethodInfo methodInfo = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (methodInfo != null)
        {
            action = (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), this, methodInfo);
        }

        return action;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(CAST) || Input.GetMouseButtonDown(FORGE)) // Check for left mouse button click
        {
            if (currentOrbs == maxOrbs)
            {
                int method = Input.GetMouseButtonDown(CAST) ? CAST : FORGE; // 0 for left click, 1 for right click
                string key = $"{(int)carriedOrbs[0]}{(int)carriedOrbs[1]}{(int)carriedOrbs[2]}{method}";
                Debug.Log(key);
                System.Action action = combinationActions[key];
                Debug.Log(combinationActions[key]);
                action.Invoke();

                // Deplete all orbs back to the empty orb
                for (int i = 0; i < carriedOrbs.Length; i++)
                {
                    carriedOrbs[i] = OrbType.Empty;
                }

                currentOrbs = 0;
                UpdateOrbHUD();
            }
        }
        
    }




    public bool AddOrb(OrbType type)
    {
        if (currentOrbs < carriedOrbs.Length) {
            carriedOrbs[currentOrbs] = type;
            currentOrbs += 1;
            UpdateOrbHUD();
            return true;
        }
        else
        {
            return false;
        }

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


    // 1 Cast Earth Water Fire
    private void CombinationAction_EarthWaterFire0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthWaterFire");
    }

    // 2 Forge Earth Water Fire
    private void CombinationAction_EarthWaterFire1()
    {
        Debug.Log("Performing action for Right Click EarthWaterFire");
    }

    // 3 Cast Earth Fire Water
    private void CombinationAction_EarthFireWater0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthFireWater");
    }

    // 4 Forge Earth Fire Water
    private void CombinationAction_EarthFireWater1()
    {
        Debug.Log("Performing action for Right Click EarthFireWater");
    }

    // 5 Cast Earth Earth Water
    private void CombinationAction_EarthEarthWater0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthEarthWater");
    }

    // 6 Forge Earth Earth Water
    private void CombinationAction_EarthEarthWater1()
    {
        Debug.Log("Performing action for Right Click EarthEarthWater");
    }

    // 7 Cast Earth Earth Fire
    private void CombinationAction_EarthEarthFire0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthEarthFire");
    }

    // 8 Forge Earth Earth Fire
    private void CombinationAction_EarthEarthFire1()
    {
        Debug.Log("Performing action for Right Click EarthEarthFire");
    }

    // 9 Cast Earth Earth Earth
    private void CombinationAction_EarthEarthEarth0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthEarthEarth");
    }

    // 10 Forge Earth Earth Earth
    private void CombinationAction_EarthEarthEarth1()
    {

        Debug.Log("Performing action for Right Click EarthEarthEarth");
        GameObject pylon = (GameObject)Instantiate(Resources.Load<GameObject>("Pylon"), transform.position, Quaternion.identity);
        Debug.Log("pylon object");
        Debug.Log(pylon);
        GameObject player = this.gameObject;
        pylon.GetComponent<Pylon>().SetPlayerObject(player);
        pylon.GetComponent<Pylon>().SetOrbType(OrbType.Earth, earthOrb, emptyOrb);
    }

    // void SpawnZombie() 
    // { 
    //     GameObject ZombieClone = (GameObject)Instantiate(ZombiePrefab, RandomSpawnPoint(), Quaternion.identity);
    //     //Spawns a copy of ZombiePrefab at SpawnPoint
    //     ZombieClone.GetComponent<YourScript>().SetHealth(healthValue); 

    //     public ZombieClass ZombiePrefab;

    //     ZombieClass zombieInstance = Instantiate(ZombiePrefab, RandomSpawnPoint(), Quaternion.identity) as ZombieClass;
    //     zombieInstance.SetHealth(healthValue);
    // }

    // 11 Cast Earth Fire Earth
    private void CombinationAction_EarthFireEarth0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthFireEarth");
        

    }

    // 12 Forge Earth Fire Earth
    private void CombinationAction_EarthFireEarth1()
    {
        Debug.Log("Performing action for Right Click EarthFireEarth");
        
    }

    // 13 Cast Earth Fire Earth
    private void CombinationAction_EarthWaterEarth0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthWaterEarth");
    }

    // 14 Forge Earth Water Earth
    private void CombinationAction_EarthWaterEarth1()
    {
        Debug.Log("Performing action for Right Click EarthWaterEarth");
    }

    // 15 Cast Earth Water Water
    private void CombinationAction_EarthWaterWater0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthWaterWater");
    }

    // 16 Forge Earth Water Water
    private void CombinationAction_EarthWaterWater1()
    {
        Debug.Log("Performing action for Right Click EarthWaterWater");
    }

    // 17 Cast Earth Water Water
    private void CombinationAction_EarthFireFire0()
    {
        // Action for left-click Orb1=0, Orb2=1, Orb3=2 combination
        Debug.Log("Performing action for Left Click EarthFireFire");
    }

    // 18 Forge Earth Water Water
    private void CombinationAction_EarthFireFire1()
    {
        Debug.Log("Performing action for Right Click EarthWFireFire");
    }

    // 19 Cast Water Earth Fire
    private void CombinationAction_WaterEarthFire0()
    {
        Debug.Log("Performing action for Left Click WaterEarthFire");
    }

    // 20 Forge Water Earth Fire
    private void CombinationAction_WaterEarthFire1()
    {
        Debug.Log("Performing action for Right Click WaterEarthFire");
    }

    // 21 Cast Water Fire Earth
    private void CombinationAction_WaterFireEarth0()
    {
        Debug.Log("Performing action for Left Click WaterFireEarth");
    }

    // 22 Forge Water Fire Earth
    private void CombinationAction_WaterFireEarth1()
    {
        Debug.Log("Performing action for Right Click WaterFireEarth");
    }

    // 23 Cast Water Earth Water
    private void CombinationAction_WaterEarthWater0()
    {
        Debug.Log("Performing action for Left Click WaterEarthWater");
    }

    // 24 Forge Water Earth Water
    private void CombinationAction_WaterEarthWater1()
    {
        Debug.Log("Performing action for Right Click WaterEarthWater");
    }

    // 25 Cast Water Fire Fire
    private void CombinationAction_WaterFireFire0()
    {
        Debug.Log("Performing action for Left Click WaterFireFire");
    }

    // 26 Forge Water Fire Fire
    private void CombinationAction_WaterFireFire1()
    {
        Debug.Log("Performing action for Right Click WaterFireFire");
    }

    // 27 Cast Water Earth Earth
    private void CombinationAction_WaterEarthEarth0()
    {
        Debug.Log("Performing action for Left Click WaterEarthEarth");
    }

    // 28 Forge Water Earth Earth
    private void CombinationAction_WaterEarthEarth1()
    {
        Debug.Log("Performing action for Right Click WaterEarthEarth");
    }


    // 29 Cast Water Water Earth
    private void CombinationAction_WaterWaterEarth0()
    {
        Debug.Log("Performing action for Left Click WaterWaterEarth");
    }

    // 30 Forge Water Water Earth
    private void CombinationAction_WaterWaterEarth1()
    {
        Debug.Log("Performing action for Right Click WaterWaterEarth");
    }

    // 31 Cast Water Water Fire
    private void CombinationAction_WaterWaterFire0()
    {
        Debug.Log("Performing action for Left Click WaterWaterFire");
    }

    // 32 Forge Water Water Fire
    private void CombinationAction_WaterWaterFire1()
    {
        Debug.Log("Performing action for Right Click WaterWaterFire");
    }

    // 33 Cast Water Water Water
    private void CombinationAction_WaterWaterWater0()
    {
        Debug.Log("Performing action for Left Click WaterWaterWater");
    }

    // 34 Forge Water Water Water
    private void CombinationAction_WaterWaterWater1()
    {
        Debug.Log("Performing action for Right Click WaterWaterWater");
    }

    // 35 Cast Water Fire Water
    private void CombinationAction_WaterFireWater0()
    {
        Debug.Log("Performing action for Left Click WaterFireWater");
    }

    // 36 Forge Water Fire Water
    private void CombinationAction_WaterFireWater1()
    {
        Debug.Log("Performing action for Right Click WaterFireWater");
    }



    // 37 Cast Fire Water Water
    private void CombinationAction_FireWaterWater0()
    {
        Debug.Log("Performing action for Left Click FireWaterWater");
    }

    // 38 Forge Fire Water Water
    private void CombinationAction_FireWaterWater1()
    {
        Debug.Log("Performing action for Right Click FireWaterWater");
    }


    // 39 Cast Fire Earth Water
    private void CombinationAction_FireEarthWater0()
    {
        Debug.Log("Performing action for Left Click FireEarthWater");
    }

    // 40 Forge Fire Earth Water
    private void CombinationAction_FireEarthWater1()
    {
        Debug.Log("Performing action for Right Click FireEarthWater");
    }

    // 41 Cast Fire Earth Fire
    private void CombinationAction_FireEarthFire0()
    {
        Debug.Log("Performing action for Left Click FireEarthFire");
    }

    // 42 Forge Fire Earth Fire
    private void CombinationAction_FireEarthFire1()
    {
        Debug.Log("Performing action for Right Click FireEarthFire");
    }

    // 43 Cast Fire Earth Earth
    private void CombinationAction_FireEarthEarth0()
    {
        Debug.Log("Performing action for Left Click FireEarthEarth");
    }

    // 44 Forge Fire Earth Earth
    private void CombinationAction_FireEarthEarth1()
    {
        Debug.Log("Performing action for Right Click FireEarthEarth");
    }

    // 45 Cast Fire Water Earth
    private void CombinationAction_FireWaterEarth0()
    {
        Debug.Log("Performing action for Left Click FireWaterEarth");
    }

    // 46 Forge Fire Water Earth
    private void CombinationAction_FireWaterEarth1()
    {
        Debug.Log("Performing action for Right Click FireWaterEarth");
    }

    // 47 Cast Fire Water Fire
    private void CombinationAction_FireWaterFire0()
    {
        Debug.Log("Performing action for Left Click FireWaterFire");
    }

    // 48 Forge Fire Water Fire
    private void CombinationAction_FireWaterFire1()
    {
        Debug.Log("Performing action for Right Click FireWaterFire");
    }

    // 49 Cast Fire Fire Fire
    private void CombinationAction_FireFireFire0()
    {
        Debug.Log("Performing action for Left Click FireFireFire");
    }

    // 50 Forge Fire Fire Fire
    private void CombinationAction_FireFireFire1()
    {
        Debug.Log("Performing action for Right Click FireFireFire");
    }

    // 51 Cast Fire Fire Earth
    private void CombinationAction_FireFireEarth0()
    {
        Debug.Log("Performing action for Left Click FireFireEarth");
    }

    // 52 Forge Fire Fire Earth
    private void CombinationAction_FireFireEarth1()
    {
        Debug.Log("Performing action for Right Click FireFireEarth");
    }

    // 53 Cast Fire Fire Water
    private void CombinationAction_FireFireWater0()
    {
        Debug.Log("Performing action for Left Click FireFireWater");
    }

    // 54 Forge Fire Fire Water
    private void CombinationAction_FireFireWater1()
    {
        Debug.Log("Performing action for Right Click FireFireWater");
    }



    

    
}
