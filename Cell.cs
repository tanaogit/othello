using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using オセロ.Enums;

namespace オセロ
{
    public partial class Cell : PictureBox
    {
        public delegate void CellClickEventHandler(int x, int y);
        public event CellClickEventHandler CellClick;

        private StoneColor _stoneColor;

        public int Row { get; private set; }
        public int Column { get; private set; }

        public StoneColor StoneColor
        {
            get { return _stoneColor; }
            set
            {
                _stoneColor = value;

                if (value == StoneColor.Black)
                    this.Image = Properties.Resources.black;
                else if (value == StoneColor.White)
                    this.Image = Properties.Resources.white;
                else
                    this.Image = null;
            }
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            this.Size = new Size(80, 80);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.Green;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Click += Cell_Click;
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            CellClick?.Invoke(Row, Column);
        }
    }
}
