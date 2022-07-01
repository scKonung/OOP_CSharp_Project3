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
    public partial class PROJEKT : Form
    {
        Graphics scRysownica, scPowierzchniagraficznawzernikalinii;
        List<BryłaAbstrakcyjna> scLBG = new List<BryłaAbstrakcyjna>();
        Pen scPióro;
        bool scKierunkeObrotu;
        //deklaracje pomocniczne zmiennej
        Point scPunktLokalizacjiBryły = new Point(-1, -1);
        public PROJEKT()
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
            scPowierzchniagraficznawzernikalinii.DrawLine(scPióro, scOdstęp, pbWiernikAtrybutów.Height / 2,
                pbWiernikAtrybutów.Width - 2 * scOdstęp, pbWiernikAtrybutów.Height / 2);
            pbWiernikAtrybutów.Refresh();

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

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //zamkniękcie aplikacji
            Application.Exit();
        }

        private void kolorLiniiBryłyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog scPaleta = new ColorDialog();
            scPaleta.Color = scPióro.Color;
            if (scPaleta.ShowDialog() == DialogResult.OK)
                scPióro.Color = scPaleta.Color;
            pbWziernikKoloru.BackColor = scPaleta.Color;
            scWykreślenieWziernikaLinii();
            scPaleta.Dispose();
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.Dash;
            scWykreślenieWziernikaLinii();
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scPióro.DashStyle = DashStyle.Dot;
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

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            scPióro.Width = 3F;
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

        private void bttnPowrot_Click(object sender, EventArgs e)
        {
            FormGlówny scForma = new FormGlówny();
            this.Hide();
            scForma.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //wymazanie punktu
            using (SolidBrush scPędzel = new SolidBrush(pbRysownica.BackColor))
            {
                scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X - 3, scPunktLokalizacjiBryły.Y - 3, 6, 6);
            }
            //pobranie atrybutów ustawionych dla wybranej bryły
            int scWysokośćBryły = tbWysokość.Value;
            int scPromieńBryły = tbPromień.Value;
            int scStopieńWielokąta = (int)numStopień.Value;
            float scKątPochyleniabryły = tbKąt.Value;
            int scXsP = scPunktLokalizacjiBryły.X; int scYsP = scPunktLokalizacjiBryły.Y;
            switch (cmbListaBrył.SelectedItem)
            {
                case "Walec Pochylony":
                    WalecPochylony scEgzemplrzWalecPochylony = new WalecPochylony(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta, scKątPochyleniabryły,
                        scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzWalecPochylony.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzWalecPochylony);
                    break;
                case "Stożek Pochylony":
                    StożekPochylony scEgzemplrzStożekPochylony = new StożekPochylony(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta, scKątPochyleniabryły,
                        scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzStożekPochylony.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzStożekPochylony);
                    break;
                case "Graniastosłup Pochylony":
                    GraniastosłupPochyony scEgzemplrzGraniastosłupPochyony = new GraniastosłupPochyony(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta, scKątPochyleniabryły,
                       scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzGraniastosłupPochyony.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzGraniastosłupPochyony);
                    break;
                case "Ostrosłup Pochylony":
                    OstrosłupPochylony scEgzemplrzOstrosłupPochylony = new OstrosłupPochylony(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta, scKątPochyleniabryły,
                       scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzOstrosłupPochylony.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzOstrosłupPochylony);
                    break;
                case "Krzystał":
                    Krzystał scEgzemplrzKrzystał = new Krzystał(scPromieńBryły, scWysokośćBryły, scStopieńWielokąta,
                     scXsP, scYsP, scPióro.Color, scPióro.DashStyle, scPióro.Width);
                    scEgzemplrzKrzystał.scWykreśl(scRysownica);
                    scLBG.Add(scEgzemplrzKrzystał);
                    break;

                default: MessageBox.Show("nad tą bryłą eszcze pracuję!"); break;
            }
            ZegarObrotu.Enabled = true;
            pbRysownica.Refresh();
            button1.Enabled = false;
            bttnWlacz.Enabled = true;
            bttnPrzesuń.Enabled = true;
            gpWyborPokazu.Enabled = true;
            bttnPrawo.Enabled = true;
            button2.Enabled = true;

            
        }

        private void pbRysownica_MouseClick(object sender, MouseEventArgs e)
        {
            using (SolidBrush scPędzel = new SolidBrush(Color.Coral))
            {
                if (scPunktLokalizacjiBryły.X != -1)
                {
                    scPędzel.Color = pbRysownica.BackColor;
                    scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X - 3, scPunktLokalizacjiBryły.Y - 3, 6, 6);
                    scPędzel.Color = Color.Coral;

                }


                scPunktLokalizacjiBryły = e.Location;
                scRysownica.FillEllipse(scPędzel, scPunktLokalizacjiBryły.X - 3, scPunktLokalizacjiBryły.Y - 3, 6, 6);
                button1.Enabled = true;
                pbRysownica.Refresh();


            }
        }

        private void ZegarObrotu_Tick(object sender, EventArgs e)
        {
            const float scKątObrotu = 3f;
            for (int sci = 0; sci < scLBG.Count; sci++)
                scLBG[sci].scObróć_i_Wykreśl(pbRysownica, scRysownica, scKątObrotu);
            pbRysownica.Refresh();
        }

        private void cmbListaBrył_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Pamiętaj,że po wyborze bryły geometrycznej i ustaweniu jej" +
                "atrybutów geomtrycznych i graficznych musisz ustalić miejsce " +
                "wykreślenia wybranej bryły,a następnie kliknij przycisk: Dodaj Nową Brylę ");
            if ((cmbListaBrył.SelectedItem != "Krzystał"))
            {
                tbWysokość.Enabled = true;
                tbPromień.Enabled = true;
                tbKąt.Enabled = true;
                numStopień.Enabled = true;
            }
            else
            {
                tbWysokość.Enabled = true;
                tbPromień.Enabled = true;
                tbKąt.Enabled = false;
                numStopień.Enabled = true;
            }
        }

        private void bttnWlacz_Click(object sender, EventArgs e)
        {

            //wyczyszczenie rysownicy
           
            scRysownica.Clear(pbRysownica.BackColor);
            ZegarObrotu.Enabled = false;
            //ustawienie indeksu pierwszej figury zapisanej w TFG
            timer1.Tag = 0;
            txtNumerFigury.Text = 0.ToString();
            //wyznaczenie rozmiaru Rysownicy
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            //trzeba bęzie zmienić kod
            scLBG[0].scPrzesuńDoNowegoXY(pbRysownica, scRysownica, scXmax / 2, scYmax / 2);
            //odświeżenie powierzchni graficznej
            pbRysownica.Refresh();
            //rozpoznanie wybrenego trybu
            if (radioButtonAutomatyczny.Checked == true)
                timer1.Enabled = true;
            else if (radioButtonManualny.Checked == true)
            {
                //uaktywnenie przycisków poleceń
                bttn_Next.Enabled = true;
                bttnBack.Enabled = true;
                txtNumerFigury.Enabled = true;
            }
            //ustawenie stanu braku aktywności
            bttnWlacz.Enabled = false;
            //ustawenie stanu aktywności
            bttnWylacz.Enabled = true;
            menuStrip1.Enabled = false;
            bttnPrzesuń.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //wymażenie całej powierhni graficznej
            scRysownica.Clear(pbRysownica.BackColor);
            //wyznaczenie rozmiaru powierzchni graficznej
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            //wpisanie do kontrolki slajder indeksu TFG pokazywnej figury
            txtNumerFigury.Text = timer1.Tag.ToString();
            //wykreślenie figury o indeksie timer1.Tag w żrodku powierzchni grfaficznej
            scLBG[(int)(timer1.Tag)].scPrzesuńDoNowegoXY(pbRysownica, scRysownica,
                scXmax / 2, scYmax / 2);
            //odświedczenie powierzchni graficznej
            pbRysownica.Refresh();
            //wyznazenie indeksu w polu timer1.Tag
            timer1.Tag = ((int)timer1.Tag + 1) % scLBG.Count;
        }

        private void bttnWylacz_Click(object sender, EventArgs e)
        {
            scRysownica.Clear(pbRysownica.BackColor);
            //wyłączenie zegara
            timer1.Enabled = false;
            ZegarObrotu.Enabled = true;
            //ustawenie stanu  aktywności
            bttnWlacz.Enabled = true;
            //wykreślenie wszystkich figur
            //wyznaczenie rozmiaru Rysownicy
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            //deklaracje pomocnicz
            Random scrnd = new Random();
            int scX, scY;
            Color scKolorLinii, scKolorWypelnenia;
            int scGruboscLinii;
            DashStyle scStylLinii;
            for (int sci = 0; sci < scLBG.Count; sci++)
            {
               
                //ustalenie nowego polożenia
                scX = scrnd.Next(10, scXmax - 10);
                scY = scrnd.Next(10, scYmax - 10);
                //przesunięcie figury no nowych koordynatów
                scLBG[sci].scPrzesuńDoNowegoXY(pbRysownica, scRysownica, scX, scY);
            }
            pbRysownica.Refresh();
            //ustalenie stanu braku aktywności
            bttnWylacz.Enabled = false;
            bttn_Next.Enabled = false;
            bttnBack.Enabled = false;
            txtNumerFigury.Enabled = false;
            txtNumerFigury.Text = 0.ToString();
            radioButtonAutomatyczny.Checked = true;
            menuStrip1.Enabled = true;
            bttnPrzesuń.Enabled = true;
        }

        private void bttnPrzesuń_Click(object sender, EventArgs e)
        {
            //deklaracje pomocnicz
            int scXp, scYp;
            //generator liczb łosowych
            Random scrnd = new Random();
            //wymazanie kontrolki grficznej
            scRysownica.Clear(pbRysownica.BackColor);
            //wyznaczenie rozmiarów powierhni
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            for (int sci = 0; sci < scLBG.Count; sci++)
            {//wylosowanie nowego polożenia
                scXp = scrnd.Next(10, scXmax - 10);
                scYp = scrnd.Next(10, scYmax - 10);
                scLBG[sci].scPrzesuńDoNowegoXY(pbRysownica, scRysownica, scXp, scYp);
            }
            //odwiaczenie powierhni graficznej
            pbRysownica.Refresh();
        }

        private void bttn_Next_Click(object sender, EventArgs e)
        {
            ushort scIndeksFigury;
            //pobrenie wartości indeksu z kontrolki textNumerFigury
            if (!ushort.TryParse(txtNumerFigury.Text, out scIndeksFigury))
            {
                MessageBox.Show("ERROR: w zapisie indeksu figury do prezentacji wystąpił" +
                    " niedozwolony znak", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //sprawdzenie wartośći
            if ((scIndeksFigury < 0) || (scIndeksFigury >= scLBG.Count))
            {
                MessageBox.Show("ERROR: przekroczenie indeksów TFG", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //wyznaczenie rozmiarów powierhzchni geometrycznej
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            //wymażenie aktualnej wykreślonej figury geometrycznej
            scRysownica.Clear(pbRysownica.BackColor);
            //przesunięcie figury do sródka risownicy
            scLBG[scIndeksFigury].scPrzesuńDoNowegoXY(pbRysownica, scRysownica, scXmax / 2, scYmax / 2);
            //odświadczenie powierzchni graficznej
            pbRysownica.Refresh();
            if (scIndeksFigury < scLBG.Count - 1)
                scIndeksFigury++;
            else
                scIndeksFigury = 0;
            txtNumerFigury.Text = scIndeksFigury.ToString();
        }

        private void bttnBack_Click(object sender, EventArgs e)
        {
            ushort scIndeksFigury;
            //pobrenie wartości indeksu z kontrolki textNumerFigury
            if (!ushort.TryParse(txtNumerFigury.Text, out scIndeksFigury))
            {
                MessageBox.Show("ERROR: w zapisie indeksu figury do prezentacji wystąpił" +
                    " niedozwolony znak", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //sprawdzenie wartośći
            if ((scIndeksFigury < 0) || (scIndeksFigury >= scLBG.Count))
            {
                MessageBox.Show("ERROR: przekroczenie indeksów TFG", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //wyznaczenie rozmiarów powierhzchni geometrycznej
            int scXmax = pbRysownica.Width;
            int scYmax = pbRysownica.Height;
            //wymażenie aktualnej wykreślonej figury geometrycznej
            scRysownica.Clear(pbRysownica.BackColor);
            //przesunięcie figury do sródka risownicy
            scLBG[scIndeksFigury].scPrzesuńDoNowegoXY(pbRysownica, scRysownica, scXmax / 2, scYmax / 2);
            //odświadczenie powierzchni graficznej
            pbRysownica.Refresh();
            if (scIndeksFigury > 0)
                scIndeksFigury--;
            else
                scIndeksFigury = (ushort)(scLBG.Count - 1);
            txtNumerFigury.Text = scIndeksFigury.ToString();
        }

        private void bttLewo_Click(object sender, EventArgs e)
        {
             scKierunkeObrotu = true;
            for (int sci = 0; sci < scLBG.Count; sci++)
                scLBG[sci].scZmieńKierunekObrotu(scKierunkeObrotu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scKierunkeObrotu = false;
            for (int sci = 0; sci < scLBG.Count; sci++)
                scLBG[sci].scZmieńKierunekObrotu(scKierunkeObrotu);

        }

        private void PROJEKT_FormClosed(object sender, FormClosedEventArgs e)
        {
            //zamkniękcie aplikacji
            Application.Exit();
        }
    }
}
