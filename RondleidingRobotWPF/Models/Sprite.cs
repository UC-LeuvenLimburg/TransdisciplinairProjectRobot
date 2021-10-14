using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace RondleidingRobotWPF.Models
{
    public abstract class Sprite
    {
        protected Canvas canvas;
        private float x, y;
        private int width, height;

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

        public float X
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

        public float Y
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
