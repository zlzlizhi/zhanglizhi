using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] charactPrefabs;
    private GameObject[] charactGameObjects;
    public UIInput name;
    private int selectedIndex = 0;
    private int lenght;
    // Start is called before the first frame update
    void Start()
    {
        lenght = charactPrefabs.Length;
        charactGameObjects = new GameObject[lenght];
        for(int i=0;i<lenght;i++)
        {
            charactGameObjects[i] = GameObject.Instantiate(charactPrefabs[i], transform.position, transform.rotation) as GameObject;
        }
        ShowCaractGameObject();
    }

    void ShowCaractGameObject( )
    {
        charactGameObjects[selectedIndex].SetActive(true);
        for (int i = 0; i < lenght; i++)
        {
            if(i!=selectedIndex)
            {
                charactGameObjects[i].SetActive(false);
            }
        }
    }
    public void NextBtn()
    {
        selectedIndex++;
        selectedIndex %= lenght;
        ShowCaractGameObject();
        

    }
    public void PreBtn()
    {
        selectedIndex--;
        if(selectedIndex==-1)
        {
            selectedIndex = lenght - 1;
        }
        ShowCaractGameObject();
    }
    public void OKBtn()
    {
        PlayerPrefs.SetInt(" charactGameObjects", selectedIndex);
        PlayerPrefs.SetString("Name", name.value);
       // Application.LoadLevel("1");
        SceneManager.LoadScene("Loading");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
