using System;

namespace PolynomialTask
{
    /// <summary>
    /// Represents a polynomial of integer degree n (in one variable, with real coefficients)
    /// a[n] * x^n + a[n-1] * x^(n-1) + a[n-2] * x^(n-2) +...+ a[1] * x + a[0].
    /// <see cref="http://www.berkeleycitycollege.edu/wp/wjeh/files/2015/01/algebra_note_polynomial.pdf"/>
    /// Implements <see cref="ICloneable"/> and <see cref="IEquatable{T}"/> interfaces.
    /// </summary>
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        /// <summary>
        ///  Internal structure for storing coefficients of polynomial.
        /// </summary>
        private readonly double[] coefficients;

        /// <summary>
        /// Initializes Polynomial class.
        /// Set the default value of the accuracy of equality comparing two real numbers.
        /// Default value is equal to Double.Epsilon.
        /// </summary>
        static Polynomial()
        {
            AppSettings = new AppSettings
            {
                Epsilon = Double.Epsilon,
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="coefficients">Coefficients of polynomial according rule
        /// coefficients[0] -> a[0], coefficients[1] -> a[1], ..., coefficients[n] -> a[n].
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when array of coefficients is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array of coefficients is empty.</exception>
        public Polynomial(params double[] coefficients)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Gets the Degree value.
        /// </summary>
        public int Degree { get; }

        /// <summary>
        /// Gets the AppSettings value.
        /// <see cref="AppSettings"/> class
        /// </summary>
        public static AppSettings AppSettings { get; }

        /// <summary>
        /// Returns polynomial coefficient at degree index.
        /// </summary>
        /// <param name="index">The zero-based index of the coefficient to get.</param>
        /// <returns>The polynomial coefficient associated with the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index is not a valid.</exception>
        public double this[int index]
        {
            get => throw new NotImplementedException("You need to implement this function.");
            private set => throw new NotImplementedException("You need to implement this function.");
	    }

        /// <summary>
        /// Determines whether or not polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="obj">The object to equality compare.</param>
        /// <returns>true if polynomials are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Determines whether or not polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="other">The polynomial to equality compare.</param>
        /// <returns>true if polynomials are equal; otherwise, false.</returns>
        public bool Equals(Polynomial other)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the string representation of current <see cref="Polynomial"/> class instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the sum of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The sum of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the difference of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The difference of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the product of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The product of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Determines whether polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>true if left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Determines whether polynomials are not equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the sum of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand polynomial.</param>
        /// <returns>The sum of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial Add(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the difference of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand polynomial.</param>
        /// <returns>The difference of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial Subtract(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns the product of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operator.</param>
        /// <param name="rhs">Right-hand side operator.</param>
        /// <returns>The product of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        public static Polynomial Multiply(Polynomial lhs, Polynomial rhs)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns polynomial value in point x.
        /// </summary>
        /// <param name="x">value of variable.</param>
        /// <returns>Polynomial value in point x.</returns>
        public double CalculateValue(double x)
        {
            throw new NotImplementedException("You need to implement this function.");
        }

        /// <summary>
        /// Returns copy of coefficients of the polynomial instance.
        /// </summary>
        /// <returns>Coefficients of the polynomial.</returns>
        public double[] GetCoefficients()
        {
            throw new NotImplementedException("You need to implement this function.");
        }
        
        /// <summary>
        /// Returns a shallow copy.
        /// </summary>
        /// <returns>A shallow copy.</returns>
        public object Clone()
        {
            throw new NotImplementedException("You need to implement this function.");
        }
    }
}