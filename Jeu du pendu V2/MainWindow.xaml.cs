using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jeu_du_pendu_V2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Initialisation de toutes les variables
        int JOUER = 0;
        bool Lettre_dedans = false;
        bool gagne = false;
        bool perdu = false;
        string mot_devine = "";
        string mot_affiche = "";
        public string[] Mots = {
        "POIRE", "PINTADE", "NATURE", "CLAVIER", "MAISON", "TRAIN", "XILOPHONE", "ECRAN", "RETARD", "OISEAU",
        "COEUR", "JARDIN", "PARAPLUIE", "CHATEAU", "CARTE", "BOUSSOLE", "CASCADE", "MER", "ESPACE", "POISSON",
        "TORTUE", "ROUTE", "CHAPEAU", "CLOCHE", "FEE", "BALLON", "OR", "PINCEAU", "BULLE", "FRAISE", "FEU",
        "TIMBRE", "PLONGEE", "MONTRE", "CHAUSSETTE", "CERF", "VENT", "POEME", "ORCHESTRE", "LIVRE", "SAUT",
        "CHANT", "VAGUE", "BONBON", "FETE", "AURORE", "TABLE", "MONTAGNE", "POMME", "CHAT", "SOLEIL", "BONHEUR",
        "AVION", "CHANSON", "CAFE", "MUSIQUE", "PLAGE", "ETOILE", "AMOUR", "RIRE", "PAPILLON", "FORÊT", "BATEAU",
        "RÊVE", "LIBERTÉ", "ARC-EN-CIEL", "GUITARE", "VOYAGE", "ECOLE", "DANSE", "TELEPHONE", "NEIGE", "ECUREUIL",
        "PLUIE", "EGLISE", "NOEL", "CAFÉ", "ELEPHANT", "VOITURE", "THEATRE", "ORANGE", "DESSIN", "MAGIE", "HIVER",
        "ECOUTE", "PARADIS", "FILM", "BONJOUR", "ÉMERAUDE", "JOIE", "BIBLIOTHEQUE", "GENIE", "CONFITURE", "NUIT",
        "RADIO", "FAMILLE", "AVENTURE", "PARFUM", "MUSEE", "TELEVISION", "BOUGIE", "AVRIL", "CAFÉ", "DRAGON",
        "SERPENT", "HISTOIRE", "MAGASIN", "CRAYON", "OCEAN", "CITRON", "INVENTION", "BANANE", "CHAUSSURE",
        "COCCINELLE", "CHEMIN", "MATIN", "PAIX", "FOOTBALL", "CIRQUE", "PIANO", "SAPIN", "CUISINE", "SOMMEIL",
        "AMI", "RIVIERE", "CROISSANT", "FEUILLE", "CITROUILLE", "CAMERA", "GATEAU", "AILES", "POCHE", "TOURNESOL",
        "LUNE", "CABANE", "SOURIRE", "CLE", "COLOMBE", "CHAMPIGNON", "CHOCOLAT", "GOUTTE", "PLANETE", "LABYRINTHE",
        "ECLAIR", "PLUME", "MAGICIEN", "CREME", "MELODIE", "FLEUR", "SABLE", "NOIX", "RAYON", "REVE", "POESIE",
        "EXPLORATION", "DRAPEAU", "INSTRUMENT", "NUAGE", "COURAGE", "AMITIE", "MONTGOLFIERE", "ANIMAUX", "ESCALADE",
        "PASSION", "BROUILLARD", "CAMPEMENT", "CHANCE", "VOYAGEUR", "FORET", "FRAIS", "GENEROUS", "BOULEVARD",
        "LIBERTY", "HEROS", "SECRET", "DIAMANT", "CACTUS", "DICTIONNAIRE", "NAVIGUER", "HELICOPTERE", "ROBOT", "BANJO",
        "OBELISQUE", "FANTOME", "GRANDIOSE", "VAMPIRE", "BALLOON", "BRILLIANT", "CHAMPAGNE", "DELICIEUX", "ZEBRE",
        "ETINCELLE", "FLAMANT", "HARMONIE", "ILLUSION", "JUPITER", "KIWI", "LUXE", "MYSTERE", "NOMADE", "PEACOCK",
        "QUARTZ", "RHINOCEROS", "SATELLITE", "TORNADO", "UNICORN", "WONDERFUL", "XANADU", "YAKUZA", "ZODIAQUE"};

        int vie = 11;


        // Bouton de démarrage
        private void BTN_Start(object sender, RoutedEventArgs e)
        {
            JOUER = 1;
            // Initialiser le mot à deviner
            Random var = new Random();
            mot_devine = Mots[var.Next(Mots.Length)];
            // Remplacer chaque lettre par un espace dans le mot à afficher
            for (int i = 0; i < mot_devine.Length; i++)
            {
                mot_affiche += "_ ";
            }
            TB_Mot.Text = mot_affiche;
            // Désactiver le bouton de démarrage
            Start.IsEnabled = false;
        }

        // Bouton de redémarrage
        private void BTN_Restart(object sender, RoutedEventArgs e)
        {
            JOUER = 1;
            // Activer tous les boutons de lettres
            foreach (Button tout_bouton in Touches.Children.OfType<Button>())
            {
                tout_bouton.IsEnabled = true;
                Start.IsEnabled = false;
            }
            // Initialiser l'image du pendu
            Image_Pendu.Source = new BitmapImage(new Uri("/Images/Pendu_11.png", UriKind.Relative));
            // Initialiser le mot à deviner
            mot_affiche = "";
            vie = 11;
            TB_Vie.Text = vie.ToString();
            Random var = new Random();
            mot_devine = Mots[var.Next(Mots.Length)];
            // Remplacer chaque lettre par un espace dans le mot à afficher
            for (int i = 0; i < mot_devine.Length; i++)
            {
                mot_affiche += "_ ";
            }
            TB_Mot.Text = mot_affiche;
        }

        // Gestion du clic sur une lettre
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            if (JOUER == 1)
            {
                Button btn = sender as Button;
                string btnContent = btn.Content.ToString();
                btn.IsEnabled = false;
                // Parcourir chaque lettre du mot à deviner
                for (int i = 0; i < mot_devine.Length; i++)
                {
                    if (btnContent == mot_devine[i].ToString())
                    {
                        Lettre_dedans = true;
                        // Mettre à jour l'affichage du mot avec la lettre correcte
                        mot_affiche = mot_affiche.Substring(0, 2 * i) + btnContent + " " + mot_affiche.Substring(2 * i + 2);
                    }


                    // Vérifier si le mot a été entièrement deviné
                    if (mot_devine == mot_affiche.Replace(" ", ""))
                    {
                        gagne = true;
                        mot_affiche = "Vous avez gagné !\nLe mot était " + mot_devine + ".";
                        // Désactiver tous les boutons de lettres sauf Restart
                        foreach (Button Touches in Touches.Children.OfType<Button>())
                        {
                            Touches.IsEnabled = false;
                            Restart.IsEnabled = true;
                        }
                        JOUER = 0;
                    }
                }
                TB_Mot.Text = mot_affiche;


                // Si la lettre n'est pas dans le mot, décrémenter le nombre de vies
                if (Lettre_dedans == false)
                {
                    vie = vie - 1;
                    // Mettre à jour l'image du pendu
                    Image_Pendu.Source = new BitmapImage(new Uri("/Images/Pendu_" + vie.ToString() + ".png", UriKind.Relative));
                    // Si le nombre de vies atteint 0, la partie est perdue
                    if (vie <= 0)
                    {
                        vie = 0;
                        perdu = true;
                        TB_Mot.Text = "Le mot était " + mot_devine + ".";
                        // Désactiver tous les boutons de lettres sauf Restart
                        foreach (Button Touches in Touches.Children.OfType<Button>())
                        {
                            Touches.IsEnabled = false;
                            Restart.IsEnabled = true;
                        }
                        JOUER = 0;
                    }
                    TB_Vie.Text = vie.ToString();
                }
                Lettre_dedans = false;
            }
        }
    }
}