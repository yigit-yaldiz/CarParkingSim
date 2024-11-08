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
        InputManager.GetIn += ChangeStateToDrive;
        InputManager.GetOut += ChangeStateToWalk;
    }

    private void OnDisable()
    {
        InputManager.GetIn -= ChangeStateToDrive;
        InputManager.GetOut -= ChangeStateToWalk;
    }

    public void ChangeState(State state)
    {
        _currentState = state;
    }

    void ChangeStateToDrive()
    {
        ChangeState(State.Drive);
    }

    void ChangeStateToWalk()
    {
        ChangeState(State.Walk);
    }
}
