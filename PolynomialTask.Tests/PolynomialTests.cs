#nullable enable

using System;
using System.Reflection;
using NUnit.Framework;

#pragma warning disable CA1707

namespace PolynomialTask.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForConstructor))]
        public void Constructor_UseDeepCopyOfCoefficients(double[] coefficients)
        {
            Polynomial polynomial = new Polynomial(coefficients);

            FieldInfo? fieldInfo = polynomial.GetType().GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            var polynomialCoefficient = fieldInfo?.GetValue(polynomial);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Polynomial>(polynomial);
                Assert.AreEqual(polynomialCoefficient, coefficients);
                Assert.AreNotSame(polynomialCoefficient, coefficients);
            });
        }

        [Test]
        public void Constructor_ArrayIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { var polynomial = new Polynomial(null); }, "Coefficients cannot be null.");
        }

        [Test]
        public void Constructor_ArrayIsEmpty_ThrowArgumentException()
            => Assert.Throws<ArgumentException>(() => { var polynomial = new Polynomial(Array.Empty<double>()); }, "Coefficients cannot be empty.");

        [Test]
        public void Constructor_ArrayIsNull_ThrowArgumentException() => Assert.Throws<ArgumentNullException>(() => new Polynomial(null));

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForEquals))]
        public bool Equals_WithObjectParameter(Polynomial polynomial, object obj) => polynomial.Equals(obj);

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForToString))]
        public string ToString_ReturnPolynomialStringRepresentation(Polynomial polynomial) => polynomial.ToString();

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForGetHashCode))]
        public void GetHashCode_PolynomialAreEquals_Thus_GetHashCodeAreEquals(Polynomial polynomial, Polynomial other)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomial, other);
                Assert.AreEqual(polynomial.GetHashCode(), other.GetHashCode());
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForEquals))]
        public bool Equals_WithPolynomialParameter(Polynomial polynomial, Polynomial other) => polynomial.Equals(other);

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForEquals))]
        public bool OperatorEqual(Polynomial lhs, Polynomial rhs) => lhs == rhs;

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForEquals))]
        public bool OperatorNotEqualTests(Polynomial lhs, Polynomial rhs) => !(lhs != rhs);

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForAddition))]
        public void AdditionOfPolynomials(Polynomial lhs, Polynomial rhs, Polynomial sum)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs + rhs, sum);
                Assert.AreEqual(Polynomial.Add(lhs, rhs), sum);
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForOperationException))]
        public void Addition_OneOfPolynomialsIsEqualsNull_ThrowArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs + rhs;
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForSubtraction))]
        public void SubtractionOfPolynomials(Polynomial lhs, Polynomial rhs, Polynomial difference)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs - rhs, difference);
                Assert.AreEqual(Polynomial.Subtract(lhs, rhs), difference);
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForOperationException))]
        public void Subtraction_OneOfPolynomialsIsEqualsNull_ThrowArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs - rhs;
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForMultiplication))]
        public void MultiplicationOfPolynomials(Polynomial lhs, Polynomial rhs, Polynomial product)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs * rhs, product);
                Assert.AreEqual(Polynomial.Multiply(lhs, rhs), product);
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForOperationException))]
        public void Multiplication_OneOfPolynomialsIsEqualsNull_ThrowArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs * rhs;
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForClone))]
        public void Clone_Return_ShallowCopyOfPolynomial(Polynomial polynomial)
        {
            Polynomial clone = (Polynomial)polynomial.Clone();

            FieldInfo? fieldInfo = polynomial.GetType().GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomial, clone);
                Assert.AreSame(fieldInfo?.GetValue(polynomial), fieldInfo?.GetValue(clone));
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForIndexer))]
        public double Indexer_ReturnCoefficientByIndex(Polynomial polynomial, int index) => polynomial[index];

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForIndexerException))]
        public void Indexer_IndexOutOfDegree_ThrowArgumentOutOfRangeException(Polynomial polynomial, int index)
            => Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var value = polynomial[index];
            });

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForGetCoefficients))]
        public void GetCoefficients_Return_CopyOfCoefficients(Polynomial polynomial)
        {
            FieldInfo? fieldInfo = polynomial.GetType().GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            var polynomialCoefficient = fieldInfo?.GetValue(polynomial);

            var copyCoefficients = polynomial.GetCoefficients();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomialCoefficient, copyCoefficients);
                Assert.AreNotSame(polynomialCoefficient, copyCoefficients);
            });
        }

        [TestCaseSource(typeof(TestCasesData), nameof(TestCasesData.TestCasesForCalculateValue))]
        public void CalculateValue_Return_ValueInPoint(Polynomial polynomial, double x, double value)
        {
            Assert.AreEqual(value, polynomial.CalculateValue(x), Polynomial.AppSettings.Epsilon);
        }
    }
}
