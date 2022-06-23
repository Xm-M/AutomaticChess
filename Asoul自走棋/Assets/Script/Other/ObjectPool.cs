using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public Scene poolScene;
    Dictionary<string, Stack<GameObject>> objectPool;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        objectPool = new Dictionary<string, Stack<GameObject>>();
        poolScene = SceneManager.CreateScene(name);
    }
    public GameObject Create(GameObject a)
    {
        GameObject creat;
        if (objectPool.ContainsKey(a.name))
        {
            if (objectPool[a.name].Count != 0)
            {
                creat= objectPool[a.name].Pop();
                if (creat)
                {
                    creat.SetActive(true);
                    return creat;
                }
            }          
        }
        else
        {
            objectPool.Add(a.name, new Stack<GameObject>());
        }        
        creat = Instantiate(a);
        if (creat.GetComponent<Chess>())
        {
            Chess.numOfChess++;
            creat.GetComponent<Chess>().instanceID = Chess.numOfChess;
        }
        SceneManager.MoveGameObjectToScene(creat, poolScene);
        return creat;
    }
    public void Recycle(GameObject a)
    {
        string name = a.name.Replace("(Clone)", "");
        if(!objectPool.ContainsKey(name))objectPool.Add(name, new Stack<GameObject>());
        objectPool[name].Push(a);
        a.SetActive(false);
    }
}
