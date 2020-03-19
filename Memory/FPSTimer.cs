using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public class FPSTimer {
        public float FPS = 0;
        private Stopwatch frameTimer = new Stopwatch();
        private int lastFrameCount = 0;
        public void Update(int frameCount) {
            if (frameCount - lastFrameCount >= 20) {
                frameTimer.Stop();

                FPS = (float)((double)10000000 * (double)(frameCount - lastFrameCount) / (double)frameTimer.ElapsedTicks);
                frameTimer.Restart();
                lastFrameCount = frameCount;
            }
        }
    }
}