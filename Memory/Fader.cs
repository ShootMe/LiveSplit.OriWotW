namespace LiveSplit.OriWotW {
    public enum FadeState {
        Null = -1,
        FadeToBlack = 0,
        FadeStay = 1,
        FadeFromBlack = 2,
        Invisible = 3,
        Timeline = 4,
        EditorDebug = 5,
        TimelineFinished = 6
    };
    public enum StartPauseState : int {
        Null = -1,
        StartedValueFalse = 0,
        InGameUberValueTrue = 1,
        QTMValueFalse = 2,
        ReInGameUberValueTrue = 3,
    };
    public class Fader {
        private FadeState CurrentState = FadeState.Null;
        private float LastFadeTimer = -1.0f;
        private bool FaderPaused = false;
        private bool FadeStateChanged = false;
        private int LastFrame = -1;
        private StartPauseState startQTMState = StartPauseState.Null;
        private bool startQTMStateShouldPause = true;
        private bool lastRunning = false;

        public bool IsPaused() { return FaderPaused; }
        public void UpdateLastFader(float fadeTimer, int frameCount) {
            LastFadeTimer = fadeTimer;
            LastFrame = frameCount;

            if (FadeStateChanged == true) {
                FadeStateChanged = false;
                //FaderPaused = false;
            }
        }
        public void UpdateFadeState(FadeState fadeState) {
            if (fadeState != CurrentState)
                FadeStateChanged = true;

            switch (fadeState) {
                case FadeState.FadeFromBlack: CurrentState = FadeState.Null; LastFadeTimer = -1.0f; break;
                case FadeState.FadeToBlack: CurrentState = FadeState.Null; LastFadeTimer = -1.0f; break;
                case FadeState.FadeStay: CurrentState = fadeState; break;
                case FadeState.Invisible: CurrentState = FadeState.Null; LastFadeTimer = -1.0f; break;
                case FadeState.Timeline: CurrentState = FadeState.Null; LastFadeTimer = -1.0f;  break;
                case FadeState.TimelineFinished: CurrentState = FadeState.Null; LastFadeTimer = -1.0f; break;
            }
        }

        public void CheckStartQTM(string uberValue, GameState state, bool running) {
            if (lastRunning == false && running == true && state != GameState.TitleScreen) {
                startQTMStateShouldPause = false;
                return;
            }

            switch (startQTMState) {
                case StartPauseState.Null:
                    if (state == OriWotW.GameState.TitleScreen && uberValue.Equals("False")) {
                        startQTMState = StartPauseState.StartedValueFalse;
                        startQTMStateShouldPause = true;
                    }
                    break;
                case StartPauseState.StartedValueFalse:
                    if (state == OriWotW.GameState.Game && uberValue.Equals("True")) {
                        startQTMState = StartPauseState.InGameUberValueTrue;
                        startQTMStateShouldPause = true;
                    }
                    break;
                case StartPauseState.InGameUberValueTrue:
                    if (state == OriWotW.GameState.StartScreen && startQTMState == StartPauseState.InGameUberValueTrue && uberValue.Equals("False")) {
                        startQTMState = StartPauseState.QTMValueFalse;
                        startQTMStateShouldPause = true;
                    }
                    break;
                case StartPauseState.QTMValueFalse:
                    if (state == OriWotW.GameState.Game && startQTMState == StartPauseState.QTMValueFalse && uberValue.Equals("True")) {
                        startQTMState = StartPauseState.ReInGameUberValueTrue;
                        startQTMStateShouldPause = false;
                    }
                    break;
                case StartPauseState.ReInGameUberValueTrue:
                    startQTMStateShouldPause = false;
                    break;
            }

            lastRunning = running;
        }

        public void ResetStartQTM() {
            startQTMState = StartPauseState.Null;
            startQTMStateShouldPause = true;
            lastRunning = false;
        }

        public void ShouldPause(float fadeTimer, int currentFrame) {
            if (currentFrame != LastFrame) {
                FaderPaused = false;

                if (fadeTimer != LastFadeTimer && CurrentState != FadeState.Null)
                    FaderPaused = true;
            }

            if (startQTMStateShouldPause == false) {
                FaderPaused = false;
                return;
            }

            FaderPaused = CurrentState != FadeState.Null ? FaderPaused : false;
        }

        public FadeState GetFadeState() {
            return this.CurrentState;
        }
    }
}
