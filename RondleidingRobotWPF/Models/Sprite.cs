using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace RondleidingRobotWPF.Models
{
    public abstract class Sprite
    {
        protected Canvas canvas;
        private double x, y;
        private int width, height;
        private double angle;

        public Sprite(Canvas drawingCanvas)
        {
            canvas = drawingCanvas;
        }

        public Sprite(Canvas drawingCanvas, int x, int y)
        {
            canvas = drawingCanvas;
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                UpdateElement();
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                UpdateElement();
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                UpdateElement();
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                UpdateElement();
            }
        }

        public double Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                UpdateElement();
            }
        }
        protected abstract void UpdateElement();
    }
}
