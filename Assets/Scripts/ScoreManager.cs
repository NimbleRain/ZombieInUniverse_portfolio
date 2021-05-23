using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;                    //정적변수 : 다른 클래스에서 호출이 가능하다. data영역에서 가져와 사용
    void Awake()
    {
        score = 0;        
    }
}
