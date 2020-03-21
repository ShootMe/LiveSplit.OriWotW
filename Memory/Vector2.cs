using System;
using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Vector2 {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;

        public override bool Equals(object obj) {
            return obj is Vector2 temp && temp.X == X && temp.Y == Y;
        }
        public static bool operator ==(Vector2 one, Vector2 two) {
            return one.X == two.X && one.Y == two.Y;
        }
        public static bool operator !=(Vector2 one, Vector2 two) {
            return one.X != two.X || one.Y != two.Y;
        }
        public static Vector2 operator -(Vector2 one, Vector2 two) {
            return new Vector2() { X = one.X - two.X, Y = one.Y - two.Y };
        }
        public static Vector2 operator +(Vector2 one, Vector2 two) {
            return new Vector2() { X = one.X + two.X, Y = one.Y + two.Y };
        }
        public static Vector2 operator *(Vector2 one, Vector2 two) {
            return new Vector2() { X = one.X * two.X, Y = one.Y * two.Y };
        }
        public static Vector2 operator *(Vector2 one, int two) {
            return new Vector2() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector2 operator *(int two, Vector2 one) {
            return new Vector2() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector2 operator *(Vector2 one, float two) {
            return new Vector2() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector2 operator *(float two, Vector2 one) {
            return new Vector2() { X = one.X * two, Y = one.Y * two };
        }
        public static float operator !(Vector2 one) {
            return (float)Math.Sqrt(one.X * one.X + one.Y * one.Y);
        }
        public override string ToString() {
            return $"{X:0.00}, {Y:0.00}";
        }
        public override int GetHashCode() {
            return (int)(X * Y);
        }
    }
}
