﻿#region Sharp3D.Math, Copyright(C) 2004 Eran Kampf, Licensed under LGPL.
//	Sharp3D.Math math library
//	Copyright (C) 2003-2004  
//	Eran Kampf
//	eran@ekampf.com
//	http://www.ekampf.com
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion
#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.ComponentModel;

using Sharp3D.Math.Core;

#endregion

namespace Sharp3D.Math.Geometry2D
{
	/// <summary>
	/// Represents a ray in 2D space.
	/// </summary>
	/// <remarks>
	/// A ray is R(t) = Origin + t * Direction where Direction isnt necessarily of unit length.
	/// </remarks>
	[Serializable]
	[TypeConverter(typeof(RayConverter))]
	public struct Ray : ICloneable
	{
		#region Private Fields
		private Vector2D _origin;
        private Vector2D _direction;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Ray"/> class using given origin and direction vectors.
		/// </summary>
		/// <param name="origin">Ray's origin point.</param>
		/// <param name="direction">Ray's direction vector.</param>
        public Ray(Vector2D origin, Vector2D direction)
		{
			_origin = origin;
			_direction = direction;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Ray"/> class using given ray.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance to assign values from.</param>
		public Ray(Ray ray)
		{
			_origin = ray.Origin;
			_direction = ray.Direction;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the ray's origin.
		/// </summary>
        public Vector2D Origin
		{
			get { return _origin; }
			set { _origin = value; }
		}
		/// <summary>
		/// Gets or sets the ray's direction vector.
		/// </summary>
        public Vector2D Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Ray"/> object.
		/// </summary>
		/// <returns>The <see cref="Ray"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Ray(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Ray"/> object.
		/// </summary>
		/// <returns>The <see cref="Ray"/> object this method creates.</returns>
		public Ray Clone()
		{
			return new Ray(this);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Ray"/> equivalent.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="Ray"/></param>
		/// <returns>A <see cref="Ray"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static Ray Parse(string s)
		{
			Regex r = new Regex(@"Ray\(Origin=(?<origin>\([^\)]*\)), Direction=(?<direction>\([^\)]*\))\)", RegexOptions.None);
			Match m = r.Match(s);
			if (m.Success)
			{
				return new Ray(
                    Vector2D.Parse(m.Result("${origin}")),
                    Vector2D.Parse(m.Result("${direction}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets a point on the ray.
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
        public Vector2D GetPointOnRay(double t)
		{
			return (Origin + Direction * t);
		}
		#endregion

		#region System.Object Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _origin.GetHashCode() ^ _direction.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="Vector2F"/> and has the same values as this instance; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Ray)
			{
				Ray r = (Ray)obj;
				return (_origin == r.Origin) && (_direction == r.Direction);
			}
			return false;
		}
		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("Ray(Origin={0}, Direction={1})", _origin, _direction);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified rays are equal.
		/// </summary>
		/// <param name="a">The first of two rays to compare.</param>
		/// <param name="b">The second of two rays to compare.</param>
		/// <returns><see langword="true"/> if the two rays are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Ray a, Ray b)
		{
			return ValueType.Equals(a, b);
		}
		/// <summary>
		/// Tests whether two specified rays are not equal.
		/// </summary>
		/// <param name="a">The first of two rays to compare.</param>
		/// <param name="b">The second of two rays to compare.</param>
		/// <returns><see langword="true"/> if the two rays are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Ray a, Ray b)
		{
			return !ValueType.Equals(a, b);
		}

		#endregion
	}

	#region RayConverter class
	/// <summary>
	/// Converts a <see cref="Ray"/> to and from string representation.
	/// </summary>
	public class RayConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
		/// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}
		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">A <see cref="System.Globalization.CultureInfo"/> object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The Type to convert the <paramref name="value"/> parameter to.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType == typeof(string)) && (value is Ray))
			{
				Ray r = (Ray)value;
				return r.ToString();
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture. </param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>An <see cref="Object"/> that represents the converted value.</returns>
		/// <exception cref="ParseException">Failed parsing from string.</exception>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				return Ray.Parse((string)value);
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
	#endregion
}
