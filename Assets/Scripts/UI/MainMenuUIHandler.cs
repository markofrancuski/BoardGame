using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _playGameCanvas;
    [SerializeField] private GameObject _deckCustomizationCanvas;
    [SerializeField] private GameObject _settingsCanvas;

    #region Public Methods
    public void PlayGame()
    {
        /// TODO: Should be another panel to choose game mode or it load new level
        if(!_playGameCanvas)
        {

            /// TODO: Delete this after
            SceneLoader.Instance.LoadScene(SceneLoader.GameSceneIndex);

            //PrintError("Play Game");
            return;
            
        }

        // Show/Hide Play Game Canvas
        ActivateCanvas(_playGameCanvas, _mainMenuCanvas);
    }

    public void DeckCustomization()
    {
        if (!_deckCustomizationCanvas)
        {
            PrintError("Deck Customization");
            return;
        }

        // Show/Hide Deck Customization Canvas
        ActivateCanvas(_deckCustomizationCanvas, _mainMenuCanvas);
    }

    public void Settings()
    {
        if (!_settingsCanvas)
        {
            PrintError("Settings");
            return;
        }

        // Show/Hide Settings Canvas
        ActivateCanvas(_settingsCanvas, _mainMenuCanvas);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion Public Methods

    #region Private Methods
    private void ActivateCanvas(GameObject canvasToActivate, GameObject mainMenuCanvas)
    {
        // Show/Hide Settings Panel
        bool show = !canvasToActivate.activeInHierarchy;
        canvasToActivate.SetActive(show);
        if (show) mainMenuCanvas.SetActive(true);
    }
    private void PrintError(string canvasName)
    {
        Debug.LogError($"Cannot Show/Hide {canvasName} Canvas missing reference");
    }
    #endregion Private Methods
}