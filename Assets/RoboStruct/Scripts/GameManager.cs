using System;
using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Persistent singleton that manages the gameloop and game states.
    /// </summary>
    public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
    {
        private bool _isPaused = false;

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                OnGamePaused?.Invoke(value);
            }
        }

        /// <summary>
        /// Levels are series of waves that are split up by shop breaks.
        /// </summary>
        public FSM GameStateFSM { get; private set; }
        public FSM.StateCallback MenuStateCallback { get; private set; }
        public FSM.StateCallback GameplayStateCallback { get; private set; }
        public FSM.StateCallback CinematicStateCallback { get; private set; }
        public bool IsPlayerControlDisabled { get; private set; }

        public event Action<bool> OnGamePaused;

        private void Start()
        {
            OnGamePaused += DisableTimeScale;

            MenuStateCallback = MenuState;
            GameplayStateCallback = GameplayState;
            CinematicStateCallback = CinematicState;

            GameStateFSM = new FSM();
            GameStateFSM.Start(MenuStateCallback);
        }

        private void Update()
        {
            GameStateFSM.OnUpdate();
        }

        private void DisableTimeScale(bool isTimeScaleDisabled)
        {
            if (isTimeScaleDisabled) { Time.timeScale = 0; }
            else { Time.timeScale = 1; }
        }

        private void EnableGameplayUIElements(bool state)
        {
            if (FindObjectOfType<GameplayUI>())
            {
                GameplayUI ui = FindObjectOfType<GameplayUI>();
                ui.SetVirtualGamepad(state);
                ui.DisableHUD(state);
            };
        }

        #region GameState FSM

        /// <summary>
        /// For non-gameplay context. Intended for menu screens.
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="step"></param>
        /// <param name="state"></param>
        private void MenuState(FSM fsm, FSM.Step step, FSM.StateCallback state)
        {
            switch (step)
            {
                case FSM.Step.Enter:
                    EnableGameplayUIElements(false);
                    IsPaused = true;
                    break;

                case FSM.Step.Update:
                    break;

                case FSM.Step.Exit:
                    IsPaused = false;
                    break;
            }
        }

        /// <summary>
        /// Time-scale is enabled. Intended for gameplay levels.
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="step"></param>
        /// <param name="state"></param>
        private void GameplayState(FSM fsm, FSM.Step step, FSM.StateCallback state)
        {
            switch (step)
            {
                case FSM.Step.Enter:
                    EnableGameplayUIElements(true);
                    break;

                case FSM.Step.Update:
                    break;

                case FSM.Step.Exit:
                    break;
            }
        }

        /// <summary>
        /// Camera is cinematic and characters are disabled.
        /// </summary>
        /// <param name="fsm"></param>
        /// <param name="step"></param>
        /// <param name="state"></param>
        private void CinematicState(FSM fsm, FSM.Step step, FSM.StateCallback state)
        {
            switch (step)
            {
                case FSM.Step.Enter:
                    EnableGameplayUIElements(false);
                    IsPlayerControlDisabled = true;
                    break;

                case FSM.Step.Update:
                    break;

                case FSM.Step.Exit:
                    EnableGameplayUIElements(true);
                    IsPlayerControlDisabled = false;
                    break;
            }
        }
        #endregion
    }
}
