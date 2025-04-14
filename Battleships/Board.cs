using System;
using Microsoft.VisualBasic;

namespace battleships {

    public class Board {   
        
        Random rand = new Random();
        int[,] grid;
        List<Ship> ships = new List<Ship>();
        int shots = 0;

        public Board(int size, int shots) {
            grid = new int[size, size];
            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    grid[i, j] = 0;
                }
            }
            this.shots = shots;
        }

        public void draw() {
            Console.Write(" ");
            for (int j = -1; j < grid.GetLength(0); j++) {
                for (int i = -1; i < grid.GetLength(1); i++) {
                    if (j == -1) {
                        if (i == -1) {
                            Console.Write("  ");
                            continue;
                        }
                        Console.Write(" " + ((char)(65 + i)).ToString() + " ");
                        if (i == grid.GetLength(1) - 1) Console.WriteLine("\n");
                        continue;
                    }
                    if (i == -1) {
                        Console.Write(" " + (j + 1) + " ");
                        continue;
                    }
                    Console.Write(" " + getChar(grid[i, j]) + " ");
                    if (i == grid.GetLength(1) - 1) Console.WriteLine("\n");
                }
            }
        }

        private string getChar(int i) {
            if (i == 1) return "O";
            if (i == 2) return "X";
            return "-";
        }

        public int getWidth() {
            return grid.GetLength(0);
        }

        public int getHeight() {
            return grid.GetLength(1);
        }

        public void addShip(int size) {
            Ship ship = new Ship(size);
            int x = rand.Next(grid.GetLength(0));
            int y = rand.Next(grid.GetLength(1));
            bool vertical = rand.Next(2) == 1;
            while (!ship.canPlace(this, x, y, vertical)) {
                rand.Next(grid.GetLength(0));
                rand.Next(grid.GetLength(1));
                vertical = rand.Next(2) == 1;
            }
            ship.place(x, y, vertical);
            ships.Add(ship);
        }

        public Ship getShip(int x, int y) {
            foreach (Ship ship in ships) {
                if (ship.isOn(x, y)) return ship;
            }
            return null;
        }

        public bool hasShot(int x, int y) {
            return grid[x, y] != 0;
        }

        //return 0 if miss, 1 if hit, 2 if destroyed
        public int fire(int x, int y) {
            shots--;
            Ship ship = getShip(x, y);
            if (ship == null) {
                grid[x, y] = 1;
                return 0;
            }
            grid[x, y] = 2;
            ship.hit();
            if (ship.isDestroyed()) {
                ships.Remove(ship);
                return 2;
            }
            return 1;
        }

        public bool destroyedAll() {
            return ships.Count <= 0;
        }
        
        public int getShots() {
            return shots;
        }

    }

}