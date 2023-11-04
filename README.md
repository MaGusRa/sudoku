# sudoku

This is a repo for me (and anyone who wants to help) to figure out some backtracking for sudoku generation.
This is just a console app to get going but soon it will be a fully fledged app to rival the NY Times one! I am just obsessed with sudoku and specifically
the one from the Times because it has the best UI of all the others I have seen.

11/3 - 11/4 2023
Got a solver function working.  Works pretty well, but for generating _more_ random boards it didnt work so well. It would just put 1-9 in order for the first row, which really isn't ideal. So to fix that, in my GenerateSolvedBoard function I use the ShuffledRow function to shuffle the first row.  ShuffledRow uses tuples and LINQ to randomly order the array.  The first number in each tuple is one of the numbers 1-9 and the second number is a randomly generated one which is what I use to order the tuple in a _more_ random order.

I added a FillHard function that will loop through 25 times and randomly place a value throughout the empty sudoku board.  It checks to validate each spot before it fills it, but just continues.  I am going to implement a test to see the average number of *misses* I get to see if it's an acceptable number, i.e. we need to keep at least 17 numbers in a board in order to solve it.


