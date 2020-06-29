using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionNPC : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            ShopDurg._instance.TranformStatus();
        }
            
    }
}
