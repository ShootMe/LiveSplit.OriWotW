using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace LiveSplit.OriWotW {
    public partial class Manager : Form {
        public MemoryManager Memory { get; set; }
        private Thread timerLoop;
        private bool useLivesplitColors = true;
        private Vector2 lastPosition;
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
            Vector2 position = Memory.Position();
            Vector2 speed = (position - lastPosition) * 120;
            lastPosition = position;
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


            if (gameState == GameState.Game) {
                lblHP.Text = $"HP: {stats.Health:0} / {stats.MaxHealth}";
                lblEN.Text = $"EN: {stats.Energy:0.0} / {stats.MaxEnergy:0}";
                lblOre.Text = $"Ore: {Memory.Ore()}";
                lblKeys.Text = $"Keys: {Memory.Keystones()}";
            } else {
                lblHP.Text = "HP: N/A";
                lblEN.Text = "EN: N/A";
                lblOre.Text = "Ore: N/A";
                lblKeys.Text = "Keys: N/A";
            }
        }
        private void Manager_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.L) {
                useLivesplitColors = !useLivesplitColors;
                if (useLivesplitColors) {
                    this.BackColor = Color.White;
                    this.ForeColor = Color.Black;
                } else {
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.White;
                }
            }
        }
    }
}
