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

namespace オセロ.Forms
{
    public partial class OthelloBoardForm : Form
    {
        private StoneColor _userColor = StoneColor.Black;
        private StoneColor _cpuColor = StoneColor.White;

        private List<Cell> _cells = new List<Cell>();
        private bool _isUserTrun = false;
        private Point _leftTopPoint = new Point(30, 30);

        public OthelloBoardForm()
        {
            InitializeComponent();
            CreateBoard();
            StoneColorSetting();
            GameStart();
        }

        private void CreateBoard()
        {
            for (int row = 0; row < R.ROW_MAX; row++)
            {
                for (int colum = 0; colum < R.COLUMN_MAX; colum++)
                {
                    Cell cell = new Cell(row, colum)
                    {
                        Parent = this,
                        Location = new Point(_leftTopPoint.X + colum * 80, _leftTopPoint.Y + row * 80),
                    };
                    cell.CellClick += OnCellClick;
                    _cells.Add(cell);
                }
            }
        }

        private void StoneColorSetting()
        {
            foreach (Cell cell in _cells)
            {
                if ((cell.Row == 3 && cell.Column == 3) || (cell.Row == 4 && cell.Column == 4))
                    cell.StoneColor = StoneColor.Black;
                else if ((cell.Row == 3 && cell.Column == 4) || (cell.Row == 4 && cell.Column == 3))
                    cell.StoneColor = StoneColor.White;
                else
                    cell.StoneColor = StoneColor.None;
            }
        }

        private void GameStart()
        {
            _isUserTrun = true;
            StatusLabel.Text = "あなたの番です。";
        }

        private void SetCellColor(int posRow, int posCol, StoneColor color)
        {
            Cell cell = _cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol);
            cell.StoneColor = color;
        }

        private void OnCellClick(int posRow, int posCol)
        {
            if (!_isUserTrun)
            {
                StatusLabel.Text = "あなたの番ではありません。";
                return;
            }

            Cell clickCell = _cells.FirstOrDefault(c => c.Row == posRow && c.Column == posCol);
            if (clickCell.StoneColor != StoneColor.None) return;

            List<Cell> reverseCells = GetRevarseCells.Run(_cells, posRow, posCol, _userColor);

            if (reverseCells.Count > 0)
            {
                SetCellColor(posRow, posCol, _userColor);
                reverseCells.ForEach(c => SetCellColor(c.Row, c.Column, _userColor));

                _isUserTrun = false;
                StatusLabel.Text = "コンピュータが考えています。";

                CpuThink();
            }
            else
            {
                StatusLabel.Text = "ここには打てません。";
            }
        }

        private async void CpuThink()
        {
            await Task.Delay(1000);

            // プレイヤーの石を挟むことができる場所を探す
            List<Cell> cpuHandCells = GetAllRevarsePlaces.Run(_cells, _cpuColor);

            List<NextCandidate> nextCandidates = new List<NextCandidate>();
            foreach (Cell handCell in cpuHandCells)
            {
                // Cellsのコピーをつくる
                List<Cell> copiedCells = new List<Cell>();
                foreach (Cell cell in _cells)
                {
                    copiedCells.Add(new Cell(cell.Row, cell.Column) { StoneColor = cell.StoneColor });
                }

                Cell cpuCell = copiedCells.FirstOrDefault(c => c.Row == handCell.Row && c.Column == handCell.Column);
                cpuCell.StoneColor = _cpuColor;

                List<Cell> reverseCells = GetRevarseCells.Run(copiedCells, handCell.Row, handCell.Column, _cpuColor);
                foreach (Cell cell in reverseCells)
                {
                    cell.StoneColor = _cpuColor;
                }

                List<Cell> yourHandCells = GetAllRevarsePlaces.Run(copiedCells, _userColor);

                // 角を奪われるような手は候補から外す
                bool isDeprivedConer = yourHandCells.Any(c =>
                    (c.Row == 0 && c.Column == 0) ||
                    (c.Row == R.ROW_MAX - 1 && c.Column == 0) ||
                    (c.Row == 0 && c.Column == R.COLUMN_MAX - 1) ||
                    (c.Row == R.ROW_MAX - 1 && c.Column == R.COLUMN_MAX - 1)
                );

                if (!isDeprivedConer)
                    nextCandidates.Add(new NextCandidate(handCell, yourHandCells.Count));
            }

            // 敵はできるだけプレイヤーの次の手が少なくなるような手を選ぶ
            Cell nextHandCell = null;
            bool isCpuPassed = false;
            if (nextCandidates.Count > 0)
            {
                int min = nextCandidates.Min(x => x.HandsCount);
                nextHandCell = nextCandidates.First(x => x.HandsCount == min).StonePosition;
            }
            else
            {
                // 候補手がない場合は、角を奪われても仕方がないので適当に選ぶしかない
                int count = cpuHandCells.Count;

                if (count > 0)
                {
                    Random random = new Random();
                    int r = random.Next(count);
                    nextHandCell = cpuHandCells[r];
                }
                else
                {
                    // 次の手がまったく存在しない場合はパス
                    isCpuPassed = true;
                }
            }

            if (nextHandCell != null)
            {
                SetCellColor(nextHandCell.Row, nextHandCell.Column, _cpuColor);
                List<Cell> reversedCells = GetRevarseCells.Run(_cells, nextHandCell.Row, nextHandCell.Column, _cpuColor);
                reversedCells.ForEach(c => SetCellColor(c.Row, c.Column, _cpuColor));
            }

            // プレイヤーの手番になったとき、次の手は存在するのか？
            List<Cell> userNextHandCells = GetAllRevarsePlaces.Run(_cells, _userColor);

            if (userNextHandCells.Count > 0)
            {
                _isUserTrun = true;
                if (!isCpuPassed)
                    StatusLabel.Text = "あなたの番です。";
                else
                    StatusLabel.Text = "コンピュータはパスしました。あなたの番です。";
            }
            else
            {
                if (!isCpuPassed)
                {
                    StatusLabel.Text = "あなたの番ですがパスするしかありません。";
                    CpuThink();
                }
                else
                {
                    End();
                }
            }
        }

        private void End()
        {
            int black = _cells.Where(c => c.StoneColor == StoneColor.Black).Count();
            int white = _cells.Where(c => c.StoneColor == StoneColor.White).Count();

            string result = "";
            if (black > white)
                result = "あなたの勝ちです。";
            else if (black < white)
                result = "コンピューターの勝ちです。";
            else
                result = "引き分けです。";

            StatusLabel.Text = string.Format("終了しました。黒：{0} 対 白：{1} で{2}", black, white, result);
        }
    }
}
