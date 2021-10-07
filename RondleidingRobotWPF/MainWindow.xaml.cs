using RondleidingRobot.Uart;
using RondleidingRobotWPF.Models;
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
using System.Windows.Threading;

namespace RondleidingRobotWPF
{
    

    public partial class MainWindow : Window
    {

        private Robot robot;
        private DispatcherTimer animationTimer;
        public MainWindow()
        {
            InitializeComponent();

            robot = new Robot(drawingCanvas); 

            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(300);
            animationTimer.Tick += animationTimer_Tick;
            animationTimer.Start();
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            robot.Move();
        }
    }
}
