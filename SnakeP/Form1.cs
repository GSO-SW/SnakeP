using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeP
{
    public partial class MainForm : Form
    {
        // Die Schlange
        private Snake snake;

        // Timer zum Bewegen der Schlamge
        private Timer timerBewegen;

        // Timer zum Wachsen der Schlamge
        private Timer timerWachsen;

        public MainForm()
        {
            InitializeComponent();

            // Schlange mit Startlänge und Startposition erzeugen
            snake = new Snake();

            // KeyPreviw setzen um Tastatureingaben zu verarbeiten bevor sie vom
            // entsprechenden Control-Element verarbeitet werden
            KeyPreview = true;

            // Flackern bei der Ausgabe reduzieren
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Timer zum Bewegen der Schlange erzeugen
            timerBewegen = new Timer(); // Timer anlegen
            timerBewegen.Interval = 50; // Intervall festlegen (ms)
            timerBewegen.Tick += new EventHandler(timerBewegen_Tick); // Eventhandler ezeugen, der beim Timerablauf aufgerufen wird

            // Timer zum Wachsen der Schlange erzeugen
            timerWachsen = new Timer(); // Timer anlegen
            timerWachsen.Interval = 120; // Intervall festlegen (ms)
            timerWachsen.Tick += new EventHandler(timerWachsen_Tick); // Eventhandler ezeugen der beim Timerablauf aufgerufen wird
        }

        // Startet neues Spiel
        private void SpielStarten()
        {
            labelGrafik.Text = "";

            // Schlange initialisieren
            snake.Init();

            // Bewegungstimer starten
            timerBewegen.Start();

            // Wachstumstimer starten
            timerWachsen.Start();
        }

        // Stopt das Spiel
        private void SpielStoppen()
        {
            timerBewegen.Stop(); //
            timerWachsen.Stop(); //
        }

        // Funktion wird nach Ablauf des Bewegungstimers aufgerufen
        // Bewegt die Schlange um eine Kachel
        private void timerBewegen_Tick(object sender, EventArgs e)
        {
            snake.Bewegen();
            labelGrafik.Invalidate();
        }

        // Funktion wird nach Ablauf des Wachstunstimers aufgerufen
        // Vergrößert die Schlange um eine Kachel
        private void timerWachsen_Tick(object sender, EventArgs e)
        {
            snake.Wachsen();
        }

        // Zeichnet die Schlange
        private void labelGrafik_Paint(object sender, PaintEventArgs e)
        {
            snake.Draw(e.Graphics, labelGrafik.Width, labelGrafik.Height);
        }

        // Wird aufgerufen, nachdem eine Taste gedrückt wurde
        private void TasteGedrueckt(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                // Linke Pfeiltaste => Setze Richtung auf "Links"
                case Keys.Left:
                case Keys.NumPad4:
                    snake.richtung = Richtung.Links;
                    break;
                
                // Rechte Pfeiltaste => Setze Richtung auf "Rechts"
                case Keys.Right:
                case Keys.NumPad6:
                    snake.richtung = Richtung.Rechts;
                    break;

                // Nach oben Pfeiltaste => Setze Richtung auf "Rauf"
                case Keys.Up:
                case Keys.NumPad8:
                    snake.richtung = Richtung.Rauf;
                    break;

                // Nach unten Pfeiltaste => Setze Richtung auf "Runter"
                case Keys.Down:
                case Keys.NumPad2:
                    snake.richtung = Richtung.Runter;
                    break;

                // Enter Taste => Neues Spiel
                case Keys.Enter:
                    SpielStarten();
                    break;

                default:
                    break;
            }
        }

        // Startet neues Spiel nach Button-Click
        private void buttonNeuesSpiel_Click(object sender, EventArgs e)
        {
            SpielStarten();
        }

        // Notwendig damit alle Tasten-Events hier verarbeitet werden können
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }
    }
}
