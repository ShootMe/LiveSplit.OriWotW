using System;
using System.Windows.Forms;
namespace LiveSplit.OriWotW {
    public partial class LogViewer : Form {
        public object DataSource { get; set; }
        public LogViewer() {
            InitializeComponent();
        }

        private void LogViewer_Load(object sender, EventArgs e) {
            gridDetails.DataSource = DataSource;
        }
    }
}
