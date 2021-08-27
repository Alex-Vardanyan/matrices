using System;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Demo Program
            Console.Write("Please input number of rows for the A Matrix: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please input number of columns for the A Matrix: ");
            int columns = Convert.ToInt32(Console.ReadLine());
            var A = new Matrix(rows, columns);
            A.PopulateRandom();
            Console.WriteLine("A matrix:");
            A.Display();
            Console.WriteLine("The transpose of A matrix:");
            A.Transpose().Display();


            Console.Write("Please input number of rows for B,C,D Matrices: ");
            rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Please input number of columns for the B,C,D Matrices: ");
            columns = Convert.ToInt32(Console.ReadLine());
            var B = new Matrix(rows, columns);
            var C = new Matrix(rows, columns);
            var D = new Matrix(rows, columns);
            var E = new Matrix(rows, columns);

            B.PopulateRandom();
            Console.WriteLine("B matrix:");
            B.Display();
            double[,] arr = new double[rows, columns];
            int s = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    arr[i, j] = s;
                    s++;
                }
            }
            C.Populate(arr);
            C.MultScalar(3);
            Console.WriteLine("C matrix:");
            C.Display();
            double[,] newArr = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newArr[i, j] = 5;
                }
            }
            D.Populate(newArr);
            Console.WriteLine("D matrix:");
            D.Display();
            E = B + C + D;
            Console.WriteLine(" E matrix = B + C + D");
            E.Display();

            var F = new Matrix(columns, rows);
            F.PopulateRandom();
            Console.WriteLine("F matrix:");
            F.Display();
            var G = new Matrix(rows, rows);
            G = B * F;
            Console.WriteLine("G matrix = B * F");
            G.Display();

            if (G.IsOrthogonal())
            {
                Console.WriteLine("G is orthogonal!");
            }
            else
            {
                Console.WriteLine("G is not orthogonal!");
            }

            Console.WriteLine("The bigest element in F matrix is {0}", F.Max());
            Console.WriteLine("The smallest element in F matrix is {0}", F.Min());

            Console.Write("Please input n, where n is number rows and cols in nxn square matrix: ");
            int n = Convert.ToInt32(Console.ReadLine());
            double[,] m = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"Please input the value of the [{i},{j}] element of the Matrix M: ");
                    m[i,j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Matrix M = new Matrix(n,n);
            M.Populate(m);
            Console.WriteLine("M matrix: ");
            M.Display();
            M.Inverse();
            Console.WriteLine("Inverse of M:");
            M.Display();

            Console.ReadLine();
            #endregion
        }

        struct Matrix
        {
            private int m;
            private int n;
            private double[,] matrix;
            private int size;

            public Matrix(int rows, int cols)
            {
                n = rows;
                m = cols;
                matrix = new double[n, m];
                size = n;
            }

            public void Display()
            {
                Console.WriteLine($"The matrix is {n} by {m} long");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write("{0} ", matrix[i, j]);
                    }
                    Console.WriteLine();
                }
            }

            private string Length() => $"{n}x{m}";

            public void PopulateRandom()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Random rnd = new Random();
                        matrix[i, j] = rnd.NextDouble() * 10;
                    }
                }
            }

            public void Populate(double[,] array)
            {
                if (array.GetLength(0) == n && array.GetLength(1) == m)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            matrix[i, j] = array[i, j];
                        }
                    }
                }
                else
                {
                    throw new Exception("The lenght of the array is not the same as the created matrix");
                }
            }

            private double Determinant(double[,] matrix, int size)
            {
                double det = 0;
                int s = 1;
                double[,] b = new double[size, size];
                if (size == 1)
                {
                    return matrix[0, 0];
                }
                else
                {
                    det = 0;
                    for (int c = 0; c < size; c++)
                    {
                        int m = 0;
                        int k = 0;
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                b[i, j] = 0;
                                if (i != 0 && j != c)
                                {
                                    b[m, k] = matrix[i, j];
                                    if (k < size - 2)
                                    {
                                        k++;
                                    }
                                    else
                                    {
                                        k = 0;
                                        m++;
                                    }
                                }
                            }
                        }
                        det = det + s * matrix[0, c] * Determinant(b, size - 1);
                        s *= -1;
                    }
                }
                return det;
            }

            public void Inverse()
            {
                if (n == m)
                {
                    double determinant = Determinant(matrix, n);
                    if (determinant == 0)
                    {
                        throw new Exception("Provided matrix is singular (determinant = 0)!");
                    }
                    else
                    {
                        double[,] cofactorMatrix = new double[n, n];
                        double[,] b = new double[n, n];
                        for (int c = 0; c < n; c++)
                        {
                            for (int d = 0; d < n; d++)
                            {
                                int m = 0;
                                int k = 0;
                                for (int p = 0; p < n; p++) 
                                {
                                    for (int q = 0; q < n; q++)
                                    {
                                        if (p != c && q != d)
                                        {
                                            b[m, k] = matrix[p, q];
                                            if (k < n-2)
                                            {
                                                k++;
                                            }
                                            else
                                            {
                                                k = 0;
                                                m++;
                                            }
                                        }   
                                        int e = (c + d) % 2 == 0 ? -1 : 1;
                                        cofactorMatrix[c, d] = -1 * e *Determinant(b, n - 1);
                                    }
                                }
                            }
                        }

                        double[,] transposedCofactorMatrix = new double[n, n];
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                transposedCofactorMatrix[i, j] = cofactorMatrix[j, i];
                            }
                        }

                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                matrix[i,j] = transposedCofactorMatrix[i, j] / determinant;
                            }
                        }

                    }
                }
                else
                {
                    throw new Exception("The matrix cannot be inversed because it is not square!");
                }   
            }

            public Matrix Transpose()
            {
                double[,] transposedMatrix = new double[m, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        transposedMatrix[j, i] = matrix[i, j];
                    }
                }
                Matrix c = new Matrix(m, n);
                c.Populate(transposedMatrix);
                return c;
            }

            public void MultScalar(double num)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        matrix[i, j] *= num;
                    }
                }
            }
            
            public static Matrix operator +(Matrix a, Matrix b)
            {
                if (a.Length() == b.Length())
                {
                    double[,] sumOfMatrices = new double[a.n, a.m];
                    for (int i = 0; i < a.n; i++)
                    {
                        for (int j = 0; j < a.m; j++)
                        {
                            sumOfMatrices[i, j] = a.matrix[i, j] + b.matrix[i, j];
                        }
                    }
                    Matrix c = new Matrix(a.n, a.m);
                    c.Populate(sumOfMatrices);
                    return c;
                }
                else 
                { 
                    throw new Exception("Unable to perform addition please check the lengths of the matrices!");
                }
            }

            public static Matrix operator *(Matrix a, Matrix b)
            {
                if (a.m == b.n)
                {
                    double[,] productOfMatrices = new double[a.n, b.m];

                    for (int i = 0; i < a.n; i++)
                    {
                        for (int j = 0; j < b.m; j++)
                        {
                            for (int k = 0; k < a.m; k++)
                            {
                                productOfMatrices[i,j] += a.matrix[i, k] * b.matrix[k, j];
                            }
                        }
                    }
                    Matrix c = new Matrix(a.n, b.m);
                    c.Populate(productOfMatrices);
                    return c;
                }
                else
                {
                    throw new Exception("Unable to perform multiplication please check the lengths of the matrices!");
                }
            }

            public bool IsOrthogonal()
            {
                if(n==m)
                {
                    double[,] transposedMatrix = new double[n, n];
                    double[,] finalMatrix = new double[n, n];
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            transposedMatrix[j, i] = matrix[i, j];
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                finalMatrix[i, j] += matrix[i, k] * transposedMatrix[k, j];
                            }
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (i == j)
                            {
                                if (matrix[i, j]!=1)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (matrix[i,j]!=0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                }
                return false;
            }

            public double Min()
            {
                double min = matrix[0, 0];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (matrix[i,j] < min)
                        {
                            min = matrix[i, j];
                        }
                    }
                }
                return min;
            }

            public double Max()
            {
                double max = matrix[0, 0];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (matrix[i, j] > max)
                        {
                            max = matrix[i, j];
                        }
                    }
                }
                return max;
            }
        }
    }
}
