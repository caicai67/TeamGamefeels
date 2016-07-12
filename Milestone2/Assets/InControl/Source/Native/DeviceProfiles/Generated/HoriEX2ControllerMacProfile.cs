using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class HoriEX2ControllerMacProfile : Xbox360DriverMacProfile
	{
		public HoriEX2ControllerMacProfile()
		{
			Name = "Hori EX2 Controller";
			Meta = "Hori EX2 Controller on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x0f0d,
					ProductID = 0x000d,
				},
			};
		}
	}
	// @endcond
}


