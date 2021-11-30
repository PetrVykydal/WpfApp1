
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        private bool m1PlayerTurn;

        private bool mGameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }

        #endregion

        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.free;

            m1PlayerTurn = true;

            Container.Children.Cast<Button>().ToList().ForEach(Button =>
            {
                Button.Content = string.Empty;
                Button.Background = Brushes.White;
                Button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.free)
                return;

            mResults[index] = m1PlayerTurn ? MarkType.cross : MarkType.nought;

            button.Content = m1PlayerTurn ? "X" : "O";

            if (!m1PlayerTurn)
                button.Background = Brushes.Red;

            if (m1PlayerTurn)
                m1PlayerTurn = false;



            else
                m1PlayerTurn = true;


            CheckForWinner();

        }

        private void CheckForWinner()
        {

            #region Horizontal wins

            if (mResults[0] != MarkType.free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;


                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;


            }
            /////
            if (mResults[3] != MarkType.free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;


                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;


            }
            ///////
            if (mResults[6] != MarkType.free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;


                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;


            }
            #endregion

            #region vertical wins 
            if (mResults[0] != MarkType.free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;


                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }


            if (mResults[1] != MarkType.free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;


                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }

            if (mResults[2] != MarkType.free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;


                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }

            #endregion

            #region diagonal wins
            if (mResults[0] != MarkType.free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;


                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }

            if (mResults[2] != MarkType.free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;


                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

            }

            #endregion

            #region no winners

            if (!mResults.Any(f => f == MarkType.free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
