using System.Diagnostics;

namespace StateMachineBehaviour
{
    public abstract class BaseState : IState
    {
        protected PlayerController _playerController;

        protected BaseState(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }  
    }
}