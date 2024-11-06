using UnityEngine;

public class InteractionController : MonoBehaviour
{
    const float _raycastMaxDist = 5f;

    private void Update()
    {
        bool doesHitSomething = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, _raycastMaxDist);

        if (doesHitSomething && hit.transform.GetComponent<VehicleController>() is IInteractable)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _raycastMaxDist, Color.cyan);
        }
    }
}
