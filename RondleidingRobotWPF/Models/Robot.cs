using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RondleidingRobotWPF.Models
{
    public class Robot : Sprite
    {
        private Ellipse ellipse;

        public Robot(Canvas drawingCanvas) : base(drawingCanvas)
        {
            ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);

            X = Convert.ToInt32(drawingCanvas.Width) / 2;
            Y = Convert.ToInt32(drawingCanvas.Height) / 2;

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

        public void Move(Beweging beweging)
        {
                switch (Convert.ToString(beweging.Richting))
                {
                    case "links": //Naar links
                        X = X - beweging.Afstand; 
                        Width = 25;
                        Height = 15;
                        break;

                    case "rechts": //Naar rechts
                        X = X + beweging.Afstand;
                        Width = 25;
                        Height = 15;
                        break;

                    case "vooruit": //Naar voor
                        Y = Y - beweging.Afstand;
                        Width = 15;
                        Height = 25;
                        break;

                    case "achteruit": //Naar achter
                        Y = Y + beweging.Afstand;
                        Width = 15;
                        Height = 25;
                        break;
                }
            }
        }
    }
