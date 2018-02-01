using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DELL_ISPtool
{
    class DrawProgressBar :PictureBox
    {
        public DrawProgressBar()
        {
            _barvalue = 0;
            _maximum = 100;
            _minimum = 0;
            _dellcolor = Color.FromArgb(31, 157, 250);
        }
        private int _barvalue;
        public int Barvalue
        {
            get
            {
                return _barvalue;
            }
            set
            {
                _barvalue = value;
                Refresh();
            }
        }
        private int _rect_width;
        public int Rect_width
        {
            get { return _rect_width; }
            set { _rect_width = value; }
        }
        private int _rect_height;
        public int Rect_height
        {
            get { return _rect_height; }
            set { _rect_height = value; }
        }
        private int _rect_x;
        public int Rect_x
        {
            get { return _rect_x; }
            set { _rect_x = value; }
        }
        private int _rect_y;
        public int Rect_y
        {
            get { return _rect_y; }
            set { _rect_y = value; }
        }
        private Color _dellcolor;
        public Color Dellcolor
        {
            get { return _dellcolor; }
            set { _dellcolor = value; }
        }
        private int _maximum;
        public int Maximum
        {
            get { return _maximum; }
            set { _maximum = value; }
        }
        private int _minimum;
        public int Minimum
        {
            get { return _minimum; }
            set { _minimum = value;}
        }

        public void PaintBar(object sender,PaintEventArgs e)
        {
                int picBoxWidth = _rect_width; 
                int picBoxHeight = _rect_height;
                Graphics g = e.Graphics; //**請注意這一行**
                Rectangle rec = e.ClipRectangle;

                Pen pen = new Pen(Color.FromArgb(128,Color.Black),2);
                g.DrawRectangle(pen, 0, 0, picBoxWidth, picBoxHeight);
                g.FillRectangle(Brushes.LightGray, rec);

                rec.Width = (int)(rec.Width * ((double)_barvalue / _maximum));
                rec.Height = rec.Height;
                SolidBrush myBrushes = new SolidBrush(_dellcolor);
                e.Graphics.FillRectangle(myBrushes, 0, 0, rec.Width, rec.Height);

        }
    }
}
