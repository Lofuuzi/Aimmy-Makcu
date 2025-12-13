using Aimmy2.Class;
using InputLogic;
using System.Windows.Threading;

namespace Other
{
    public class AntiRecoilManager
    {
        public DispatcherTimer HoldDownTimer = new();
        public int IndependentMousePress = 0;
        
        private static readonly Random _rng = new Random();
        
        // Session random
        private static float _sessionDelayBias = 1.0f;
        private static float _sessionStrengthBias = 1.0f;
        private static bool _sessionActive = false;
        
        // UI-controlled
        public static bool EnableRandomization = true;
        public static float RandomIntensity = 1.0f; // 0.0 ~ 1.0

        private static void BeginSession()
        {
            if (_sessionActive) return;
        
            _sessionActive = true;
        
            if (!EnableRandomization)
            {
                _sessionDelayBias = 1.0f;
                _sessionStrengthBias = 1.0f;
                return;
            }
        
            _sessionDelayBias = Rand(0.95f, 1.05f);
            _sessionStrengthBias = Rand(0.9f, 1.1f);
        }

        private static void EndSession()
        {
            _sessionActive = false;
        }
        
        private static void ExecuteRecoil()
        {
            BeginSession();
        
            float delay = RecoilDelayMs * _sessionDelayBias;
            float strength = RecoilStrength * _sessionStrengthBias;
        
            if (EnableRandomization)
            {
                float dVar = Lerp(0.95f, 1.05f, RandomIntensity);
                float sVar = Lerp(0.9f, 1.1f, RandomIntensity);
        
                delay *= Rand(1f / dVar, dVar);
                strength *= Rand(1f / sVar, sVar);
            }
        
            // Makcu API mouse move（只走 dy）
            MouseManager.Move(0, (int)Math.Round(strength));
        
            _nextRecoilTime = DateTime.Now.AddMilliseconds(delay);
        }
        
        private static float Rand(float min, float max)
        {
            return (float)(_rng.NextDouble() * (max - min) + min);
        }
        
        private static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public void HoldDownLoad()
        {
            if (HoldDownTimer != null)
            {
                HoldDownTimer.Tick += new EventHandler(HoldDownTimerTicker!);
                HoldDownTimer.Interval = TimeSpan.FromMilliseconds(1);
            }
        }

        private void HoldDownTimerTicker(object sender, EventArgs e)
        {
            IndependentMousePress += 1;
            if (IndependentMousePress >= Dictionary.AntiRecoilSettings["Hold Time"])
                MouseManager.DoAntiRecoil();
        }
    }
}
