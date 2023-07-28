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

    public Sprite fireOrb;
    public Sprite waterOrb;
    public Sprite earthOrb;

    public List<SpellStatObjectUI> spellStatObjects;
    //Make sure SpellStatObjects is same length as allSpells by assigning in editor


    private Sprite getOrbImage (OrbManager.OrbType type)
    {
        switch (type)
        {
            case OrbManager.OrbType.Earth:
                return earthOrb;

            case OrbManager.OrbType.Fire:
                return fireOrb;

            case OrbManager.OrbType.Water:
                return waterOrb;

            default:
                return waterOrb;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < allSpells.Count; i++)
        {
            SpellStatObjectUI spellStatObject = spellStatObjects[i];
            Spell spell = allSpells[i];

            spellStatObject.spellName.text = spell.getName();
            spellStatObject.spellDesc.text = spell.GenerateDescription();
            List<OrbManager.OrbType> orbList= spell.GetComboString();
            spellStatObject.orb1.sprite = getOrbImage(orbList[0]);
            spellStatObject.orb2.sprite = getOrbImage(orbList[1]);
            spellStatObject.orb3.sprite = getOrbImage(orbList[2]);

        }


    }

}
