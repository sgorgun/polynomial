using System;
using System.Reflection;
using NUnit.Framework;

namespace PolynomialTask.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForConstructor))]
        public void Constructor_Use_Deep_Copy_Of_Coefficients(double[] coefficients)
        {
            Polynomial polynomial = new Polynomial(coefficients);

            FieldInfo fieldInfo = polynomial.GetType()
                .GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            var polynomialCoefficient = fieldInfo?.GetValue(polynomial);

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Polynomial>(polynomial);
                Assert.AreEqual(polynomialCoefficient, coefficients);
                Assert.AreNotSame(polynomialCoefficient, coefficients);
            });
        }

        [Test]
        public void Constructor_Array_Is_Null_Throw_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = new Polynomial(null);
            }, "Coefficients cannot be null.");

        [Test]
        public void Constructor_Array_Is_Empty_Throw_ArgumentException()
            => Assert.Throws<ArgumentException>(() =>
                {
                    var polynomial = new Polynomial(new double[] { });
                },
                "Coefficients cannot be empty.");

        [Test]
        public void Constructor_Array_Is_Null_Throw_ArgumentException()
            => Assert.Throws<ArgumentNullException>(() => new Polynomial(null));

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForVirtualEquals))]
        public bool Equals_With_Object_Parameter(Polynomial polynomial, object obj)
            => polynomial.Equals(obj);

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForToString))]
        public string ToString_Return_Polynomial_String_Representation(Polynomial polynomial)
            => polynomial.ToString();

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForGetHashCode))]
        public void GetHashCode_Polynomial_Are_Equals_Thus_GetHashCode_Are_Equals(Polynomial polynomial,
            Polynomial other)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomial, other);
                Assert.AreEqual(polynomial.GetHashCode(), other.GetHashCode());
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForInterfaceEquals))]
        public bool Equals_With_Polynomial_Parameter(Polynomial polynomial, Polynomial other)
            => polynomial.Equals(other);

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForInterfaceEquals))]
        public bool OperatorEqual(Polynomial lhs, Polynomial rhs)
            => lhs == rhs;

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForInterfaceEquals))]
        public bool OperatorNotEqualTests(Polynomial lhs, Polynomial rhs)
            => !(lhs != rhs);

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForAddition))]
        public void Addition_Of_Polynomials(Polynomial lhs, Polynomial rhs, Polynomial sum)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs + rhs, sum);
                Assert.AreEqual(Polynomial.Add(lhs, rhs), sum);
            });
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForOperationException))]
        public void Addition_One_Of_Polynomials_Is_Equals_Null_Throw_ArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs + rhs;
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForSubtraction))]
        public void Subtraction_Of_Polynomials(Polynomial lhs, Polynomial rhs, Polynomial difference)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs - rhs, difference);
                Assert.AreEqual(Polynomial.Subtract(lhs, rhs), difference);
            });
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForOperationException))]
        public void Subtraction_One_Of_Polynomials_Is_Equals_Null_Throw_ArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs - rhs;
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForMultiplication))]
        public void Multiplication_Of_Polynomials(Polynomial lhs, Polynomial rhs, Polynomial product)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(lhs * rhs, product);
                Assert.AreEqual(Polynomial.Multiply(lhs, rhs), product);
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForOperationException))]
        public void Multiplication_One_Of_Polynomials_Is_Equals_Null_Throw_ArgumentNullException(Polynomial lhs, Polynomial rhs)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var polynomial = lhs * rhs;
            });
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForClone))]
        public void Clone_Return_Shallow_Copy_Of_Polynomial(Polynomial polynomial)
        {
            Polynomial clone = (Polynomial) polynomial.Clone();

            FieldInfo fieldInfo = polynomial.GetType()
                .GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomial, clone);
                Assert.AreSame(fieldInfo?.GetValue(polynomial), fieldInfo?.GetValue(clone));
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForIndexer))]
        public double Indexer_Return_Coefficient_By_Index(Polynomial polynomial, int index)
            => polynomial[index];

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForIndexerException))]
        public void Indexer_Index_Out_Of_Degree_Throw_ArgumentOutOfRangeException(Polynomial polynomial, int index)
            => Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var value = polynomial[index];
            });

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForGetCoefficients))]
        public void GetCoefficients_Return_Copy_Of_Coefficients(Polynomial polynomial)
        {
            FieldInfo fieldInfo = polynomial.GetType()
                .GetField("coefficients", BindingFlags.NonPublic | BindingFlags.Instance);

            var polynomialCoefficient = fieldInfo?.GetValue(polynomial);

            var copyCoefficients = polynomial.GetCoefficients();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(polynomialCoefficient, copyCoefficients);
                Assert.AreNotSame(polynomialCoefficient, copyCoefficients);
            });
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForCalculateValue))]
        public void CalculateValue(Polynomial polynomial, double x, double value)
        {
            Assert.AreEqual(value,polynomial.CalculateValue(x),TestData.Tolerance);
        }
    }
}