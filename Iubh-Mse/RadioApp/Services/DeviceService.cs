using Iubh.RadioApp.Common.Options;
using Iubh.RadioApp.Common.Services;

namespace Iubh.RadioApp.Droid.Services
{
    public class DeviceService: IDeviceService
    {
        public DeviceOption Platform => DeviceOption.Droid;

    }
}