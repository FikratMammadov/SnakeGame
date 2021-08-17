using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
         
        private int _snakePieceNum;
        private int _snakeWidth = 20;
        private int _foodWidth = 20;
        private Label _food;
        private Random _random;
        private Label _snake;
        private Label _snakePiece;
        private int _score;
        private bool _up, _down, _right, _left = true;
        private Direction direction;
        List<Label> snakePieces = new List<Label>();
        //List<int> scores = new List<int>();
        //private string _filePath = "c:/Users/Fikret/Documents/score.txt";




        enum Direction
        {
            up,
            down,
            right,
            left
        }
        public Form1()
        {
            InitializeComponent();
            _random = new Random();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            PlaceSnake();
            snakePieces.Add(_snake);
        }


        private void Reset()
        {
            CreateFood();
            ChangeFoodPlace();
        }
        private void GameOver()
        {
            //scores.Add(_score);
            //StreamWriter file = new StreamWriter("c:/Users/Fikret/Documents/score.txt");
            //List<string> lines = File.ReadAllLines(_filePath).ToList<string>();
            //lines.Add(_score.ToString());
            panel.Controls.Clear();
            snakePieces.Clear();
            Reset();
            PlaceSnake();
            snakePieces.Add(_snake);
            _score = 0;
            labelScore.Text = _score.ToString();
        }
        private Label SnakePieceCreate(int locationX,int locationY)
        {
            _snakePieceNum++;
            Label lbl = new Label() {
                Name = "snakePiece" + _snakePieceNum,
                BackColor = Color.Red,
                Width = _snakeWidth,
                Height = _snakeWidth,
                Location = new Point(locationX, locationY),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.panel.Controls.Add(lbl);
            return lbl;
        }
         
        private void PlaceSnake()
        {
            var snakeHead = SnakePieceCreate(0, 0);
            snakeHead.BackColor = Color.White;
            var pnlLocationX = (panel.Width / 2);
            var pnlLocationY = (panel.Height / 2);
            snakeHead.Location = new Point(pnlLocationX, pnlLocationY);
            _snake = snakeHead;
        }

        private void CreateFood()
        {
            Label lbl = new Label()
            {
                Name = "food",
                BackColor = Color.Yellow,
                Width = _foodWidth,
                Height = _foodWidth
            };
            _food = lbl;
            this.panel.Controls.Add(lbl);
        }

        private void ChangeFoodPlace()
        {
            //int locationX, locationY;
            //do
            //{
            //    locationX = _random.Next(0, panel.Width - _foodWidth);

            //} while (locationX % 20 != 0);

            //do
            //{
            //    locationY = _random.Next(0, panel.Height - _foodWidth);
            //} while (locationY % 20 != 0);
            //_food.Location = new Point(locationX, locationY);

            int locationX, locationY;

            LocationXY:
            locationX = _random.Next(0, panel.Width/20 - 1)*20;
            locationY = _random.Next(0, panel.Height / 20 - 1) * 20;

            for (int i = 0; i < snakePieces.Count; i++)
            {
                 if (locationX == snakePieces[i].Location.X && locationY == snakePieces[i].Location.Y)
                 {
                     goto LocationXY;
                 }
            }
            _food.Location = new Point(locationX, locationY);

            //LocationY:
            //locationY = _random.Next(0, panel.Height / 20 - 1) * 20;

            //for (int i = 0; i < snakePieces.Count; i++)
            //{
            //    if (locationY == snakePieces[i].Location.Y)
            //    {
            //        goto LocationY;
            //    }
            //}
            //do
            //{
            //    LocationX:
            //    locationX = _random.Next(0, panel.Width - _foodWidth);

            //    for (int i = 0; i < snakePieces.Count; i++)
            //    {
            //        if (locationX == snakePieces[i].Location.X)
            //        {
            //            //locationX = _random.Next(0, panel.Width - _foodWidth);
            //            //i = -1;
            //            goto LocationX;
            //        }
            //    }
            //} while (locationX % 20 != 0);

            //do
            //{
            //    LocationY:
            //    locationY = _random.Next(0, panel.Height - _foodWidth);
            //    for (int i = 0; i < snakePieces.Count; i++)
            //    {
            //        if (locationY == snakePieces[i].Location.Y)
            //        {
            //            //locationY = _random.Next(0, panel.Height - _foodWidth);
            //            goto LocationY;
            //        }
            //    }

            //} while (locationY % 20 != 0);
        }
        private void Burn()
        {
            for (int i = 2; i < snakePieces.Count; i++)
            {
                if (_snake.Location.X == snakePieces[i].Location.X && _snake.Location.Y == snakePieces[i].Location.Y)
                {
                    timer1.Stop();
                    MessageBox.Show("Yandi");
                    GameOver();
                    break;
                }
            }
        }
        private void Find()
        {
            Burn();
            if ((_snake.Location.X == _food.Location.X) && (_snake.Location.Y == _food.Location.Y))
            {
                _snakePiece = SnakePieceCreate(0,0);
                _snakePiece.Location = new Point(_snake.Location.X, _snake.Location.Y);
                snakePieces.Add(_snakePiece);
                panel.Controls.Remove(_food);
                _score += 20;
                labelScore.Text = _score.ToString();
                Reset();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;

            if (e.KeyCode == Keys.Up && direction != Direction.down)
            {
                direction = Direction.up;
            }
            else if (e.KeyCode == Keys.Down && direction != Direction.up)
            {
                direction = Direction.down;
            }
            else if (e.KeyCode == Keys.Right && direction != Direction.left)
            {
                direction = Direction.right;
            }
            else if (e.KeyCode == Keys.Left && direction != Direction.right)
            {
                direction = Direction.left;
            }

            //if (e.KeyCode == Keys.Up && _up == true)
            //{     
            //    _up = true;
            //    _right = true;
            //    _left = true;
            //    _down = false;
            //    direction = Direction.up;

            //}
            //else if (e.KeyCode == Keys.Down && _down == true)
            //{
            //    _up = false;
            //    _right = true;
            //    _left = true;
            //    _down = true;
            //    direction = Direction.down;
            //}
            //else if (e.KeyCode == Keys.Right && _right==true)
            //{
            //    _up = true;
            //    _right = true;
            //    _left = false;
            //    _down = true;
            //    direction = Direction.right;
            //}
            //else if (e.KeyCode == Keys.Left && _left==true)
            //{
            //    _up = true;
            //    _right = false;
            //    _left = true;
            //    _down = true;
            //    direction = Direction.left;
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            switch (direction)
            {
                case Direction.up:
                    _snake.Location = new Point(_snake.Location.X, _snake.Location.Y - _snakeWidth);

                    if (_snake.Location.Y < 0)
                    {
                        _snake.Location = new Point(_snake.Location.X, panel.Height - _snakeWidth);
                    }
                    
                    for (int i= snakePieces.Count-1; i>0; i--)
                    {
                         var nextPiece = snakePieces[i];
                         var prevPiece = snakePieces[i - 1];
                         nextPiece.Location = prevPiece.Location;              
                    }
                    break;
                case Direction.down:
                    _snake.Location = new Point(_snake.Location.X, _snake.Location.Y + _snakeWidth);
                    if (_snake.Location.Y == panel.Height)
                    {
                        _snake.Location = new Point(_snake.Location.X, 0);
                    }
                    
                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {

                        var nextPiece = snakePieces[i];
                        var prevPiece = snakePieces[i - 1];
                        nextPiece.Location = prevPiece.Location;
                    }
                    break;
                case Direction.right:
                    _snake.Location = new Point(_snake.Location.X + _snakeWidth, _snake.Location.Y);
                    if (_snake.Location.X == panel.Width)
                    {
                        _snake.Location = new Point(0, _snake.Location.Y);
                    }
                   
                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {

                        var nextPiece = snakePieces[i];
                        var prevPiece = snakePieces[i - 1];
                        nextPiece.Location = prevPiece.Location;
                    }
                    break;
                case Direction.left:
                    _snake.Location = new Point(_snake.Location.X - _snakeWidth, _snake.Location.Y);
                    if (_snake.Location.X < 0)
                    {
                        _snake.Location = new Point(panel.Width - _snakeWidth, _snake.Location.Y);
                    }
                    for (int i= snakePieces.Count-1; i>0; i--)
                    {
                         
                         var nextPiece = snakePieces[i];
                         var prevPiece = snakePieces[i - 1];
                         nextPiece.Location = prevPiece.Location;              
                    }
                    break;
                default:
                    break;

                   
            }
            Find();
        }
    }
}
