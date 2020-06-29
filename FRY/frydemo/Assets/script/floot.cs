using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floot : MonoBehaviour {

   public Transform target;
    private Vector3 offset;
    public float smoothing = 3;
     
   void Start()
    {
       offset = transform.position - target.position;
      
   }
 
     void Update()
      {
        // Vector3 pos=  target.position+offset;
        transform.position = target.position + offset;
        // transform.position = Vector3.Lerp(transform.position, pos, smoothing * Time.deltaTime);
        this.transform.LookAt(target.position);
       // transform.rotation = Quaternion.LookRotation(target.position);

       // transform.position = Vector3.Lerp(transform.position, pos, smoothing * Time.deltaTime);
    }
   
}


