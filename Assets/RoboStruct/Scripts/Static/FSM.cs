
namespace RoboStruct
{
    /// <summary>
    /// Finite State Machine.
    /// </summary>
    public class FSM
    {
        public enum Step
        {
            Enter,
            Update,
            Exit
        }

        public delegate void StateCallback(FSM fsm, Step step, StateCallback state);

        StateCallback _currentState;

        public void Start(StateCallback startState)
        {
            TransitionTo(startState);
        }

        public void OnUpdate()
        {
            _currentState.Invoke(this, Step.Update, null);
        }

        public void TransitionTo(StateCallback state)
        {
            _currentState?.Invoke(this, Step.Exit, state);
            var oldState = _currentState;
            _currentState = state;
            _currentState.Invoke(this, Step.Enter, oldState);
        }
    }

}
