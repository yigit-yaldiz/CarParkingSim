using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotatoionSpeed;

    CharacterController _characterController;

    public static Character Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _characterController = GetComponent<CharacterController>();
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
        Move();
    }

    private void Move()
    {
        if (StateMachine.Instance.CurrentState != State.Walk)
        {
            return;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        _characterController.Move(movement * (Time.deltaTime * _speed));

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotatoionSpeed * Time.deltaTime);
        }
    }

    void Disappear()
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
    }

    void Appear()
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

        transform.position = InteractionController.CurrentInteraction.GetComponentInChildren<SpawnPosition>().transform.position;
    }
}
