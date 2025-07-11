public static class UnityLayers
{
	public static readonly int LayerDefault = UnityEngine.LayerMask.NameToLayer("Default");
	public static readonly int LayerMaskDefault = 1 << LayerDefault;

	public static readonly int LayerTransparentFX = UnityEngine.LayerMask.NameToLayer("TransparentFX");
	public static readonly int LayerMaskTransparentFX = 1 << LayerTransparentFX;

	public static readonly int LayerIgnoreRaycast = UnityEngine.LayerMask.NameToLayer("Ignore Raycast");
	public static readonly int LayerMaskIgnoreRaycast = 1 << LayerIgnoreRaycast;

	public static readonly int LayerWater = UnityEngine.LayerMask.NameToLayer("Water");
	public static readonly int LayerMaskWater = 1 << LayerWater;

	public static readonly int LayerUI = UnityEngine.LayerMask.NameToLayer("UI");
	public static readonly int LayerMaskUI = 1 << LayerUI;

}
