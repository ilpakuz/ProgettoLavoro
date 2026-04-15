using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProgettoLavoro
{
    public partial class MainWindow : Window
    {
        // VARIABILI DI GIOCO
        private bool turnoX = true;       // Indica se è il turno di X (true) o di O (false)
        private int mosse = 0;            // Conta le mosse fatte (max 9)
        private bool giocoFinito = false; // Diventa true quando qualcuno vince o c'è pareggio

        // VARIABILI PER NOMI E PUNTEGGIO
        private string nomeX = "Giocatore 1";
        private string nomeO = "Giocatore 2";
        private int vittorieX = 0;
        private int vittorieO = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // AZIONE: Inizia la sfida dal menu iniziale
        private void IniziaPartita_Click(object sender, RoutedEventArgs e)
        {
            // Se i nomi sono vuoti, usiamo quelli predefiniti
            nomeX = string.IsNullOrWhiteSpace(inputNomeGiocatoreX.Text) ? "Giocatore 1" : inputNomeGiocatoreX.Text;
            nomeO = string.IsNullOrWhiteSpace(inputNomeGiocatoreO.Text) ? "Giocatore 2" : inputNomeGiocatoreO.Text;

            // Passiamo alla schermata di gioco
            schermataIniziale.Visibility = Visibility.Collapsed;
            schermataGioco.Visibility = Visibility.Visible;

            AggiornaPunteggio();
            AvviaNuovaPartita();
        }

        // AZIONE: Gestisce il click su una casella della griglia
        private void GestisciClickPulsante(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Non fare nulla se il gioco è finito o la casella è già occupata
            if (giocoFinito || btn.Content?.ToString() != "") return;

            // Imposta il simbolo e il colore (Azzurro per X, Rosso per O)
            btn.Content = turnoX ? "X" : "O";
            btn.Foreground = turnoX ? new SolidColorBrush(Color.FromRgb(144, 202, 249)) : new SolidColorBrush(Color.FromRgb(239, 83, 80));

            mosse++;

            // Controlla se questa mossa ha portato alla vittoria
            if (ControllaVincitore())
            {
                string vincitore = turnoX ? nomeX : nomeO;
                testoStatoGioco.Text = $"Vittoria! {vincitore} ha vinto!";

                // Aggiorna il punteggio totale
                if (turnoX) vittorieX++; else vittorieO++;
                AggiornaPunteggio();
                giocoFinito = true;
            }
            // Se non c'è vittoria e le mosse sono 9, è pareggio
            else if (mosse == 9)
            {
                testoStatoGioco.Text = "Pareggio!";
                giocoFinito = true;
            }
            // Altrimenti passa il turno all'altro giocatore
            else
            {
                turnoX = !turnoX;
                testoStatoGioco.Text = $"Turno di: {(turnoX ? nomeX : nomeO)}";
            }
        }

        // LOGICA: Controlla tutte le linee possibili per trovare un vincitore
        private bool ControllaVincitore() =>
            ControllaLinea(pulsante0_0, pulsante0_1, pulsante0_2) || // Riga 1
            ControllaLinea(pulsante1_0, pulsante1_1, pulsante1_2) || // Riga 2
            ControllaLinea(pulsante2_0, pulsante2_1, pulsante2_2) || // Riga 3
            ControllaLinea(pulsante0_0, pulsante1_0, pulsante2_0) || // Colonna 1
            ControllaLinea(pulsante0_1, pulsante1_1, pulsante2_1) || // Colonna 2
            ControllaLinea(pulsante0_2, pulsante1_2, pulsante2_2) || // Colonna 3
            ControllaLinea(pulsante0_0, pulsante1_1, pulsante2_2) || // Diagonale 1
            ControllaLinea(pulsante0_2, pulsante1_1, pulsante2_0);    // Diagonale 2

        // LOGICA: Verifica se tre pulsanti hanno lo stesso simbolo (non vuoto)
        private bool ControllaLinea(Button b1, Button b2, Button b3)
        {
            string c1 = b1.Content?.ToString();
            return !string.IsNullOrEmpty(c1) && c1 == b2.Content?.ToString() && c1 == b3.Content?.ToString();
        }

        // AZIONE: Pulisce la griglia per iniziare un nuovo round
        private void AvviaNuovaPartita(object sender = null, RoutedEventArgs e = null)
        {
            Button[] pulsanti = { pulsante0_0, pulsante0_1, pulsante0_2, pulsante1_0, pulsante1_1, pulsante1_2, pulsante2_0, pulsante2_1, pulsante2_2 };
            foreach (Button btn in pulsanti) btn.Content = "";

            turnoX = true;
            mosse = 0;
            giocoFinito = false;
            testoStatoGioco.Text = $"Turno di: {nomeX}";
        }

        // LOGICA: Aggiorna i testi del punteggio a video
        private void AggiornaPunteggio()
        {
            testoPunteggioX.Text = $"{nomeX}: {vittorieX}";
            testoPunteggioO.Text = $"{nomeO}: {vittorieO}";
        }

        // AZIONE: Ritorna alla schermata dei nomi
        private void TornaAlMenu_Click(object sender, RoutedEventArgs e)
        {
            schermataGioco.Visibility = Visibility.Collapsed;
            schermataIniziale.Visibility = Visibility.Visible;
        }

        // AZIONE: Chiude l'app
        private void Esci_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
