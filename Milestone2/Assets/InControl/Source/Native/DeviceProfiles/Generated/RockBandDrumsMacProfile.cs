using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class RockBandDrumsMacProfile : Xbox360DriverMacProfile
	{
		public RockBandDrumsMacProfile()
		{
			Name = "Rock Band Drums";
			Meta = "Rock Band Drums on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x1bad,
					ProductID = 0x0003,
				},
			};
		}
	}
	// @endcond
}


