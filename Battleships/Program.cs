using System;
using System.Reflection.Metadata.Ecma335;

namespace battleships {
    
    class Program {

        static void Main(string[] args) {
           Console.WriteLine("Welcome to battleships.");
           Console.WriteLine("Press any key to begin");
           Console.Read();
           startGame();
        }

        static void startGame() {
            Board board = new Board(10, 30);
            board.addShip(2);
            board.addShip(3);
            board.addShip(3);
            board.addShip(4);
            board.addShip(5);
            Console.WriteLine("After starting you will have 30 shots to hit all ships.");
            Console.WriteLine("Press any key to start the game.");
            Console.Read();
            while(gameLoop(board));
            Console.WriteLine("Would you like to play again? (Y/N)");
            string key = Console.ReadLine().ToUpper();
            while (key != "Y" && key != "N");
            if (key == "Y") startGame();
        }

        static bool gameLoop(Board board) {
            board.draw();
            Console.WriteLine("Shots remaining " + board.getShots());
            Console.WriteLine("Which grid space would you like to fire at? (e.g A10, E5, C5)");
            string str = Console.ReadLine();
            char[] chars = str.ToUpper().ToCharArray();
            if (chars.Length < 2) {
                Console.WriteLine(str + " is not a valid input.");
                Console.ReadLine();
                return true;
            } 
            int x = (int) chars[0];
            if (x < 65 || x >= 65 + board.getWidth()) {
                Console.WriteLine(str + " is not a valid input.");
                Console.ReadLine();
                return true;
            }
            x -= 65;
            int y = getNumber(chars);
            if (y < 0 || y >= board.getWidth()) {
                Console.WriteLine(str + " is not a valid input.");
                Console.ReadLine();
                return true;
            }
            if (board.hasShot(x, y)) {
                Console.WriteLine("You have already shot at " + str + ", please select another square.");
                Console.ReadLine();
                return true;
            }
            int result = board.fire(x, y);
            if (result == 0) Console.WriteLine("MISS!");
            if (result == 1) Console.WriteLine("HIT!");
            if (result == 2) {
                Console.WriteLine("HIT");
                Console.WriteLine("Battleship sunk!");
            }
            if (board.destroyedAll()) {
                Console.WriteLine("Congratulations, you sunk all the ships!");
                return false;
            }
            if (board.getShots() >= 30) {
                Console.WriteLine("All shots fired, you lose.");
                return false;
            }
            return true;
        }

        //return -1 if not number
        private static int getNumber(char[] chars) {
            int j = 0;
            for (int k = 1; k < chars.Length; k++) {
                char c = chars[k];
                if (!Char.IsNumber(c)) return -1;
                j = j * 10 + c - '0';
            }
            return j - 1;
        }

    } 

}