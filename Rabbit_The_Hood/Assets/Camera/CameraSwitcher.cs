using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCam;
    [SerializeField] private CinemachineCamera aimCam;
    [SerializeField] private CinemachineInputAxisController inputAxisController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject crosshairUI;
    [SerializeField] private PlayerControls input;

    private InputAction aimAction;
    private bool isAiming = false;

    private Transform yawTarget;
    private Transform pitchTarget;

    private AimCameraController aimCameraController;

    private void Start()
    {
        aimCameraController = aimCam.GetComponent<AimCameraController>();

        inputAxisController = freeLookCam.GetComponent<CinemachineInputAxisController>();

        input = new PlayerControls();
        input.Enable();
        aimAction = input.Gameplay.Aim;
    }

    private void Update()
    {
        bool aimPressed = aimAction.IsPressed();
        player.isAiming = aimPressed;

        if(aimPressed && !isAiming)
        {
            StartAimMode();
        }
        else if (!aimPressed && isAiming)
        {
            StopAimMode();
        }
    }

    private void StartAimMode()
    {
        isAiming = true;

        SnapAimCameraToPlayerForward();
        aimCam.Priority = 20;
        freeLookCam.Priority = 10;

        inputAxisController.enabled = false;
    }

    private void StopAimMode()
    {
        isAiming = false;

        SnapFreeLookBehindPlayer();
        aimCam.Priority = 10;
        freeLookCam.Priority = 20;

        inputAxisController.enabled = true;
    }

    private void SnapFreeLookBehindPlayer()
    {
        CinemachineOrbitalFollow orbitalFollow = freeLookCam.GetComponent<CinemachineOrbitalFollow>();
        Vector3 forward = aimCam.transform.forward;
        float angle = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;
        orbitalFollow.HorizontalAxis.Value = angle;
    }

    private void SnapAimCameraToPlayerForward()
    {
        aimCameraController.SetYawPitchFromCameraForward(freeLookCam.transform);
    }
}
