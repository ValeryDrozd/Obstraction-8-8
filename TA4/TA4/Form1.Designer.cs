namespace TA4
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.startBtn = new System.Windows.Forms.Button();
			this.firstPlayer = new System.Windows.Forms.RadioButton();
			this.firstBot = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(410, 410);
			this.panel1.TabIndex = 0;
			this.panel1.Click += new System.EventHandler(this.panel1_Click);
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// startBtn
			// 
			this.startBtn.Location = new System.Drawing.Point(169, 431);
			this.startBtn.Name = "startBtn";
			this.startBtn.Size = new System.Drawing.Size(75, 23);
			this.startBtn.TabIndex = 1;
			this.startBtn.Text = "Start";
			this.startBtn.UseVisualStyleBackColor = true;
			this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
			// 
			// firstPlayer
			// 
			this.firstPlayer.AutoSize = true;
			this.firstPlayer.Checked = true;
			this.firstPlayer.Location = new System.Drawing.Point(16, 431);
			this.firstPlayer.Name = "firstPlayer";
			this.firstPlayer.Size = new System.Drawing.Size(66, 17);
			this.firstPlayer.TabIndex = 2;
			this.firstPlayer.TabStop = true;
			this.firstPlayer.Text = "First You";
			this.firstPlayer.UseVisualStyleBackColor = true;
			// 
			// firstBot
			// 
			this.firstBot.AutoSize = true;
			this.firstBot.Location = new System.Drawing.Point(353, 434);
			this.firstBot.Name = "firstBot";
			this.firstBot.Size = new System.Drawing.Size(63, 17);
			this.firstBot.TabIndex = 3;
			this.firstBot.TabStop = true;
			this.firstBot.Text = "First Bot";
			this.firstBot.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 464);
			this.Controls.Add(this.firstBot);
			this.Controls.Add(this.firstPlayer);
			this.Controls.Add(this.startBtn);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button startBtn;
		private System.Windows.Forms.RadioButton firstPlayer;
		private System.Windows.Forms.RadioButton firstBot;
	}
}

