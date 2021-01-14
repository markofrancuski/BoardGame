using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class CardBaseScriptable : ScriptableObject
{
    public virtual CardType CardType => CardType.NORMAL;
    public string Name;
    public string Description;
    public int ManaCost;

    public virtual void ActivateCard()
    {

    }
}
