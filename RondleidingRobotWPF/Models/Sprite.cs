using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace RondleidingRobotWPF.Models
{
    public abstract class Sprite
    {
        protected Canvas canvas;
        private int x, y, width, height;

        public Sprite(Canvas drawingCanvas)
        {
            canvas = drawingCanvas;
        }

        public int X
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

        public int Y
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

        protected abstract void UpdateElement();
    }
}
