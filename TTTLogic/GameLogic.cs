using System;
using System.Collections.Generic;
using System.Linq;

namespace TTTLogic
{
    public class GameLogic {
        public GameUnit[][] field { set; get; }
        public GameUnit turn { set; get; }
        public readonly int size;
        public GameLogic()
        {
            size = 3;
            field = new GameUnit[size][];
            for (int i = 0; i < size; i++) {
                field[i] = new GameUnit[size];
                for (int j = 0; j < size; j++) {
                    field[i][j] = GameUnit.NONE;
                }
            }
        }
        public GameLogic(GameUnit[][] field)
        {
            size = field.Length;
            this.field = field;
            
        }
        public GameLogic(int size)
        {
            this.size = size;
            field = new GameUnit[size][];
            for (int i = 0; i < size; i++)
            {
                field[i] = new GameUnit[size];
                for (int j = 0; j < size; j++)
                {
                    field[i][j] = GameUnit.NONE;
                }
            }
        }
        public GameStatus CheckStatus() {
            GameStatus status = GameStatus.PLAY;
            if (field.ToList().All(row => row.ToList().All(cell => cell != GameUnit.NONE))) {
                status = GameStatus.DRAW;
            }
            if (CheckUnit(GameUnit.X)) {
                status = GameStatus.XWIN;
            }
            if (CheckUnit(GameUnit.O))
            {
                status = GameStatus.OWIN;
            }

            return status;
        }
        public bool CheckHorizontal(GameUnit unit) => field.ToList().Any(row => row.All(cell => cell == unit));
        public bool CheckVertical(GameUnit unit) => RotateMatrix().ToList().Any(row => row.All(cell => cell == unit));
        public bool CheckDiagonal(GameUnit unit) {
            bool check = false;
            List<GameUnit> diagonal = new List<GameUnit>();
            for (int i = 0; i < this.size; i++) {
                diagonal.Add(this.field[i][i]);
            }
            if (diagonal.All((u) => u == unit)) {
                check = true;
            }
            diagonal.Clear();
            for (int i = 1; i <= this.size; i++)
            {
                diagonal.Add(this.field[i-1][this.size - i]);
            }
            if (diagonal.All((u) => u == unit))
            {
                check = true;
            }

            return check;
        }
        public bool CheckUnit(GameUnit unit) => CheckHorizontal(unit) || CheckVertical(unit) || CheckDiagonal(unit);
        public GameUnit[][] RotateMatrix() {
            GameUnit[][] matrix = new GameUnit[size][];
            for (int i = 0; i < size; i++)
            {
                matrix[i] = new GameUnit[size];
                for (int j = 0; j < size; j++)
                {
                    matrix[i][j] = this.field[j][i];
                }
            }


            return matrix;
        }

        public bool SetUnit(int x,int y) {
            bool correct = true;
            if ((x < size && y < size) && (x >= 0 && y >= 0))
            {
                if (this.field[x][y] == GameUnit.NONE)
                {
                    this.field[x][y] = turn;
                    this.ChangeTurn();
                }
                else {
                    correct = false;
                }
                
            }
            else {
                correct = false;
            }
                return correct;
        }
        public void ChangeTurn() {
            if (turn == GameUnit.X)
            {
                turn = GameUnit.O;
            }
            else {
                turn = GameUnit.X;
            }
        }

        public override string ToString()
        {
            string matrix = " ";
            


            for (int i = 0; i < size; i++) {
                matrix += " "+(i+1).ToString();
            }
            matrix += "\n";
            for (int i = 1; i <= size; i++) {
                matrix += i.ToString() + " ";
                this.field[i - 1].ToList().ForEach((cell)=> {
                    matrix += cell.ToString() + " ";
                });
                matrix += "\n";
            }



            matrix = matrix.Replace("NONE".ToString(),"#");
            return matrix;

        }

    }


}
