using RondleidingRobot.Models;
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

        public MainWindow()
        {
            InitializeComponent();

            Robot robot = new Robot(drawingCanvas);
            IOutput movementConnetion = new SimulationOutput(robot);
            MoveModel model = new MoveModel(movementConnetion);
        }

        

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}