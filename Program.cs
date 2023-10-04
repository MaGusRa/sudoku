static int[,] InitGameBoard() 
{ 
    int[,] board = new int[9,9];
    foreach (var i in board) {
        board[i,0] = 0;
    }
    return board;
}

static int[,] GenerateGameBoard(int[,] board) {
    Random rand = new Random();
    int[] numPool = ShuffledRow();
    for(int i = 0; i < 81; i++) {
        int currentRow = (int)Math.Floor((double)i/9);
        int currentCol = i % 9;
        Console.WriteLine("Current Row -- Col: " + currentRow + " -- " + currentCol);
        if(board[currentRow,currentCol] == 0) {
            int temp = rand.Next(1,9);
            if(!ValidateSpace(board, currentCol, currentRow, temp)) temp = rand.Next(1,9);
            board[currentRow,currentCol] = temp;
        }

    }
    return board;
}

static bool ValidateSpace(int[,] board, int col, int row, int num) {
    //int boxRow = (int)Math.Floor((double) row/3);
    //int boxCol = (int)Math.Floor((double) col/3);
    for(int i = 0; i < 9; i++) {
        if(board[i, col] == num) return false;
        for(int j = 0; j < 9; j++) {
            if(board[row,i] == num) return false;
        }
        
    }
    
    
    return true;
}

static int[] ShuffledRow() {
    Random rand = new Random();
    int[] row = {1,2,3,4,5,6,7,8,9};
    row = row.Select(x => (x, rand.Next()))
    .OrderBy(tuple => tuple.Item2)
    .Select(tuple => tuple.Item1)
    .ToArray();
    return row;
}

static void PrintBoard(int[,] board) {
    for (int i = 0; i < 9; i++) {
        Console.Write("| ");
        for (int j = 0; j < 9; j++) {
            Console.Write(board[i,j] + " | ");
        }
        Console.WriteLine();
    }
}
int[,] testBoard = InitGameBoard();
PrintBoard(GenerateGameBoard(testBoard));