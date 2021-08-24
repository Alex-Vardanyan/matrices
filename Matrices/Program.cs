using System;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        struct Matrix
        {
            private int m;
            private int n;
            private double[,] matrix;

            public Matrix(int rows, int cols)
            {
                n = rows;
                m = cols;
                matrix = new double[n, m];
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
                        matrix[i,j] = rnd.NextDouble()*10;
                    }
                }
            }

            public void Populate(double[,] array)
            {
                if(array.GetLength(0) == n && array.GetLength(1) == m)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            matrix[i,j] = array[i,j];
                        }
                    }
                }
                else
                {
                    throw new Exception("The lenght of the array is not the same as the created matrix");
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
                    Matrix c = new Matrix(a.n, a.m);
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
                                if (finalMatrix[i, j] != 0 || finalMatrix[i, j] != 1)
                                {
                                    return false;
                                }
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
