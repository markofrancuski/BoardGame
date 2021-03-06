using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{

    [SerializeField] private GameObject _playGameCanvas;
    [SerializeField] private GameObject _deckCustomizationCanvas;
    [SerializeField] private GameObject _settingsCanvas;

    public void PlayGame()
    {
        if(!_playGameCanvas)
        {
            PrintError("Play Game");
            return;
        }

        // Show/Hide Play Game Panel
        _playGameCanvas.SetActive(!_playGameCanvas.activeInHierarchy);
    }

    public void DeckCustomization()
    {
        if (!_deckCustomizationCanvas)
        {
            PrintError("Deck Customization");
            return;
        }

        // Show/Hide Deck Customization Panel
        _deckCustomizationCanvas.SetActive(!_deckCustomizationCanvas.activeInHierarchy);
    }

    public void Settings()
    {
        if (!_settingsCanvas)
        {
            PrintError("Settings");
            return;
        }

        // Show/Hide Settings Panel
        _settingsCanvas.SetActive(!_settingsCanvas.activeInHierarchy);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void PrintError(string canvasName)
    {
        Debug.LogError($"Cannot Show/Hide {canvasName} Canvas missing reference");
    }
}
