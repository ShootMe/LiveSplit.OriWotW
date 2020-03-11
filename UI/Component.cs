using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
namespace LiveSplit.OriWotW {
    public class Component : IComponent {
        public string ComponentName { get { return Factory.AutosplitterName; } }
        public TimerModel Model { get; set; }
        public IDictionary<string, Action> ContextMenuControls { get { return null; } }
        private LogicManager logic;
        private UserSettings userSettings;
        private LogManager log;
        private Thread timerLoop;

        public static void Main(string[] args) {
            Component component = new Component(null);
            Application.Run();
        }
        public Component(LiveSplitState state) {
            logic = new LogicManager();
            log = new LogManager();
            userSettings = new UserSettings(state,log);

            if (state != null) {
                Model = new TimerModel() { CurrentState = state };
                Model.InitializeGameTime();
                Model.CurrentState.IsGameTimePaused = true;
                state.OnReset += OnReset;
                state.OnPause += OnPause;
                state.OnResume += OnResume;
                state.OnStart += OnStart;
                state.OnSplit += OnSplit;
                state.OnUndoSplit += OnUndoSplit;
                state.OnSkipSplit += OnSkipSplit;
            }

            StartAutosplitter();
        }

        public void StartAutosplitter() {
            if (timerLoop != null) { return; }

            timerLoop = new Thread(UpdateLoop);
            timerLoop.IsBackground = true;
            timerLoop.Priority = ThreadPriority.AboveNormal;
            timerLoop.Start();
        }
        private void UpdateLoop() {
            while (timerLoop != null) {
                try {
                    if (logic.IsHooked()) {
                        logic.Update(userSettings.Settings);
                        log.Update(logic);
                    }
                    HandleLogic();
                } catch (Exception ex) {
                    log.AddEntry(new EventLogEntry(ex.ToString()));
                }
                Thread.Sleep(8);
            }
        }
        private void HandleLogic() {
            if (Model == null) { return; }

            Model.CurrentState.IsGameTimePaused = logic.Paused;
            if (logic.GameTime >= 0) {
                Model.CurrentState.SetGameTime(TimeSpan.FromSeconds(logic.GameTime));
            }

            if (logic.ShouldReset) {
                if (logic.CurrentSplit >= 0) {
                    Model.Reset();
                }
            } else if (logic.ShouldSplit) {
                if (!logic.Running) {
                    Model.Start();
                } else {
                    Model.Split();
                }
            }
        }
        public void OnReset(object sender, TimerPhase e) {
            logic.Reset();
            log.AddEntry(new EventLogEntry("Reset Splits"));
        }
        public void OnResume(object sender, EventArgs e) {
            log.AddEntry(new EventLogEntry("Resumed Splits"));
        }
        public void OnPause(object sender, EventArgs e) {
            log.AddEntry(new EventLogEntry("Paused Splits"));
        }
        public void OnStart(object sender, EventArgs e) {
            Model.CurrentState.SetGameTime(TimeSpan.Zero);
            Model.CurrentState.IsGameTimePaused = true;
            log.AddEntry(new EventLogEntry("Started Splits"));
        }
        public void OnUndoSplit(object sender, EventArgs e) {
            logic.Undo();
            log.AddEntry(new EventLogEntry("Undo Current Split"));
        }
        public void OnSkipSplit(object sender, EventArgs e) {
            logic.Skip();
            log.AddEntry(new EventLogEntry("Skip Current Split"));
        }
        public void OnSplit(object sender, EventArgs e) {
            log.AddEntry(new EventLogEntry("Split"));
        }
        public void Update(IInvalidator invalidator, LiveSplitState lvstate, float width, float height, LayoutMode mode) {
            
        }
        public Control GetSettingsControl(LayoutMode mode) { return userSettings; }
        public void SetSettings(XmlNode document) { userSettings.InitializeSettings(document); }
        public XmlNode GetSettings(XmlDocument document) { return userSettings.UpdateSettings(document); }
        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion) { }
        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion) { }
        public float HorizontalWidth { get { return 0; } }
        public float MinimumHeight { get { return 0; } }
        public float MinimumWidth { get { return 0; } }
        public float PaddingBottom { get { return 0; } }
        public float PaddingLeft { get { return 0; } }
        public float PaddingRight { get { return 0; } }
        public float PaddingTop { get { return 0; } }
        public float VerticalHeight { get { return 0; } }
        public void Dispose() {
            if (timerLoop != null) {
                timerLoop = null;
            }
            if (Model != null) {
                Model.CurrentState.OnReset -= OnReset;
                Model.CurrentState.OnPause -= OnPause;
                Model.CurrentState.OnResume -= OnResume;
                Model.CurrentState.OnStart -= OnStart;
                Model.CurrentState.OnSplit -= OnSplit;
                Model.CurrentState.OnUndoSplit -= OnUndoSplit;
                Model.CurrentState.OnSkipSplit -= OnSkipSplit;
                Model = null;
            }
            userSettings.Dispose();
        }
    }
}