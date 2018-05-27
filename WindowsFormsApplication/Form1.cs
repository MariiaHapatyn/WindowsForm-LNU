using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication {
    public partial class Form1 : Form {

        public List<Quad> figures = new List<Quad>();
        Quad thisFigure;
        Point pointA = Point.Empty;
        Point MouseDownLocation = Point.Empty;
        bool moving;

        public Form1() {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        
        private void newToolStripMenuItem_Click ( object sender, EventArgs e ) {
            figures.Clear();
            this.Refresh();
        }

        protected override void OnMouseDoubleClick ( MouseEventArgs e ) {
            if( pointA == Point.Empty ) {
                pointA = new Point( e.X, e.Y );
            } else {
                figures.Add( new Quad( pointA, new Point( e.X, e.Y ) ) );
                pointA = Point.Empty;
                this.Refresh();
            }
        }

        protected override void OnPaint ( PaintEventArgs e ) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach( var a in figures ) {
                a.Draw( e.Graphics );
            }
        }

        protected override void OnMouseDown ( MouseEventArgs e ) {
            if ( thisFigure != null && e.Button == MouseButtons.Left)
            {
                moving = true;
                MouseDownLocation = e.Location;

            }
            if( thisFigure != null && e.Button == MouseButtons.Right ) {
                colorDialog1.ShowDialog();
                thisFigure.FigureColor = colorDialog1.Color;
                this.Invalidate();
            }
            base.OnMouseDown( e );
        }
        protected override void OnMouseMove ( MouseEventArgs e ) {
            if( e.Button == MouseButtons.Right && e.Button == MouseButtons.Left ) {
                var a = new Point( ( e.X - MouseDownLocation.X ), ( e.Y - MouseDownLocation.Y ) );
                thisFigure.Move( a );
                MouseDownLocation = e.Location;
                this.Invalidate();
            }
            base.OnMouseMove( e );
        }

        protected override void OnMouseUp ( MouseEventArgs e ) {
            if( e.Button == MouseButtons.Right && e.Button == MouseButtons.Left ) {
                thisFigure = null;
                moving = false;
            }
            base.OnMouseUp( e );
        }

        private void openToolStripMenuItem_Click ( object sender, EventArgs e ) {
            openFileDialog1 = new OpenFileDialog {
                Filter = "(*.xml)|*.xml",
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Title = "Choose file"
            };
            if( openFileDialog1.ShowDialog() == DialogResult.OK ) {
                figures.Clear();
                figures = QuadBL.DeserializeList( openFileDialog1.FileName );
                this.Invalidate();
            }
        }
        private void sharpeToolStripMenuItem_Click ( object sender, EventArgs e ){
         ShapesMenuItem.DropDownItems.Clear();
         List<ToolStripMenuItem> ul = new List<ToolStripMenuItem>();
         foreach (var rect in figures )
         {
             ToolStripMenuItem li = new ToolStripMenuItem( rect.Name );
             li.Click += new EventHandler ( ShapesMenuDropDown_Click);
             ul.Add(li);
         }
         ShapesMenuItem.DropDownItems.AddRange(ul.ToArray());
     }

     private void ShapesMenuDropDown_Click ( object sender, EventArgs e )
     {
         ToolStripMenuItem tsim = sender as ToolStripMenuItem;
         var rect = figures.Find( p => p.Name.ToString() == tsim.Text );
            figures.Remove(rect);
            figures.Add(rect);
         this.Invalidate();
     }
    private void saveToolStripMenuItem_Click ( object sender, EventArgs e ) {
            saveFileDialog1 = new SaveFileDialog {
                RestoreDirectory = true,
                DefaultExt = "xml",
                CheckPathExists = true,
                Title = "Save your picture",
                ValidateNames = true
            };

            if( saveFileDialog1.ShowDialog() == DialogResult.OK ) {
               QuadBL.SerializeList( figures, saveFileDialog1.FileName );
            }
        }

        private void infoToolStripMenuItem_Click ( object sender, EventArgs e ) {
            MessageBox.Show(" \t Для того щоб намалювати прямокутник, натисніть ліву кнопку \n"+
              "\t миші два рази, створиться одна вершина, пізніше \n" +
              "\tаналогічно додайте іншу\n"+
                "\tЩоб змінити колір натисніть на прямокутник правою\n" +
                "\t кнопкою миші, та оберіть колір.\n" + 
                "\tЩоб зберегти малюнок нажміть кнопку 'Save'.\n" +
                "\tЩоб створити новий малюнок нажміть кнопку 'New'.\n" +
                "\tЩоб відкрити існуючий малюнок, нажміть кнопку 'Open'\n" +
                "\tі виберіть відповідний файл.\n");
        }
    }
    }

