using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class HORIRealArcadeProEXPremiumVLXMacProfile : Xbox360DriverMacProfile
	{
		public HORIRealArcadeProEXPremiumVLXMacProfile()
		{
			Name = "HORI Real Arcade Pro EX Premium VLX";
			Meta = "HORI Real Arcade Pro EX Premium VLX on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x1bad,
					ProductID = 0xf506,
				},
			};
		}
	}
	// @endcond
}


