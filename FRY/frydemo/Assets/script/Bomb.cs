using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombfect;//爆炸特效

    public float bombFuhuo = 10f;

    public bool bombPlayer = true;

    void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        bombBreak();
        if (other.tag == "Player" && bombPlayer)
        {
            other.GetComponent<player>().Playdie();
        }
        if (other.tag == "diss")
        {
            other.GetComponent<Diss>().dissdie();

            //GameObject.FindGameObjectsWithTag("player").GetComponent<player>().PlayerSkillDiss();        
            player._instance.PlayerSkillDiss();
        }
    }

    public void bombBreak()
    {
        Audiomanager.Instance.PlayBomb();
       // Instantiate(bombfect, transform.position, Quaternion.identity);       
        gameObject.SetActive(false);
        if(bombPlayer)
            Invoke("bombbb", bombFuhuo);
    }

    public void bombbb()
    {
        gameObject.SetActive(true);
    }
}
