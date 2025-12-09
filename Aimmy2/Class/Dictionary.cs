using Visuality;

namespace Aimmy2.Class
{
    public static class Dictionary
    {
        public static string lastLoadedModel = "N/A";
        public static string lastLoadedConfig = "N/A";
        public static DetectedPlayerWindow? DetectedPlayerOverlay;
        public static FOV? FOVWindow;

        public static Dictionary<string, dynamic> bindingSettings = new()
        {
            { "Aim Keybind", "Right"},
            { "Second Aim Keybind", ""},
            { "Dynamic FOV Keybind", ""},
            { "Emergency Stop Keybind", ""},
            { "Model Switch Keybind", ""},
            { "Anti Recoil Keybind", ""},
            { "Disable Anti Recoil Keybind", ""},
            { "Gun 1 Key", ""},
            { "Gun 2 Key", ""},
        };

        public static Dictionary<string, dynamic> sliderSettings = new()
        {
            { "Suggested Model", ""},
            { "FOV Size", 366 },
            { "Dynamic FOV Size", 200 },
            { "Mouse Sensitivity (+/-)", 0.92 },
            { "Mouse Jitter", 0 },
            { "Sticky Aim Threshold", 50 },
            { "Y Offset (Up/Down)", 105.0 },
            { "Y Offset (%)", 20.0 },
            { "X Offset (Left/Right)", 0 },
            { "X Offset (%)", 50 },
            { "EMA Smoothening", 0.66},
            { "Auto Trigger Delay", 0.1 },
            { "AI Minimum Confidence", 50 },
            { "AI Confidence Font Size", 20 },
            { "Corner Radius", 0 },
            { "Border Thickness", 1 },
            { "Opacity", 1 }
        };

        // Make sure the Settings Name is the EXACT Same as the Toggle Name or I will smack you :joeangy:
        // nori
        public static Dictionary<string, dynamic> toggleState = new()
        {
            { "Aim Assist", false },
            { "Sticky Aim", false },
            { "Constant AI Tracking", false },
            { "Predictions", false },
            { "EMA Smoothening", true },
            { "Enable Model Switch Keybind", false },
            { "Enable Gun Switching Keybind", false },
            { "Auto Trigger", false },
            { "Anti Recoil", false },
            { "FOV", false },
            { "Dynamic FOV", false },
            { "Masking", false },
            { "Show Detected Player", false },
            { "Cursor Check", false },
            { "Spray Mode", false },
            //{ "Only When Held", false },
            { "Show FOV", false },
            { "Show AI Confidence", false },
            { "Show Tracers", false },
            { "Collect Data While Playing", false },
            { "Auto Label Data", false },
            { "LG HUB Mouse Movement", false },
            { "Mouse Background Effect", true },
            { "Debug Mode", false },
            { "UI TopMost", false },
            //--
            { "StreamGuard", false },
            //--
            { "X Axis Percentage Adjustment", false },
            { "Y Axis Percentage Adjustment", false }
        };

        public static Dictionary<string, dynamic> minimizeState = new()
        {
            { "Aim Assist", false },
            { "Aim Config", false },
            { "Auto Trigger", true },
            { "Anti Recoil", true },
            { "Anti Recoil Config", true },
            { "FOV Config", true },
            { "ESP Config", true },
            { "Settings Menu", false },
            { "X/Y Percentage Adjustment", true },
            { "Theme Settings", true },
            { "Display Settings", true }
        };

        public static Dictionary<string, dynamic> dropdownState = new()
        {
            { "Prediction Method", "Kalman Filter" },
            { "Detection Area Type", "Closest to Center Screen" },
            { "Aiming Boundaries Alignment", "Bottom" },
            { "Mouse Movement Method", "MAKCU Support" },
            { "Screen Capture Method", "DirectX" },
            { "Tracer Position", "Bottom" },
            { "Movement Path", "Cubic Bezier" },
            { "Image Size", "640" },
            { "Target Class", "Best Confidence" }
        };

        public static Dictionary<string, dynamic> colorState = new()
        {
            { "FOV Color", "#FF8080FF"},
            { "Detected Player Color", "#FF00FFFF"},
            { "Theme Color", "#00BFFF" }
        };

        public static Dictionary<string, dynamic> AntiRecoilSettings = new()
        {
            { "Hold Time", 10 },
            { "Fire Rate", 200 },
            { "Y Recoil (Up/Down)", 10 },
            { "X Recoil (Left/Right)", 0 }
        };

        public static Dictionary<string, dynamic> filelocationState = new()
        {
            { "ddxoft DLL Location", ""},
            { "Gun 1 Config", "" },
            { "Gun 2 Config", "" }
        };
    }
}
