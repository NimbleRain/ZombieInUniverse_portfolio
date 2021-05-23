using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask CharTarget;
    private LivingEntity targetEntity;
    private NavMeshAgent pathFinder;

    //사운드
    public ParticleSystem hitEffect;
    public AudioClip deathSound;
    public AudioClip hitSound;

    //컴포넌트
    private Animator enemyAnimator;
    private AudioSource enemyAudioPlayer;
    private MeshRenderer enemyRenderer; //피격 시 외형의 색깔을 바꾸기 위해

    //공격
    public float damage = 10f;
    public float timeBetweenAtk = 0.3f;
    private float lastAttacktime;

    private bool hasTarget //추적할 대상 파악
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead) //플레이어 존재 및 살아있을 때
            {
                return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();
        enemyRenderer = GetComponentInChildren<MeshRenderer>(); //자식 컴포넌트에 존재하므로.
    }

    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        startingHealth = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
        enemyRenderer.material.color = skinColor;
       
        
    }
    void Start()
    {
        StartCoroutine(UpdatePath());                                                                               //활성화 동시에 AI 추적
    }
    void Update()
    {
        enemyAnimator.SetBool("HasTarget", hasTarget);                                                                      //타겟 존재 여부에 따른 애니메이션 재생(move)
    }

    IEnumerator UpdatePath()                                                                                        //추적 대상을 주기적으로 찾아 경로 갱신
    {
        while (!dead)                                                                                                               //살아있다
        {
            if (hasTarget) //추적 대상 존재
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);     
            }
            else //추적 대상 없음
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, CharTarget);                

                for(int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    if (livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)   //공격받았다
    {
        if (!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();
            enemyAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] enemyColliders = GetComponents<Collider>();    //자신이 죽으면 다른 AI 방해하지 않고 콜라이더 비활성화
        for(int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        pathFinder.isStopped = true; //추적 중지
        pathFinder.enabled = false;  //네비메쉬 비활성화
                
        ScoreManager.score += 10; //에너미가 죽을 때 10점 추가

        enemyAnimator.SetTrigger("Die");
        enemyAudioPlayer.PlayOneShot(deathSound);       
    }
    private void OnTriggerStay(Collider other) //공격 대상이 맞다면 공격!
    {
        if(!dead && Time.time >= lastAttacktime + timeBetweenAtk)
        {
            LivingEntity atkTarget = other.GetComponent<LivingEntity>();

            if(atkTarget != null && atkTarget == targetEntity)
            {
                lastAttacktime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                atkTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
