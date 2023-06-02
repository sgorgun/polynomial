using System;
using System.Globalization;
using System.Text;

namespace PolynomialTask
{
    /// <summary>
    /// Represents a polynomial of integer degree n (in one variable, with real coefficients) - a[n] * x^n + a[n-1] * x^(n-1) + a[n-2] * x^(n-2) +...+ a[1] * x + a[0].
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
        /// Initializes static members of the <see cref="Polynomial"/> class.
        /// Set the default value of the accuracy of equality comparing two real numbers to double.Epsilon.
        /// </summary>
        static Polynomial()
        {
            AppSettings = new AppSettings { Epsilon = double.Epsilon, };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="coefficients">Coefficients of polynomial according rule coefficients[0] -> a[0], coefficients[1] -> a[1], ..., coefficients[n] -> a[n].
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when array of coefficients is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array of coefficients is empty.</exception>
        /// <example>
        /// 0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1 -> { -1, 0.2, 3.313, 0.004, 0.05, 0.16 };
        /// 3.3*x^2+2.001*x+1.21394 -> { 1.21394, 2.001, 3.3 }.
        /// </example>
        public Polynomial(params double[]? coefficients)
        {
            if (coefficients is null)
            {
                throw new ArgumentNullException(nameof(coefficients), "Array of coefficients should not be null.");
            }

            if (coefficients.Length == 0)
            {
                throw new ArgumentException("Array of coefficients should not be empty.", nameof(coefficients));
            }

            this.Degree = coefficients.Length - 1;
            this.coefficients = new double[coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                this.coefficients[i] = coefficients[i];
            }
        }

        /// <summary>
        /// Gets the AppSettings value.
        /// <see cref="AppSettings"/> class.
        /// </summary>
        public static AppSettings AppSettings { get; }

        /// <summary>
        /// Gets the degree value.
        /// </summary>
        /// <example>
        /// The degree of polynomial 3.3*x^2+2.001*x+1.21394 is equal 2;
        /// The degree of polynomial 0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1 is equal 5.
        /// </example>
        public int Degree { get; }

        /// <summary>
        /// Returns polynomial coefficient at degree `index`.
        /// </summary>
        /// <param name="index">The zero-based index of the coefficient to get.</param>
        /// <returns>The polynomial coefficient associated with the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index is not a valid.</exception>
        /// <example>
        /// For polynomial 3.3*x^2+2.001*x+1.21394 at degree 1 returns 2.001;
        /// For polynomial 0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1 at degree 2 returns 3.313.
        /// For polynomial 0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1 at degree 4 returns 0.05.
        /// </example>
        public double this[int index]
        {
            get => (index < 0 || index > this.Degree) ?
               throw new ArgumentOutOfRangeException(nameof(index), "Index is not valid.") :
               this.coefficients[index];
            private set => this.coefficients[index] = (index < 0 || index > this.Degree) ?
                throw new ArgumentOutOfRangeException(nameof(index), "Index is not valid.") :
                value;
        }

        /// <summary>
        /// Calculates the sum of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The sum of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (3.3*x^2+2.001*x+1.21394) + (0.002*x+0.1) => 3.3*x^2+2.003*x+1.31394;
        /// (0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1) + (-4.4*x^3+3.3*x^2-2.2*x+1.1) => 0.16*x^5+0.05*x^4-4.396*x^3+6.613*x^2-2*x+0.1.
        /// </example>
        public static Polynomial operator +(Polynomial? lhs, Polynomial? rhs)
        {
            if (lhs is null || rhs is null)
            {
                throw new ArgumentNullException(lhs is null ? nameof(lhs) : nameof(rhs), "Operand should not be null.");
            }

            int resultLenght = Math.Max(lhs.coefficients.Length, rhs.coefficients.Length);
            double[] result = new double[resultLenght];

            for (int i = 0; i < resultLenght; i++)
            {
                result[i] = (i < lhs.coefficients.Length ? lhs.coefficients[i] : 0) +
                    (i < rhs.coefficients.Length ? rhs.coefficients[i] : 0);
            }

            return new Polynomial(result);
        }

        /// <summary>
        /// Calculates the difference of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The difference of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (0.002*x+0.1) - (3.3*x^2+2.001*x+1.21394) = -3.3*x^2-1.999*x-1.11394;
        /// (9*x^5-0.896*x^4+4.879*x^3+3.987*x^2-2.569*x+1.204) - (4*x^3-3*x^2-2*x+1) = 9*x^5-0.896*x^4+0.879*x^3+6.987*x^2-0.569*x+0.204.
        /// </example>
        public static Polynomial operator -(Polynomial? lhs, Polynomial? rhs)
        {
            if (lhs is null)
            {
                throw new ArgumentNullException(nameof(lhs), "Left-hand side operand should not be null.");
            }

            if (rhs is null)
            {
                throw new ArgumentNullException(nameof(rhs), "Right-hand side operand should not be null.");
            }

            return lhs + -rhs;
        }

        /// <summary>
        /// Negates all coefficients of the polynomial.
        /// </summary>
        /// <param name="polynomial">Polynomial to negate.</param>
        /// <returns>Negated polynomial.</returns>
        public static Polynomial operator -(Polynomial polynomial)
        {
            if (polynomial is null)
            {
                throw new ArgumentNullException(nameof(polynomial), "operand is null.");
            }

            double[] result = new double[polynomial.coefficients.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = polynomial.coefficients[i] * -1;
            }

            return new Polynomial(result);
        }

        /// <summary>
        /// Calculates the product of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>The product of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (3.3*x^2+2.001*x+1.21394) * (0.002*x+0.1) = 0.0066*x^3+0.334*x^2+0.20253*x+0.12139;
        /// (0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1) * (-4.4*x^3+3.3*x^2-2.2*x+1.1) = -0.704*x^8+0.308*x^7-0.2046*x^6-14.498*x^5+10.0991*x^4-2.2242*x^3-0.0957*x^2+2.42*x-1.1.
        /// </example>
        public static Polynomial operator *(Polynomial? lhs, Polynomial? rhs)
        {
            if (lhs is null || rhs is null)
            {
                throw new ArgumentNullException(lhs is null ? nameof(lhs) : nameof(rhs), "Operand should not be null.");
            }

            double[] result = new double[lhs.coefficients.Length + rhs.coefficients.Length - 1];

            for (int i = 0; i < lhs.coefficients.Length; i++)
            {
                for (int j = 0, count = i; j < rhs.coefficients.Length; j++, count++)
                {
                    result[count] += lhs[i] * rhs[j];
                }
            }

            return new Polynomial(result);
        }

        /// <summary>
        /// Determines whether polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>true if left and right are equal; otherwise, false.</returns>
        /// <example>
        /// 0.5*x+1 = 0.49999999*x+1, if Polynomial.AppSettings.Epsilon = 0.00001;
        /// 5.89*x-10.12300013 = 5.89*x-10.123, if Polynomial.AppSettings.Epsilon = 0.00001;
        /// -0.123 = -0.1230000065, if Polynomial.AppSettings.Epsilon = 0.00001.
        /// -0.123 != -0.123065, if Polynomial.AppSettings.Epsilon = 0.00001.
        /// </example>
        public static bool operator ==(Polynomial? lhs, Polynomial? rhs)
        {
            if (lhs is null || rhs is null || lhs.coefficients.Length != rhs.coefficients.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.coefficients.Length; i++)
            {
                if (Math.Abs(lhs[i] - rhs[i]) > AppSettings.Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether polynomials are not equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand.</param>
        /// <returns>true if left and right are not equal; otherwise, false.</returns>
        /// <example>
        /// 0.5*x+1 = 0.49999999*x+1, if Polynomial.AppSettings.Epsilon = 0.00001;
        /// 5.89*x-10.12300013 = 5.89*x-10.123, if Polynomial.AppSettings.Epsilon = 0.00001;
        /// -0.123 = -0.1230000065, if Polynomial.AppSettings.Epsilon = 0.00001.
        /// -0.123 != -0.123065, if Polynomial.AppSettings.Epsilon = 0.00001.
        /// </example>
        public static bool operator !=(Polynomial? lhs, Polynomial? rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Calculates the sum of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand polynomial.</param>
        /// <returns>The sum of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (3.3*x^2+2.001*x+1.21394) + (0.002*x+0.1) => 3.3*x^2+2.003*x+1.31394;
        /// (0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1) + (-4.4*x^3+3.3*x^2-2.2*x+1.1) => 0.16*x^5+0.05*x^4-4.396*x^3+6.613*x^2-2*x+0.1.
        /// </example>
        public static Polynomial Add(Polynomial? lhs, Polynomial? rhs)
        {
            return lhs + rhs;
        }

        /// <summary>
        /// Calculates the difference of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operand.</param>
        /// <param name="rhs">Right-hand side operand polynomial.</param>
        /// <returns>The difference of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (0.002*x+0.1) - (3.3*x^2+2.001*x+1.21394) = -3.3*x^2-1.999*x-1.11394;
        /// (9*x^5-0.896*x^4+4.879*x^3+3.987*x^2-2.569*x+1.204) - (4*x^3-3*x^2-2*x+1) = 9*x^5-0.896*x^4+0.879*x^3+6.987*x^2-0.569*x+0.204.
        /// </example>
        public static Polynomial Subtract(Polynomial? lhs, Polynomial? rhs)
        {
            return lhs - rhs;
        }

        /// <summary>
        /// Calculates the product of two polynomials.
        /// </summary>
        /// <param name="lhs">Left-hand side operator.</param>
        /// <param name="rhs">Right-hand side operator.</param>
        /// <returns>The product of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Left-hand side operand or right-hand side operand is null.</exception>
        /// <example>
        /// (3.3*x^2+2.001*x+1.21394) * (0.002*x+0.1) = 0.0066*x^3+0.334*x^2+0.20253*x+0.12139;
        /// (0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1) * (-4.4*x^3+3.3*x^2-2.2*x+1.1) = -0.704*x^8+0.308*x^7-0.2046*x^6-14.498*x^5+10.0991*x^4-2.2242*x^3-0.0957*x^2+2.42*x-1.1.
        /// </example>
        public static Polynomial Multiply(Polynomial? lhs, Polynomial? rhs)
        {
            return lhs * rhs;
        }

        /// <summary>
        /// Determines whether or not polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="obj">The object to equality compare.</param>
        /// <returns>true if polynomials are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return this.Equals((Polynomial?)obj);
        }

        /// <summary>
        /// Determines whether or not polynomials are equal based on the equality of the coefficients at the same degrees.
        /// Use Epsilon value of <see cref="AppSettings"/> class to equality compare coefficients of polynomials.
        /// </summary>
        /// <param name="other">The polynomial to equality compare.</param>
        /// <returns>true if polynomials are equal; otherwise, false.</returns>
        public bool Equals(Polynomial? other)
        {
            if (other is null || this.Degree != other.Degree)
            {
                return false;
            }

            for (int i = 0; i < this.Degree; i++)
            {
                if (Math.Abs(this.coefficients[i] - other.coefficients[i]) > AppSettings.Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.Degree.GetHashCode();
        }

        /// <summary>
        /// Creates the string representation of current <see cref="Polynomial"/> class instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        /// <example>
        /// For polynomial with coefficients { 0.0001, -0.003, 0.31, -0.00731, 0.000402, 0.000300021 } -> "0.000300021*x^5+0.000402*x^4-0.00731*x^3+0.31*x^2-0.003*x+0.0001"
        /// For polynomial with coefficients { -1.1, -0.0000007, -0.0957, -2.2242, 10.0991, -14.498, -0.2046, 0.0000012, -0.704 } -> "-0.704*x^8-0.2046*x^6-14.498*x^5+10.0991*x^4-2.2242*x^3-0.0957*x^2-1.1"
        /// For polynomial with coefficients { -1, 0.2, 3.313, 0.004, 0.05, 0.16 } -> "0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1".
        /// </example>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = this.coefficients.Length - 1; i >= 0; i--)
            {
                if (this.coefficients[i] != 0)
                {
                    if (Math.Abs(this.coefficients[i]) < AppSettings.Epsilon)
                    {
                        continue;
                    }

                    if (sb.Length > 0 && this.coefficients[i] > 0)
                    {
                        sb.Append('+');
                    }

                    if (i > 1)
                    {
                        sb.AppendFormat(CultureInfo.InvariantCulture, $"{this.coefficients[i]}*x^{i}");
                    }
                    else if (i == 1)
                    {
                        sb.AppendFormat(CultureInfo.InvariantCulture, $"{this.coefficients[i]}*x");
                    }
                    else
                    {
                        sb.Append(this.coefficients[i]);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculates a polynomial value in point x.
        /// </summary>
        /// <param name="x">value of variable.</param>
        /// <returns>Polynomial value in point x.</returns>
        /// <example>
        /// Value of polynomial 1.004*x^2+0.014*x+3 in the point -0.5 is equal 3.244;
        /// Value of polynomial 3.3*x^2+2.001*x+1.21394 in the point 1.5 is equal 11.64044.
        /// </example>
        public double CalculateValue(double x)
        {
            double result = 0;

            for (int i = 0; i <= this.Degree; i++)
            {
                result += this.coefficients[i] * Math.Pow(x, i);
            }

            return result;
        }

        /// <summary>
        /// Gets copy of coefficients of the polynomial instance.
        /// </summary>
        /// <returns>Coefficients of the polynomial.</returns>
        public double[] GetCoefficients()
        {
            double[] result = new double[this.coefficients.Length];
            for (int i = 0; i < this.coefficients.Length; i++)
            {
                result[i] = this.coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// Creates a shallow copy.
        /// </summary>
        /// <returns>A shallow copy.</returns>
        public Polynomial Clone()
        {
            return (Polynomial)this.MemberwiseClone();
        }

        /// <summary>
        /// Creates a shallow copy.
        /// </summary>
        /// <returns>A shallow copy.</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
