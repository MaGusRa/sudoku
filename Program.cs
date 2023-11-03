using System.Data;

static int[,] InitGameBoard()
{
    int[,] board = new int[9, 9];
    foreach (var i in board)
    {
        board[i, 0] = 0;
    }
    return board;
}

// Generates a *more* random Board than Solve() can generate by itself
// Shuffles the first row using ShuffledRow() to make it *more* random
static int[,] GenerateSolvedBoard()
{
    int[,] board = InitGameBoard();
    int[] seedRow = ShuffledRow();
    for (int i = 0; i < 9; i++) board[0, i] = seedRow[i];
    Solve(board);
    return board;
}

static bool Solve(int[,] board, int row = 0, int col = 0)
{
    if (row == 9) return true;
    else if (col == 9) return Solve(board, row + 1, 0);
    else if (board[row, col] != 0) return Solve(board, row, col + 1);
    else
    {
        for (int i = 1; i < 10; i++)
        {
            if (ValidateSpace(board, row, col, i))
            {
                board[row, col] = i;
                if (Solve(board, row, col + 1)) return true;
                board[row, col] = 0;
            }
        }
        return false;
    }
}

static bool ValidateSpace(int[,] board, int row, int col, int num)
{
    bool not_in_row = true;
    bool not_in_col = true;
    bool not_in_box = true;
    int box_row = (int)Math.Floor((double)row / 3) * 3;
    int box_col = (int)Math.Floor((double)col / 3) * 3;

    for (int i = 0; i < 9; i++)
    {
        if (board[row, i] == num) not_in_row = false;
    }
    for (int i = 0; i < 9; i++)
    {
        if (board[i, col] == num) not_in_col = false;
    }
    for (int i = box_row; i < box_row + 3; i++)
    {
        for (int j = box_col; j < box_col + 3; j++)
        {
            if (board[i, j] == num) not_in_box = false;
        }
    }
    return not_in_row && not_in_col && not_in_box;
}

// This is pretty but idk if I need it. Creates a randomly shuffled row with LINQ
// Might be helpful for seeding the board? idk yet though
static int[] ShuffledRow()
{
    Random rand = new Random();
    int[] row = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    row = row.Select(x => (x, rand.Next()))
    .OrderBy(tuple => tuple.Item2)
    .Select(tuple => tuple.Item1)
    .ToArray();
    return row;
}

static void PrintBoard(int[,] board)
{
    for (int i = 0; i < 9; i++)
    {
        Console.Write("| ");
        for (int j = 0; j < 9; j++)
        {
            Console.Write(board[i, j] + " | ");
        }
        Console.WriteLine();
    }
}
Console.WriteLine("Generated Board:");
Console.WriteLine();
int[,] testBoard = GenerateSolvedBoard();
PrintBoard(testBoard);

Console.WriteLine("Can it solve a hard board??");
Console.WriteLine();
int[,] hardBoard = {{9, 0, 0, 0, 2, 7, 0, 5, 0},
                    {0, 5, 0, 0, 0, 0, 9, 0, 4},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {8, 0, 0, 0, 7, 5, 6, 4, 9},
                    {1, 0, 0, 0, 4, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 9, 8, 0, 0},
                    {0, 0, 0, 4, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 3, 0, 0, 1, 0},
                    {5, 0, 1, 0, 0, 2, 0, 3, 7}
                    };
Solve(hardBoard);
PrintBoard(hardBoard);
