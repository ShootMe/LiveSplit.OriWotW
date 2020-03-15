using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
namespace LiveSplit.OriWotW {
    public class Factory : IComponentFactory {
        public static string AutosplitterName = "Ori Will of the Wisps Autosplitter";
        static Factory() {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        public string ComponentName { get { return $"{AutosplitterName} v{Version.ToString(3)}"; } }
        public string Description { get { return AutosplitterName; } }
        public ComponentCategory Category { get { return ComponentCategory.Control; } }
        public IComponent Create(LiveSplitState state) { return new Component(state); }
        public string UpdateName { get { return this.ComponentName; } }
        public string UpdateURL { get { return "https://raw.githubusercontent.com/ShootMe/LiveSplit.OriWotW/master/"; } }
        public string XMLURL { get { return this.UpdateURL + "Components/Updates.xml"; } }
        public Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
    }
}