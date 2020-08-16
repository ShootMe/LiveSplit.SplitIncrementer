using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;
namespace LiveSplit.SplitIncrementer {
    public class IncrementerFactory : IComponentFactory {
        public string ComponentName { get { return "Split Name Auto Incrementer"; } }
        public string Description { get { return "Split Name Auto Incrementer"; } }
        public ComponentCategory Category { get { return ComponentCategory.Control; } }
        public IComponent Create(LiveSplitState state) { return new IncrementerComponent(); }
        public string UpdateName { get { return this.ComponentName; } }
		public string UpdateURL { get { return ""; } }
		public string XMLURL { get { return ""; } }
		public Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
    }
}