/*
 * Couper Ebbs-Picken
 * 6/18/2018
 * Does a problem
 */ 
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
using System.IO;

namespace culm_S1SumGame_CouperEbbsPicken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global variables
        StreamReader streamReader;
        int N;
        string swiftScoresStr;
        string semaScoresStr;
        int[] swiftScores;
        int[] semaScores;
        int[] spacesSwift;
        int[] spacesSema;
        int counter;
        bool done;
        int K;
        int semaSum;
        int swiftSum;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            // resetting variables
            streamReader = new StreamReader("Input.txt");
            int.TryParse(streamReader.ReadLine(), out N);
            swiftScoresStr = streamReader.ReadLine();
            semaScoresStr = streamReader.ReadLine();
            swiftScores = new int[N];
            semaScores = new int[N];
            spacesSwift = new int[N - 1];
            spacesSema = new int[N - 1];
            counter = 0;
            semaSum = 0;
            swiftSum = 0;
            K = 0;



            // creates an array of space indexes
            foreach (char c in swiftScoresStr)
            {
                if (c == ' ')
                {
                    spacesSwift[counter] = swiftScoresStr.IndexOf(c);
                    swiftScoresStr = swiftScoresStr.Substring(0, swiftScoresStr.IndexOf(c)) + "_" + swiftScoresStr.Substring(swiftScoresStr.IndexOf(c) + 1);
                    counter++;
                }
            }

            counter = 0;
            foreach (char c in semaScoresStr)
            {
                if (c == ' ')
                {
                    spacesSema[counter] = semaScoresStr.IndexOf(c);
                    semaScoresStr = semaScoresStr.Substring(0, semaScoresStr.IndexOf(c)) + "_" + semaScoresStr.Substring(semaScoresStr.IndexOf(c) + 1);
                    counter++;
                }
            }

            // does the first score 
            int.TryParse(swiftScoresStr.Substring(0, spacesSwift[0]), out int j);
            swiftScores[0] = j;
            int.TryParse(semaScoresStr.Substring(0, spacesSema[0]), out j);
            semaScores[0] = j;

            // does all the middle scores
            for (int i = 0; i < N - 2; i++)
            {
                int.TryParse(swiftScoresStr.Substring(spacesSwift[i] + 1, spacesSwift[i + 1] - (spacesSwift[i] + 1)), out j);
                swiftScores[i + 1] = j;
                int.TryParse(semaScoresStr.Substring(spacesSema[i] + 1, spacesSema[i + 1] - (spacesSema[i] + 1)), out j);
                semaScores[i + 1] = j;
            }

            // does the last scores
            int.TryParse(swiftScoresStr.Substring(spacesSwift[N - 2] + 1), out j);
            swiftScores[swiftScores.Length - 1] = j;
            int.TryParse(semaScoresStr.Substring(spacesSema[N - 2] + 1), out j);
            semaScores[semaScores.Length - 1] = j;

            // checks the sums
            counter = 0;
            while (done != true)
            {
                if (counter != N - 1)
                {
                    swiftSum += swiftScores[counter];
                    semaSum += semaScores[counter];
                    if (swiftSum == semaSum)
                    {
                        K = counter + 1;
                    }
                }

                else if (counter == N - 1)
                {
                    if (K == 0)
                    {
                        done = true;
                        K = 0;
                    }

                    else if(semaSum == swiftSum)
                    {
                        K = counter + 1;
                        done = true;
                    }

                    else
                    {
                        done = true;
                    }
                }

                counter++;

            }

            // does the output
            lblOutput.Content = K.ToString();
        }
    }
}
