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
            this.rbPMR = new System.Windows.Forms.RadioButton();
            this.rbDDA = new System.Windows.Forms.RadioButton();
            this.rbEqReta = new System.Windows.Forms.RadioButton();
            this.Circunferencia = new System.Windows.Forms.TabPage();
            this.rbPMC = new System.Windows.Forms.RadioButton();
            this.rbTrigo = new System.Windows.Forms.RadioButton();
            this.rbEqCircu = new System.Windows.Forms.RadioButton();
            this.picBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chekBoxReta = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.Retas.SuspendLayout();
            this.Circunferencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Retas);
            this.tabControl.Controls.Add(this.Circunferencia);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(244, 234);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // Retas
            // 
            this.Retas.Controls.Add(this.chekBoxReta);
            this.Retas.Controls.Add(this.rbPMR);
            this.Retas.Controls.Add(this.rbDDA);
            this.Retas.Controls.Add(this.rbEqReta);
            this.Retas.Location = new System.Drawing.Point(4, 22);
            this.Retas.Name = "Retas";
            this.Retas.Padding = new System.Windows.Forms.Padding(3);
            this.Retas.Size = new System.Drawing.Size(236, 208);
            this.Retas.TabIndex = 1;
            this.Retas.Text = "Retas";
            this.Retas.UseVisualStyleBackColor = true;
            // 
            // rbPMR
            // 
            this.rbPMR.AutoSize = true;
            this.rbPMR.Location = new System.Drawing.Point(69, 166);
            this.rbPMR.Name = "rbPMR";
            this.rbPMR.Size = new System.Drawing.Size(85, 17);
            this.rbPMR.TabIndex = 2;
            this.rbPMR.TabStop = true;
            this.rbPMR.Text = "Ponto Médio";
            this.rbPMR.UseVisualStyleBackColor = true;
            // 
            // rbDDA
            // 
            this.rbDDA.AutoSize = true;
            this.rbDDA.Location = new System.Drawing.Point(69, 141);
            this.rbDDA.Name = "rbDDA";
            this.rbDDA.Size = new System.Drawing.Size(48, 17);
            this.rbDDA.TabIndex = 1;
            this.rbDDA.TabStop = true;
            this.rbDDA.Text = "DDA";
            this.rbDDA.UseVisualStyleBackColor = true;
            // 
            // rbEqReta
            // 
            this.rbEqReta.AutoSize = true;
            this.rbEqReta.Location = new System.Drawing.Point(69, 118);
            this.rbEqReta.Name = "rbEqReta";
            this.rbEqReta.Size = new System.Drawing.Size(109, 17);
            this.rbEqReta.TabIndex = 0;
            this.rbEqReta.TabStop = true;
            this.rbEqReta.Text = "Equação da Reta";
            this.rbEqReta.UseVisualStyleBackColor = true;
            // 
            // Circunferencia
            // 
            this.Circunferencia.Controls.Add(this.rbPMC);
            this.Circunferencia.Controls.Add(this.rbTrigo);
            this.Circunferencia.Controls.Add(this.rbEqCircu);
            this.Circunferencia.Location = new System.Drawing.Point(4, 22);
            this.Circunferencia.Name = "Circunferencia";
            this.Circunferencia.Padding = new System.Windows.Forms.Padding(3);
            this.Circunferencia.Size = new System.Drawing.Size(236, 208);
            this.Circunferencia.TabIndex = 2;
            this.Circunferencia.Text = "Circunferência";
            this.Circunferencia.UseVisualStyleBackColor = true;
            // 
            // rbPMC
            // 
            this.rbPMC.AutoSize = true;
            this.rbPMC.Location = new System.Drawing.Point(48, 116);
            this.rbPMC.Name = "rbPMC";
            this.rbPMC.Size = new System.Drawing.Size(85, 17);
            this.rbPMC.TabIndex = 3;
            this.rbPMC.TabStop = true;
            this.rbPMC.Text = "Ponto Médio";
            this.rbPMC.UseVisualStyleBackColor = true;
            // 
            // rbTrigo
            // 
            this.rbTrigo.AutoSize = true;
            this.rbTrigo.Location = new System.Drawing.Point(48, 93);
            this.rbTrigo.Name = "rbTrigo";
            this.rbTrigo.Size = new System.Drawing.Size(95, 17);
            this.rbTrigo.TabIndex = 2;
            this.rbTrigo.TabStop = true;
            this.rbTrigo.Text = "Trigonometrica";
            this.rbTrigo.UseVisualStyleBackColor = true;
            // 
            // rbEqCircu
            // 
            this.rbEqCircu.AutoSize = true;
            this.rbEqCircu.Location = new System.Drawing.Point(48, 70);
            this.rbEqCircu.Name = "rbEqCircu";
            this.rbEqCircu.Size = new System.Drawing.Size(154, 17);
            this.rbEqCircu.TabIndex = 1;
            this.rbEqCircu.TabStop = true;
            this.rbEqCircu.Text = "Equação da Circunfêrencia";
            this.rbEqCircu.UseVisualStyleBackColor = true;
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
            // chekBoxReta
            // 
            this.chekBoxReta.AutoSize = true;
            this.chekBoxReta.Location = new System.Drawing.Point(54, 56);
            this.chekBoxReta.Name = "chekBoxReta";
            this.chekBoxReta.Size = new System.Drawing.Size(98, 17);
            this.chekBoxReta.TabIndex = 2;
            this.chekBoxReta.Text = "Desenhar Reta";
            this.chekBoxReta.UseVisualStyleBackColor = true;
            this.chekBoxReta.CheckedChanged += new System.EventHandler(this.chekBoxReta_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 841);
            this.Controls.Add(this.picBox1);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.Retas.ResumeLayout(false);
            this.Retas.PerformLayout();
            this.Circunferencia.ResumeLayout(false);
            this.Circunferencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage Retas;
        private System.Windows.Forms.RadioButton rbPMR;
        private System.Windows.Forms.RadioButton rbDDA;
        private System.Windows.Forms.RadioButton rbEqReta;
        private System.Windows.Forms.TabPage Circunferencia;
        private System.Windows.Forms.RadioButton rbPMC;
        private System.Windows.Forms.RadioButton rbTrigo;
        private System.Windows.Forms.RadioButton rbEqCircu;
        private System.Windows.Forms.PictureBox picBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chekBoxReta;
    }
}

