using HarmonyLib;
using UnityStandardAssets.ImageEffects;
using BepInEx.Logging;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.PostProcessing.PostProcessLayer;
using static UnityEngine.QualitySettings;
using Antialiasing = UnityEngine.Rendering.PostProcessing.PostProcessLayer.Antialiasing;

namespace AdvancedSettings.Patches 
{
	[HarmonyPatch(typeof(CameraQuality))]
	public static class CameraQualityPatches
	{
		[HarmonyPatch(nameof(CameraQuality.RefreshQuality)), HarmonyPostfix]
		private static void CameraQuality_RefreshQuality_Postfix(CameraQuality __instance)
		{
            AdvancedSettings.Log.LogMessage("SETTING ANTIALIASING CHANGING...");
            if (OptionManager.Instance == null || !OptionManager.Instance.OptionsLoaded)
			{
				return;
			}
			Antialiasing mode = Antialiasing.None;
			switch (AdvancedSettings.AAType.Value) 
			{
				case "TAA":
					mode = Antialiasing.TemporalAntialiasing;
                    break;

                case "SMAA":
                    mode = Antialiasing.SubpixelMorphologicalAntialiasing;
                    break;

                case "FXAA":
                    mode = Antialiasing.FastApproximateAntialiasing;
                    break;

                case "None":
                    mode = Antialiasing.None;
                    break;

				default:
                    mode = Antialiasing.None;
                    break;
			}
			antiAliasing = AdvancedSettings.MSAA.Value;
            __instance.GetComponent<PostProcessLayer>().antialiasingMode = mode;
			AdvancedSettings.Log.LogMessage("CHANGED SETTING ANTIALIASING");
        }
	}
}