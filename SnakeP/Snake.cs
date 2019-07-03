using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeP
{
    // Mögliche Bewegungsrichtungen der Schlange
    public enum Richtung
    {
        Links,
        Rechts,
        Rauf,
        Runter
    }

    // Klasse SnakeSegment
    // Eine Schlange besteht aus einzelnen Segmenten.
    // Diese Klasse enthält die Position des Segements und zeichnet es.
    class SnakeSegment
    {
        // Typ des Schlangensegments
        public enum SegmentTyp
        {
            Kopf,       // Kopfsegment
            Schwanz,    // Schwanzsegment
            Mitte       // Rumpfsegment
        }

        // Position des Segments.
        // Das Spielfeld ist in Kacheln aufgeteilt. 
        // x- und y-Koordinates sind dabei die Kachelposition im Spielfeld.
        public int x { get; set; } // x-Position
        public int y { get; set; } // y-Position

        // Konstruktor
        public SnakeSegment(int px, int py)
        {
            x = px;
            y = py;
        }

        // Zeichnet ein Schlangensegment
        public void Draw(Graphics g, int FensterBreite, int FensterHoehe, SegmentTyp typ)
        {
            // Berechnet die Größe eines Segments in Abhängigkeit von der Fenstergröße
            double SegmentGroesseX = (double)FensterBreite / (double)Snake.AnzahlSpielfeldKacheln;
            double SegmentGroesseY = (double)FensterHoehe / (double)Snake.AnzahlSpielfeldKacheln;

            // Berechne Fensterkoordinaten aus Kachelkoordinaten
            int FensterPosX = (int)(SegmentGroesseX * (double)x + 0.5);
            int FensterPosY = (int)(SegmentGroesseY * (double)y + 0.5);

            // Zeichne Kopf, Rumpf und Schwanz
            // Zurzeit unterscheiden sich diese nur durch die Farbe
            if (typ == SegmentTyp.Mitte)
            {
                // Zeichne Rumpfsegment
                g.FillRectangle(new SolidBrush(System.Drawing.Color.Black), FensterPosX, FensterPosY, (int)(SegmentGroesseX + 0.99), (int)(SegmentGroesseY + 0.99));
            }
            else if (typ == SegmentTyp.Kopf)
            {
                // Zeichne Kopfsegment
                g.FillRectangle(new SolidBrush(System.Drawing.Color.Red), FensterPosX, FensterPosY, (int)(SegmentGroesseX + 0.99), (int)(SegmentGroesseY + 0.99));
            }
            else
            {
                // Zeichne Schwanzsegment
                g.FillRectangle(new SolidBrush(System.Drawing.Color.Blue), FensterPosX, FensterPosY, (int)(SegmentGroesseX + 0.99), (int)(SegmentGroesseY + 0.99));
            }
        }
    }

    // Klasse Snake
    class Snake
    {
        // Das Spielfeld ist in Kacheln aufgeteilt.
        // Diese Konstante legt die Anzahl der Kacheln in x- und y-Richtung fest
        public const int AnzahlSpielfeldKacheln = 50;

        // Liste mit Schlangensegmenten
        private List<SnakeSegment> SegmentList;

        // Die aktuelle Richtung der Schlange
        public Richtung richtung { get; set; }

        // Kontruktor
        public Snake()
        {
            Init();
        }

        // Initialisierungsfunktion
        // Wird vom Konstruktor bzw. wenn ein neues Spiel gestartet wurde, aufgerufen
        public void Init()
        {
            int Zentrum = AnzahlSpielfeldKacheln / 2;

            // Startricht ist nach rechts
            richtung = Richtung.Rechts;

            // Eine Schlange mit 4 Segmenten erzeugen
            SegmentList = new List<SnakeSegment>();
            SegmentList.Add(new SnakeSegment(Zentrum - 0, Zentrum)); // Kopf
            SegmentList.Add(new SnakeSegment(Zentrum - 1, Zentrum)); // Rumpf
            SegmentList.Add(new SnakeSegment(Zentrum - 2, Zentrum)); // Rumpf
            SegmentList.Add(new SnakeSegment(Zentrum - 3, Zentrum)); // Schwanz
        }

        // Gibt die Länge (Anzahl der Segmente) der Schlange zurück
        public int Laenge
        {
            get
            {
                return SegmentList.Count;
            }
        }

        // Diese Funktion bewegt die Schlange in die gesetzte Richtung
        // Das Bewegen wird durch Hinzufügen eines neuen Kopfsegment und durch Löschen des Schwanzsegments erreicht
        // Falls die Schlange wachsen soll, wird nur der Kopf hinzugefügt und der Schwanz nicht gelöscht.
        // Beim Errechen des Spielfeldrandes erscheint die Schlange wieder auf der anderen Seite
        public void Bewegen()
        {
            // Koordinaten des Kopfsegments
            SnakeSegment Head = SegmentList[0];
            int xNeu = Head.x;
            int yNeu = Head.y;

            switch (richtung)
            {
                // Bewegung nach links
                case Richtung.Links:
                    if (xNeu == 0)
                    {
                        // Linker Spiefeldrand erreicht, neuer Kopf ercheint am rechten RAnd
                        xNeu = AnzahlSpielfeldKacheln - 1;
                    }
                    else
                    {
                        // Neuer Kopf erscheint eine Kachel weiter links
                        xNeu--;
                    }
                    break;

                // Bewegung nach rechts
                case Richtung.Rechts:
                    if (xNeu == (AnzahlSpielfeldKacheln - 1))
                    {
                        // Rechter Spiefeldrand erreicht, neuer Kopf ercheint am linken Rand
                        xNeu = 0;
                    }
                    else
                    {
                        // Neuer Kopf erscheint eine Kachel weiter rechts
                        xNeu++;
                    }
                    break;

                // Bewegung nach oben
                case Richtung.Rauf:
                    if (yNeu == 0)
                    {
                        // Oberer Spiefeldrand erreicht, neuer Kopf ercheint am unteren Rand
                        yNeu = AnzahlSpielfeldKacheln - 1;
                    }
                    else
                    {
                        // Neuer Kopf erscheint eine Kachel weiter oben
                        yNeu--;
                    }
                    break;

                // Bewegung nach unten
                default:
                case Richtung.Runter:
                    if (yNeu == (AnzahlSpielfeldKacheln - 1))
                    {
                        // Unterer Spiefeldrand erreicht, neuer Kopf ercheint am oberen Rand
                        yNeu = 0;
                    }
                    else
                    {
                        // Neuer Kopf erscheint eine Kachel weiter unten
                        yNeu++;
                    }
                    break;
            }

            // Füge neues Kopfsegment zur Segmentliste hinzu
            SegmentList.Insert(0, new SnakeSegment(xNeu, yNeu));

            // Lösche Schwanzsegment
            SegmentList.RemoveAt(SegmentList.Count - 1);
        }

        // Zeichne Schlange
        public void Draw(Graphics g, int FensterBreite, int FensterHoehe)
        {
            // Zeichne Rumpf
            for (int jj = 1; jj < (SegmentList.Count - 1); jj++)
            {
                SegmentList[jj].Draw(g, FensterBreite, FensterHoehe, SnakeSegment.SegmentTyp.Mitte);
            }
            // Schwanz und Kopf werden als letzes gezeichnet, damit diese auch nach der Kollsion noch sichtbar sind
            SegmentList[SegmentList.Count - 1].Draw(g, FensterBreite, FensterHoehe, SnakeSegment.SegmentTyp.Schwanz);
            SegmentList[0].Draw(g, FensterBreite, FensterHoehe, SnakeSegment.SegmentTyp.Kopf);
        }
    }
}
