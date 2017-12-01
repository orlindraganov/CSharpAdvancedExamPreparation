namespace BitShiftMatrix
{
    using System;
    using System.Linq;
    using System.Numerics;

    class BitShift
    {
        static int[] position;

        static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            position = new int[] { rows - 1, 0};

            var matrix = new BigInteger[rows, cols];

            var numberOfMoves = int.Parse(Console.ReadLine());

            var codes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            for (int i = 0; i < rows; i++)
            {
                var currentRow = rows - i - 1;
                for (int j = 0; j < cols; j++)
                {
                    var power = i + j;
                    matrix[currentRow, j] = (BigInteger)Math.Pow(2, power);
                }
            }

            BigInteger sum = matrix[position[0], position[1]];
            matrix[position[0], position[1]] = 0;

            foreach (var code in codes)
            {
                var coeff = Math.Max(rows, cols);

                var targetRow = code / coeff;
                var targetCol = code % coeff;

                while (position[1] != targetCol)
                {
                    if (position[1] < targetCol)
                        position[1]++;
                    else
                        position[1]--;

                    sum += matrix[position[0], position[1]];
                    matrix[position[0], position[1]] = 0;
                }

                while (position[0] != targetRow)
                {
                    if (position[0] < targetRow)
                        position[0]++;
                    else
                        position[0]--;

                    sum += matrix[position[0], position[1]];
                    matrix[position[0], position[1]] = 0;
                }
            }
            Console.WriteLine(sum);
        }
    }
}
