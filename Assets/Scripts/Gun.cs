using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready, Empty, Reloading
    }

    public State state { get; private set; } //현재의 총의 상태
    public Transform fireTransform; //발사 위치
    public ParticleSystem muzzleFlashEffect; //총구 화염 효과
    public ParticleSystem shellEjectEffect; //탄피 배출 효과

    private LineRenderer bulletLineRenderer; //탄알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25; //공격력
    private float fireDistance = 50f; //사정거리

    public int ammoRemain = 100; //탄알
    public int magCapacity = 25; //한 탄창에 들어갈 탄알 수
    public int magAmmo; //현재 탄창에 남은 탄알

    public float timeBetFire = 0.12f; //발사 간격
    public float reloadTime = 1.0f; //재장전 소요 시간
    private float lastFireTime; //마지막 발사 시점

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable() //오브젝트가 활성화되면 실행
    {
        magAmmo = magCapacity; //현재 탄창 가득 채우기
        state = State.Ready; //총의 현재 상태를 쏠 준비가 된 상태로 변경
        lastFireTime = 0; //초기화
    }

    public void Fire() //발사 시도 구현
    {
        if(state == State.Ready && Time.time >= lastFireTime + timeBetFire) //발사 준비 및 마지막 발사 시점에서 간격만큼 더함
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    public void Shot()
    {
        RaycastHit hit; 
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
                hitPosition = hit.point;                
            }
            else
            {
                hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
            }

            StartCoroutine(ShotEffect(hitPosition));

            magAmmo--;
            if (magAmmo <= 0)
            {
                state = State.Empty;
            }
        }

        IEnumerator ShotEffect(Vector3 hitPosition2)
        {
            muzzleFlashEffect.Play();
            shellEjectEffect.Play();

            gunAudioPlayer.PlayOneShot(shotClip);
            bulletLineRenderer.SetPosition(0, fireTransform.position);
            bulletLineRenderer.SetPosition(1, hitPosition2);
            bulletLineRenderer.enabled = true;

            yield return new WaitForSeconds(0.03f);

            bulletLineRenderer.enabled = false;
        }
    }
        //재장전
        public bool Reload()
        {
            if(state == State.Reloading || ammoRemain <= 0 || magAmmo >= magCapacity)
            {
                return false;
            }

            StartCoroutine(ReloadRoutine());
            return true;
        }

        IEnumerator ReloadRoutine()
        {
            state = State.Reloading;
            gunAudioPlayer.PlayOneShot(reloadClip);
            yield return new WaitForSeconds(reloadTime);
            int ammoToFill = magCapacity - magAmmo;

            if(ammoRemain < ammoToFill)
            {
                ammoToFill = ammoRemain;
            }

            magAmmo += ammoToFill;
            ammoRemain -= ammoToFill;

            state = State.Ready;
        }

    }

