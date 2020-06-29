using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Minimap : MonoBehaviour
{
    public  Camera MiniMapCamera;
    // Start is called before the first frame update
    void Start()
    {
      // MiniMapCamera = GameObject.FindGameObjectWithTag("Minimap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlus()
    {
        MiniMapCamera.orthographicSize--;
    }
    public void OnDes()
    {
        MiniMapCamera.orthographicSize++;
    }
}
