using System;


namespace InControl.NativeProfile
{
	// @cond nodoc
	public class MadCatzSSF4ChunLiFightStickTEMacProfile : Xbox360DriverMacProfile
	{
		public MadCatzSSF4ChunLiFightStickTEMacProfile()
		{
			Name = "Mad Catz SSF4 Chun-Li FightStick TE";
			Meta = "Mad Catz SSF4 Chun-Li FightStick TE on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x1bad,
					ProductID = 0xf03d,
				},
			};
		}
	}
	// @endcond
}


