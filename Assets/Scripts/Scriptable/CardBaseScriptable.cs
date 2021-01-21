using Enums;
using UnityEngine;

public abstract class CardBaseScriptable : ScriptableObject
{
    public virtual CardType CardType => CardType.NORMAL;
    public string Name;
    public string Description;
    public int ManaCost;

    public Sprite Icon;

    public virtual void SpawnCard()
    {

    }

}
