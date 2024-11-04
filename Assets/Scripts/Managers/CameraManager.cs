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

    public void ChangeCameraPosition(Transform target)
    {
        _virtualCamera.Follow = target;
    }
}
