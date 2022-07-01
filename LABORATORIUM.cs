using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static BryłyGeometryczne.KlasaBrylGeometrycznych;

namespace Projekt3_Chalyi_59022
{
    public partial class LABORATORIUM : Form
    {
        Graphics scRysownica, scPowierzchniagraficznawzernikalinii;
        List<BryłaAbstrakcyjna> scLBG = new List<BryłaAbstrakcyjna>();
        Pen scPióro;
        //deklaracje pomocniczne zmiennej
        Point scPunktLokalizacjiBryły = new Point(-1, -1);

        public LABORATORIUM()
        {
            InitializeComponent();
            //ustalenie lokalizacji i rozmiaru pbRysownica
            pbRysownica.Image = new Bitmap(pbRysownica.Width, pbRysownica.Height);
            //utworzenie egzemplarzy Rysownicy
            scRysownica = Graphics.FromImage(pbRysownica.Image);
            //utowrzenie pióra
            scPióro = new Pen(Color.Black, 1F);
            scPióro.DashStyle = DashStyle.Solid;
            //sformatowanie wzierników
            pbWziernikKoloru.BorderStyle = BorderStyle.Fixed3D;
            pbWziernikKoloru.BackColor = pbRysownica.BackColor;
            pbWiernikAtrybutów.Image = new Bitmap(pbWiernikAtrybutów.Width, pbWiernikAtrybutów.Height);
            scPowierzchniagraficznawzernikalinii = Graphics.FromImage(pbWiernikAtrybutów.Image);
            //wykreślenie domyślnego wzorca linii
            scWykreślenieWziernikaLinii();

        }
        //deklaracja metody pomocniczej
        void scWykreślenieWziernikaLinii()
        {
            const int scOdstęp = 5;
            scPowierzchniagraficznawzernikalinii.Clear(pbWiernikAtrybutów.BackColor);
            scPowierzchniagraficznawzernikalinii.DrawLine(scPióro, scOdstęp, pbWiernikAtrybutów.Height/2, 
                pbWiernikAtrybutów.Width - 2*scOdstęp, pbWiernikAtrybutów.Height / 2);
            pbWiernikAtrybutów.Refresh();

        }



        private void button1_Click(object sender, EventArgs e)
        {//wymazanie punktu
            using (SolidBrush scPędzel = new SolidBrush(pbRysownica.BackColor))
            {
                    scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X - 3, scPunktLokalizacjiBryły.Y - 3, 6, 6);
            }
                //pobranie atrybutów ustawionych dla wybranej bryły
                int scWysokośćBryły = tbWysokość.Value;
            int scPromieńBryły = tbPromień.Value;
            int scStopieńWielokąta = (int)numStopień.Value;
            int scXsP = scPunktLokalizacjiBryły.X; int scYsP = scPunktLokalizacjiBryły.Y;
            switch (cmbListaBrył.SelectedItem)
            {
                case "Walec":
                    Walec scEgzemplarzWałec = new Walec(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta,
                        scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplarzWałec.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplarzWałec);
                    break;
                case "Stożek":
                    Stożek scEgzemplrzStożek = new Stożek(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta,
                        scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzStożek.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzStożek);
                    break;
                case "Graniastosłup":
                    Graniastosłup scEgzemplrzGraniastosłup = new Graniastosłup(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta,
                       scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzGraniastosłup.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzGraniastosłup);
                    break;
                case "Ostrosłup":
                    Ostrosłup scEgzemplrzOstrosłup = new Ostrosłup(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta,
                      scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzOstrosłup.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzOstrosłup);
                    break;
                case "Kula":
                    Kula scEgzemplrzKula = new Kula(scPromieńBryły, new Point(
                     scXsP, scYsP), scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzKula.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzKula);
                    break;

                default: MessageBox.Show("nad tą bryłą eszcze pracuję!");break;
            }
            ZegarObrotu.Enabled = true;
            pbRysownica.Refresh();
            button1.Enabled = false;

        }
        private void kolorLiniiBryłyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog scPaleta = new ColorDialog();
            scPaleta.Color = scPióro.Color;
            if (scPaleta.ShowDialog() == DialogResult.OK)
                scPióro.Color = scPaleta.Color;
            scWykreślenieWziernikaLinii();
            scPaleta.Dispose();
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.Dot;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            scPióro.Width = 3F;
            scWykreślenieWziernikaLinii();
        }

        private void ZegarObrotu_Tick(object sender, EventArgs e)
        {
            const float scKątObrotu = 3f;
            for (int sci = 0; sci < scLBG.Count; sci++)
                scLBG[sci].scObróć_i_Wykreśl(pbRysownica,scRysownica,scKątObrotu);
            pbRysownica.Refresh();
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.Dash;
            scWykreślenieWziernikaLinii();
        }

        private void dashDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.DashDot;
            scWykreślenieWziernikaLinii();
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.DashDotDot;
            scWykreślenieWziernikaLinii();
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.Solid;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            scPióro.Width = 1F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            scPióro.Width = 2F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            scPióro.Width = 4F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            scPióro.Width = 5F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            scPióro.Width = 6F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            scPióro.Width = 7F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            scPióro.Width = 8F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            scPióro.Width = 9F;
            scWykreślenieWziernikaLinii();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            scPióro.Width = 10F;
            scWykreślenieWziernikaLinii();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //zamkniękcie aplikacji
            Application.Exit();
        }

        private void zapiszBitmapeDoPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*deklaracja i utworzenie egzemplarza okna dialowego dla wyboru
            * (lub dla utworzenia nowego pliku do zapisu*/
            SaveFileDialog scOknoZapisuPliku = new SaveFileDialog();

            //ustanawenie flitrów dla plików wyświetlanych w oknie dialogowym
            scOknoZapisuPliku.Filter = "Image Files(.JPG)|.JPG|Image Files(.BMP)|.BMP|Image Files(.GIF)|.GIF|Image Files(.PNG)|.PNG|All files (.)|.";
            //wybor filtru domyśłnego
            scOknoZapisuPliku.FilterIndex = 1;
            //przywracania bieżący katalogu do zamknięciu okna dialogowego
            scOknoZapisuPliku.RestoreDirectory = true;
            //domyślny wybór dysku w oknie dialogowym wyboru pliku
            scOknoZapisuPliku.InitialDirectory = "G:\\";
            //ustalenie tytułu okna dialogowego wyboru pliku
            scOknoZapisuPliku.Title = "Zapisanie danych z Rysownicy w pliku";
            if (scOknoZapisuPliku.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbRysownica.Image.Save(scOknoZapisuPliku.FileName);
                }
                catch
                {
                    MessageBox.Show("Zapisanie jest niemożliwe", "Błod",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void odczytajBitmapeZPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /*deklaracja i utworzenie egzemplarza okna dialogowego dla wyboru pliku
            do odczytu*/
            OpenFileDialog scOknoOdczytuPliku = new OpenFileDialog();
            //ustawenie filtrów plików, które mogą być pokazane w oknie dialogowym
            scOknoOdczytuPliku.Filter = "Image Files(.JPG)|.JPG|Image Files(.BMP)|.BMP|Image Files(.GIF)|.GIF|Image Files(.PNG)|.PNG|All files (.)|.";
            //wybor filtru domyślnego
            scOknoOdczytuPliku.FilterIndex = 1;
            //przywracanie bieżącego katalogu po zamknięciu okna dialogowego
            scOknoOdczytuPliku.RestoreDirectory = true;
            //domyśly wybór dyskku i folderu w oknie dialogowym wyboru pliku
            scOknoOdczytuPliku.InitialDirectory = "G: \\";
            //ustalenie tytułu okna dialogowego wyboru pliku
            scOknoOdczytuPliku.Title = "Odczytanie (pobranie ) danych z pliku";
            //sprawdzeniee
            if (scOknoOdczytuPliku.ShowDialog() == DialogResult.OK)
            {
                int scx, scy;
                scx = 0;
                scy = 0;
                scRysownica.DrawImage(Image.FromFile(scOknoOdczytuPliku.FileName), scx, scy);
                pbRysownica.Refresh();
            }
        }

        private void cmbListaBrył_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Pamiętaj,że po wyborze bryły geometrycznej i ustaweniu jej" +
                "atrybutów geomtrycznych i graficznych musisz ustalić miejsce " +
                "wykreślenia wybranej bryły,a następnie kliknij przycisk: Dodaj Nową Brylę ");
            if((cmbListaBrył.SelectedItem != "Kula"))
            {
                tbWysokość.Enabled = true;
                tbPromień.Enabled = true;
                numStopień.Enabled = true;
            }
            else
            {
                tbPromień.Enabled = true;
                tbWysokość.Enabled = false;
                numStopień.Enabled = false;
            }

        }

        private void pbRysownica_MouseClick(object sender, MouseEventArgs e)
        {
            using (SolidBrush scPędzel = new SolidBrush(Color.Coral))
            { 
              if(scPunktLokalizacjiBryły.X != -1)
                { scPędzel.Color = pbRysownica.BackColor;
                    scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X - 3, scPunktLokalizacjiBryły.Y - 3, 6, 6);
                    scPędzel.Color = Color.Coral;
                    
                }
           
                
                    scPunktLokalizacjiBryły = e.Location;
                    scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X -3, scPunktLokalizacjiBryły.Y -3,6,6);
                    button1.Enabled = true;
                    pbRysownica.Refresh();
                
           
            }
        }

        private void bttnPowrot_Click(object sender, EventArgs e)
        {
            FormGlówny scForma = new FormGlówny();
            this.Hide();
            scForma.Show();
        }

        private void LABORATORIUM_FormClosed(object sender, FormClosedEventArgs e)
        {
            //zamkniękcie aplikacji
            Application.Exit();
        }
    }
}
