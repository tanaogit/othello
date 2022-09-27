using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using オセロ.Enums;

namespace オセロ
{
    public static class GetRevarseCells
    {
        public static List<Cell> Run(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            List<Cell> reverseCells = new List<Cell>();

            List<Cell> reverseUpCells = GetReverseUp(cells, posRow, posCol, judgeColor);
            if (reverseUpCells != null) reverseCells.AddRange(reverseUpCells);
            List<Cell> reverseDownCells = GetReverseDown(cells, posRow, posCol, judgeColor);
            if (reverseDownCells != null) reverseCells.AddRange(reverseDownCells);
            List<Cell> reverseLeftCells = GetReverseLeft(cells, posRow, posCol, judgeColor);
            if (reverseLeftCells != null) reverseCells.AddRange(reverseLeftCells);
            List<Cell> reverseRightCells = GetReverseRight(cells, posRow, posCol, judgeColor);
            if (reverseRightCells != null) reverseCells.AddRange(reverseRightCells);
            List<Cell> reverseLeftUpCells = GetReverseLeftUp(cells, posRow, posCol, judgeColor);
            if (reverseLeftUpCells != null) reverseCells.AddRange(reverseLeftUpCells);
            List<Cell> reverseLeftDownCells = GetReverseLeftDown(cells, posRow, posCol, judgeColor);
            if (reverseLeftDownCells != null) reverseCells.AddRange(reverseLeftDownCells);
            List<Cell> reverseRightUpCells = GetReverseRightUp(cells, posRow, posCol, judgeColor);
            if (reverseRightUpCells != null) reverseCells.AddRange(reverseRightUpCells);
            List<Cell> reverseRightDownCells = GetReverseRightDown(cells, posRow, posCol, judgeColor);
            if (reverseRightDownCells != null) reverseCells.AddRange(reverseRightDownCells);

            return reverseCells;
        }

        private static List<Cell> GetReverseUp(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow == 0)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow - 1 && c.Column == posCol);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow - 1 - i < 0)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow - 1 - i && c.Column == posCol);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseDown(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow + 1 >= R.ROW_MAX)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow + 1 && c.Column == posCol);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow + 1 + i >= R.ROW_MAX)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow + 1 + i && c.Column == posCol);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseLeft(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posCol == 0)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol - 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posCol - 1 - i < 0)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol - 1 - i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseRight(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posCol + 1 >= R.COLUMN_MAX)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol + 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posCol + 1 + i >= R.COLUMN_MAX)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol + 1 + i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseLeftUp(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow == 0 || posCol == 0)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow - 1 && c.Column == posCol - 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow - 1 - i < 0 || posCol - 1 - i < 0)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow - 1 - i && c.Column == posCol - 1 - i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseLeftDown(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow + 1 >= R.ROW_MAX || posCol == 0)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow + 1 && c.Column == posCol - 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow + 1 + i >= R.ROW_MAX || posCol - 1 - i < 0)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow + 1 + i && c.Column == posCol - 1 - i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseRightUp(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow == 0 || posCol + 1 >= R.COLUMN_MAX)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow - 1 && c.Column == posCol + 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow - 1 - i < 0 || posCol + 1 + i >= R.COLUMN_MAX)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow - 1 - i && c.Column == posCol + 1 + i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }

        private static List<Cell> GetReverseRightDown(List<Cell> cells, int posRow, int posCol, StoneColor judgeColor)
        {
            StoneColor reverseColor;
            if (judgeColor == StoneColor.Black)
                reverseColor = StoneColor.White;
            else
                reverseColor = StoneColor.Black;

            // 盤の端なので石が存在しない
            if (posRow + 1 >= R.ROW_MAX || posCol + 1 >= R.COLUMN_MAX)
                return null;

            // となりの石は存在するが自分の石、または石が存在しない
            Cell nextCell = cells.FirstOrDefault(c => c.Row == posRow + 1 && c.Column == posCol + 1);
            if (nextCell.StoneColor == judgeColor || nextCell.StoneColor == StoneColor.None)
                return null;

            List<Cell> resultCells = new List<Cell>();
            for (int i = 0; ; i++)
            {
                // もう片方が存在しない。実は挟めていなかった
                if (posRow + 1 + i >= R.ROW_MAX || posCol + 1 + i >= R.COLUMN_MAX)
                    return null;

                // 連続して敵の石の場合、挟めているかもしれないのでリストに追加する
                Cell nextsCell = cells.FirstOrDefault(c => c.Row == posRow + 1 + i && c.Column == posCol + 1 + i);
                if (nextsCell.StoneColor == reverseColor)
                {
                    resultCells.Add(nextsCell);
                    continue;
                }

                // もう片方が自分の石であるならリストのなかにある敵の石は挟めているということなのでリストを返す
                // それ以外であれば実は挟めていなかったのでNullを返す
                if (nextsCell.StoneColor == judgeColor)
                    return resultCells;
                else
                    return null;
            }
        }
    }
}
