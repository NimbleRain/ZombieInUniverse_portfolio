using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; 
    public Slider healthSliderUI;
    
    public AudioClip deathSound;
    public AudioClip hittingSound;
    public AudioClip pickupSound;

    private AudioSource playerAudio;
    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;

    private void Awake() //플레이어 게임 오브젝트에서 필요한 컴포넌트 가져옴
    {
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);        //인게임 체력 슬라이더 활성화
        healthSlider.maxValue = startingHealth;         //인게임 체력 슬라이더 최댓값을 기본 체력값으로 변경
        healthSlider.value = health;                    //인게임 체력 슬라이더 값을 현재 체력값으로 변경
        healthSliderUI.gameObject.SetActive(true);      //UI
        healthSliderUI.maxValue = startingHealth;
        healthSliderUI.value = health;

        playerMovement.enabled = true;                  //플레이어 조작을 받는 컴포넌트 활성화
        playerShooter.enabled = true;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {

        if (!dead)
        {
            playerAudio.PlayOneShot(hittingSound);
        }

        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = health;
        healthSliderUI.value = health;
    }
    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        healthSliderUI.gameObject.SetActive(false);

        playerAudio.PlayOneShot(deathSound);
        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //아이템과 충돌 시 아이템을 사용        
        if(!dead)
        {
            IItem item = other.GetComponent<IItem>();

            if(item != null)
            {
                item.Use(gameObject);
                playerAudio.PlayOneShot(pickupSound);
            }
        }
    }
}
