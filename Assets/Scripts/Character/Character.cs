using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotatoionSpeed;

    CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        InputManager.GetInVehicle += Disappear;
    }

    private void OnDisable()
    {
        InputManager.GetInVehicle -= Disappear;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
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
        gameObject.SetActive(false);
    }
}
