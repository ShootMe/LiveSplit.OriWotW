using System;
using System.Collections.Generic;
namespace LiveSplit.OriWotW {
    public enum LogObject {
        None,
        CurrentSplit,
        Pointers,
        Position,
        Keystones,
        Ore,
        MapCompletion,
        GameTime,
        Abilities,
        Shards,
        Area,
        Dead,
        GameState,
        TitleScreen,
        LoadingGame,
        WorldStates,
        Scene,
        UberState
    }
    public class LogManager {
        public List<ILogEntry> LogEntries = new List<ILogEntry>();
        private Dictionary<LogObject, string> currentValues = new Dictionary<LogObject, string>();
        private Dictionary<AbilityType, Ability> currentAbilities = new Dictionary<AbilityType, Ability>();
        private Dictionary<ShardType, Shard> currentShards = new Dictionary<ShardType, Shard>();
        private Dictionary<long, UberState> currentUberStates = new Dictionary<long, UberState>();
        public bool EnableLogging;

        public LogManager() {
            EnableLogging = false;
            Clear();
            AddEntryUnlocked(new EventLogEntry("Autosplitter Initialized"));
        }
        public void Clear() {
            lock (LogEntries) {
                LogEntries.Clear();
                foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                    currentValues[key] = null;
                }
            }
        }
        public void AddEntry(ILogEntry entry) {
            lock (LogEntries) {
                AddEntryUnlocked(entry);
            }
        }
        private void AddEntryUnlocked(ILogEntry entry) {
            LogEntries.Add(entry);
            Console.WriteLine(entry.ToString());
        }
        public void Update(LogicManager logic, SplitterSettings settings) {
            if (!EnableLogging) { return; }

            lock (LogEntries) {
                DateTime date = DateTime.Now;
                bool isDead = logic.Memory.Dead();
                bool isLoading = logic.Memory.IsLoadingGame();
                GameState gameState = logic.Memory.GameState();
                bool dontCheckValue = isDead || isLoading || gameState != GameState.Game;
                foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                    string previous = currentValues[key];
                    string current = null;

                    switch (key) {
                        case LogObject.CurrentSplit: current = $"{logic.CurrentSplit} ({GetCurrentSplit(logic, settings)})"; break;
                        case LogObject.Pointers: current = logic.Memory.GamePointers(); break;
                        case LogObject.Keystones: current = dontCheckValue ? previous : logic.Memory.Keystones().ToString(); break;
                        case LogObject.Ore: current = dontCheckValue ? previous : logic.Memory.Ore().ToString(); break;
                        case LogObject.MapCompletion: current = dontCheckValue ? previous : logic.Memory.MapCompletion().ToString("0.000"); break;
                        case LogObject.Abilities: if (!dontCheckValue) { CheckAbilities(logic); } break;
                        case LogObject.Shards: if (!dontCheckValue) { CheckShards(logic); } break;
                        case LogObject.Area: current = dontCheckValue ? previous : logic.Memory.PlayerArea().ToString(); break;
                        case LogObject.Dead: current = isDead.ToString(); break;
                        case LogObject.GameState: current = gameState.ToString(); break;
                        case LogObject.TitleScreen: current = logic.Memory.TitleScreen().ToString(); break;
                        case LogObject.LoadingGame: current = isLoading.ToString(); break;
                        case LogObject.Scene: string scene = logic.Memory.CurrentScene(); current = string.IsNullOrEmpty(scene) ? previous : scene; break;
                        case LogObject.UberState: if (!dontCheckValue) { CheckUberStates(logic); } break;
                            //case LogObject.GameTime: current = dontCheckValue ? previous : logic.Memory.ElapsedTime().ToString("0"); break;
                            //case LogObject.Position: Vector2 point = logic.Memory.Position(); current = $"{point.X:0}, {point.Y:0}"; break;
                    }

                    if (previous != current) {
                        AddEntryUnlocked(new ValueLogEntry(date, key, previous, current));
                        currentValues[key] = current;
                    }
                }
            }
        }
        private void CheckAbilities(LogicManager logic) {
            DateTime date = DateTime.Now;
            Dictionary<AbilityType, Ability> abilities = logic.Memory.PlayerAbilities();
            foreach (KeyValuePair<AbilityType, Ability> pair in abilities) {
                AbilityType key = pair.Key;
                Ability state = pair.Value;

                Ability oldState;
                if (currentAbilities.TryGetValue(key, out oldState)) {
                    byte value = state.HasAbility;
                    byte oldValue = oldState.HasAbility;
                    if (value != oldValue) {
                        AddEntryUnlocked(new ValueLogEntry(date, LogObject.Abilities, oldState, state));
                        currentAbilities[key] = state;
                    }
                } else {
                    currentAbilities[key] = state;
                }
            }
        }
        private void CheckShards(LogicManager logic) {
            DateTime date = DateTime.Now;
            Dictionary<ShardType, Shard> shards = logic.Memory.PlayerShards();
            foreach (KeyValuePair<ShardType, Shard> pair in shards) {
                ShardType key = pair.Key;
                Shard state = pair.Value;

                Shard oldState;
                if (currentShards.TryGetValue(key, out oldState)) {
                    byte value = state.Gained;
                    byte oldValue = oldState.Gained;
                    int valueLevel = state.Level;
                    int oldValueLevel = oldState.Level;
                    if (value != oldValue || valueLevel != oldValueLevel) {
                        AddEntryUnlocked(new ValueLogEntry(date, LogObject.Abilities, oldState, state));
                        currentShards[key] = state;
                    }
                } else {
                    currentShards[key] = state;
                }
            }
        }
        private void CheckUberStates(LogicManager logic) {
            DateTime date = DateTime.Now;
            Dictionary<long, UberState> uberStates = logic.Memory.GetUberStates();
            foreach (KeyValuePair<long, UberState> pair in uberStates) {
                long key = pair.Key;
                UberState state = pair.Value;

                if (state.GroupName == "statsUberStateGroup" || (state.GroupName == "achievementsGroup" && state.Name == "spiritLightGainedCounter")) {
                    continue;
                }

                UberState oldState = null;
                if (currentUberStates.TryGetValue(key, out oldState)) {
                    UberValue value = state.Value;
                    UberValue oldValue = oldState.Value;
                    if (value.Int != oldValue.Int) {
                        AddEntryUnlocked(new ValueLogEntry(date, LogObject.UberState, oldState.Clone(), state.Clone()));
                        oldState.Value = state.Value;
                    }
                } else {
                    currentUberStates[key] = state.Clone();
                }
            }
        }
        private string GetCurrentSplit(LogicManager logic, SplitterSettings settings) {
            if (logic.CurrentSplit >= settings.Autosplits.Count) { return "N/A"; }
            return settings.Autosplits[logic.CurrentSplit].ToString();
        }
    }
    public interface ILogEntry { }
    public class ValueLogEntry : ILogEntry {
        public DateTime Date;
        public LogObject Type;
        public object PreviousValue;
        public object CurrentValue;

        public ValueLogEntry(DateTime date, LogObject type, object previous, object current) {
            Date = date;
            Type = type;
            PreviousValue = previous;
            CurrentValue = current;
        }

        public override string ToString() {
            return string.Concat(
                Date.ToString(@"HH\:mm\:ss.fff"),
                ": (",
                Type.ToString(),
                ") ",
                PreviousValue,
                " -> ",
                CurrentValue
            );
        }
    }
    public class EventLogEntry : ILogEntry {
        public DateTime Date;
        public string Event;

        public EventLogEntry(string description) {
            Date = DateTime.Now;
            Event = description;
        }
        public EventLogEntry(DateTime date, string description) {
            Date = date;
            Event = description;
        }

        public override string ToString() {
            return string.Concat(
                Date.ToString(@"HH\:mm\:ss.fff"),
                ": ",
                Event
            );
        }
    }
}
