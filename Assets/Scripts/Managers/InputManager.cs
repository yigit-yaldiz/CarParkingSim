using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action RespawnCar;
    public static Action GetInVehicle;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && StateMachine.Instance.CurrentState == State.Drive)
        {
            RespawnCar();
        }

        if (Input.GetKeyDown(KeyCode.F) && StateMachine.Instance.CurrentState == State.Walk)
        {
            GetInVehicle();
        }
    }
}
