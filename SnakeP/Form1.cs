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

        public MainForm()
        {
            InitializeComponent();

            // Schlange mit Startlänge und Startposition erzeugen
            snake = new Snake();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Timer zum Bewegen der Schlange erzeugen
            timerBewegen = new Timer(); // Timer anlegen
            timerBewegen.Interval = 50; // Intervall festlegen (ms)
            timerBewegen.Tick += new EventHandler(timerBewegen_Tick); // Eventhandler ezeugen, der beim Timerablauf aufgerufen wird

            timerBewegen.Start(); // Timer starten
        }

        // Funktion wird nach Ablauf des Bewegungstimers aufgerufen
        // Bewegt die Schlange um eine Kachel
        private void timerBewegen_Tick(object sender, EventArgs e)
        {
            snake.Bewegen();
            labelGrafik.Invalidate();
        }

        // Zeichnet die Schlange
        private void labelGrafik_Paint(object sender, PaintEventArgs e)
        {
            snake.Draw(e.Graphics, labelGrafik.Width, labelGrafik.Height);
        }
    }
}
