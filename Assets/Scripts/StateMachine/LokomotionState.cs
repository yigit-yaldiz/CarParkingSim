using UnityEngine;

namespace StateMachineBehaviour
{
    public class LokomotionState : BaseState
    {
        public LokomotionState(PlayerController playerController) : base(playerController) { }

        public override void OnEnter()
        {
            Debug.Log("Lokomotion starts");
        }

        public override void Update()
        {
            InteractionController.Instance.CastCone();
        }

        public override void FixedUpdate()
        {
            _playerController.Move();
        }
    }
}