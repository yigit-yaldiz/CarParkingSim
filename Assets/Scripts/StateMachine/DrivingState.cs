using UnityEngine;

namespace StateMachineBehaviour
{
    public class DrivingState : BaseState
    {
        public DrivingState(PlayerController playerController) : base(playerController) { }

        public override void OnEnter()
        {
            Debug.Log("Driving starts");
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnExit()
        {
            Debug.Log("Existing from Lokomotion State");
        }
    }
}