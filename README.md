## Task description ## 

- Develop immutable [Polynomial](PolynomialTask/Polynomial.cs#L11) class for working with [polynomials](http://www.berkeleycitycollege.edu/wp/wjeh/files/2015/01/algebra_note_polynomial.pdf) of integer degree `n > 0` (in one variable, with real coefficients). Use the *sz*-array (single dimension, zero-based) as an internal structure for storing coefficients.   
- Implement an equivalence protocol by value.   
- Overload basic operations   
   - addition;   
   - multiplication;   
   - subtraction;   
   - equality and inequality. 
- Develop unit tests. *Use accuracy 0.00001 in tests.*

A polynomial of degree `n` is an expression of the form: $`a_nx^n + a_{n−1}x^{n-1}+a_{n−2}x^{n−2}+...+a_2x^2+a_1x+a_0`$.
