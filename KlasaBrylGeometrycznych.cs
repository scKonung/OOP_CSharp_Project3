using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BryłyGeometryczne
{
    class KlasaBrylGeometrycznych
    {
        const float scKątProsty = 90.0F;
        //deklaracja klasy abstrakcyjnej
        public abstract class BryłaAbstrakcyjna
        {
            public enum scTypyBrył
            { scBG_Wałec, scBG_Stożek, scBG_Kula, scBG_Ostrosłup, scBG_Graniatosłup, scBG_Sześcian, scBG_StożekPochylony, scBG_WalecPochylony, scBG_GraniatosłupPochylony , scBGOstrosłupPochylony, scBG_Krzystał };
            //deklaracja zmiennych dla wspólnych atrybutów geometrycznych
            protected int scXsp, scYsp;
            protected int scWysokośćBryły;
            protected float scKątPochylenia;
            //deklaracja zmiennych dla wspólnych atrybutów graficznych
            protected Color scKolorLinii;
            protected DashStyle scStyłlinii;
            protected float scGrubośćLinii;
            //deklaracja zmiennych dla imlementacji przyszłych funkcjonalności
            public scTypyBrył scRodzajBryły;
            protected bool scKierunekObrotu;// f- w prawo , t - w lewo
            protected bool scWidoczny;
            protected float scPowierzchniaBryły;
            protected float scObjetośćBryły;
            //deklaracja konstruktorska
            public BryłaAbstrakcyjna(Color KolorLinii, DashStyle StyłLinii, float GrubośćLinii)
            {
                this.scKolorLinii = KolorLinii;
                scStyłlinii = StyłLinii;
                scGrubośćLinii = GrubośćLinii;
                scKątPochylenia = scKątProsty;
            }
            //deklaracja metod abstrakcynych (dla których nie jesteśmy w stanie zapisać ich implementacje)
            public abstract void scWykreśl(Graphics scRysownica);
            public abstract void scWymaż(Control scKontrolka, Graphics scRysownica);
            public abstract void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu);
            public abstract void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY);
            public abstract void scZmieńKierunekObrotu(bool scKierunekObrotu);
            //deklaracja metod publicznych z ich pelą impłementacją
            public void scUstałAtrybutyGraficzne(Color KolorLinii, DashStyle StyłLinii, int GrubośćLinii)
            {
                scKolorLinii = KolorLinii;
                scStyłlinii = StyłLinii;
                scGrubośćLinii = GrubośćLinii;
            }

        }
        //deklaracja klasy Bryły obrotowe
        public class BryłyObrotówe : BryłaAbstrakcyjna
        {
            protected int scPromieńBryły;
            //deklaracja konstuktorska
            public BryłyObrotówe(int r, Color KolorLinii, DashStyle StyłLinii, float GrubośćLinii) :
                base(KolorLinii, StyłLinii, GrubośćLinii)
            {
                //zapisanie promienia r
                scPromieńBryły = r;
            }
            //nadpisanie wszystkich metod abstrakcyjnych z klasy BryłaAbstrakcyjna
            public override void scWykreśl(Graphics scRysownica)
            {

            }
            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {

            }
            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {

            }
            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {

            }
            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
               
            }
        }

        //deklaracja klasy potomnej Walec
        public class Walec : BryłyObrotówe
        {//deklaracja uzupełniającę bryły Walec
            protected Point[] scWielokątPodłogi;
            protected Point[] scWielokątSufitu;
            protected int scXsS, scYsS;
            //stopień wielokąta podstawy i sutifu Wałca
            protected int scStopieńWielokątaPodstawy;
            protected float scOś_duża, scOś_mała;
            //kąta środkowy między wierzchołkami wielokąta podstawy
            protected float scKątMiędzyWierzchołkami;
            //kąt położenia pierwszego wierzchołka wielokąta podstawy
            protected float scKątPolożenia;
            //wektor przesunięcia środka sufitu  pochylonego Wałca
            protected int scWektorPrzesunięciaŚródkaSufituWałca;


            //deklaracja konstruktorska
            public Walec(int r, int WysokośćWałca, int StopieńWielokątaPodstawy,
                int Xsp, int Ysp,
                Color KolorLinii, DashStyle StyłLinii, float GrubośćLinii) : base(r, KolorLinii, StyłLinii, GrubośćLinii)
            {
                scWidoczny = false;
                //ustawienie rodzaju bryły
                scRodzajBryły = scTypyBrył.scBG_Wałec;
                scWysokośćBryły = WysokośćWałca;
                this.scStopieńWielokątaPodstawy = StopieńWielokątaPodstawy;
                //zapisanie wspołrzędnych środka podłogi Wałca
                this.scXsp = Xsp; this.scYsp = Ysp;
                //wyznaczenie os ielipsy wykreślanej w podłodze i suficie wałca
                scOś_duża = 2 * scPromieńBryły;
                scOś_mała = scPromieńBryły / 2;

                //wyznaczenie  współrzędnych środka sufitu Wałac
                scXsS = scXsp;
                scYsS = scYsp - WysokośćWałca;
                //wyznaczenia kątów polożenia
                scKątMiędzyWierzchołkami = 360 / scStopieńWielokątaPodstawy;
                scKątPolożenia = 0F;
                //wyznaczenie wspólrzędnych punktów w "podłodze" i "suficie" walca dla wykreślenia
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                scWielokątSufitu = new Point[scStopieńWielokątaPodstawy + 1];
                //utpwrzenie egzemplarzy punktów w podloże i sufitu
                for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątSufitu[sci] = new Point();
                    //podłoga
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                       Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    //sufit
                    scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                      Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                }
                //obliczenie pola powierzchni Wałca
                //obliczenie objetności Wałca

            }

            //nadpisanie metod abstrakcyjnych zadeklarowanych w klasie BryłyAbstrakcyjne
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pioro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pioro.DashStyle = scStyłlinii;
                    //wykreślenie podłogi Wałca
                    scRysownica.DrawEllipse(Pioro, scXsp - scOś_duża / 2, scYsp - scOś_mała / 2,
                        scOś_duża, scOś_mała);
                    //wykreślenie sufitu Wałca
                    scRysownica.DrawEllipse(Pioro, scXsS - scOś_duża / 2, scYsS - scOś_mała / 2,
                       scOś_duża, scOś_mała);
                    using (Pen PioroPrążków = new Pen(scKolorLinii, 0.5F))
                    {
                        PioroPrążków.DashStyle = DashStyle.Dot;
                        //wykreślenie prążków
                        for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(PioroPrążków, scWielokątPodłogi[sci], scWielokątSufitu[sci]);

                        //wykreślenie krawędzi bocznych Wałca
                        //wykreślenei krawędzi lewej
                        scRysownica.DrawLine(Pioro, scXsp - scOś_duża / 2, scYsp,
                            scXsS - scOś_duża / 2, scYsS);
                        //wykreślenei krawędzi prawej
                        scRysownica.DrawLine(Pioro, scXsp + scOś_duża / 2, scYsp,
                            scXsS + scOś_duża / 2, scYsS);
                        scWidoczny = true;



                    }
                }

            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pioro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pioro.DashStyle = scStyłlinii;
                        //wykreślenie podłogi Wałca
                        scRysownica.DrawEllipse(Pioro, scXsp - scOś_duża / 2, scYsp - scOś_mała / 2,
                            scOś_duża, scOś_mała);
                        //wykreślenie sufitu Wałca
                        scRysownica.DrawEllipse(Pioro, scXsS - scOś_duża / 2, scYsS - scOś_mała / 2,
                           scOś_duża, scOś_mała);
                        using (Pen PioroPrążków = new Pen(Pioro.Color, 0.5F))
                        {
                            PioroPrążków.DashStyle = DashStyle.Dot;
                            //wykreślenie prążków
                            for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                                scRysownica.DrawLine(PioroPrążków, scWielokątPodłogi[sci], scWielokątSufitu[sci]);

                            //wykreślenie krawędzi bocznych Wałca
                            //wykreślenei krawędzi lewej
                            scRysownica.DrawLine(Pioro, scXsp - scOś_duża / 2, scYsp,
                                scXsS - scOś_duża / 2, scYsS);
                            scWidoczny = false;


                        }
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                //obramy Wałec gdy on jest wykreślony
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);

                    if (scKierunekObrotu)
                        scKątPolożenia -= scKątObrotu;
                    else
                        scKątPolożenia += scKątObrotu;
                    //wyznaczenie nowych wspólrzednych wielokąta podłogi i sufitu
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {

                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                          Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                    }
                    scWykreśl(scRysownica);

                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                //deklaracje pomocnicze
                int scdX, scdY;
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    //wyznaczenie wartości przyrostów wspólrzednych dX, dY
                    scdX = scXsp < scX ? scX - scXsp : -(scXsp - scX);
                    scdY = scYsp < scY ? scY - scYsp : -(scYsp - scY);
                    //zmiana wartośći współrzędnych środka
                    scXsp = scXsp + scdX;
                    scYsp = scYsp + scdY;
                    scXsS = scYsS + scdX;
                    scYsS = scYsS + scdY;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {

                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                          Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }

        }

        //deklaracja klasy potomnej Walec Pochylony
        public class WalecPochylony : Walec
        {
            //deklaracja konstruktorska
            public WalecPochylony(int r, int WysokośćWałca, int StopieńWielokątaPodstawy, float scKątPochyleniaWalca,
                int Xsp, int Ysp,
                Color KolorLinii, DashStyle StyłLinii, float GrubośćLinii) :
                base(r, WysokośćWałca, StopieńWielokątaPodstawy, Xsp,Ysp,KolorLinii, StyłLinii, GrubośćLinii)
            {
                scWidoczny = false;
                //ustawienie rodzaju bryły
                scRodzajBryły = scTypyBrył.scBG_WalecPochylony;
               
                //wyznaczenie os ielipsy wykreślanej w podłodze i suficie wałca
                scOś_duża = 2 * scPromieńBryły;
                scOś_mała = scPromieńBryły / 2;
                //wyznaczenie  współrzędnych środka sufitu Wałac
                scXsS = Xsp + (int)(WysokośćWałca / Math.Tan(Math.PI * scKątPochyleniaWalca / 180F));
                scYsS = scYsp - WysokośćWałca;
                //wyznaczenia kątów polożenia
                scKątMiędzyWierzchołkami = 360 / scStopieńWielokątaPodstawy;
                scKątPolożenia = 0F;
                //wyznaczenie wspólrzędnych punktów w "podłodze" i "suficie" walca dla wykreślenia
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                scWielokątSufitu = new Point[scStopieńWielokątaPodstawy + 1];
                //utpwrzenie egzemplarzy punktów w podloże i sufitu
                for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątSufitu[sci] = new Point();
                    //podłoga
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                       Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    //sufit
                    scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                      Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                    scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                }
                //obliczenie pola powierzchni Wałca
                //obliczenie objetności Wałca

            }

            //nadpisanie metod abstrakcyjnych zadeklarowanych w klasie BryłyAbstrakcyjne
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pioro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pioro.DashStyle = scStyłlinii;
                    //wykreślenie podłogi Wałca
                    scRysownica.DrawEllipse(Pioro, scXsp - scOś_duża / 2, scYsp - scOś_mała / 2,
                        scOś_duża, scOś_mała);
                    //wykreślenie sufitu Wałca
                    scRysownica.DrawEllipse(Pioro, scXsS - scOś_duża / 2, scYsS - scOś_mała / 2,
                       scOś_duża, scOś_mała);
                    using (Pen PioroPrążków = new Pen(scKolorLinii, 0.5F))
                    {
                        PioroPrążków.DashStyle = DashStyle.Dot;
                        //wykreślenie prążków
                        for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(PioroPrążków, scWielokątPodłogi[sci], scWielokątSufitu[sci]);

                        //wykreślenie krawędzi bocznych Wałca
                        //wykreślenei krawędzi lewej
                        scRysownica.DrawLine(Pioro, scXsp - scOś_duża / 2, scYsp,
                            scXsS - scOś_duża / 2, scYsS);
                        //wykreślenei krawędzi prawej
                        scRysownica.DrawLine(Pioro, scXsp + scOś_duża / 2, scYsp,
                            scXsS + scOś_duża / 2, scYsS);
                        scWidoczny = true;



                    }
                }

            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pioro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pioro.DashStyle = scStyłlinii;
                        //wykreślenie podłogi Wałca
                        scRysownica.DrawEllipse(Pioro, scXsp - scOś_duża / 2, scYsp - scOś_mała / 2,
                            scOś_duża, scOś_mała);
                        //wykreślenie sufitu Wałca
                        scRysownica.DrawEllipse(Pioro, scXsS - scOś_duża / 2, scYsS - scOś_mała / 2,
                           scOś_duża, scOś_mała);
                        using (Pen PioroPrążków = new Pen(Pioro.Color, 0.5F))
                        {
                            PioroPrążków.DashStyle = DashStyle.Dot;
                            //wykreślenie prążków
                            for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                                scRysownica.DrawLine(PioroPrążków, scWielokątPodłogi[sci], scWielokątSufitu[sci]);

                            //wykreślenie krawędzi bocznych Wałca
                            //wykreślenei krawędzi lewej
                            scRysownica.DrawLine(Pioro, scXsp - scOś_duża / 2, scYsp,
                                scXsS - scOś_duża / 2, scYsS);
                            scWidoczny = false;


                        }
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                //obramy Wałec gdy on jest wykreślony
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);

                    if (scKierunekObrotu)
                        scKątPolożenia -= scKątObrotu;
                    else
                        scKątPolożenia += scKątObrotu;
                    //wyznaczenie nowych wspólrzednych wielokąta podłogi i sufitu
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {

                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                          Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                    }
                    scWykreśl(scRysownica);

                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                //deklaracje pomocnicze
                int scdX, scdY;
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    //wyznaczenie wartości przyrostów wspólrzednych dX, dY
                    scdX = scXsp < scX ? scX - scXsp : -(scXsp - scX);
                    scdY = scYsp < scY ? scY - scYsp : -(scYsp - scY);
                    //zmiana wartośći współrzędnych środka
                    scXsp = scXsp + scdX;
                    scYsp = scYsp + scdY;
                    scXsS = scYsS + scdX;
                    scYsS = scYsS + scdY;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {

                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(scXsS + scOś_duża / 2 *
                          Math.Cos(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));
                        scWielokątSufitu[sci].Y = (int)(scYsS + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPolożenia + sci * scKątMiędzyWierzchołkami) / 100F));

                    }
                    scWykreśl(scRysownica);
                }
            }
            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }



        }

        //deklaracja klasy potomnej Stożek
        public class Stożek: BryłyObrotówe
        {
            protected int scXsS, scYsS;
            protected int scStopieńWielokątaPodstawy;
            protected int scOś_duża, scOś_mała;
            protected float scKątPołożeniaPierwszegoWierzchołka;
            protected float scKątŚrodkowyMiędzyWierzchołkami;
            protected Point[] scWielokątPodłogi;

            //deklaracja konstruktorska
            public Stożek(int scR, int scWysokośćStożka, int scStopieńWielokąta,
                int scXsP, int scYsP, Color scKolorLinii, DashStyle scStylLinii,
                float scGrubośćLinii) : base(scR, scKolorLinii, scStylLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBG_Stożek;
                scWidoczny = false;
                scKierunekObrotu = false;
                scWysokośćBryły = scWysokośćStożka;
                scStopieńWielokątaPodstawy = scStopieńWielokąta;
                this.scXsp = scXsP; this.scYsp = scYsP;
                //wyznaczenie współrzędnych wierzchołka Stożka
                scXsS = scXsP; scYsS = scYsP - scWysokośćStożka;
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                scKątPołożeniaPierwszegoWierzchołka = 0f;
                scKątŚrodkowyMiędzyWierzchołkami = 360 / scStopieńWielokąta;
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy];
                //wyznaczenie współrzędnych wierzchołków wielokąta podstawy Stożka
                for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża/2 * 
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                        sci * scKątŚrodkowyMiędzyWierzchołkami)/ 180));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                       sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                }
                //obliczenie ola powierzchni Stożka
                //obliczenie objetości Stożka

            }

            //nadpisanie metod abstrakcyjnych
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //wykreślenie podstawy Stożek
                    scRysownica.DrawEllipse(Pióro , scXsp - scOś_duża/2,
                        scYsp - scOś_mała/2, scOś_duża , scOś_mała);
                    //wykreślenie prązków na śianie bocznej Stożka
                    using (Pen PióroPrążków = new Pen(Pióro.Color , Pióro.Width/3))
                    {
                        for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(PióroPrążków, scWielokątPodłogi[sci], new Point(scXsS, scYsS));

                    }
                    scRysownica.DrawLine(Pióro, scXsp - scOś_duża/2, scYsp, scXsS, scYsS);
                    scRysownica.DrawLine(Pióro, scXsp + scOś_duża/2, scYsp, scXsS , scYsS);
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
               if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //wykreślenie podstawy Stożek
                        scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża / 2,
                            scYsp - scOś_mała / 2, scOś_duża, scOś_mała);
                        //wykreślenie prązków na śianie bocznej Stożka
                        using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                                scRysownica.DrawLine(PióroPrążków, scWielokątPodłogi[sci], new Point(scXsS, scYsS));

                        }
                        scRysownica.DrawLine(Pióro, scXsp - scOś_duża / 2, scYsp, scXsS, scYsS);
                        scRysownica.DrawLine(Pióro, scXsp + scOś_duża / 2, scYsp, scXsS, scYsS);
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);

                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzchołka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzchołka += scKątObrotu;
                    //wyznaczenie nowych wspólrzednych wielokąta podłogi i sufitu
                    for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                    {

                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                            sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));

                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                //deklaracje pomocnicze
                int scdX, scdY;
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    //wyznaczenie wartości przyrostów wspólrzednych dX, dY
                    scdX = scXsp < scX ? scX - scXsp : -(scXsp - scX);
                    scdY = scYsp < scY ? scY - scYsp : -(scYsp - scY);
                    scXsp = scXsp + scdX;
                    scYsp = scYsp + scdY;
                    scXsS = scYsS + scdX;
                    scYsS = scYsS + scdY;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {

                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));

                    }
                    scWykreśl(scRysownica);
                }
            }
        }

        //deklaracja klasy potomnej Stożek pochylony
        public class StożekPochylony : Stożek
        {

            //deklaracja konstruktorska
            public StożekPochylony(int scR, int scWysokośćStożka, int scStopieńWielokąta, float scKątPochyleniaStożka,
                int scXsP, int scYsP, Color scKolorLinii, DashStyle scStylLinii,
                float scGrubośćLinii) : base(scR, scWysokośćStożka, scStopieńWielokąta, scXsP, scYsP, scKolorLinii, scStylLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBG_StożekPochylony;
                scWidoczny = false;
                scKierunekObrotu = false;
                //wyznaczenie współrzędnych wierzchołka Stożka
                scXsS = scXsP + (int)(scWysokośćStożka/Math.Tan(Math.PI * scKątPochyleniaStożka/180F));
                scYsS = scYsP - scWysokośćStożka;
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                scKątPołożeniaPierwszegoWierzchołka = 0f;
                scKątŚrodkowyMiędzyWierzchołkami = 360 / scStopieńWielokąta;
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy];
                //wyznaczenie współrzędnych wierzchołków wielokąta podstawy Stożka
                for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                        sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                       sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                }
                //obliczenie ola powierzchni Stożka
                //obliczenie objetości Stożka

            }

            //nadpisanie metod abstrakcyjnych
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //wykreślenie podstawy Stożek
                    scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża / 2,
                        scYsp - scOś_mała / 2, scOś_duża, scOś_mała);
                    //wykreślenie prązków na śianie bocznej Stożka
                    using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                    {
                        for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(PióroPrążków, scWielokątPodłogi[sci], new Point(scXsS, scYsS));

                    }
                    scRysownica.DrawLine(Pióro, scXsp - scOś_duża / 2, scYsp, scXsS, scYsS);
                    scRysownica.DrawLine(Pióro, scXsp + scOś_duża / 2, scYsp, scXsS, scYsS);
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //wykreślenie podstawy Stożek
                        scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża / 2,
                            scYsp - scOś_mała / 2, scOś_duża, scOś_mała);
                        //wykreślenie prązków na śianie bocznej Stożka
                        using (Pen PióroPrążków = new Pen(Pióro.Color, Pióro.Width / 3))
                        {
                            for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                                scRysownica.DrawLine(PióroPrążków, scWielokątPodłogi[sci], new Point(scXsS, scYsS));

                        }
                        scRysownica.DrawLine(Pióro, scXsp - scOś_duża / 2, scYsp, scXsS, scYsS);
                        scRysownica.DrawLine(Pióro, scXsp + scOś_duża / 2, scYsp, scXsS, scYsS);
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);

                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzchołka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzchołka += scKątObrotu;
                    //wyznaczenie nowych wspólrzednych wielokąta podłogi i sufitu
                    for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                    {

                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                            sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));

                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                //deklaracje pomocnicze
                int scdX, scdY;
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    //wyznaczenie wartości przyrostów wspólrzednych dX, dY
                    scdX = scXsp < scX ? scX - scXsp : -(scXsp - scX);
                    scdY = scYsp < scY ? scY - scYsp : -(scYsp - scY);
                    scXsp = scXsp + scdX;
                    scYsp = scYsp + scdY;
                    scXsS = scYsS + scdX;
                    scYsS = scYsS + scdY;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                    {

                        scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                           Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));
                        scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                           Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzchołka +
                           sci * scKątŚrodkowyMiędzyWierzchołkami) / 180));

                    }
                    scWykreśl(scRysownica);
                }
            }
            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }
        }

        //deklaracja klasy Wielościany
        public class Wielościany: BryłaAbstrakcyjna
        {
            //deklaracja tablicy
            protected Point[] scWielokątPodłogi;
            protected Point[] scWielokątSufitu;
            //stopień wielpkąta
            protected int scStopieńWielokątaPodstawy;
            protected int scXsS, scYsS;
            //promień okręgu podłogi wielościanu
            protected int scPromieńBryły;

            public Wielościany(int scR, int scStopieńWielokątaPodstawy, Color scKolorLinii,
                DashStyle scStyłLinii, float scGrubośćLinii) : base(scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                this.scPromieńBryły = scR;
                this.scStopieńWielokątaPodstawy = scStopieńWielokątaPodstawy;
            }

            public override void scWykreśl(Graphics scRysownica)
            {
                throw new NotImplementedException();
            }
            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                throw new NotImplementedException();
            }
            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                throw new NotImplementedException();
            }
            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                throw new NotImplementedException();
            }
            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                throw new NotImplementedException();
            }


        }

        //deklaracja klasy potomnej Graniastosłup
        public class Graniastosłup: Wielościany
        {
            //deklaracje pomocnicze
            protected float scOś_duża, scOś_mała;
            protected float scKątŚrodkowyMiędzyWierzcholkami;
            protected float scKątPołożeniaPierwszegoWierzcholka;

            //deklaracja konstruktorska
            public Graniastosłup(int scR, int scWysokoścGraniatosłupa, int scStopieńWielokątaPodstawy,
                int scXsp, int scYsp, Color scKolorLinii, DashStyle scStyłLinii, float scGrubośćLinii) : base(scR, scStopieńWielokątaPodstawy, scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                //ustawenie rodzaju bryły
                scRodzajBryły = scTypyBrył.scBG_Graniatosłup;
                scWidoczny = false;
                scKierunekObrotu = false;
                scWysokośćBryły = scWysokoścGraniatosłupa;
                this.scStopieńWielokątaPodstawy = scStopieńWielokątaPodstawy;
                //zapisanie współrzędnych środek Graniastosłupa
                this.scXsp = scXsp; this.scYsp = scYsp; this.scXsS = scXsp; this.scYsS = scYsp;

                //wyznaczenie osi elipsy wyznaczonej w podlodze i suficice
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                //wyznaczenie kątów polożenia
                scKątŚrodkowyMiędzyWierzcholkami = 360 / scStopieńWielokątaPodstawy;
                scKątPołożeniaPierwszegoWierzcholka = 0f;

                //utworzenie egzemplarzy tablicy
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                scWielokątSufitu = new Point[scStopieńWielokątaPodstawy + 1];

                for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątSufitu[sci] = new Point();
                    //podłoga
                    scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                        Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    //sufit
                    scWielokątSufitu[sci].X = scWielokątPodłogi[sci].X;
                    scWielokątSufitu[sci].Y = scWielokątPodłogi[sci].Y - scWysokoścGraniatosłupa;
                }
                //obliczenie powierzchni
                //obliczenie objętości
            }
            //nadpisanie metod abstrakcyjnych

            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //podłoga
                    for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci+1]);
                    //sufit
                    for (int sci = 0; sci < scWielokątSufitu.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątSufitu[sci], scWielokątSufitu[sci + 1]);
                    //krawędzi bocznych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątSufitu[sci]);
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //podłoga
                        for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                        //sufit
                        for (int sci = 0; sci < scWielokątSufitu.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątSufitu[sci], scWielokątSufitu[sci + 1]);
                        //krawędzi bocznych
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątSufitu[sci]);
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka,scRysownica);
                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzcholka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzcholka += scKątObrotu;
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        //sufit
                        scWielokątSufitu[sci].X = scWielokątPodłogi[sci].X;
                        scWielokątSufitu[sci].Y = scWielokątPodłogi[sci].Y - scWysokośćBryły;
                    }
                    //wykreślenie 
                    scWykreśl(scRysownica);
                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    scXsp = scX;
                    scYsp = scY;
                    scXsS = scXsp;
                    scYsS = scYsp - scWysokośćBryły;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        //sufit
                        scWielokątSufitu[sci].X = scWielokątPodłogi[sci].X;
                        scWielokątSufitu[sci].Y = scWielokątPodłogi[sci].Y - scWysokośćBryły;
                    }
                    scWykreśl(scRysownica);
                }
            }
        }

        //deklaracja klasy potomnej Graniastosłup pochylony
         public class GraniastosłupPochyony: Graniastosłup
        {

            //deklaracja konstruktorska
            public GraniastosłupPochyony(int scR, int scWysokoścGraniatosłupa, 
                int scStopieńWielokątaPodstawy, float scKątPochyleniaGraniastosłupa,
                int scXsp, int scYsp, Color scKolorLinii, DashStyle scStyłLinii, float scGrubośćLinii) :
                base(scR,scWysokoścGraniatosłupa, scStopieńWielokątaPodstawy,
                    scXsp, scYsp, scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                //ustawenie rodzaju bryły
                scRodzajBryły = scTypyBrył.scBG_GraniatosłupPochylony;
                scWidoczny = false;
                scKierunekObrotu = false;
                 this.scXsS = scXsp + (int)(scWysokoścGraniatosłupa / Math.Tan(Math.PI * scKątPochyleniaGraniastosłupa / 180F));
                this.scYsS = scYsp - scWysokoścGraniatosłupa;

                //wyznaczenie osi elipsy wyznaczonej w podlodze i suficice
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                //wyznaczenie kątów polożenia
                scKątŚrodkowyMiędzyWierzcholkami = 360 / scStopieńWielokątaPodstawy;
                scKątPołożeniaPierwszegoWierzcholka = 0f;

                //utworzenie egzemplarzy tablicy
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                scWielokątSufitu = new Point[scStopieńWielokątaPodstawy + 1];

                for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątSufitu[sci] = new Point();
                    //podłoga
                    scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                        Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    //sufit
                    scWielokątSufitu[sci].X = (int)(this.scXsS + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    scWielokątSufitu[sci].Y = (int)(this.scYsS + scOś_mała / 2 *
                        Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));

                }
                //obliczenie powierzchni
                //obliczenie objętości
            }
            //nadpisanie metod abstrakcyjnych

            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //podłoga
                    for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci+1]);
                    //sufit
                    for (int sci = 0; sci < scWielokątSufitu.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątSufitu[sci], scWielokątSufitu[sci + 1]);
                    //krawędzi bocznych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątSufitu[sci]);
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //podłoga
                        for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                        //sufit
                        for (int sci = 0; sci < scWielokątSufitu.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątSufitu[sci], scWielokątSufitu[sci + 1]);
                        //krawędzi bocznych
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątSufitu[sci]);
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka,scRysownica);
                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzcholka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzcholka += scKątObrotu;
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(this.scXsS + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                        scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątSufitu[sci].Y = (int)(this.scYsS + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    //wykreślenie 
                    scWykreśl(scRysownica);
                }

            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    scXsp = scX;
                    scYsp = scY;
                    scXsS = scXsp;
                    scYsS = scYsp - scWysokośćBryły;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        //sufit
                        scWielokątSufitu[sci].X = (int)(this.scXsS + scOś_duża / 2 *
                         Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                         scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątSufitu[sci].Y = (int)(this.scYsS + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }
        }

        //deklaracja klasy potomnej Ostrosłup
        public class Ostrosłup : Wielościany
        {
            //deklaracje pomocnicze
            protected float scOś_duża, scOś_mała;
            protected float scKątŚrodkowyMiędzyWierzcholkami;
            protected float scKątPołożeniaPierwszegoWierzcholka;
            public Ostrosłup(int scR, int scWysokoścOstrosłupa, int scStopieńWielokąta,
              int scXsp, int scYsp, Color scKolorLinii, DashStyle scStyłLinii, float scGrubośćLinii) : base(scR, scStopieńWielokąta, scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBG_Ostrosłup;
                scWidoczny = false;
                scKierunekObrotu = false;
                scWysokośćBryły = scWysokoścOstrosłupa;
                scStopieńWielokątaPodstawy = scStopieńWielokąta;
                this.scXsp = scXsp;this.scYsp = scYsp;
                //wyznaczenie współrzędnych wierzchołka
                scXsS = scXsp; scYsS = scYsp - scWysokoścOstrosłupa;
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                scKątPołożeniaPierwszegoWierzcholka = 0f;
                scKątŚrodkowyMiędzyWierzcholkami = 360 / scStopieńWielokąta;
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                        sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                       sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                }
                //obliczenie pola powierzchni
                //obliczenie objetości

            }

            //nadpisanie metod abstrakcyjnych
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen (scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //podstawa
                    for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci+1]);
                    //krawędzi bocznych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro,scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //podstawa
                        for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                        //krawędzi bocznych
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka,scRysownica);
                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzcholka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzcholka += scKątObrotu;
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    //wykreślenie 
                    scWykreśl(scRysownica);

                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    scXsp = scX;
                    scYsp = scY;
                    scXsS = scXsp;
                    scYsS = scYsp - scWysokośćBryły;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    scWykreśl(scRysownica);
                }
            }
        }

        //deklaracja klasy potomnej Ostrosłup pochylony
        public class OstrosłupPochylony : Ostrosłup
        {
            public OstrosłupPochylony(int scR, int scWysokoścOstrosłupa, int scStopieńWielokąta,float scKątPochyleniaOstrosłupa, 
              int scXsp, int scYsp, Color scKolorLinii, DashStyle scStyłLinii, float scGrubośćLinii) :
                base(scR, scWysokoścOstrosłupa, scStopieńWielokąta, scXsp ,scYsp,scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBGOstrosłupPochylony;
                scWidoczny = false;
                scKierunekObrotu = false;
                //wyznaczenie współrzędnych wierzchołka
                scXsS = scXsp + (int)(scWysokoścOstrosłupa / Math.Tan(Math.PI * scKątPochyleniaOstrosłupa / 180F)); ; ;
                scYsS = scYsp - scWysokoścOstrosłupa;
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                scKątPołożeniaPierwszegoWierzcholka = 0f;
                scKątŚrodkowyMiędzyWierzcholkami = 360 / scStopieńWielokąta;
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                        sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                       sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                }
                //obliczenie pola powierzchni
                //obliczenie objetości

            }

            //nadpisanie metod abstrakcyjnych
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //podstawa
                    for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                    //krawędzi bocznych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //podstawa
                        for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                        //krawędzi bocznych
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzcholka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzcholka += scKątObrotu;
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    //wykreślenie 
                    scWykreśl(scRysownica);

                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    scXsp = scX;
                    scYsp = scY;
                    scXsS = scXsp;
                    scYsS = scYsp - scWysokośćBryły;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }
        }

        //deklaracja klasy potomnej Kula
        public class Kula: BryłyObrotówe
        {
            protected float scOś_duża, scOś_mala;
            protected int scPrzesuńięcieObręczy;
            protected float scKątPołożeniaObręczy;
            //konstruktor klasy Kula
            public Kula(int scR, Point scŚrodekPodłogi, Color scKolorLinii, DashStyle scStylLinii,
                float scGrubośćLinii) : base(scR, scKolorLinii, scStylLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBG_Kula;
                scWidoczny = false;
                scKierunekObrotu = false;
                scXsp = scŚrodekPodłogi.X;
                scYsp = scŚrodekPodłogi.Y;
                scOś_duża = scR * 2;
                scOś_mala = scR / 2;
                scKątPołożeniaObręczy = 0;
                scPrzesuńięcieObręczy = 0;
                //obliczenie objętości i pola powierzchni kuli
                this.scObjetośćBryły = 4 / 3 * (float)(Math.PI * ((scOś_duża/2) * (scOś_duża/2)
                    * (scOś_duża / 2) * (scOś_duża / 2)));
                this.scPowierzchniaBryły = 4 * (float)(Math.PI * ((scOś_duża / 2)*(scOś_mala / 2)));
            }

            public override void scWykreśl(Graphics scRysownica)
            {
                Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii);
                Pen PióroObręczy = new Pen(Pióro.Color, 0.5f);
                Pióro.DashStyle = scStyłlinii;
                scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża/2, scYsp - scOś_mala/2,
                    scOś_duża, scOś_duża);
                scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża/2, scYsp + scOś_mala, scOś_duża, scOś_mala);
                scRysownica.DrawEllipse(PióroObręczy, scPrzesuńięcieObręczy/2 + scXsp - scOś_duża/ 2, 
                    scYsp - scOś_mala/2 , scOś_duża - scPrzesuńięcieObręczy , scOś_duża);
                scWidoczny = true;
                PióroObręczy.Dispose();
                Pióro.Dispose();
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
               if (scWidoczny)
                {
                    Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii);
                    Pen PióroObręczy = new Pen(Pióro.Color, 0.5f);
                    Pióro.DashStyle = scStyłlinii;
                    scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża / 2, scYsp - scOś_mala / 2,
                        scOś_duża, scOś_mala);
                    scRysownica.DrawEllipse(Pióro, scXsp - scOś_duża / 2, scYsp + scOś_mala, scOś_duża, scOś_mala);
                    scRysownica.DrawEllipse(PióroObręczy, scPrzesuńięcieObręczy / 2 + scXsp - scOś_duża / 2,
                        scYsp - scOś_mala / 2, scOś_duża - scPrzesuńięcieObręczy, scOś_duża);
                    scWidoczny = false;
                    PióroObręczy.Dispose();
                    Pióro.Dispose();
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                scKątPołożeniaObręczy = (scKątPołożeniaObręczy + scKątObrotu) % 360;
                scWymaż(scKontrolka, scRysownica);
                scPrzesuńięcieObręczy = (int)(scKątPołożeniaObręczy % (int)(scOś_duża)) * 2;
                scWykreśl(scRysownica);
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                scWymaż(scKontrolka, scRysownica);
                scXsp = scX;
                scYsp = scY;
                scWykreśl(scRysownica);
            }

            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }
        }

        //deklaracja klasy potomnej krzystał
        public class Krzystał:Ostrosłup
        {
            //deklaracje pomocnicze
            protected int scXn, scYn;
            public Krzystał(int scR, int scWysokoścOstrosłupa, int scStopieńWielokąta,
              int scXsp, int scYsp, Color scKolorLinii, DashStyle scStyłLinii, float scGrubośćLinii) :
                base(scR, scWysokoścOstrosłupa, scStopieńWielokąta, scXsp, scYsp, scKolorLinii, scStyłLinii, scGrubośćLinii)
            {
                scRodzajBryły = scTypyBrył.scBG_Krzystał;
                scWidoczny = false;
                scKierunekObrotu = false;
                scWysokośćBryły = scWysokoścOstrosłupa;
                scStopieńWielokątaPodstawy = scStopieńWielokąta;
                this.scXsp = scXsp; this.scYsp = scYsp;
                //wyznaczenie współrzędnych wierzchołka
                scXsS = scXsp; scYsS = scYsp - scWysokoścOstrosłupa;
                scXn = scXsp; scYn = scYsp + scWysokoścOstrosłupa;
                scOś_duża = 2 * scR;
                scOś_mała = scR / 2;
                scKątPołożeniaPierwszegoWierzcholka = 0f;
                scKątŚrodkowyMiędzyWierzcholkami = 360 / scStopieńWielokąta;
                scWielokątPodłogi = new Point[scStopieńWielokątaPodstawy + 1];
                for (int sci = 0; sci < scStopieńWielokątaPodstawy; sci++)
                {
                    scWielokątPodłogi[sci] = new Point();
                    scWielokątPodłogi[sci].X = (int)(scXsp + scOś_duża / 2 *
                        Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                        sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                    scWielokątPodłogi[sci].Y = (int)(scYsp + scOś_mała / 2 *
                       Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka +
                       sci * scKątŚrodkowyMiędzyWierzcholkami) / 180));
                }
                //obliczenie pola powierzchni
                //obliczenie objetości

            }

            //nadpisanie metod abstrakcyjnych
            public override void scWykreśl(Graphics scRysownica)
            {
                using (Pen Pióro = new Pen(scKolorLinii, scGrubośćLinii))
                {
                    Pióro.DashStyle = scStyłlinii;
                    //podstawa
                    for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                    //krawędzi bocznych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                        scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXn, scYn));
                    scWidoczny = true;
                }
            }

            public override void scWymaż(Control scKontrolka, Graphics scRysownica)
            {
                if (scWidoczny)
                {
                    using (Pen Pióro = new Pen(scKontrolka.BackColor, scGrubośćLinii))
                    {
                        Pióro.DashStyle = scStyłlinii;
                        //podstawa
                        for (int sci = 0; sci < scWielokątPodłogi.Length - 1; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], scWielokątPodłogi[sci + 1]);
                        //krawędzi bocznych
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXsS, scYsS));
                        for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                            scRysownica.DrawLine(Pióro, scWielokątPodłogi[sci], new Point(scXn, scYn));
                        scWidoczny = false;
                    }
                }
            }

            public override void scObróć_i_Wykreśl(Control scKontrolka, Graphics scRysownica, float scKątObrotu)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    if (scKierunekObrotu)
                        scKątPołożeniaPierwszegoWierzcholka -= scKątObrotu;
                    else
                        scKątPołożeniaPierwszegoWierzcholka += scKątObrotu;
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    //wykreślenie 
                    scWykreśl(scRysownica);

                }
            }

            public override void scPrzesuńDoNowegoXY(Control scKontrolka, Graphics scRysownica, int scX, int scY)
            {
                if (scWidoczny)
                {
                    scWymaż(scKontrolka, scRysownica);
                    scXsp = scX;
                    scYsp = scY;
                    scXsS = scXsp;
                    scYsS = scYsp - scWysokośćBryły;
                    scXn = scXsp;
                    scYn = scYsp + scWysokośćBryły;
                    //wyznaczenie nowych wspólrzednych
                    for (int sci = 0; sci <= scStopieńWielokątaPodstawy; sci++)
                    {
                        //podłoga
                        scWielokątPodłogi[sci].X = (int)(this.scXsp + scOś_duża / 2 *
                            Math.Cos(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                        scWielokątPodłogi[sci].Y = (int)(this.scYsp + scOś_mała / 2 *
                            Math.Sin(Math.PI * (scKątPołożeniaPierwszegoWierzcholka + sci *
                            scKątŚrodkowyMiędzyWierzcholkami) / 180F));
                    }
                    scWykreśl(scRysownica);
                }
            }

            public override void scZmieńKierunekObrotu(bool scKierunekObrotu)
            {
                this.scKierunekObrotu = scKierunekObrotu;
            }
        }
    }
}
