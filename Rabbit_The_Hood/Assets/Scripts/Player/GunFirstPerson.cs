using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunFirstPerson : MonoBehaviour
{
    [Header("Gun Parameters")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float singleFireCooldown = 0.1f;

    private Bullet bullet;

    public bool canFire = true;

    [SerializeField] private float minChargeTime = 0.1f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float minSpeed = 6f;
    [SerializeField] private float maxSpeed = 25f;

    private float chargeStartTime;
    private bool isCharging = false;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Shoot.started += OnShootStarted;
        inputActions.Gameplay.Shoot.performed += OnShootPerformed; 
        inputActions.Gameplay.Shoot.canceled += OnShootCanceled;  
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Shoot.started -= OnShootStarted;
        inputActions.Gameplay.Shoot.performed -= OnShootPerformed;
        inputActions.Gameplay.Shoot.canceled -= OnShootCanceled;
        inputActions.Disable();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (!canFire) return;
        isCharging = true;
        chargeStartTime = Time.time;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
  
        if (!canFire || !isCharging) return;
        isCharging = false;
        ShootCharged(maxSpeed);
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        if (!canFire || !isCharging) return;
        isCharging = false;
        float held = Time.time - chargeStartTime;
        float t = Mathf.Clamp01(held / maxChargeTime);
        float speedToUse = Mathf.Lerp(minSpeed, maxSpeed, t);
        ShootCharged(speedToUse);
    }

    private void ShootCharged(float bulletSpeed)
    {
        bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation).GetComponent<Bullet>();
        bullet.speed = bulletSpeed;
     
        StartCoroutine(CoolDown(singleFireCooldown));
    }

    IEnumerator CoolDown(float cooldown)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
}
