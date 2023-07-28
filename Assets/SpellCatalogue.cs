using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellCatalogue : MonoBehaviour
{
    public List<Spell> allSpells = new List<Spell>() // list to store all spells
    {
        new Fireball(),
        new EarthShield(),
        new Geyser(),
        new Icicle(),
        new Tarpit(),
        new Boulder(),
        new Flamebreath(),
        new GreekFire(),
        new Sunstrike()
    };

    public TextMeshProUGUI [] descriptions;
 
    // Start is called before the first frame update
    void Start()
    {
        Flamebreath test = new Flamebreath();
        string teststr = test.GenerateDescription();

        descriptions[0].SetText(teststr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
