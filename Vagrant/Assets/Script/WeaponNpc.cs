using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNpc : MonoBehaviour
{
    // Start is called before the first frame update
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
            ShopWeaponUI._instance.TranformStatus();
        }

    }
}
