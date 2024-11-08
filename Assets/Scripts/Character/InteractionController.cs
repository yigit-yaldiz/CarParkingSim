using UnityEngine;

public enum InteractionType
{
    None,
    Vehicle,
    Object,
    Door
}

public class InteractionController : MonoBehaviour
{
    [SerializeField] InteractionType _currentType;
    static Transform _currentInteraction;

    const float _coneAngle = 45f;  
    const float _rayRange = 5f;   
    const int _rayCount = 10;

    public InteractionType CurrentType  => _currentType;
    public static Transform CurrentInteraction => _currentInteraction;

    private void Update() //its not finished. First we need to take if is interacable. After that which type of interacable we need to find
    {
        CastCone();
    }

    void CastCone()
    {
        if (StateMachine.Instance.CurrentState != State.Walk)
        {
            return;
        }

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        float halfAngle = _coneAngle / 2f;

        for (int i = 0; i < _rayCount; i++)
        {
            float currentAngle = -halfAngle + (i * (_coneAngle / (_rayCount - 1)));
            Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);

            Vector3 rayDirection = rotation * forward;

            Ray ray = new Ray(origin, rayDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, _rayRange))
            {
                Debug.DrawLine(origin, hit.point, Color.red);
                _currentType = FindIntreactionType(hit);
                _currentInteraction = hit.transform;
            }
            else
            {
                Debug.DrawLine(origin, origin + rayDirection * _rayRange, Color.blue);
                _currentType = FindIntreactionType(hit);
                _currentInteraction = null;   
            }
        }

        Debug.Log(_currentInteraction);
    }

    InteractionType FindIntreactionType(RaycastHit hit)
    {
        if (hit.transform == null) return InteractionType.None; //guard break

        if (hit.transform.TryGetComponent(out VehicleController vehicle))
        {
            return InteractionType.Vehicle;
        }
        else
        {
            return InteractionType.None;
        }
    }

    void DetectInteracable()
    {
        int _raycastMaxDist = 5;

        bool doesHitSomething = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _raycastMaxDist);

        _currentType = FindIntreactionType(hit);

        if (doesHitSomething)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            switch (_currentType)
            {
                case InteractionType.Vehicle:
                    hit.transform.TryGetComponent(out VehicleController vehicle);
                    break;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _raycastMaxDist, Color.cyan);
        }
    }
}
