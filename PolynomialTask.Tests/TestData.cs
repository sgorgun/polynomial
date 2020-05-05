using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace PolynomialTask.Tests
{
    public class TestData
    {
        public static double Tolerance { get; }

        static TestData()
        {
            try
            {
                var configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSettings.json")
                    .Build();

                Polynomial.AppSettings.Epsilon = double.Parse(configurationRoot["Epsilon"]);
            }
            catch
            {
                // ignored
            }

            Tolerance = Polynomial.AppSettings.Epsilon;
        }

        public static IEnumerable<TestCaseData> TestCasesForVirtualEquals
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1d, 2d, 3d)).Returns(true);
                yield return new TestCaseData(new Polynomial(0.5), new Polynomial(0.5 - Tolerance))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001),
                        new Polynomial(0, 0.1, 0.0001 + 0.1 * Tolerance))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(-10.123 + 0.1 * Tolerance, 5.89),
                        new Polynomial(-10.123, 5.89 + 0.1 * Tolerance))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d),
                    new Polynomial(1d, 2d, 3d + +0.01 * Tolerance)).Returns(true);
                yield return new TestCaseData(new Polynomial(0.5),
                    new Polynomial(0.5 - 0.001 * Tolerance)).Returns(true);
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001), new Polynomial(0, 0.1, 0.0001))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(-10.123, 0d, 5.89), new Polynomial(-10.123, -0d, 5.89))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1.5, 2d, 3d)).Returns(false);
                yield return new TestCaseData(new Polynomial(0.5),
                    new Polynomial(0.5 + 200 * Tolerance)).Returns(false);
                yield return new TestCaseData(new Polynomial(-100.123, 5.89, double.MinValue, double.MaxValue),
                        new Polynomial(-10.123, 5.89, double.MinValue, double.MaxValue))
                    .Returns(false);
                yield return new TestCaseData(new Polynomial(-0.123, 0.0, -0.0),
                        new Polynomial(-0.123 + 10 * Tolerance, 0.0, -0.0))
                    .Returns(false);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), null).Returns(false);
                yield return new TestCaseData(new Polynomial(-0.5, 0.5), new Polynomial(-0.5, 0.5, 0)).Returns(false);
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001), "Test string")
                    .Returns(false);
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForInterfaceEquals
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1d, 2d, 3d)).Returns(true);
                yield return new TestCaseData(new Polynomial(0.5), new Polynomial(0.5 - Tolerance)).Returns(true);
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001),
                        new Polynomial(0, 0.1, 0.0001 + Tolerance))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(-10.123 + 0.001 * Tolerance, 5.89),
                        new Polynomial(-10.123, 5.89))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1d, 2d, 3d)).Returns(true);
                yield return new TestCaseData(new Polynomial(0.5), new Polynomial(0.5 + Tolerance)).Returns(true);
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001), new Polynomial(0, 0.1, 0.0001))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(-10.123, 5.89),
                        new Polynomial(-10.123 - 0.001 * Tolerance, 5.89))
                    .Returns(true);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1.5, 2d, 3d)).Returns(false);
                yield return new TestCaseData(new Polynomial(0.5), new Polynomial(0.49)).Returns(false);
                yield return new TestCaseData(new Polynomial(-100.123, 5.89, double.MinValue, double.MaxValue),
                        new Polynomial(-10.123, 5.89, double.MinValue, double.MaxValue))
                    .Returns(false);
                yield return new TestCaseData(new Polynomial(-0.123, 0.0, -0.0),
                        new Polynomial(-0.127 + 10 * Tolerance, 0.0, -0.0))
                    .Returns(false);
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), null).Returns(false);
                yield return new TestCaseData(new Polynomial(-0.5, 0d, 0.5), new Polynomial(-0.5, 0.5, 0))
                    .Returns(false);
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForVirtualToString
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new[]
                        {0.0001, -0.003, 0.31, -0.00731, 0.000402, 0.000300021}))
                    .Returns("0.000300021*x^5+0.000402*x^4-0.00731*x^3+0.31*x^2-0.003*x+0.0001");
                yield return new TestCaseData(new Polynomial(new[] {-1, 0.2, 3.313, 0.004, 0.05, 0.16}))
                    .Returns("0.16*x^5+0.05*x^4+0.004*x^3+3.313*x^2+0.2*x-1");
                yield return new TestCaseData(new Polynomial(new[]
                        {-1.1, 2.42, -0.0957, -2.2242, 10.0991, -14.498, -0.2046, 0.308, -0.704}))
                    .Returns("-0.704*x^8+0.308*x^7-0.2046*x^6-14.498*x^5+10.0991*x^4-2.2242*x^3-0.0957*x^2+2.42*x-1.1");
                yield return new TestCaseData(new Polynomial(new[]
                        {-1.1, -0.0000007, -0.0957, -2.2242, 10.0991, -14.498, -0.2046, 0.0000012, -0.704}))
                    .Returns("-0.704*x^8-0.2046*x^6-14.498*x^5+10.0991*x^4-2.2242*x^3-0.0957*x^2-1.1");
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForVirtualGetHashCode
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1d, 2d, 3d));
                yield return new TestCaseData(new Polynomial(0.5), new Polynomial(0.5 - Tolerance));
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001),
                    new Polynomial(0, 0.1, 0.0001 + 0.1 * Tolerance));
                yield return new TestCaseData(new Polynomial(-10.123 + 0.0001 * Tolerance, 5.89),
                    new Polynomial(-10.123, 5.89));
                yield return new TestCaseData(new Polynomial(1d, 2d, 3d), new Polynomial(1d, 2d, 3d - Double.Epsilon));
                yield return new TestCaseData(new Polynomial(0, 0.1, 0.0001), new Polynomial(0, 0.1, 0.0001));
                yield return new TestCaseData(new Polynomial(-10.123, 5.89),
                    new Polynomial(-10.123 - Tolerance, 5.89 + Tolerance));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForMultiplication
        {
            get
            {
                yield return new TestCaseData(
                    new Polynomial(1.21394, 2.001, 3.3),
                    new Polynomial(0.1, 0.002),
                    new Polynomial(0.12139, 0.20253, 0.334, 0.0066));
                yield return new TestCaseData(
                    new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16),
                    new Polynomial(1.1, -2.2, 3.3, -4.4),
                    new Polynomial(-1.1, 2.42, -0.0957, -2.2242, 10.0991, -14.498, -0.2046, 0.308, -0.704));
                yield return new TestCaseData(
                    new Polynomial(-3, 0.014, 1.004),
                    new Polynomial(1, 0, 5),
                    new Polynomial(-3, 0.014, -13.996, 0.07, 5.02));
                yield return new TestCaseData(
                    new Polynomial(1.204, -2.569, 3.987, 4.879, -0.896, 9),
                    new Polynomial(1, -2, -3, 4),
                    new Polynomial(1.204, -4.977, 5.513, 9.428, -32.891, 12.103, 4.204, -30.584, 36));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForSubtraction
        {
            get
            {
                yield return new TestCaseData(
                    new Polynomial(0.1, 0.002),
                    new Polynomial(1.21394, 2.001, 3.3),
                    new Polynomial(-1.11394, -1.999, -3.3));
                yield return new TestCaseData(
                    new Polynomial(1.21394, 2.001, 3.3),
                    new Polynomial(0.1, 0.002),
                    new Polynomial(1.11394, 1.999, 3.3));
                yield return new TestCaseData(
                    new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16),
                    new Polynomial(1.1, -2.2, 3.3, -4.4),
                    new Polynomial(-2.1, 2.4, 0.013, 4.404, 0.05, 0.16));
                yield return new TestCaseData(
                    new Polynomial(-3, 0.014, 1.004, 0),
                    new Polynomial(1, -0.0d, 5),
                    new Polynomial(-4, 0.014, -3.996, 0));
                yield return new TestCaseData(
                    new Polynomial(1.204, -2.569, 3.987, 4.879, -0.896, 9),
                    new Polynomial(1, -2, -3, 4),
                    new Polynomial(0.204, -0.569, 6.987, 0.879, -0.896, 9));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForAddition
        {
            get
            {
                yield return new TestCaseData(
                    new Polynomial(1.21394, 2.001, 3.3),
                    new Polynomial(0.1, 0.002),
                    new Polynomial(1.31394, 2.003, 3.3));
                yield return new TestCaseData(
                    new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16),
                    new Polynomial(1.1, -2.2, 3.3, -4.4),
                    new Polynomial(0.1, -2, 6.613, -4.396, 0.05, 0.16));
                yield return new TestCaseData(
                    new Polynomial(-3, 0.014, 1.004, 0),
                    new Polynomial(1, -0.0d, 5),
                    new Polynomial(-2, 0.014, 6.004, 0));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForClone
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3));
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16));
                yield return new TestCaseData(new Polynomial(-3, 0.014, 1.004, 0));
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForIndexer
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3), 1).Returns(2.001);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), 2).Returns(3.313);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), 5).Returns(0.16);
                yield return new TestCaseData(new Polynomial(-3, 0.014, 1.004, 0), 0).Returns(-3);
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForGetCoefficients
        {
            get
            {
                yield return new TestCaseData(new Polynomial(new double[] {1.21394, 2.001, 3.3}));
                yield return new TestCaseData(new Polynomial(new double[] {-1, 0.2, 3.313, 0.004, 0.05, 0.16}));
                yield return new TestCaseData(new Polynomial(new double[] {-3, 0.014, 1.004, 0}));
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForConstructor
        {
            get
            {
                yield return new TestCaseData(new double[] {1.21394, 2.001, 3.3});
                yield return new TestCaseData(new double[] {-1, 0.2, 3.313, 0.004, 0.05, 0.16});
                yield return new TestCaseData(new double[] {-3, 0.014, 1.004, 0});
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForIndexerException
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3), -1);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), -2);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), 9);
                yield return new TestCaseData(new Polynomial(-3, 0.014, 1.004, 9), 12);
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForOperationException
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3), null);
                yield return new TestCaseData(null, new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16));
                yield return new TestCaseData(null, null);
            }
        }
        
        public static IEnumerable<TestCaseData> TestCasesForCalculateValue
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3), -0.5, -3.25747);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), -0.5, -1.3635);
                yield return new TestCaseData(new Polynomial(3, 0.014, 1.004, 0), -0.5, 0.991);
                yield return new TestCaseData(new Polynomial(1.21394, 2.001, 3.3), 1.5, 9.77241);
                yield return new TestCaseData(new Polynomial(-1, 0.2, 3.313, 0.004, 0.05, 0.16), 1.5, 4.0905);
                yield return new TestCaseData(new Polynomial(3, 0.014, 1.004, 0), 1.5,  -2.973);
            }
        }
    }
}