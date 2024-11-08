using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        Instance = this;
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        InputManager.GetInVehicle += ChangeCameraToCurrentInteraction;
    }

    private void OnDisable()
    {
        InputManager.GetInVehicle -= ChangeCameraToCurrentInteraction;
    }

    public void ChangeCameraPosition(Transform target)
    {
        _virtualCamera.Follow = target;
    }

    void ChangeCameraToCurrentInteraction()
    {
        ChangeCameraPosition(InteractionController.CurrentInteraction);
    }
}
