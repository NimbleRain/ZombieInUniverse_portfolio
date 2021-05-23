using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{    
    private Animator enemyAnim;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyAnim.SetBool("IsAttack", true);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyAnim.SetBool("IsAttack", false);
        }
    }


}
