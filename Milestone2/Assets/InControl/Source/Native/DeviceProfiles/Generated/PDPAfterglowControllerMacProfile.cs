using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class PDPAfterglowControllerMacProfile : Xbox360DriverMacProfile
	{
		public PDPAfterglowControllerMacProfile()
		{
			Name = "PDP Afterglow Controller";
			Meta = "PDP Afterglow Controller on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x0e6f,
					ProductID = 0x0413,
				},
				new NativeInputDeviceMatcher {
					VendorID = 0x1bad,
					ProductID = 0xf900,
				},
				new NativeInputDeviceMatcher {
					VendorID = 0x0e6f,
					ProductID = 0x0113,
				},
				new NativeInputDeviceMatcher {
					VendorID = 0x0e6f,
					ProductID = 0x0213,
				},
			};
		}
	}
	// @endcond
}


