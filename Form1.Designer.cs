
namespace Projekt3_Chalyi_59022
{
    partial class FormGlówny
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGlówny));
            this.bttnProjekt = new System.Windows.Forms.Button();
            this.bttnLaboratorium = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bttnProjekt
            // 
            this.bttnProjekt.Font = new System.Drawing.Font("Rockwell", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttnProjekt.Location = new System.Drawing.Point(495, 240);
            this.bttnProjekt.Name = "bttnProjekt";
            this.bttnProjekt.Size = new System.Drawing.Size(281, 132);
            this.bttnProjekt.TabIndex = 0;
            this.bttnProjekt.Text = "PROJEKT N3\r\n Wybrane bryłe zlożone";
            this.bttnProjekt.UseVisualStyleBackColor = true;
            this.bttnProjekt.Click += new System.EventHandler(this.bttnProjekt_Click);
            // 
            // bttnLaboratorium
            // 
            this.bttnLaboratorium.Font = new System.Drawing.Font("Rockwell", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttnLaboratorium.Location = new System.Drawing.Point(62, 240);
            this.bttnLaboratorium.Name = "bttnLaboratorium";
            this.bttnLaboratorium.Size = new System.Drawing.Size(281, 132);
            this.bttnLaboratorium.TabIndex = 1;
            this.bttnLaboratorium.Text = "LABORATORIUM Wybrane bryły regularne";
            this.bttnLaboratorium.UseVisualStyleBackColor = true;
            this.bttnLaboratorium.Click += new System.EventHandler(this.bttnLaboratorium_Click);
            // 
            // FormGlówny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.BackgroundImage = global::Projekt3_Chalyi_59022.Properties.Resources.images__1_;
            this.ClientSize = new System.Drawing.Size(869, 577);
            this.Controls.Add(this.bttnLaboratorium);
            this.Controls.Add(this.bttnProjekt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGlówny";
            this.Text = "Wizualizacja brył geometrycznych";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGlówny_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bttnProjekt;
        private System.Windows.Forms.Button bttnLaboratorium;
    }
}

