using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RondleidingRobotWPF.Models
{
    public class Robot : Sprite
    {
        private const int StepSize = 150;

        private Ellipse ellipse;
        private DispatcherTimer moveTimer;

        public string Command { get; set; }

        public Robot(Canvas drawingCanvas) : base(drawingCanvas)
        {
            Command = "X:0";
            ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);

            X = Convert.ToInt32(drawingCanvas.Width) / 2;
            Y = Convert.ToInt32(drawingCanvas.Height) / 2;

            Width = 15;
            Height = 25;

            canvas.Children.Add(ellipse);
 

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(10);
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            move();
        }

        protected override void UpdateElement()
        {
            ellipse.RenderTransform = new RotateTransform(Angle , Width /2, Height/2);
            ellipse.Margin = new Thickness(X, Y, 0, 0);
            ellipse.Width = Width;
            ellipse.Height = Height;
        }

        private void move() 
        {
            string[] columns;
            char[] separators = { ':' };
            columns = Command.Split(separators);
            string beweging = Convert.ToString(columns[0]);
            int snelheid = Convert.ToInt32(columns[1]);

            switch (beweging)
            {
                case "A":
                    Angle -= snelheid / StepSize;
                    break;
                case "D":
                    Angle += snelheid / StepSize;
                    break;
                case "W":
                    X = X + snelheid * Math.Cos((Math.PI / 180) * Angle) / StepSize;
                    Y = Y + snelheid * Math.Sin((Math.PI / 180) * Angle) / StepSize;
                    break;
                case "S":
                    X = X - snelheid * Math.Cos((Math.PI / 180) * Angle) / StepSize;
                    Y = Y - snelheid * Math.Sin((Math.PI / 180) * Angle) / StepSize;
                    break;
                default:
                    break;
            }
        }
    }
}
