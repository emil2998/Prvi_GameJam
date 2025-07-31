using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Parameters")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;

    //[SerializeField] private int maxAmmo = 90;
    [SerializeField] private float singleFireCooldown = 0.5f;

    private bool canFire = true;
 
    [SerializeField] private CinemachineCamera playerCamera;

    private bool isZooming = false;
    private float minZoom = 60f;
    private float maxZoom = 30f;


    private void Update()
    {
        if (Input.GetMouseButton(0) && canFire && isZooming)
        {
            Shoot();
            Debug.Log(canFire);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isZooming = true;
           
             StartCoroutine(SetZoom(minZoom, maxZoom));               
        }
        if (Input.GetMouseButtonUp(1))
        {
            StartCoroutine(SetZoom(maxZoom, minZoom));
            isZooming = false;
        }
    }

    IEnumerator SetZoom(float start, float end)
    {
        float timer = 0;
        float elapse = 0.2f;
        while (timer < elapse)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            playerCamera.Lens.FieldOfView = Mathf.Lerp(start, end, timer / elapse);
            
        }
    }

    private void Shoot()
    {
        CreateBullet();
        StartCooldown(singleFireCooldown);
    }

    private void CreateBullet()
    {    
      Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);                  
    }

    private void StartCooldown(float cooldown)
    {
        StartCoroutine(CoolDown(cooldown));
    }

    IEnumerator CoolDown(float cooldown)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
}
