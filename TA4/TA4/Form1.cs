using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TA4
{
	public partial class Form1 : Form
	{
		int bw = 0, pw = 0;
		Game game = new Game();
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			Pen blackpen = new Pen(Color.Black, 3);
			Pen bluepen = new Pen(Color.Blue, 2);
			Pen greenpen = new Pen(Color.Green, 2);
			SolidBrush b = new SolidBrush(Color.DimGray);
			Graphics g = panel1.CreateGraphics();

			for (int i = 1; i <= 8; i++)
			{
				for (int j = 1; j <= 8; j++)
				{
					if (game.field[i, j] == 1)
					{
						g.FillRectangle(b, 5 + (j - 1) * 50, 5 + (i - 1) * 50, 50, 50);
					}
					else
						if (game.field[i, j] == 2)
					{
						g.DrawEllipse(greenpen, 5 + 50 * (j - 1) + 10, 5 + 50 * (i - 1) + 10, 30, 30);
					}
					else
						if (game.field[i, j] == 3)
					{                       //X                    //Y                    //X               //Y
						g.DrawLine(bluepen, 5 + 50 * (j - 1) + 10, 5 + 50 * (i - 1) + 10, 5 + 50 * j - 10, 5 + 50 * i - 10);
						g.DrawLine(bluepen, 5 + 50 * (j - 1) + 10, 5 + 50 * i - 10, 5 + 50 * j - 10, 5 + 50 * (i - 1) + 10);
					}
				}
			}
			for (int i = 0; i <= 8; i++)
			{
				g.DrawLine(blackpen, 5, 5 + 50 * i, 405, 5 + 50 * i);
				g.DrawLine(blackpen, 5 + 50 * i, 5, 5 + 50 * i, 405);
			}
		}

		private void panel1_Click(object sender, EventArgs e)
		{
			if (game.inited)
			{
				Point point = panel1.PointToClient(Cursor.Position);
				int x = (point.X - 5) / 50 + 1;
				int y = (point.Y - 5) / 50 + 1;
				if (game.field[y, x] != 0) MessageBox.Show("You cannot make this step!");
				else
				{
					game.ban(x, y);
					game.field[y, x] = 3;
					panel1.Refresh();
					game.updateHead();
					game.stepBot();
					panel1.Refresh();
					if (Math.Abs(game.t.root.rezult) == 64)
					{
						String winText = "";
						if (game.t.root.rezult == -64)
						{
							winText = "You lose))))";
							bw++;
						}
						else
						{
							winText = "You win))))";
							pw++;
						}
						String score = "\nBot: " + bw.ToString() + " You:" + pw.ToString();
						MessageBox.Show(winText + score);
						game = new Game();
						panel1.Refresh();
					}
				}
			}
		}

		private void startBtn_Click(object sender, EventArgs e)
		{
			Random rnd = new Random();

			game = new Game();
			if (firstBot.Checked)
			{
				int x, y; x = rnd.Next(2, 7); y = rnd.Next(2, 7); game.field[y, x] = 2; game.ban(x, y);
			}
			game.inited = true;
			if (startBtn.Text == "Start") startBtn.Text = "Restart";
			panel1.Refresh();
		}
	}
	class Game
	{
		public int botCount = 0;
		public int humanCount = 0;
		int depth = 5;
		public byte[,] field = new byte[9, 9];
		public bool inited = false;
		bool built = false;
		public Tree t;
		int alpha = 111;
		public Game()
		{
			this.inited = false;
		}
		public void stepBot()
		{
			if (this.t.root.rezult != 64)
			{
				if (this.t.root.depth == this.depth) { this.t = new Tree(this.field, this.depth); }
				this.field = (byte[,])this.t.root.children[this.t.root.minOptiChild].state.Clone();
				this.alpha = this.t.root.children[this.t.root.minOptiChild].rezult;
				this.t.root = this.t.root.children[this.t.root.minOptiChild];
			}

		}
		public void updateHead()
		{
			if (built)
			{
				for (int i = 0; i < this.t.root.children.Count; i++)
				{
					bool equ = true;
					for (int j = 1; j <= 8; j++)
					{
						for (int k = 1; k <= 8; k++)
						{
							if (this.field[j, k] != this.t.root.children[i].state[j, k])
							{
								equ = false;
								break;
							}
							if (!equ) break;
						}
					}
					if (equ)
					{
						this.t.root = this.t.root.children[i];
						break;
					}
				}
			}
			else
			{
				this.built = true;
				this.t = new Tree(this.field, this.depth);
			}
		}
		public void ban(int x, int y)
		{
			int[] a = new int[3] { -1, 0, 1 };
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int newY = Math.Min(8, Math.Max(1, y + a[i]));
					int newX = Math.Min(8, Math.Max(1, x + a[j]));

					if (x == newX && y == newY) this.field[newY, newX] = this.field[newY, newX];
					else
						this.field[newY, newX] = 1;
				}
			}
		}
	}
	class treeNode
	{
		//	public int minmaxval;
		int who;
		public int rezult;
		public byte[,] state;
		public int minOptiChild;
		public List<treeNode> children = new List<treeNode>();
		public List<int> optiChildren = new List<int>();
		public int depth;
		int minmaxval;
		//BOT IS MINIMIZING!!!
		//0 - step player
		//1 - step bot
		public treeNode(int who, byte[,] state, int lvl, int maxDepth)
		{
			this.who = who;
			//Player is maximizing
			int inc = ((who == 0) ? 1 : -1);
			this.minmaxval = 120;
			this.depth = lvl;
			this.state = (byte[,])state.Clone();
			this.rezult = 0;

			for (int i = 1; i <= 8; i++)
			{
				for (int j = 1; j <= 8; j++)
				{
					this.rezult += (Convert.ToInt32(this.state[i, j] >= 1) * inc);
					if (this.state[i, j] == 0 && lvl != maxDepth)
					{
						byte[,] copyState = (byte[,])state.Clone();
						copyState[i, j] = Convert.ToByte((who == 1) ? 3 : 2);
						ban(j, i, copyState);
						int copyStateRezult = 0;
						if (who == 0)
						{
							for (int i1 = 1; i1 <= 8; i1++)
							{
								for (int j1 = 1; j1 <= 8; j1++)
								{
									if (copyState[i1, j1] != 0) copyStateRezult -= 1;
								}
							}
						}
						if (this.who == 1 || (who == 0 && minmaxval > copyStateRezult))
						{
							if (this.who == 0) { this.minmaxval = copyStateRezult; }
							treeNode newChild = new treeNode(this.who ^ 1, copyState, lvl + 1, maxDepth);
							this.children.Add(newChild);
							this.optiChildren.Add(this.children.Last().rezult);
						}

					}
				}
			}
			if (Math.Abs(this.rezult) != 64 && this.depth != maxDepth)
			{
				if (who == 0) this.minOptiChild = this.optiChildren.IndexOf(this.optiChildren.Min());
				else
					this.minOptiChild = this.optiChildren.IndexOf(this.optiChildren.Max());
			}

		}
		public void ban(int x, int y, byte[,] gf)
		{
			int[] a = new int[3] { -1, 0, 1 };
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					int newY = Math.Min(8, Math.Max(1, y + a[i]));
					int newX = Math.Min(8, Math.Max(1, x + a[j]));
					if (x == newX && y == newY) gf[newY, newX] = gf[newY, newX];
					else
						gf[newY, newX] = 1;
				}
			}
		}

	}
	class Tree
	{
		public treeNode root;
		int alpha;
		public int depth;
		public Tree(byte[,] state, int depth)
		{
			this.depth = depth;
			this.root = new treeNode(0, state, 1, depth);
		}
	}
}
