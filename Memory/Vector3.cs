using System;
using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 1)]
    public struct Vector3 {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;
        [FieldOffset(8)]
        public float Z;

        public Vector3(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Vector3(float x) {
            this.X = x;
            this.Y = x;
            this.Z = x;
        }
        public Vector3(Vector2 pos, float z) {
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = z;
        }
        public static Vector3 _Vector3(Vector2 pos, float z) {
            return new Vector3(pos, z);
        }
        public override bool Equals(object obj) {
            return obj is Vector3 temp && temp.X == X && temp.Y == Y;
        }
        public static bool operator ==(Vector3 one, Vector3 two) {
            return one.X == two.X && one.Y == two.Y;
        }
        public static bool operator !=(Vector3 one, Vector3 two) {
            return one.X != two.X || one.Y != two.Y;
        }
        public static Vector3 operator -(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X - two.X, Y = one.Y - two.Y };
        }
        public static Vector3 operator +(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X + two.X, Y = one.Y + two.Y };
        }
        public static Vector3 operator *(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X * two.X, Y = one.Y * two.Y };
        }
        public static Vector3 operator *(Vector3 one, int two) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(int two, Vector3 one) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(Vector3 one, float two) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(float two, Vector3 one) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static float operator !(Vector3 one) {
            return (float)Math.Sqrt(one.X * one.X + one.Y * one.Y);
        }
        public static bool Equal(Vector3 a, Vector3 b) {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static float Distance(ref Vector3 position, ref Vector3 subtract) {
			double d = (double)((position.Y - subtract.Y) * (position.Y - subtract.Y)) + ((position.X - subtract.X) * (position.X - subtract.X));
            return (float)Math.Sqrt(d);
		}
    public override string ToString() {
            return $"{X:0.00}, {Y:0.00}, {Z:0.00}";
        }
        public override int GetHashCode() {
            return (int)(X * Y * Z);
        }
    }
}
