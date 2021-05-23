using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public static UIManager Instance
    { get
        {
            if(m_instance == null)                                  //m_instance가 비어 있는지 
            {
                m_instance = FindObjectOfType<UIManager>();         //UIManager를 가지고 있는지
            }
            return m_instance;
        }
    }


    public Text ammoText;      
    public GameObject gameoverUI;
    public GameObject gameSuccessUI;
    

    public void UpdateAmmoText(int magAmmo, int remainAmmo)                                     //탄알텍스트갱신
    {
        ammoText.text = "Remain Bullets : " + magAmmo + "/" + remainAmmo;
    }    

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }


    //if (Input.GetKeyDown(KeyCode.R) && Input.GetMouseButtonDown(0))
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}


    //public void SetActiveGameSuccessUI(bool active)
    //{
    //    gameSuccessUI.SetActive(active);
    //}          
    public void GameExitButton() //종료
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
