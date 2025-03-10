using StateMachineBehaviour;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotatoionSpeed;

    CharacterController _characterController;
    StateMachine _stateMachine;

    bool _isDriving;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _characterController = GetComponent<CharacterController>();
        
        //state machine
        _stateMachine = new();

        //defining states
        var locomotionState = new LokomotionState(this);
        var drivingState = new DrivingState(this);

        //defining transitions
        At(locomotionState, drivingState, new FuncPredicate(() => _isDriving));
        At(drivingState, locomotionState, new FuncPredicate(() => !_isDriving));

        //set initial state
        _stateMachine.SetState(locomotionState);
    }

    private void OnEnable()
    {
        InputManager.GetIn += Disappear;
        InputManager.GetOut += Appear;
    }

    private void OnDisable()
    {
        InputManager.GetIn -= Disappear;
        InputManager.GetOut -= Appear;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Move()
    {
        if (States.Instance.CurrentState != State.Walk)
        {
            return;
        }

        Vector3 movement = new(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _characterController.Move(movement * (Time.deltaTime * _speed));

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotatoionSpeed * Time.deltaTime);
        }
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    public void Disappear()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        foreach (Collider collider in colliders) 
        {
            collider.enabled = false;
        }

        _isDriving = true;
    }

    public void Appear()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        Vector3 newPos;
        newPos.x = InteractionController.CurrentInteraction.position.x - 4;
        newPos.y = transform.position.y;
        newPos.z = transform.position.z;

        transform.position = newPos;

        _isDriving = false;
    }
}
