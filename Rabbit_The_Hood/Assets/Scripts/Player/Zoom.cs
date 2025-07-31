using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private GameObject FirstPersonSling;
    private MeshRenderer slingOfDavid;

    [SerializeField] private CinemachineCamera FirstPersonCamera;
    [SerializeField] private CinemachineCamera ThirdPersonCamera;

    private bool isFirstPerson = false;
    private float minZoom = 60f;
    private float maxZoom = 30f;

    [SerializeField]private GunFirstPerson gunFirstPerson;

    private void Awake()
    {
        slingOfDavid = FirstPersonSling.GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        slingOfDavid.enabled = false;
     
        SetCameraPriority();
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0 && FirstPersonCamera.Lens.FieldOfView == minZoom)
        {
            StartCoroutine(SetZoom(minZoom, maxZoom));
        }
        if (Input.mouseScrollDelta.y < 0 && FirstPersonCamera.Lens.FieldOfView == maxZoom)
        {
            StartCoroutine(SetZoom(maxZoom, minZoom));
        }

        if (Input.GetMouseButtonDown(1))
        {
         
            slingOfDavid.enabled = true;
            isFirstPerson = !isFirstPerson;

            if (isFirstPerson)
            {
                gunFirstPerson.canFire = true;
            }
            else
            {
                gunFirstPerson.canFire = false;
            }
            SetCameraPriority();
        }

        
        
    }

    void SetCameraPriority()
    {
        if (isFirstPerson)
        {
            FirstPersonCamera.Priority = 20;
            ThirdPersonCamera.Priority = 10;
        }
        else
        {
            FirstPersonCamera.Priority = 10;
            ThirdPersonCamera.Priority = 20;

           slingOfDavid.enabled = false;
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
            FirstPersonCamera.Lens.FieldOfView = Mathf.Lerp(start, end, timer / elapse);
        }
    }
}
