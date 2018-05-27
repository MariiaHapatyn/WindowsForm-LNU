using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication {
    [Serializable]
    public class Quad : ISharpe {
        public Color FigureColor { get; set; }
        public Point UppLeft { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string  Name { get; }

        public Quad () {
            Name = DateTime.Now.Ticks.ToString();
            FigureColor = Color.Red;
        }

        public Quad ( Point a, Point b ) : this() {
            Width = Math.Abs( a.X - b.X );
            Height = Math.Abs( a.Y - b.Y );
        }

        public Quad ( Point a, Point b, Color color ) : this( a, b ) {
            FigureColor = color;
        }

        public GraphicsPath GetPath () {
            GraphicsPath path = new GraphicsPath();
            Point p = UppLeft;
            path.AddRectangle( new RectangleF( UppLeft.X, UppLeft.Y, Width, Height ) );
            return path;
        }

        public void Draw ( Graphics g ) {
            using( var path = GetPath() )
            using( var brush = new SolidBrush( FigureColor ) )
                g.FillPath( brush, path );
        }

        public void Move ( Point a ) {
            UppLeft = new Point( UppLeft.X + a.X, UppLeft.Y + a.Y );
        }
    }
}
