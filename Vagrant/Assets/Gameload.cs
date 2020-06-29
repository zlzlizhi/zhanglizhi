using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameload : MonoBehaviour
{
    public GameObject Magician;
    public GameObject Swordman;
    private void Awake()
    {
        int h = PlayerPrefs.GetInt(" charactGameObjects");
      
       
        if (h == 1)
        {
            Magician.gameObject.SetActive(true);
        }
        if (h == 0)
        {
            Swordman.gameObject.SetActive(false);
            Magician.gameObject.SetActive(true);
        }
        
        
      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
