using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public class FPSTimer {
        private TimeCalculation averageTime;
        private TimeCalculation shortTime;
        private Stopwatch frameTimer;
        private int lastFrameCount = 0;
        private long lastTicks = 0;
        private long updateDisplay;
        private float cacheFPS;
        public float FPS {
            get {
                if (updateDisplay > 2500000) {
                    updateDisplay = 0;
                    cacheFPS = averageTime.FPS;
                }
                return cacheFPS;
            }
        }
        public float FPSShort { get { return shortTime.FPS; } }
        public FPSTimer(int aveSamples, int shortSamples) {
            frameTimer = new Stopwatch();
            frameTimer.Start();
            averageTime = new TimeCalculation(aveSamples);
            shortTime = new TimeCalculation(shortSamples);
        }
        public void Reset() {
            lastTicks = 0;
            lastFrameCount = 0;
            averageTime.Reset();
            shortTime.Reset();
        }
        public void Update(int frameCount) {
            long ticks = frameTimer.ElapsedTicks;
            long ticksDifference = ticks - lastTicks;
            if (frameCount <= 0) { frameCount = lastFrameCount; }
            int frameDifference = frameCount - lastFrameCount;
            if (lastFrameCount > 0) {
                updateDisplay += ticksDifference;
                averageTime.AddSample(ticksDifference, frameDifference);
                shortTime.AddSample(ticksDifference, frameDifference);
            }
            lastTicks = ticks;
            lastFrameCount = frameCount;
        }
    }
    public class TimeCalculation {
        private TimeSlice[] samples;
        private long totalTime;
        private int totalFrames;
        private int index;

        public TimeCalculation(int sampleCount) {
            samples = new TimeSlice[sampleCount];
        }
        public void Reset() {
            totalTime = 0;
            totalFrames = 0;
            for (int i = 0; i < samples.Length; i++) {
                samples[i] = default(TimeSlice);
            }
        }
        public void AddSample(long ticks, int frames) {
            TimeSlice slice = samples[index];
            totalTime += ticks - slice.Ticks;
            totalFrames += frames - slice.Frames;
            slice.Ticks = ticks;
            slice.Frames = frames;
            samples[index++] = slice;
            if (index >= samples.Length) {
                index = 0;
            }
        }
        public float FPS {
            get { return (float)((double)10000000 * (double)totalFrames / (double)totalTime); }
        }
        public override string ToString() {
            return FPS.ToString("0.0");
        }
    }
    public struct TimeSlice {
        public long Ticks;
        public int Frames;

        public override string ToString() {
            return $"{Ticks}-{Frames}";
        }
    }
}