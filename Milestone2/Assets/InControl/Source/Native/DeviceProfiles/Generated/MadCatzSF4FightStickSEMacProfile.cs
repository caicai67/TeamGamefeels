using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class MadCatzSF4FightStickSEMacProfile : Xbox360DriverMacProfile
	{
		public MadCatzSF4FightStickSEMacProfile()
		{
			Name = "Mad Catz SF4 FightStick SE";
			Meta = "Mad Catz SF4 FightStick SE on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x0738,
					ProductID = 0x4718,
				},
			};
		}
	}
	// @endcond
}


