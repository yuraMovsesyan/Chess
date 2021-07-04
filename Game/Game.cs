using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Game
    {
        public char[,] Placement { get; private set; } = new char[8, 8];
        private int GameMode = 0;
        private bool WhiteMove = true;

        public string EatenWhite { get; private set; } = "";
        public string EatenBlack { get; private set; } = "";

        private void Swap<T>(ref T v1, ref T v2) { T v3 = v1; v1 = v2; v2 = v3; }

        public Game(string placementFenStart = "RNBQKBNR/PPPPPPPP/8/8/8/8/pppppppp/rnbqkbnr w KQkq - 0 1")
        {
            FenToArray(placementFenStart);
        }

        private void FenToArray (string fen)
        {
            string[] fenArray = fen.Split(' ');
            string[] placementFigure = fenArray[0].Split('/');
            //Console.WriteLine(placementFigure[0]);
            for (int y = 0; y < 8; y++)
            {
                int x = 0;
                foreach (char figure in placementFigure[y])
                {
                    if (!CheckNumber(figure))
                    {
                        Placement[y, x] = figure;
                        x += 1;
                    }
                    else
                    {
                        for (int i = 0; i < (int)Char.GetNumericValue(figure); i++)
                        {
                            Placement[y, x] = '·';
                            x += 1;
                        }
                    }
                }
            }

            if (fenArray[1].ToLower() == "w")
                WhiteMove = true;
            else
                WhiteMove = false;

        }

        private bool CheckNumber (char n)
        {
            switch (n)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8': return true;
                default: return false;
            }
        }

        private int GetNumber (char n)
        {
            switch (n)
            {
                case 'a': return 0;
                case 'b': return 1;
                case 'c': return 2;
                case 'd': return 3;
                case 'e': return 4;
                case 'f': return 5;
                case 'g': return 6;
                case 'h': return 7;
                default: return -1;
            }
        }

        private bool CheckWhiteFigure (char n)
        {
            switch (n)
            {
                case 'R':
                case 'N':
                case 'B':
                case 'K':
                case 'Q':
                case 'P': return true;
                default: return false;
            }
        }

        public string Move(string placementCoordinates)
        {

            string status = Status();
            
            if (status != "OK") return status;

            placementCoordinates = placementCoordinates.ToLower();

            int x1, x2;
            int y1, y2;

            //x1 and x2
            if (GetNumber(placementCoordinates[0]) != -1) x1 = GetNumber(placementCoordinates[0]); else return "there is no such coordinate (x1 == -1)";
            if (GetNumber(placementCoordinates[2]) != -1) x2 = GetNumber(placementCoordinates[2]); else return "there is no such coordinate (x2 == -1)";

            //y1 and y2
            if (CheckNumber(placementCoordinates[1])) y1 = (int)Char.GetNumericValue(placementCoordinates[1]); else return "there is no such coordinate (y1 not in [1,8]";
            if (CheckNumber(placementCoordinates[3])) y2 = (int)Char.GetNumericValue(placementCoordinates[3]); else return "there is no such coordinate (y2 not in [1,8]";

            /*
            Console.WriteLine("x1 = {0}, x2 = {1}", x1, x2);
            Console.WriteLine("y1 = {0}, y2 = {1}", y1, y2);
            */
            char placement2 = Placement[y2 - 1, x2];
            if (Placement[y1 - 1, x1] == '·') return "the initial cell is empty";

            if (CheckWhiteFigure(Placement[y1 - 1, x1]) != WhiteMove)
            {
                if (WhiteMove)
                    return "Black people don't go";
                else
                    return "White people don't go";
            }

            if (Placement[y2 - 1, x2] != '·')
            {
                if (CheckWhiteFigure(Placement[y1 - 1, x1]) == CheckWhiteFigure(Placement[y2 - 1, x2])) {
                    return "The player cannot kill his figure";
                }
                else {

                    if (WhiteMove)
                        EatenBlack += Placement[y2 - 1, x2].ToString();
                    else
                        EatenWhite += Placement[y2 - 1, x2].ToString();
                    Placement[y2 - 1, x2] = '·';
                }

            }

            Swap(ref Placement[y1 - 1, x1], ref Placement[y2 - 1, x2]);

            if (placement2 == 'k' || placement2 == 'K')
            {
                GameMode = 3;
                return Status();
            }

            WhiteMove = !WhiteMove;
            return "OK";
        }

        private string Status()
        {
            /* 
            if (GameMode > 1)
                return "The game cannot continue";
            */

            string striker = WhiteMove ? "White " : "Black ";

            switch (GameMode)
            {
                case 3: return striker + "win!";
            }
            return "OK";
        }
    }
}
