using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;

static int[,] InitGameBoard()
{
    int[,] board = new int[9, 9];
    foreach (var i in board)
    {
        board[i, 0] = 0;
    }
    return board;
}

// step through each space and fill it with a valid value
static int[,] GenerateGameBoard(int[,] board)
{
    Random rand = new Random();
    int[] numPool = ShuffledRow();
    for (int i = 0; i < 81; i++)
    {
        int currentRow = (int)Math.Floor((double)i / 9);
        int currentCol = i % 9;
        //Console.WriteLine("Current Row: {0} Col: {1}", currentRow, currentCol);
        if (board[currentRow, currentCol] == 0)
        {
            int temp = rand.Next(1, 9);
            // while (!ValidateSpace(board, currentRow, currentCol, temp))
            while (!ValidateSpace(board, currentCol, currentRow, temp)) temp = rand.Next(1, 9);
            board[currentRow, currentCol] = temp;
        }

    }
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

// Deprecated I don't think I need this one anymore
// static bool ValidRowHelper(int[,] board, int row)
// {
//     HashSet<int> seen = new HashSet<int>();
//     for (int i = 0; i < 9; i++)
//     {
//         seen.Clear();
//         int current = board[row, i];
//         if (seen.Add(current)) seen.Add(current);
//         else return false;
//     }
//     return true;
// }

// Deprecated I don't think I need this one anymore
// static bool ValidColHelper(int[,] board, int col)
// {
//     HashSet<int> seen = new HashSet<int>();
//     for (int i = 0; i < 9; i++)
//     {
//         seen.Clear();
//         int current = board[i, col];
//         if (seen.Add(current)) seen.Add(current);
//         else return false;
//     }
//     return true;
// }

// static bool ValidSquareHelper(int[,] board, int row, int col)
// {
//     HashSet<int> seen = new HashSet<int>();

//     return false;
// }

// might not need this one either, but I will wait to remove it until I get something working
static bool ValidateGrid(int[,] board)
{
    bool result = true;
    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            int num = board[i, j];
            result = ValidateSpace(board, i, j, num);
        }
    }
    return result;
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
int[,] testBoard = InitGameBoard();
//PrintBoard(testBoard);
if (Solve(testBoard))
{
    PrintBoard(testBoard);
}
//Console.WriteLine(ValidateGrid(testBoard));