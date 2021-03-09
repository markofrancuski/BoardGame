using Enums.Game;
using UnityEngine;

[CreateAssetMenu(fileName = "Pawn Card", menuName = "Assets/Cards/Pawn Card", order = 0)]
public class CardPawnScriptable : CardBaseScriptable
{
    public GameObject PawnModel;

    public PawnActions[] Actions;

    public override void SpawnCard()
    {
    }

    public struct PawnActions
    {
        public PawnAction Action;

    }
}
