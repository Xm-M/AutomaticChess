using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelsoftGames.PixelUI;
public class UIManage : MonoBehaviour
{
    public static UIManage instance;
    public UITabbedWindow gameOver;
    public ChessMessage chessMessage;
    void Start()
    {
        EventController.Instance.AddListener(EventName.GameOver.ToString(), () => gameOver.gameObject.SetActive(true));
        EventController.Instance.AddListener(EventName.WhenSceneLoad.ToString(), Close);
    }
    public void Close()
    {
        gameOver.gameObject.SetActive(false);
    }
    public void Win()
    {
        gameOver.ActivateContentPane(0);
    }
    public void Lose()
    {
        gameOver.ActivateContentPane(1);
    }
    private void Awake()
    {
        if (instance == null||instance!=this) instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Quit() => Application.Quit();
    public void CloseWindow(GameObject window) => window.SetActive(false);
    public void OpenWindow(GameObject window) => window.SetActive(true);
    public void ShowChess(Chess chess)
    {
        chessMessage.ShowChessMessage(chess);
    }
}
