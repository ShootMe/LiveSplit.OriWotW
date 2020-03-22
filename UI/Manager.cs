using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
namespace LiveSplit.OriWotW {
    public partial class Manager : Form {
        public MemoryManager Memory { get; set; }
        private Thread timerLoop;
        private bool useLivesplitColors = true;
        private Vector2 lastPosition, lastSpeed;
        private bool noPause = false;
        private bool fpsLock = false;
        private int lastFrameCount;
#if Manager
        public static void Main(string[] args) {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Manager());
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
#endif
        public Manager() {
            this.DoubleBuffered = true;
            InitializeComponent();
            Memory = new MemoryManager();
            StartUpdateLoop();
            Text = "Ori WotW " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }
        public void StartUpdateLoop() {
            if (timerLoop != null) { return; }

            timerLoop = new Thread(UpdateLoop);
            timerLoop.IsBackground = true;
            timerLoop.Priority = ThreadPriority.AboveNormal;
            timerLoop.Start();
        }
        private void UpdateLoop() {
            bool lastHooked = false;
            while (timerLoop != null) {
                try {
                    bool hooked = Memory.HookProcess();
                    if (hooked) {
                        UpdateValues();
                    }
                    if (lastHooked != hooked) {
                        lastHooked = hooked;
                        this.Invoke((Action)delegate () { lblNote.Visible = !hooked; });
                    }
                } catch { }
                Thread.Sleep(7);
            }
        }
        public void UpdateValues() {
            if (this.InvokeRequired) { this.Invoke((Action)UpdateValues); return; }

            GameState gameState = Memory.GameState();
            float FPS = Memory.FPS();
            int frameCount = Memory.FrameCount();
            Vector2 position = Memory.Position();
            Vector2 speed = lastSpeed;
            if (frameCount != lastFrameCount || lastPosition != position) {
                speed = (position - lastPosition) * FPS;
                lastFrameCount = frameCount;
                lastSpeed = speed;
                lastPosition = position;
            }
            Stats stats = Memory.PlayerStats();
            AreaType area = Memory.PlayerArea();
            float mapCompletion = Memory.MapCompletion();
            float areaCompletion = Memory.MapCompletion(area);
            string scene = Memory.CurrentScene();
            if (string.IsNullOrEmpty(scene)) { scene = "N/A"; }

            lblMap.Text = $"Total: {mapCompletion:0.00}%";
            lblArea.Text = $"Area: {area} - {areaCompletion:0.00}%";
            lblScene.Text = $"Scene: {scene}";
            lblPos.Text = $"Pos: {position}";
            lblSpeed.Text = $"Speed: {speed} ({!speed:0.00})";
            string debugEnabled = Memory.DebugEnabled() ? "On" : "Off";
            noPause = Memory.NoPauseEnabled();
            string noPuaseEnabled = noPause ? "On" : "Off";
            fpsLock = Memory.FPSLockEnabled();
            string fpsLockEnabled = fpsLock ? "On" : "Off";
            lblExtra.Text = $"Debug: {debugEnabled}  NoPause: {noPuaseEnabled} FPS Lock: {fpsLockEnabled}";
            lblFPS.Text = $"FPS: {FPS:0.0}";

            if (gameState == GameState.Game) {
                lblHP.Text = $"HP: {stats.Health:0} / {stats.MaxHealth}";
                lblEN.Text = $"EN: {stats.Energy:0.0} / {stats.MaxEnergy:0}";
                lblOre.Text = $"Ore: {Memory.Ore()}";
                lblKeys.Text = $"Keys: {Memory.Keystones()}";
                lblSaved.Text = $"Save: {stats}";
            } else {
                lblHP.Text = "HP: N/A";
                lblEN.Text = "EN: N/A";
                lblOre.Text = "Ore: N/A";
                lblKeys.Text = "Keys: N/A";
                lblSaved.Text = "Save: N/A";
            }
        }
        private void Manager_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Control) { return; }

            if (e.KeyCode == Keys.L) {
                useLivesplitColors = !useLivesplitColors;
                if (useLivesplitColors) {
                    this.BackColor = Color.White;
                    this.ForeColor = Color.Black;
                } else {
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.White;
                }
            } else if (e.KeyCode == Keys.D) {
                Memory.EnableDebug(!Memory.DebugEnabled());
            } else if (e.KeyCode == Keys.F) {
                Memory.PatchFPSLock(!fpsLock);
            } else if (e.KeyCode == Keys.N) {
                Memory.PatchNoPause(!noPause);
            }
        }
    }
}
