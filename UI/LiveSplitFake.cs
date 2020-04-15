using System;
using System.Collections;
using System.Collections.Generic;
namespace LiveSplit.OriWotW {
#if Console
    public interface IComponentFactory { }
    public class ComponentCategory {
        public static ComponentCategory Control;
    }
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class ComponentFactoryAttribute : Attribute {
        public ComponentFactoryAttribute(Type componentFactoryClassType) {
            ComponentFactoryClassType = componentFactoryClassType;
        }
        public Type ComponentFactoryClassType { get; }
    }
    public class RunEditorDialog : Form { }
    public interface IComponent { }
    public enum TimerPhase {
        NotRunning = 0,
        Running = 1,
        Ended = 2,
        Paused = 3
    }
    public enum LayoutMode {
        Horizontal = 0,
        Vertical = 1
    }
    public interface IInvalidator { }
    public interface ILayout {
        IList<ILayoutComponent> LayoutComponents { get; set; }
    }
    public interface ILayoutComponent {
        IComponent Component { get; set; }
    }
    public class TextComponentSettings {
        public string Text1;
        public string Text2;
    }
    public class TextComponent {
        public TextComponentSettings Settings;
    }
    public struct Time {
        public TimeSpan? RealTime;
        public TimeSpan? GameTime;
    }
    public class TimerModel {
        public LiveSplitState CurrentState;
        public void InitializeGameTime() { }
        public void Start() { }
        public void Reset() { }
        public void Split() { }
    }
    public class LiveSplitState {
        public bool IsGameTimePaused;
        public TimerPhase CurrentPhase;
        public Time CurrentTime;
        public Run Run;
        public ILayout Layout;
        public int CurrentSplitIndex;
        public event EventHandler OnPause;
        public event EventHandler OnResume;
        public event EventHandlerT<TimerPhase> OnReset;
        public event EventHandler OnSkipSplit;
        public event EventHandler OnUndoSplit;
        public event EventHandler OnSplit;
        public event EventHandler OnStart;
        public delegate void EventHandlerT<T>(object sender, T value);
        public void SetGameTime(TimeSpan time) { }
    }
    public interface ISegment {
        string Name { get; }
    }
    public class Run : IEnumerable<ISegment> {
        public IEnumerator<ISegment> GetEnumerator() { return null; }
        IEnumerator IEnumerable.GetEnumerator() { return null; }
    }
#endif
}