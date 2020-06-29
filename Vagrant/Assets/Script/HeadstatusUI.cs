using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadstatusUI : MonoBehaviour
{
    public static HeadstatusUI _instance;
    private UILabel Name;
    private UISlider hpbar;
    private UISlider mpbar;
    private UILabel hpLabel;
    private UILabel mpLabel;
    private PlayerStatus ps;

    private void Awake()
    {
        _instance = this;

        Name = transform.Find("Name").GetComponent<UILabel>();
        hpbar = transform.Find("Hp").GetComponent<UISlider>();
        mpbar = transform.Find("Mp").GetComponent<UISlider>();
        hpLabel = transform.Find("Hp/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("Mp/Thumb/Label").GetComponent<UILabel>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
      
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShow();
    }
    public void UpdateShow()
    {
        Name.text = "Lv." + ps.level + " " + ps.Name;
        hpbar.value = ps.hp_remain / ps.hp;
        mpbar.value = ps.mp_remain / ps.mp;

        hpLabel.text = ps.hp_remain + "/" + ps.hp;
        mpLabel.text = ps.mp_remain + "/" + ps.mp;
    }
}
