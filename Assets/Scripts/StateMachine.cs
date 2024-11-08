using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    Walk,
    Drive
}

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance { get; private set; }
    public State CurrentState  => _currentState;

    [SerializeField] State _currentState;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InputManager.GetInVehicle += ChangeStateToDrive;
    }

    private void OnDisable()
    {
        InputManager.GetInVehicle -= ChangeStateToDrive;
    }

    public void ChangeState(State state)
    {
        _currentState = state;
    }

    void ChangeStateToDrive()
    {
        _currentState = State.Drive;
    }
}
