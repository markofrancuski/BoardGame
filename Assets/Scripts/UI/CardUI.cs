using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{

    #region UI References

    [Header("UI References - Text")]
    [SerializeField] private TextMeshProUGUI _manaCostText;
    [SerializeField] private TextMeshProUGUI _cardTypeText;
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [Header("UI References - Image")]
    [SerializeField] private Image _cardIconImage;

    #endregion UI References

    [Header("References")]
    [SerializeField] private CardBaseScriptable _card;

    public void SetCard(CardBaseScriptable card)
    {
        _card = card;
        UpdateUI();
        gameObject.SetActive(true);
    }
    private void UpdateUI()
    {
        _manaCostText.text = _card.ManaCost.ToString();
        _cardTypeText.text = StringHelper.GetCardType(_card.CardType);
        _cardNameText.text = _card.Name;

        _cardIconImage.sprite = _card.Icon;
    }

    /// <summary>
    /// Gets called when spawning card (Monster, Spell, Terrain).
    /// </summary>
    public void Action()
    {

    }

    public void SendToGraveyard()
    {

        Destroy(gameObject);
    }

}
