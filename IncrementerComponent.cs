using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
namespace LiveSplit.SplitIncrementer {
	public class IncrementerComponent : IComponent {
		public string ComponentName { get { return "Split Name Auto Incrementer"; } }
		public TimerModel Model { get; set; }
		public IDictionary<string, Action> ContextMenuControls { get { return null; } }
		private int currentSplit = -1;

		public IncrementerComponent() { }

		public void Update(IInvalidator invalidator, LiveSplitState lvstate, float width, float height, LayoutMode mode) {
			if (Model == null) {
				Model = new TimerModel() { CurrentState = lvstate };
				lvstate.OnReset += OnReset;
				lvstate.OnPause += OnPause;
				lvstate.OnResume += OnResume;
				lvstate.OnStart += OnStart;
				lvstate.OnSplit += OnSplit;
				lvstate.OnUndoSplit += OnUndoSplit;
				lvstate.OnSkipSplit += OnSkipSplit;
			}
		}

		public void OnReset(object sender, TimerPhase e) {
			currentSplit = 0;
		}
		public void OnResume(object sender, EventArgs e) { }
		public void OnPause(object sender, EventArgs e) { }
		public void OnStart(object sender, EventArgs e) {
			currentSplit = 0;
		}
		public void OnUndoSplit(object sender, EventArgs e) {
			currentSplit--;
			ChangeSplitNames(false);
		}
		public void OnSkipSplit(object sender, EventArgs e) {
			ChangeSplitNames(true);
			currentSplit++;
		}
		public void OnSplit(object sender, EventArgs e) {
			ChangeSplitNames(true);
			currentSplit++;
		}
		public void ChangeSplitNames(bool increment) {
			string splitName = Model.CurrentState.Run[currentSplit].Name;
			int counterIndex = splitName.IndexOfAny(new char[] { '(', '[', '{' });
			if (counterIndex >= 0) {
				int endCounterIndex = splitName.IndexOfAny(new char[] { ')', ']', '}' });

				int currentCounter = -1;
				if (int.TryParse(splitName.Substring(counterIndex + 1, endCounterIndex - counterIndex - 1), out currentCounter)) {
					if (increment) {
						currentCounter++;
					} else {
						currentCounter--;
					}

					Model.CurrentState.Run[currentSplit].Name = splitName.Substring(0, counterIndex + 1) + currentCounter.ToString() + splitName.Substring(endCounterIndex);
				}
			}
		}
		public Control GetSettingsControl(LayoutMode mode) { return null; }
		public void SetSettings(XmlNode settings) { }
		public XmlNode GetSettings(XmlDocument document) { return document.CreateElement("Settings"); }
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
		public void Dispose() { }
	}
}