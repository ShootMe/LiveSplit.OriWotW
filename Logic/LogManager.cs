using System;
using System.Collections.Generic;
namespace LiveSplit.OriWotW {
    public enum LogObject {
        None,
        CurrentSplit
    }
    public class LogManager {
        public List<ILogEntry> LogEntries = new List<ILogEntry>();
        private Dictionary<LogObject, object> currentValues = new Dictionary<LogObject, object>();

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
            foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                object previous = currentValues[key];

                object current = null;
                switch (key) {
                    case LogObject.CurrentSplit: current = logic.CurrentSplit; break;
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
                PreviousValue.ToString(),
                " -> ",
                CurrentValue.ToString()
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
