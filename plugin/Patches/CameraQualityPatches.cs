using HarmonyLib;
using UnityEngine.Rendering.PostProcessing;

namespace AdvancedSettings.Patches {
	[HarmonyPatch(typeof(CameraQuality))]
	public static class CameraQualityPatches {
		[HarmonyPatch(nameof(CameraQuality.RefreshQuality)), HarmonyPostfix]
		private static void CameraQuality_RefreshQuality_Postfix(CameraQuality __instance) {
			if (OptionManager.Instance == null || !OptionManager.Instance.OptionsLoaded) {
				return;
			}
			if (AdvancedSettings.EnableTAA.Value) {
				__instance.GetComponent<PostProcessLayer>().antialiasingMode = OptionManager.Instance.CurrentGraphicSettings.AntiAliasingQuality == 0 ? PostProcessLayer.Antialiasing.None : PostProcessLayer.Antialiasing.TemporalAntialiasing;
			}
		}
	}
}