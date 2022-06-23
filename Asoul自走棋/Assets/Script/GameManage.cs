using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManage : MonoBehaviour
{
    public float BaseThunderDamage;
    public static GameManage instance;
    public Camera mainCamera;
    public Chess HandChess;
    List<Chess> moveQueue;
    public List<GameObject> allChess;
    public List<GameObject> PlayerChess;
    public List<State> states;
    public bool ifGameStart;
    public Dictionary<string, FetterManage> teams;
    public GameObject window;
    public List<AIState> aiStates;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        teams = new Dictionary<string, FetterManage>();
        moveQueue = new List<Chess>();
        teams.Add("Player", new FetterManage());
        teams.Add("Enemy", new FetterManage());
    }
    private void Start()
    {
        window.SetActive(true);
        EventController.Instance.AddListener(EventName.WhenSceneLoad.ToString(),
            () => window.SetActive(true));
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) EventController.Instance.TriggerEvent("space");
        if (ifGameStart)
        {
            MoveControl();
        }
    }
    public void GameStart()
    {
        ifGameStart = true;
        EventController.Instance.TriggerEvent(EventName.GameStart.ToString());
        moveQueue.Clear();
    }
    public void AddMoveChess(Chess chess)
    {
        if (!moveQueue.Contains(chess))
        {
            moveQueue.Add(chess);
        }
    }
    public void GameOver(string tag)
    {
        ifGameStart = false;
        EventController.Instance.TriggerEvent(EventName.GameOver.ToString());
        if (tag == "Player")
        {
            UIManage.instance.Lose();
        }
        else UIManage.instance.Win();
        
    }
    public void RestartGame()
    {
        UIManage.instance.OpenWindow(window);
        EventController.Instance.TriggerEvent(EventName.RestartGame.ToString());
        foreach (var a in teams)
        {
            a.Value.Reset();
        }
    }
    public void MoveControl()
    {
        for (int i = 0; i < moveQueue.Count && i < 50; i++)
        {
            if(moveQueue[i])
            moveQueue[i].MoveToNextTile();
        }
        moveQueue.Clear();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
