using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProgettoLavoro
{
    public partial class MainWindow : Window
    {
        private bool turnoX = true;
        private int mosse = 0;
        private bool giocoFinito = false;

        public MainWindow()
        {
            InitializeComponent();
            AvviaNuovaPartita();
        }

        private void GestisciClickPulsante(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Esci se il gioco è finito o se il bottone è già premuto
            if (giocoFinito || btn.Content?.ToString() != "") return;

            // Imposta segno e colore (Azzurro per X, Rosso per O)
            btn.Content = turnoX ? "X" : "O";
            btn.Foreground = turnoX ? new SolidColorBrush(Color.FromRgb(144, 202, 249)) : new SolidColorBrush(Color.FromRgb(239, 83, 80));

            mosse++;

            if (ControllaVincitore())
            {
                testoStatoGioco.Text = $"Vittoria! Il giocatore {(turnoX ? "X" : "O")} ha vinto!";
                giocoFinito = true;
            }
            else if (mosse == 9)
            {
                testoStatoGioco.Text = "Pareggio!";
                giocoFinito = true;
            }
            else
            {
                turnoX = !turnoX;
                testoStatoGioco.Text = $"Turno del giocatore: {(turnoX ? "X" : "O")}";
            }
        }

        private bool ControllaVincitore() =>
            ControllaLinea(pulsante0_0, pulsante0_1, pulsante0_2) ||
            ControllaLinea(pulsante1_0, pulsante1_1, pulsante1_2) ||
            ControllaLinea(pulsante2_0, pulsante2_1, pulsante2_2) ||
            ControllaLinea(pulsante0_0, pulsante1_0, pulsante2_0) ||
            ControllaLinea(pulsante0_1, pulsante1_1, pulsante2_1) ||
            ControllaLinea(pulsante0_2, pulsante1_2, pulsante2_2) ||
            ControllaLinea(pulsante0_0, pulsante1_1, pulsante2_2) ||
            ControllaLinea(pulsante0_2, pulsante1_1, pulsante2_0);

        private bool ControllaLinea(Button b1, Button b2, Button b3)
        {
            string c1 = b1.Content?.ToString();
            return !string.IsNullOrEmpty(c1) && c1 == b2.Content?.ToString() && c1 == b3.Content?.ToString();
        }

        private void AvviaNuovaPartita(object sender = null, RoutedEventArgs e = null)
        {
            // Ripulisce tutti i bottoni
            Button[] pulsanti = { pulsante0_0, pulsante0_1, pulsante0_2, pulsante1_0, pulsante1_1, pulsante1_2, pulsante2_0, pulsante2_1, pulsante2_2 };
            foreach (Button btn in pulsanti) btn.Content = "";

            turnoX = true; mosse = 0; giocoFinito = false;
            testoStatoGioco.Text = "Turno del giocatore: X";
        }
    }
}