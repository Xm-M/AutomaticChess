using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float interval = 1f;
    public float manaCost;
    public Chess controller;
    public AudioSource audio;
    protected void Start()
    {
        if(audio == null)
        audio = GetComponent<AudioSource>();
        EventController.Instance.AddListener(EventName.GameOver.ToString(), OnSkillExit);
    }
    public virtual void OnSkillEnter() { }
    public virtual void SkillEffect() { }
    public virtual void OnSkillExit()
    {
        EventController.Instance.RemoveListener(EventName.GameOver.ToString(), OnSkillExit);
        ObjectPool.instance.Recycle(gameObject);   
    }
    public void PlayAudioa()
    {
        if(audio != null)
        audio.Play();
    }
}
