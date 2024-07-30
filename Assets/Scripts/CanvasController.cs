using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

public class CanvasController : MonoBehaviour
{
    private Canvas mainMenuCanvas;
    private Canvas settingsCanvas;
    private Canvas gameConfigCanvas;

    [Inject]
    private void Construct(
        [Inject(Id = Canvases.MainMenu)] Canvas mainMenuCanvasInjected,
        [Inject(Id = Canvases.Settings)] Canvas settingsCanvasInjected,
        [Inject(Id = Canvases.GameConfig)] Canvas configCanvasInjected)

    {
        mainMenuCanvas = mainMenuCanvasInjected;
        settingsCanvas = settingsCanvasInjected;
        gameConfigCanvas = configCanvasInjected;
    }

    public void BackToTheMainMenu()
    {
        gameConfigCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void EnableSettings()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToConfig()
    {
        gameConfigCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
    }
    
    public void StartGame()
    {
    }
}