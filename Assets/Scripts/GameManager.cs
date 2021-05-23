using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //점수 및 게임 오버 
    //전역변수로 클래스의 인스턴스를 하나만 사용하는 패턴
    private static GameManager m_instance; 
    public static GameManager Instance //싱글톤
    {
        get
        {
            if (m_instance == null) //없다면
            {
                m_instance = FindObjectOfType<GameManager>(); //최초실행시 추가
            }
            return m_instance; //있다면 
        }
    }       
    private void Awake()  //객체가 생성된 이후 중복 검사(예외처리)
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    
    public bool isGameover { get; private set; }                                //게임오버 상태 (클래스 외부에서 읽기만 가능)
    private void Start()
    {        
       FindObjectOfType<PlayerHealth>().OnDeath += EndGame;                     //EndGame이란 함수를 실행하는 이벤트
    }
    public void EndGame() //게임끝
    {
        isGameover = true;
        UIManager.Instance.SetActiveGameoverUI(true);
    }
   
    
}
