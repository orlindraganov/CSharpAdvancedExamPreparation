namespace Kitty
{
    using System;
    using System.Linq;

    class Kitten
    {
        const char soul = '@';
        const char food = '*';
        const char deadLock = 'x';
        const char emptySpace = '0';

        static int kittyPosition = 0;

        static int inventorySouls = 0;
        static int inventoryFoods = 0;
        static int deadLocksPassed = 0;
        static int movesPassed = 0;

        static bool isKittyAlive = true;

        static int[] moves;

        static char[] arr;
        static char[] movesSeparator = { ' ' };

        static void Main()
        {
            var input = Console.ReadLine().Trim();

            arr = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                arr[i] = input[i];
            }

            moves = Console.ReadLine()
                                .Split(movesSeparator, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

            CollectCollectible();

            foreach (var move in moves)
            {

                if (!isKittyAlive)
                {
                    break;
                }

                Move(move);
                CollectCollectible();
            }

            if (isKittyAlive)
            {
                Console.WriteLine($"Coder souls collected: {inventorySouls}");
                Console.WriteLine($"Food collected: {inventoryFoods}");
                Console.WriteLine($"Deadlocks: {deadLocksPassed}");
            }
            else
            {
                Console.WriteLine($"You are deadlocked, you greedy kitty!");
                Console.WriteLine($"Jumps before deadlock: {movesPassed}");
            }
        }

        static void Move(int numberOfSteps)
        {
            var targetPosition = kittyPosition + numberOfSteps;

            while (targetPosition < 0)
            {
                targetPosition += arr.Length;
            }

            while (targetPosition >= arr.Length)
            {
                targetPosition -= arr.Length;
            }

            kittyPosition = targetPosition;
            movesPassed++;
        }

        static void CollectCollectible()
        {
            switch (arr[kittyPosition])
            {
                case soul:
                    inventorySouls++;
                    arr[kittyPosition] = emptySpace;
                    break;

                case food:
                    inventoryFoods++;
                    arr[kittyPosition] = emptySpace;
                    break;

                case deadLock:
                    if (kittyPosition % 2 == 0)
                    {
                        if (inventorySouls < 1)
                        {
                            isKittyAlive = false;
                            break;
                        }

                        inventorySouls--;
                        arr[kittyPosition] = soul;
                        deadLocksPassed++;
                    }
                    else
                    {
                        if (inventoryFoods < 1)
                        {
                            isKittyAlive = false;
                            break;
                        }

                        inventoryFoods--;
                        arr[kittyPosition] = food;
                        deadLocksPassed++;
                    }
                    break;

                default:
                    break;
            }

        }
    }
}