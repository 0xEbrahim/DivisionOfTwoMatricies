
using System;
using System.Collections.Generic;

class Inverse
{

    static readonly int N = 3;

    static void getCofactor(int[,] A, int[,] temp, int p, int q, int n)
    {
        int i = 0, j = 0;


        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {

                if (row != p && col != q)
                {
                    temp[i, j++] = A[row, col];
                    if (j == n - 1)
                    {
                        j = 0;
                        i++;
                    }
                }
            }
        }
    }

    static int determinant(int[,] A, int n)
    {
        int D = 0;
        if (n == 1)
            return A[0, 0];

        int[,] temp = new int[N, N];

        int sign = 1;
        for (int f = 0; f < n; f++)
        {

            getCofactor(A, temp, 0, f, n);
            D += sign * A[0, f] * determinant(temp, n - 1);

            sign = -sign;
        }
        return D;
    }


    static void adjoint(int[,] A, int[,] adj)
    {
        if (N == 1)
        {
            adj[0, 0] = 1;
            return;
        }


        int sign = 1;
        int[,] temp = new int[N, N];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {

                getCofactor(A, temp, i, j, N);


                sign = ((i + j) % 2 == 0) ? 1 : -1;


                adj[j, i] = (sign) * (determinant(temp, N - 1));
            }
        }
    }


    static bool inverse(int[,] A, float[,] inverse)
    {

        int det = determinant(A, N);
        if (det == 0)
        {
            Console.Write("Singular matrix, can't find its inverse");
            return false;
        }


        int[,] adj = new int[N, N];
        adjoint(A, adj);


        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                inverse[i, j] = adj[i, j] / (float)det;

        return true;
    }


    static void display(int[,] A)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                Console.Write(A[i, j] + " ");
            Console.WriteLine();
        }
    }
    static void display(float[,] A)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                Console.Write("{0:F2} ", A[i, j]);
            Console.WriteLine();
        }
    }


    public static void Main(String[] args)
    {
        float[,] arr1 = new float[N, N];
        int[,] arr2 = new int[N, N];

        float[,] adj = new float[N, N];

        float[,] inv = new float[N, N];

        Console.WriteLine("<----------- FIRST ARRAY ------------>");
        for (int i = 0; i < N * N; i++)
        {
            int row = i / N;
            int column = i % N;
            Console.Write("Enter arr1[{0}, {1}]", row, column);
            Console.WriteLine();
            arr1[row, column] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("<----------- SECOND ARRAY ------------>");
        for (int i = 0; i < N * N; i++)
        {
            int row = i / N;
            int column = i % N;
            Console.WriteLine("Enter arr2[{0}, {1}]", row, column);
            Console.WriteLine();
            arr2[row, column] = int.Parse(Console.ReadLine());
        }
        Console.Write("\nThe Inverse is :\n");
        if (inverse(arr2, inv))
            display(inv);
        Console.WriteLine();


        Console.WriteLine("<--------- Result ---------->");



        float[,] c = new float[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++)
                {
                    c[i, j] += arr1[i, k] * inv[k, j];
                }
            }
        }
        display(c);


        Console.Read();
    }
}

