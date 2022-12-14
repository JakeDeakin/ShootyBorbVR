using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootyBorbVRMath
{
    public class Math : MonoBehaviour
    {
        public static bool InterceptionDirection(Vector3 a, Vector3 b, Vector3 vA, float sB, out Vector3 result)
        {
            var aToB = b - a;
            var dC = aToB.magnitude;
            var alpha = Vector3.Angle(aToB, vA) * Mathf.Deg2Rad;
            var sA = vA.magnitude;
            var r = sA / sB;

            if (SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out var root1, out var root2) == 0)
            {
                result = Vector3.zero;
                return false;
            }

            var dA = Mathf.Max(root1, root2);
            var t = dA / sB;
            var c = a + vA * t;
            result = (c - b).normalized;
            return true;

        }

        // Math courtesy of jean-gobert de coster on Youtube. Works beautifully! https://www.youtube.com/user/jeangodecoster
        public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                root1 = Mathf.Infinity;
                root2 = -root1;
                return 0;
            }

            root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            return discriminant > 0 ? 2 : 1;
        }
    }
}