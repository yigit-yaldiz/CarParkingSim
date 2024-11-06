using System.Collections;
using UnityEngine;

public class VehicleController : MonoBehaviour, IVehicle
{
    [SerializeField] Car _car;

    private float _horizontalInput, _verticalInput;
    private float _currentSteerAngle, _currentBreakForce;
    private bool _isBreaking, _isOnAir;

    // Wheel Colliders
    [SerializeField] private WheelCollider _frontLeftWheelCollider, _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider, _rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform _frontLeftWheelTransform, _frontRightWheelTransform;
    [SerializeField] private Transform _rearLeftWheelTransform, _rearRightWheelTransform;

    private void Update()
    {
        Drive();

        if (Input.GetKeyDown(KeyCode.R) && !_isOnAir)
        {
            StartCoroutine(RespawnCar());
        }
    }

    public void Drive()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        // Steering Input
        _horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        _verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        _isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        _frontLeftWheelCollider.motorTorque = _verticalInput * _car.MotorForce;
        _frontRightWheelCollider.motorTorque = _verticalInput * _car.MotorForce;
        _currentBreakForce = _isBreaking ? _car.BreakForce : 0f; //isBreaking == false => currentBreakForce = 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        _frontRightWheelCollider.brakeTorque = _currentBreakForce;
        _frontLeftWheelCollider.brakeTorque = _currentBreakForce;
        _rearLeftWheelCollider.brakeTorque = _currentBreakForce;
        _rearRightWheelCollider.brakeTorque = _currentBreakForce;
    }

    private void HandleSteering()
    {
        _currentSteerAngle = _car.MaxSteerAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _currentSteerAngle;
        _frontRightWheelCollider.steerAngle = _currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
        UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public IEnumerator RespawnCar()
    {
        _isOnAir = true;

        Quaternion newRotation = Quaternion.Euler(0, transform.rotation.y, 0);
        transform.rotation = newRotation;

        Vector3 newPos= new(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        transform.position = Vector3.Lerp(newPos, transform.position, 0.1f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(1f);
        _isOnAir = false;
    }
}
