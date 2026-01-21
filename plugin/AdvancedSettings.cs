using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AdvancedSettings {
	[BepInPlugin(GUID, NAME, VERSION)]
	public class AdvancedSettings : BaseUnityPlugin {
		public const string GUID = "faeryn.advancedsettings";
		public const string NAME = "AdvancedSettings";
		public const string VERSION = "0.1.0";
		internal static ManualLogSource Log;
		
		public static ConfigEntry<string> AAType;

		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeConfig();
			new Harmony(GUID).PatchAll();
		}

		private void InitializeConfig() {
			AAType = Config.Bind("AdvancedSettings", "Enable Temporal Antialiasing", "None", "Changes the antialiasing mode to TAA. Note: It only takes effect if Antialiasing is enabled in the Settings menu.");
			AAType.SettingChanged += (sender, args) => {
				CameraQuality.RefreshAllCameras();
			};
		}
	}
}