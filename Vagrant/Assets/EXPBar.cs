using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour
{
    public static EXPBar _instance;
    private UISlider progressBar;
    private void Awake()
    {
        _instance = this;
        progressBar = this.GetComponent<UISlider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValue(float value)
    {
        progressBar.value = value;
    }
}
