/*
 * Math3D.cs - Cross-platform 3D math types for CNC Core
 *
 * v0.46 / 2025-01-15 / Io Engineering (Terje Io)
 *
 */

/*

Copyright (c) 2018-2025, Io Engineering (Terje Io)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

· Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

· Redistributions in binary form must reproduce the above copyright notice, this
list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

· Neither the name of the copyright holder nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

using System;

namespace CNC.Core
{
#if !WINDOWS
    /// <summary>
    /// Cross-platform Point3D implementation for non-Windows platforms
    /// Compatible with System.Windows.Media.Media3D.Point3D
    /// </summary>
    public struct Point3D : IEquatable<Point3D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D operator +(Point3D point, Vector3D vector)
        {
            return new Point3D(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point3D operator -(Point3D point, Vector3D vector)
        {
            return new Point3D(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }

        public static Vector3D operator -(Point3D point1, Point3D point2)
        {
            return new Vector3D(point1.X - point2.X, point1.Y - point2.Y, point1.Z - point2.Z);
        }

        public static bool operator ==(Point3D point1, Point3D point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point3D point1, Point3D point2)
        {
            return !point1.Equals(point2);
        }

        public bool Equals(Point3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Point3D point && Equals(point);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }

    /// <summary>
    /// Cross-platform Vector3D implementation for non-Windows platforms
    /// Compatible with System.Windows.Media.Media3D.Vector3D
    /// </summary>
    public struct Vector3D : IEquatable<Vector3D>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public double LengthSquared => X * X + Y * Y + Z * Z;

        public void Normalize()
        {
            double length = Length;
            if (length > 0)
            {
                X /= length;
                Y /= length;
                Z /= length;
            }
        }

        public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        public static Vector3D operator -(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return new Vector3D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            return vector * scalar;
        }

        public static Vector3D operator /(Vector3D vector, double scalar)
        {
            return new Vector3D(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }

        public static double DotProduct(Vector3D vector1, Vector3D vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public static Vector3D CrossProduct(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X
            );
        }

        public static bool operator ==(Vector3D vector1, Vector3D vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector3D vector1, Vector3D vector2)
        {
            return !vector1.Equals(vector2);
        }

        public bool Equals(Vector3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3D vector && Equals(vector);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
    
    /// <summary>
    /// Cross-platform Vector3 implementation compatible with RP.Math.Vector3
    /// </summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(double[] values)
        {
            X = values.Length > 0 ? values[0] : 0;
            Y = values.Length > 1 ? values[1] : 0;
            Z = values.Length > 2 ? values[2] : 0;
        }

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public double LengthSquared => X * X + Y * Y + Z * Z;

        public void Normalize()
        {
            double length = Length;
            if (length > 0)
            {
                X /= length;
                Y /= length;
                Z /= length;
            }
        }

        public Vector3 RotateZ(double originX, double originY, double angleRadians)
        {
            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);
            
            double translatedX = X - originX;
            double translatedY = Y - originY;
            
            double rotatedX = translatedX * cos - translatedY * sin;
            double rotatedY = translatedX * sin + translatedY * cos;
            
            return new Vector3(rotatedX + originX, rotatedY + originY, Z);
        }

        public Vector3 Round(int precision)
        {
            return new Vector3(
                Math.Round(X, precision),
                Math.Round(Y, precision),
                Math.Round(Z, precision)
            );
        }

        public static Vector3 operator +(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        public static Vector3 operator -(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        public static Vector3 operator *(Vector3 vector, double scalar)
        {
            return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vector3 operator *(double scalar, Vector3 vector)
        {
            return vector * scalar;
        }

        public static Vector3 operator /(Vector3 vector, double scalar)
        {
            return new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }

        public static double DotProduct(Vector3 vector1, Vector3 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public static Vector3 CrossProduct(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X
            );
        }

        public static bool operator ==(Vector3 vector1, Vector3 vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector3 vector1, Vector3 vector2)
        {
            return !vector1.Equals(vector2);
        }

        public bool Equals(Vector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 vector && Equals(vector);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }

        /// <summary>
        /// Returns the Vector3 components as a double array for compatibility with GCodeParser
        /// </summary>
        public double[] Array => new double[] { X, Y, Z };
    }
    
    /// <summary>
    /// Cross-platform Point implementation for 2D operations
    /// </summary>
    public struct Point : IEquatable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !point1.Equals(point2);
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
#endif
}

/// <summary>
/// Namespace alias for compatibility with RP.Math
/// </summary>
namespace RP.Math
{
    using Vector3 = CNC.Core.Vector3;
}

/// <summary>
/// ValidationError compatibility class for Avalonia migration
/// </summary>
namespace CNC.Core
{
    public class ValidationError
    {
        public object ErrorContent { get; set; }
        
        public ValidationError(object errorContent)
        {
            ErrorContent = errorContent;
        }
    }
    
    /// <summary>
    /// WPF compatibility helper classes for tree navigation
    /// </summary>
    public static class VisualTreeHelper
    {
        public static object GetParent(object obj)
        {
            // TODO: Implement Avalonia visual tree navigation
            return null;
        }
        
        public static int GetChildrenCount(object obj)
        {
            // TODO: Implement Avalonia visual tree navigation
            return 0;
        }
        
        public static object GetChild(object obj, int index)
        {
            // TODO: Implement Avalonia visual tree navigation
            return null;
        }
    }
    
    public static class LogicalTreeHelper
    {
        public static object GetParent(object obj)
        {
            // TODO: Implement Avalonia logical tree navigation
            return null;
        }
        
        public static System.Collections.IEnumerable GetChildren(object obj)
        {
            // TODO: Implement Avalonia logical tree navigation
            return new object[0];
        }
    }
    
    /// <summary>
    /// WPF Mouse and Cursor compatibility classes
    /// </summary>
    public static class Mouse
    {
        public static object OverrideCursor { get; set; }
    }
    
    public static class Cursors
    {
        public static object Wait { get; } = new object();
    }
}