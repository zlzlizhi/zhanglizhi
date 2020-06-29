using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bi1 : MonoBehaviour {

    void Update()
    {
        transform.Rotate(Vector3.forward*90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Audiomanager.Instance.PlayBi();
            other.GetComponent<player>().PlayerGetGold();

            gameObject.SetActive(false);
            Invoke("relive", 30f);
        }
    }

    private void relive()
    {
        gameObject.SetActive(true);
    }
}
