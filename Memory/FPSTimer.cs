using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public class FPSTimer {
        public float FPS = 0;
        private Stopwatch frameTimer;
        private int lastFrameCount = 0;

        public FPSTimer() {
            frameTimer = new Stopwatch();
            frameTimer.Start();
        }
        public void Update(int frameCount) {
            if (frameTimer.ElapsedTicks >= 5000000) {
                FPS = (float)((double)10000000 * (double)(frameCount - lastFrameCount) / (double)frameTimer.ElapsedTicks);
                frameTimer.Restart();
                lastFrameCount = frameCount;
            }
        }
    }
}