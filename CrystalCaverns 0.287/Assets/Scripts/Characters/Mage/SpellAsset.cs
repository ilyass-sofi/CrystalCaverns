using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell")]
public class SpellAsset : ScriptableObject {

    [Header("Attributes")]
    public string spellName;

    [TextArea]
    public string description;
    public Sprite sprite;
    public string path;
    public float cooldown;

    //public float damageMultiplicator;

    //public float timeDestroy;

    //[Header("Status Applied")]
    //public EffectOverTime effect;

    //[Header("Area of effect")]
    //public InstantZone instantZone;

    //[Header("Bonus Damage Effect")]
    //public EffectOverTime bonusEffect;
    //public float bonusDamageMultiplicator;
}
