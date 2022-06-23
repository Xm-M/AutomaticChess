using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelsoftGames.PixelUI;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    public List<string> NextStage;
    public List<Sprite> pitures;
    public int stadgeNum;
    public int currentStadge=0;
    public float loadValue;
    public float sliderValue;
    public float speed = 1;
    private AsyncOperation operation;
    public Image image;  
    public ClockDemo clock; 
    int n=0;//��������
    public GameObject canvers;
    public void LoadScene()
    {
        if (operation != null) return;
        if (currentStadge < stadgeNum)
        {
            currentStadge++;
            if (currentStadge == stadgeNum)
            {
                GameManage.instance.teams["Enemy"].team[0].standTile.ChessLeave();
                Destroy(GameManage.instance.teams["Enemy"].team[0].gameObject);
                MapManage.instance.CreatHouse(MapManage.instance.EnemyHouse, 11, 7/ 2, "Enemy");
            }
        }
        else
        {
            currentStadge = 0;
            EventController.Instance.TriggerEvent(EventName.WhenSceneLoad.ToString());
            StartCoroutine(AsyncLoading());
            if (n + 1 < NextStage.Count)
                n++;
            else n = 0;
        }
    }
    public void LoadScene(string name)
    {
        n = 0;
        if (name == "��ʼ") currentStadge = stadgeNum;
        SceneManager.LoadScene(name);
    }
    IEnumerator AsyncLoading()
    {
        // �첽���س���
        canvers.SetActive(false);
        GetComponent<Animator>().SetBool("load",true);
        operation = SceneManager.LoadSceneAsync(NextStage[n]);
        // ��ֹ����������Զ��л�
        operation.allowSceneActivation = false;

        yield return operation;
    }
    private void Update()
    {
        if (operation != null)
        {
            loadValue = operation.progress;
            if (operation.progress >= 0.9f)
            {
                // operation.progress��ֵ���Ϊ0.9
                loadValue = 1.0f;
            }
            if (loadValue != sliderValue)
            {
                // ��ֵ���㣨��������ǰ���ؽ���������
                sliderValue = Mathf.Lerp(sliderValue, loadValue, Time.deltaTime * speed);
                // �����ֵ����һֱ����
                if (Mathf.Abs(sliderValue - loadValue) < 0.01f)
                {
                    sliderValue = loadValue;
                }
                clock.load = (int)(24 * sliderValue);
                SetCloce();
            }
            if (sliderValue >= 0.9)
            {
                operation.allowSceneActivation = true;
                GetComponent<Animator>().SetBool("load", false);
                sliderValue = 0;
                operation = null;
                canvers.SetActive(true);
            }
        }
    }
    public void SetCloce()
    {
        if (n>0&& NextStage[n-1] == "ѩ��")
        {
            clock.SetSnowyDay();
            image.sprite = pitures[0];
            //image.SetNativeSize();
            return;
        }else if (n > 0 && NextStage[n - 1] == "ɭ��")
        {
            clock.SetWindyDay();
            image.sprite = pitures[1];
            //image.SetNativeSize();
            return;
        }else if (n > 0 && NextStage[n - 1] == "����")
        {
            clock.SetStormyDay();
            image.sprite = pitures[2];
            //image.SetNativeSize();
            return;
        }else if (n > 0 && NextStage[n - 1] == "��ʴ")
        {
            clock.SetFestivalDay();
            image.sprite = pitures[3];
            //image.SetNativeSize();
            return;
        }
        clock.SetClearDay();
    }
}
