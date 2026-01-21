using HarmonyLib;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.PostProcessing.PostProcessLayer;

namespace AdvancedSettings.Patches 
{
	[HarmonyPatch(typeof(CameraQuality))]
	public static class CameraQualityPatches
	{
		[HarmonyPatch(nameof(CameraQuality.RefreshQuality)), HarmonyPostfix]
		private static void CameraQuality_RefreshQuality_Postfix(CameraQuality __instance)
		{
			if (OptionManager.Instance == null || !OptionManager.Instance.OptionsLoaded)
			{
				return;
			}
			Antialiasing mode = mode = Antialiasing.None ;
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
                    break;
			}
            __instance.GetComponent<PostProcessLayer>().antialiasingMode = mode;
        }
	}
}