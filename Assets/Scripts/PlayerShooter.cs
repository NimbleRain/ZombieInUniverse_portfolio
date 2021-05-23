using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;

    private PlayerInput playerInput;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInput.fire)
        {
            gun.Fire();
        }
        else if (playerInput.reload)
        {
            if (gun.Reload())
            {
                playerAnimator.SetTrigger("Reload");
            }

        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (gun != null && UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }

    //private void OnAnimatorIK(int layerIndex) //플레이어와 총의 움직임을 동일시
    //{
    //    gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    //}
}
