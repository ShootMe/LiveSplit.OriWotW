using System;
namespace LiveSplit.OriWotW {
    public class PointerCache {
        public IntPtr Value { get; private set; }
        private IntPtr cache;
        private int count;

        public PointerCache() {
            count = 0;
        }

        public void Update(IntPtr pointer) {
            if (pointer != Value) {
                if (cache != pointer) {
                    cache = pointer;
                    count = 0;
                }
                count++;

                if (count > 50 || Value == IntPtr.Zero) {
                    Value = pointer;
                    count = 0;
                }
            } else {
                count = 0;
            }
        }
        public static implicit operator IntPtr(PointerCache cache) {
            return cache.Value;
        }
    }
}