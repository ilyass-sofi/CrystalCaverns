using UnityEngine;

[CreateAssetMenu(menuName = "Mage")]
public class MageAsset : ScriptableObject
{
    [Header("Atributes")]
    public string mageName;
    public float baseHealth;
    public float baseDamage;
    public string path;
    public Sprite splashArt;

    [Header("Spells")]
    public SpellAsset passiveSpell;
    public SpellAsset basicAttack;
    public SpellAsset firstSpell;
    public SpellAsset secondSpell;
    public SpellAsset ultimate;
}
