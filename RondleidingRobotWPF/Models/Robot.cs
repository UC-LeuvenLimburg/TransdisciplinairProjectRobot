using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RondleidingRobotWPF.Models
{
    public class Robot : Sprite
    {
        float xDest, yDest;
        String currentCommand;
        private Ellipse ellipse;

        public Robot(Canvas drawingCanvas) : base(drawingCanvas)
        {
            ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);

            X = Convert.ToInt32(drawingCanvas.Width) / 2;
            Y = Convert.ToInt32(drawingCanvas.Height) / 2;
            xDest = X;
            yDest = Y;

            Width = 15;
            Height = 25;

            canvas.Children.Add(ellipse);
        }

        protected override void UpdateElement()
        {
            ellipse.Margin = new Thickness(X, Y, 0, 0);
            ellipse.Width = Width;
            ellipse.Height = Height;
            
        }

        public void Destination(Beweging beweging)
        {
            switch (Convert.ToString(beweging.Richting))
            {
                case "links": //Naar links
                    xDest = xDest - Convert.ToInt32(beweging.Tijd);
                    Width = 25;
                    Height = 15;
                    currentCommand = "links";
                    break;

                case "rechts": //Naar rechts
                    xDest = xDest + Convert.ToInt32(beweging.Tijd);
                    Width = 25;
                    Height = 15;
                    currentCommand = "rechts";
                    break;

                case "vooruit": //Naar voor
                    yDest = yDest - Convert.ToInt32(beweging.Tijd);
                    Width = 15;
                    Height = 25;
                    currentCommand = "vooruit";
                    break;

                case "achteruit": //Naar achter
                    yDest = yDest + Convert.ToInt32(beweging.Tijd);
                    Width = 15;
                    Height = 25;
                    currentCommand = "achteruit";
                    break;
            }
        }
        
        public void Move()
        {
            switch (currentCommand)
            {
                case "links": //Naar links
                    if (X > xDest + 1)
                    {
                        Debug.WriteLine("Links");
                        X -= 1;
                    }
                    else
                        X = xDest;
                    Y = yDest;
                    Width = 25;
                    Height = 15;
                    break;

                case "rechts": //Naar rechts
                    if (X < xDest - 1)
                    {
                        Debug.WriteLine("Rechts");
                        X += 1;
                    }
                    else
                        X = xDest;
                    Y = yDest;
                    Width = 25;
                    Height = 15;
                    break;

                case "vooruit": //Naar voor
                    if (Y > yDest + 1)
                    {
                        Debug.WriteLine("Vooruit");
                        Y -= 1;
                    }
                    else
                        Y = yDest;
                    X = xDest;
                    Width = 15;
                    Height = 25;
                    break;

                case "achteruit": //Naar achter
                    if (Y < yDest - 1)
                    {
                        Debug.WriteLine("Achteruit");
                        Y += 1;
                    }
                    else
                        Y = yDest;
                    X = xDest;
                    Width = 15;
                    Height = 25;
                    break;
            }
        }
    }
}
