namespace LiveSplit.OriWotW {
    public enum GameState : int {
        Logos,
        StartScreen,
        TitleScreen,
        Game,
        MenuRace,
        WatchCutscenes,
        TrialEnd,
        Prologue
    }

    public static class GameStateExtensions {
        public static bool IsGameOrRace(this GameState state) {
            return state == GameState.Game || state == GameState.MenuRace;
        }
    }
}