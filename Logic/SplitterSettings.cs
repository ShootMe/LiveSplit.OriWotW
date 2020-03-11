using System.Collections.Generic;
using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public class SplitterSettings {
        public BindingList<Split> Autosplits = new BindingList<Split>();

        public SplitterSettings() {
            Autosplits.AllowNew = true;
            Autosplits.AllowRemove = true;
            Autosplits.AllowEdit = true;
        }
    }
}