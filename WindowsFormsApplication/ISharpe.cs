using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication {
    interface ISharpe {
        GraphicsPath GetPath ();
        void Draw ( Graphics g );
        void Move ( Point a );
    }
}
