using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Every Mage Element implements this interface (One Basic Attack, 2 spells and an Ultimate along with some cooldown methods for UI purposes)
/// </summary>
public interface ISpells
{
    
    /// <summary>
    /// Reference to a basic attack
    /// </summary>
    void BasicAttack();

    /// <summary>
    /// Reference to the first spell
    /// </summary>
    void FirstSpell();

    /// <summary>
    /// Reference to the second spell
    /// </summary>
    void SecondSpell();

    /// <summary>
    /// Reference to the ultimate
    /// </summary>
    void Ultimate();

    float getFirstCd();

    float getNextFirstCd();

    float getSecondCd();

    float getNextSecondCd();

    float getUltimateCd();

    float getNextUltimateCd();

}
