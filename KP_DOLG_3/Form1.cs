namespace KP_DOLG_3
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private PointF basePoint; // Одна из точек основания
        private float baseWidth;  // Ширина основания
        private float height;     // Высота
        private float yCoordinate; 
        private float yCoordinate2; 

        private TextBox txtbasePointX, txtbasePointY, txtBaseWidth, txtHeight, txtLeftRectY, txtRightRectY;
        private System.Windows.Forms.Label labelbasePointX, labelbasePointY, labelBaseWidth, labelHeight, labelLeftRectY, labelRightRectY;
        private Button btnDraw;
        private Panel drawingPanel;

        public Form1()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);

            // Инициализация данных
            basePoint = new PointF(200, 300); // Пример координаты точки основания
            baseWidth = 200;                  // Пример ширины основания
            height = 150;                     // Пример высоты
            yCoordinate = 200;
            yCoordinate2 = 300;

            labelbasePointX = new Label { Left = 10, Top = 10, Text = "X" };
            txtbasePointX = new TextBox { Left = 30, Top = 10, Width = 25, Text = "200" };

            labelbasePointY = new Label { Left = 110, Top = 10, Text = "Y" };
            txtbasePointY = new TextBox { Left = 80, Top = 10, Width = 25, Text = "300" };

            labelBaseWidth = new Label { Left = 110, Top = 40, Text = "Ширина основания" };
            txtBaseWidth = new TextBox { Left = 10, Top = 40, Width = 100, Text = "200" };

            labelHeight = new Label { Left = 110, Top = 70, Text = "Высота треугольника" };
            txtHeight = new TextBox { Left = 10, Top = 70, Width = 100 , Text = "200"};

            labelLeftRectY = new Label { Left = 40, Top = 130, Text = "Y левого вписанного треугольника", Width = 200 };
            txtLeftRectY = new TextBox { Left = 10, Top = 130, Width = 25, Text = "200" };

            labelRightRectY = new Label { Left = 40, Top = 160, Text = "Y правого вписанного треугольника", Width = 200 };
            txtRightRectY = new TextBox { Left = 10, Top = 160, Width = 25, Text = "150" };

            btnDraw = new Button { Text = "Draw", Left = 10, Top = 200, Width = 100 };

            Controls.Add(txtbasePointX);
            Controls.Add(txtbasePointY);
            Controls.Add(txtBaseWidth);
            Controls.Add(txtHeight);
            Controls.Add(txtLeftRectY);
            Controls.Add(txtRightRectY);
            Controls.Add(btnDraw);

            Controls.Add(labelbasePointX);
            Controls.Add(labelbasePointY);
            Controls.Add(labelBaseWidth);
            Controls.Add(labelHeight);
            Controls.Add(labelLeftRectY);
            Controls.Add(labelRightRectY);
            btnDraw.Click += DrawTriangles;
        }

        private void DrawTriangles(object sender, EventArgs e)
        {
            int x = int.Parse(txtbasePointX.Text);
            int y = int.Parse(txtbasePointY.Text);
            basePoint= new Point(x, y);
            baseWidth = int.Parse(txtBaseWidth.Text);
            height = int.Parse(txtHeight.Text);
            yCoordinate = int.Parse(txtLeftRectY.Text);
            yCoordinate2 = int.Parse(txtRightRectY.Text);
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            PointF apexPoint = new PointF(basePoint.X + baseWidth / 2, basePoint.Y - height);
            g.Clear(Color.White);
            DrawIsoscelesTriangle(g);
            DrawGradientRightTriangle(g, apexPoint);
            DrawRightTriangle(g, apexPoint);
           
        }

        private void DrawIsoscelesTriangle(Graphics g)
        {
            PointF basePoint2 = new PointF(basePoint.X + baseWidth, basePoint.Y);
            PointF apexPoint = new PointF(basePoint.X + baseWidth / 2, basePoint.Y - height);

            PointF[] points = { basePoint, basePoint2, apexPoint };
            g.DrawPolygon(Pens.Black, points);
        }

        private void DrawRightTriangle(Graphics g, PointF apexPoint)
        {
            float rectangleWidth = (yCoordinate - apexPoint.Y) / height * baseWidth;
            PointF[] rectanglePoints =
                {
                new PointF(apexPoint.X - rectangleWidth / 2, yCoordinate),
                new PointF(basePoint.X + baseWidth, basePoint.Y),
                new PointF(apexPoint.X - rectangleWidth / 2, basePoint.Y)
                };
            g.DrawPolygon(Pens.Red, rectanglePoints);
        }

        private void DrawGradientRightTriangle(Graphics g, PointF apexPoint)
        {
            float rectangleWidth2 = (yCoordinate2 - apexPoint.Y) / height * baseWidth;
            PointF[] rectanglePoints2 =
                {
                new PointF(apexPoint.X + rectangleWidth2 / 2, yCoordinate2),
                new PointF(apexPoint.X + rectangleWidth2 / 2, basePoint.Y),
                new PointF(basePoint.X, basePoint.Y)
                };
            PathGradientBrush brush = new PathGradientBrush(rectanglePoints2);
            brush.CenterColor = Color.Green;
            brush.SurroundColors = new Color[] { Color.Red, Color.Blue, Color.Black };

            g.FillPolygon(brush, rectanglePoints2);
            g.DrawPolygon(Pens.Black, rectanglePoints2);
        }


    }
}
