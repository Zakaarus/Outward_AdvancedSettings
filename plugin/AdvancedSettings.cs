using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AdvancedSettings {
	[BepInPlugin(GUID, NAME, VERSION)]
	public class AdvancedSettings : BaseUnityPlugin {
		public const string GUID = "faeryn.advancedsettings";
		public const string NAME = "AdvancedSettings";
		public const string VERSION = "0.2.0";
		internal static ManualLogSource Log;
		
		public static ConfigEntry<string> AAType;
        public static ConfigEntry<int> MSAA;

        internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeConfig();
			new Harmony(GUID).PatchAll();
		}

		private void InitializeConfig() {
			AAType = Config.Bind("AdvancedSettings", "Antialiasing", "None", "Changes the antialiasing mode. None, FXAA, SMAA, TAA");
			AAType.SettingChanged += (sender, args) => {
				CameraQuality.RefreshAllCameras();
			};
			MSAA = Config.Bind("AdvancedSettings", "MSAA", 8, "MSAA quality. 0, 2, 4, 8. Don't think it does anything lmao");
			MSAA.SettingChanged += (sender, args) => {
                CameraQuality.RefreshAllCameras();
            };
        }
	}
}