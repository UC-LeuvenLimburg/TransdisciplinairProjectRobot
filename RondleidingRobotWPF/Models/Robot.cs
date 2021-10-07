using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RondleidingRobotWPF.Models
{
    public class Robot : Sprite
    {
        private Ellipse ellipse;
        private int stepSize;

        Random rnd = new Random();

        public Robot(Canvas drawingCanvas) : base(drawingCanvas)
        {
            ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);

            X = Convert.ToInt32(canvas.Width) / 2;
            Y = Convert.ToInt32(canvas.Height) / 2;
            Width = 15;
            Height = 15;
            stepSize = 15;

            canvas.Children.Add(ellipse);
        }

        protected override void UpdateElement()
        {
            ellipse.Margin = new Thickness(X, Y, 0, 0);
            ellipse.Width = Width;
            ellipse.Height = Height;
        }

        public void Move()
        {
            int num = rnd.Next(4);
            switch (num)
            {
                case 0:
                    X = X - stepSize;
                    break;

                case 1:
                    X = X + stepSize;
                    break;

                case 2:
                    Y = Y - stepSize;
                    break;

                case 3:
                    Y = Y + stepSize;
                    break;
            }
        }
    }
}
