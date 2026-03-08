namespace TrabalhoCG
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Retas = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPMC = new System.Windows.Forms.RadioButton();
            this.rbPMR = new System.Windows.Forms.RadioButton();
            this.rbTrigo = new System.Windows.Forms.RadioButton();
            this.rbDDA = new System.Windows.Forms.RadioButton();
            this.rbEqCircu = new System.Windows.Forms.RadioButton();
            this.rbEqReta = new System.Windows.Forms.RadioButton();
            this.picBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.Retas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Retas);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(244, 489);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // Retas
            // 
            this.Retas.Controls.Add(this.label2);
            this.Retas.Controls.Add(this.label1);
            this.Retas.Controls.Add(this.rbPMC);
            this.Retas.Controls.Add(this.rbPMR);
            this.Retas.Controls.Add(this.rbTrigo);
            this.Retas.Controls.Add(this.rbDDA);
            this.Retas.Controls.Add(this.rbEqCircu);
            this.Retas.Controls.Add(this.rbEqReta);
            this.Retas.Location = new System.Drawing.Point(4, 22);
            this.Retas.Name = "Retas";
            this.Retas.Padding = new System.Windows.Forms.Padding(3);
            this.Retas.Size = new System.Drawing.Size(236, 463);
            this.Retas.TabIndex = 1;
            this.Retas.Text = "Retas";
            this.Retas.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Constantia", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "CIRCUFERÊNCIA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "RETAS";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rbPMC
            // 
            this.rbPMC.AutoSize = true;
            this.rbPMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPMC.Location = new System.Drawing.Point(19, 361);
            this.rbPMC.Name = "rbPMC";
            this.rbPMC.Size = new System.Drawing.Size(105, 21);
            this.rbPMC.TabIndex = 3;
            this.rbPMC.Text = "Ponto Médio";
            this.rbPMC.UseVisualStyleBackColor = true;
            // 
            // rbPMR
            // 
            this.rbPMR.AutoSize = true;
            this.rbPMR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPMR.Location = new System.Drawing.Point(19, 142);
            this.rbPMR.Name = "rbPMR";
            this.rbPMR.Size = new System.Drawing.Size(105, 21);
            this.rbPMR.TabIndex = 2;
            this.rbPMR.Text = "Ponto Médio";
            this.rbPMR.UseVisualStyleBackColor = true;
            // 
            // rbTrigo
            // 
            this.rbTrigo.AutoSize = true;
            this.rbTrigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrigo.Location = new System.Drawing.Point(19, 334);
            this.rbTrigo.Name = "rbTrigo";
            this.rbTrigo.Size = new System.Drawing.Size(121, 21);
            this.rbTrigo.TabIndex = 2;
            this.rbTrigo.Text = "Trigonometrica";
            this.rbTrigo.UseVisualStyleBackColor = true;
            // 
            // rbDDA
            // 
            this.rbDDA.AutoSize = true;
            this.rbDDA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDDA.Location = new System.Drawing.Point(19, 119);
            this.rbDDA.Name = "rbDDA";
            this.rbDDA.Size = new System.Drawing.Size(55, 21);
            this.rbDDA.TabIndex = 1;
            this.rbDDA.Text = "DDA";
            this.rbDDA.UseVisualStyleBackColor = true;
            // 
            // rbEqCircu
            // 
            this.rbEqCircu.AutoSize = true;
            this.rbEqCircu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEqCircu.Location = new System.Drawing.Point(19, 307);
            this.rbEqCircu.Name = "rbEqCircu";
            this.rbEqCircu.Size = new System.Drawing.Size(197, 21);
            this.rbEqCircu.TabIndex = 1;
            this.rbEqCircu.Text = "Equação da Circunfêrencia";
            this.rbEqCircu.UseVisualStyleBackColor = true;
            // 
            // rbEqReta
            // 
            this.rbEqReta.AutoSize = true;
            this.rbEqReta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEqReta.Location = new System.Drawing.Point(19, 96);
            this.rbEqReta.Name = "rbEqReta";
            this.rbEqReta.Size = new System.Drawing.Size(136, 21);
            this.rbEqReta.TabIndex = 0;
            this.rbEqReta.Text = "Equação da Reta";
            this.rbEqReta.UseVisualStyleBackColor = true;
            // 
            // picBox1
            // 
            this.picBox1.Location = new System.Drawing.Point(262, 12);
            this.picBox1.Name = "picBox1";
            this.picBox1.Size = new System.Drawing.Size(1200, 800);
            this.picBox1.TabIndex = 1;
            this.picBox1.TabStop = false;
            this.picBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox1_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Location = new System.Drawing.Point(77, 536);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(110, 49);
            this.btnLimpar.TabIndex = 2;
            this.btnLimpar.Text = "Limpar tela";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 841);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.picBox1);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.Retas.ResumeLayout(false);
            this.Retas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.PictureBox picBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage Retas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPMC;
        private System.Windows.Forms.RadioButton rbPMR;
        private System.Windows.Forms.RadioButton rbTrigo;
        private System.Windows.Forms.RadioButton rbDDA;
        private System.Windows.Forms.RadioButton rbEqCircu;
        private System.Windows.Forms.RadioButton rbEqReta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLimpar;
    }
}

