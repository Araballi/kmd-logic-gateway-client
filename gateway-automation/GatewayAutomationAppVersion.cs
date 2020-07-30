using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Gateway.Automation
{
    public static class GatewayAutomationAppVersion
    {
        public static string Current =>
            FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
    }
}
