using RondleidingRobotWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace RondleidingRobotWPF
{
    public partial class MainWindow : Window
    {
        private Robot robot;
        List<Beweging> movementList = new List<Beweging>();
        private DispatcherTimer moveTimer;

        public MainWindow()
        {
            InitializeComponent();

            robot = new Robot(drawingCanvas);

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(100);
            moveTimer.Tick += moveTimer_Tick;
        }

        public List<Beweging> readMovement()
        {
            Beweging beweging;

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(folderPath, "bewegingen robot.txt");

            StreamReader reader = File.OpenText(filePath);

            string row;
            string[] columns;
            char[] separators = { ':' };

            row = reader.ReadLine();
            while (row != null)
            {
                columns = row.Split(separators);
                beweging = new Beweging();
                beweging.Richting = Convert.ToString(columns[0]);
                beweging.Tijd = Convert.ToString(columns[1]);
                movementList.Add(beweging);
                row = reader.ReadLine();
            }
            reader.Close();
            return movementList;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            readMovement();
            moveTimer.Start();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            moveTimer.Stop();
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            if (movementList.Count() != 0)
            {
                robot.Destination(movementList.First());
                movementList.Remove(movementList.First());
            }
        }
    }
}