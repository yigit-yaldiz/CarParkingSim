using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action RespawnCar;
    public static Action GetIn;
    public static Action GetOut;

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
            if (InteractionController.CurrentInteraction == null)
            {
                Debug.LogWarning("Current Interaction is null");
                return;
            }

            GetIn();
        }
        else if (Input.GetKeyDown(KeyCode.F) && StateMachine.Instance.CurrentState == State.Drive)
        {
            GetOut();
        }
    }
}
