using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Card", menuName = "Assets/Terrain Card", order = 0)]
public class CardTerrainScriptable : CardBaseScriptable
{
    public override CardType CardType => CardType.TERRAIN;
}
