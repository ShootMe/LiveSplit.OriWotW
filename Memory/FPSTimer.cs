using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public class FPSTimer {
        private const long AveTimeTicks = 10000000;
        private const long ShortTimeTicks = 1000000;
        public float FPS = 0;
        public float FPSShort = 0;
        private Stopwatch frameTimer;
        private int lastFrameCount = 0;
        private int lastFrameShort = 0;
        private long lastTicks = 0;
        private long lastTicksShort = 0;
        public FPSTimer() {
            frameTimer = new Stopwatch();
            frameTimer.Start();
        }
        public void Reset() {
            lastTicksShort = 0;
            lastTicks = 0;
            lastFrameCount = 0;
            lastFrameShort = 0;
        }
        public void Update(int frameCount) {
            long ticks = frameTimer.ElapsedTicks;
            if (ticks >= lastTicks) {
                if (lastFrameCount > 0) {
                    FPS = (float)((double)10000000 * (double)(frameCount - lastFrameCount) / (double)(ticks - lastTicks + AveTimeTicks));
                }
                lastFrameCount = frameCount;
                lastTicks = ticks + AveTimeTicks;
            }
            if (ticks >= lastTicksShort) {
                if (lastFrameShort > 0) {
                    FPSShort = (float)((double)10000000 * (double)(frameCount - lastFrameShort) / (double)(ticks - lastTicksShort + ShortTimeTicks));
                }
                lastFrameShort = frameCount;
                lastTicksShort = ticks + ShortTimeTicks;
            }
        }
    }
}