using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class RazerAtroxArcadeStickMacProfile : Xbox360DriverMacProfile
	{
		public RazerAtroxArcadeStickMacProfile()
		{
			Name = "Razer Atrox Arcade Stick";
			Meta = "Razer Atrox Arcade Stick on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x1532,
					ProductID = 0x0a00,
				},
			};
		}
	}
	// @endcond
}


