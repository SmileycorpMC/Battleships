using System;

namespace battleships {

    public class Ship {   
        
        int[,] points;
        int hits = 0;

        public Ship(int size) {
            points = new int[size, 2];
        }

        public bool canPlace(Board board, int x, int y, bool vertical) {
            if (x >= board.getWidth()) return false;
            if (y >= board.getHeight()) return false;
            for (int i = 0; i < points.GetLength(0); i++) {
                int x0 = x;
                int y0 = y;
                if (vertical) y0 += i;
                else x0 += i;
                Ship ship = board.getShip(x0, y0);
                if (ship != null) return false;
            }
            return true;
        }

        public void place(int x, int y, bool vertical) {
            for (int i = 0; i < points.GetLength(0); i++) {
                int x0 = x;
                int y0 = y;
                if (vertical) y0 += i;
                else x0 += i;
                points[i, 0] = x0;
                points[i, 1] = y0;
            }
        }

        public bool isOn(int x, int y) {
            for (int i = 0; i < points.GetLength(0); i++) {
                if (points[i, 0] == x && points[i, 1] == y) return true;
            }
            return false;
        }

        public void hit() {
            hits++;
        }

        public bool isDestroyed() {
            return hits >= points.GetLength(0);
        }

    }

}