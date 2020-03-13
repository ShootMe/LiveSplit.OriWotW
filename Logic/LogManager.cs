using System;
using System.Collections.Generic;
namespace LiveSplit.OriWotW {
    public enum LogObject {
        None,
        CurrentSplit,
        Energy,
        EnergyFragments,
        Health,
        HealthFragments,
        Position,
        Keystones,
        MapCompletion,
        GameTime,
        Abilities,
        Shards,
        Inventory,
        Area,
        Dead,
        GameState,
        TitleScreen,
        LoadingGame,
        Difficulty
    }
    public class LogManager {
        public List<ILogEntry> LogEntries = new List<ILogEntry>();
        private Dictionary<LogObject, string> currentValues = new Dictionary<LogObject, string>();

        public LogManager() {
            Clear();
            LogEntries.Add(new EventLogEntry("Autosplitter Initialized"));
        }
        public void Clear() {
            LogEntries.Clear();
            foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                currentValues[key] = null;
            }
        }
        public void AddEntry(ILogEntry entry) {
            LogEntries.Add(entry);
            Console.WriteLine(entry.ToString());
        }
        public void Update(LogicManager logic) {
            DateTime date = DateTime.Now;
            bool isDead = logic.Memory.Dead();
            GameState gameState = logic.Memory.GameState();
            bool mainMenu = gameState != GameState.Game;
            foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                string previous = currentValues[key];

                string current = null;
                switch (key) {
                    case LogObject.CurrentSplit: current = logic.CurrentSplit.ToString(); break;
                    case LogObject.Energy: current = mainMenu ? previous : logic.Memory.MaxEnergy().ToString(); break;
                    case LogObject.EnergyFragments: current = isDead || mainMenu ? previous : logic.Memory.EnergyFragments().ToString(); break;
                    case LogObject.Health: current = mainMenu ? previous : logic.Memory.MaxHealth().ToString(); break;
                    case LogObject.HealthFragments: current = isDead || mainMenu ? previous : logic.Memory.HealthFragments().ToString(); break;
                    case LogObject.Keystones: current = isDead || mainMenu ? previous : logic.Memory.Keystones().ToString(); break;
                    case LogObject.MapCompletion: current = mainMenu ? previous : logic.Memory.MapCompletion().ToString("0"); break;
                    case LogObject.GameTime: current = mainMenu ? previous : logic.Memory.ElapsedTime().ToString("0"); break;
                    case LogObject.Abilities: current = isDead || mainMenu ? previous : logic.Memory.PlayerAbilities().PrintList(); break;
                    case LogObject.Shards: current = isDead || mainMenu ? previous : logic.Memory.PlayerShards().PrintList(); break;
                    case LogObject.Inventory: current = isDead || mainMenu ? previous : logic.Memory.Inventory().PrintList(); break;
                    case LogObject.Area: current = isDead || mainMenu ? previous : logic.Memory.PlayerArea(); break;
                    case LogObject.Dead: current = isDead.ToString(); break;
                    case LogObject.GameState: current = gameState.ToString(); break;
                    case LogObject.TitleScreen: current = logic.Memory.MainMenuScreen().ToString(); break;
                    case LogObject.LoadingGame: current = logic.Memory.IsLoadingGame().ToString(); break;
                    case LogObject.Difficulty: current = logic.Memory.Difficulty().ToString(); break;
                        //case LogObject.Position: Vector2 point = logic.Memory.Position(); current = $"{point.X:0}, {point.Y:0}"; break;
                }

                if (previous != current) {
                    AddEntry(new ValueLogEntry(date, key, previous, current));
                    currentValues[key] = current;
                }
            }
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
