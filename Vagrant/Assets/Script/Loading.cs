using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    public Text text;
    public Slider slider;
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameing(3));
    }

    private IEnumerator LoadGameing(int scenes)

    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = Application.LoadLevelAsync(scenes);//异步加载
        op.allowSceneActivation = false;//禁止Unity加载完毕后自动切换场景，**加粗样式**
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    public void SetLoadingPercentage(int i)
    {
        slider.value = i * 0.01f;
        text.text ="Loading  " +i.ToString() + "%";
    }
}
