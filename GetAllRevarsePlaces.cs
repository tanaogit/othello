using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using オセロ.Enums;

namespace オセロ
{
    public static class GetAllRevarsePlaces
    {
        public static List<Cell> Run(List<Cell> cells, StoneColor color)
        {
            return cells.Where(
                cell => cell.StoneColor == StoneColor.None &&
                GetRevarseCells.Run(cells, cell.Row, cell.Column, color).Count > 0
            ).ToList();
        }
    }
}
