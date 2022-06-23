using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum EventName
{
    CreateChess,
    WhenAttackTakeDamage,
    WhenBeAttack,
    WhenDeath,
    GameOver,
    RestartGame,
    GameStart,
    WhenSceneLoad,
}
public struct EventMessage
{
    public Chess Chess;
}
public interface IEventAction
{
}
public class EventAction:IEventAction
{
    public UnityAction action;
}
public class EventAction<T> : IEventAction
{
    public UnityAction<T> action;
}
public class EventController 
{
    static EventController instance;
    public static EventController Instance
    {
        get
        {
            if (instance == null) instance = new EventController();
            return instance;
        }
    }
    public Dictionary<string, IEventAction> eventActionDic=new Dictionary<string, IEventAction>();
    //��Ӽ���
    public void AddListener(string name,UnityAction action)
    {
        if (!eventActionDic.ContainsKey(name)) eventActionDic.Add(name, new EventAction());
        (eventActionDic[name] as EventAction).action += action;
    }
    public void AddListener<T>(string name, UnityAction<T> action)
    {
        if (!eventActionDic.ContainsKey(name)) eventActionDic.Add(name, new EventAction());
        (eventActionDic[name] as EventAction<T>).action += action;
    }
    //�Ƴ�����
    public void RemoveListener(string name,UnityAction action)
    {
        if(eventActionDic.ContainsKey(name))
        (eventActionDic[name] as EventAction).action -= action;
    }
    public void RemoveListener<T>(string name, UnityAction<T> action)
    {
        if (eventActionDic.ContainsKey(name))
            (eventActionDic[name] as EventAction<T>).action -= action;
    }
    //�����¼�
    public void TriggerEvent(string name)
    {
        if (eventActionDic.ContainsKey(name))
            (eventActionDic[name] as EventAction).action?.Invoke();
    }
    public void TriggerEvent<T>(string name,T message)
    {
        if (eventActionDic.ContainsKey(name))
        {
            (eventActionDic[name] as EventAction<T>).action?.Invoke(message);
        }
    }
}
