using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace IntelligentScissors
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public RGBPixel[,] ImageMatrix;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }

            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            double[,] read = new double[10, 10];
            int w = ImageOperations.GetWidth(ImageMatrix);
            int h = ImageOperations.GetHeight(ImageMatrix);
            read = graph_.calculateWeights(ImageMatrix);
            saving_constructed_graph(read, h, w);
            saving_distance();
        }

        public void saving_distance()
        {
            int w = ImageOperations.GetWidth(ImageMatrix);
            int h = ImageOperations.GetHeight(ImageMatrix);
            double[,] g = new double[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    g[i, j] = i + j + 2.0;

            }

            double[,] dist = graph_.Dijkstra(g, 0, 0);
            using (StreamWriter writer = new StreamWriter("output1.txt"))
            {
                writer.Write("this is the distance");
                for (int i = 0; i < h; ++i)
                {
                    for (int j = 0; j < w; ++j)
                    {
                        writer.Write(g[i, j]);
                        writer.Write(" ");

                    }
                    writer.WriteLine(" ");
                }
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        writer.Write(dist[i, j]);
                        writer.Write(" ");

                    }
                    writer.WriteLine(" ");
                }
            }
        }

        double[,] energy = new double[1000, 1000];
        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            energy = graph_.calculateWeights(ImageMatrix);

            MouseEventArgs me = (MouseEventArgs)e;
            graph_.Dijkstra(energy, me.X, me.Y);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static string saving_constructed_graph_help(int nodeindex, int right, int left, int up, int down, double r, double l, double u, double d)//help saving_constructed_graph
        {
            if ((up == -1) && (down == -1) && (right == -1) && (left == -1))
                return "";
            string a = "", b = "", c = "", dd = "", s = "The  index node" + nodeindex.ToString() + "\n" + "Edges" + '\n';
            if (right != -1)
            {
                a = "edge from   " + nodeindex.ToString() + "  To  " + right.ToString() + "  With Weights  " + r.ToString() + "\n";
            }//edge from   2  To  3  With Weights  1E+16
            if (down != -1)
            {
                b = "edge from   " + nodeindex.ToString() + "  To  " + down.ToString() + "  With Weights  " + d.ToString() + "\n";
            }
            if (up != -1)
            {
                c = "edge from   " + nodeindex.ToString() + "  To  " + up.ToString() + "  With Weights  " + u.ToString() + "\n";
            }
            if (left != -1)
            {
                dd = "edge from   " + nodeindex.ToString() + "  To  " + left.ToString() + "  With Weights  " + l.ToString() + "\n";
            }

            return s + "\n" + a + "\n" + b + "\n" + c + "\n" + dd + "\n";

        }
        public static void saving_constructed_graph(double[,] graphh, int N, int M)
        {
            int counter = 0, box;
            string s = "";
            using (StreamWriter writer = new StreamWriter("output2.txt"))
            {//double[,] energy = new double[1000, 1000];
                writer.WriteLine("The constructed graph");
                writer.WriteLine("\n");
                //display index 0
                int right, left, up, down;
                right = -1; left = -1; up = -1; down = -1;
                double r = 0, l = 0, u = 0, d = 0;
                //edge from   0  To  11  With Weights  1E+16
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < M; ++j)
                    {

                        if (i != N - 1) //down
                        {
                            down = counter + M;
                            d = graphh[i + 1, j];
                        }
                        if (j != M - 1)//right
                        {
                            //MessageBox.Show(i.ToString() + " " + j.ToString());
                            right = counter + 1;
                            r = graphh[i, j + 1];
                        }

                        if (j != 0)//left
                        {
                            left = counter - 1;
                            l = graphh[i, j - 1];
                        }
                        if (i != 0)//up
                        {
                            up = counter - M;
                            u = graphh[i - 1, j];
                        }
                        // MessageBox.Show(counter.ToString() + " " + down.ToString());
                        s = saving_constructed_graph_help(counter, right, left, up, down, r, l, u, d);
                        counter++;
                        right = -1; left = -1; up = -1; down = -1;
                        if (s != null)
                            writer.WriteLine(s);
                    }


                }

            }
        }
        // The "size" of an object for mouse over purposes.
        private const int object_radius = 3;

        // We're over an object if the distance squared
        // between the mouse and the object is less than this.
        private const int over_dist_squared = object_radius * object_radius;


        private List<Point> Pt1 = new List<Point>();
        private List<Point> Pt2 = new List<Point>();

        // Points for the new line.
        private bool isDrawing = false;
        private Point newPt1, newPt2;

        // The mouse is up. See whether we're over an end point or segment.
        private void pictureBox1_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Cross;

            // See what we're over.
            Point hit_point;
            int segment_number;

            if (MouseIsOverEndpoint(e.Location, out segment_number, out hit_point))
                new_cursor = Cursors.Arrow;
            else if (MouseIsOverSegment(e.Location, out segment_number))
                new_cursor = Cursors.Hand;

            // Set the new cursor.
            if (pictureBox1.Cursor != new_cursor)
                pictureBox1.Cursor = new_cursor;
        }

        // See what we're over and start doing whatever is appropriate.
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // See what we're over.
            Point hit_point;
            int segment_number;

            if (MouseIsOverEndpoint(e.Location, out segment_number, out hit_point))
            {
                // Start moving this end point.
                pictureBox1.MouseMove -= pictureBox1_MouseMove_NotDown;
                pictureBox1.MouseMove += pictureBox1_MouseMove_MovingEndPoint;
                pictureBox1.MouseUp += pictureBox1_MouseUp_MovingEndPoint;

                // Remember the segment number.
                MovingSegment = segment_number;

                // See if we're moving the start end point.
                MovingStartEndPoint = (Pt1[segment_number].Equals(hit_point));

                // Remember the offset from the mouse to the point.
                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if (MouseIsOverSegment(e.Location, out segment_number))
            {
                // Start moving this segment.
                pictureBox1.MouseMove -= pictureBox1_MouseMove_NotDown;
                pictureBox1.MouseMove += pictureBox1_MouseMove_MovingSegment;
                pictureBox1.MouseUp += pictureBox1_MouseUp_MovingSegment;

                // Remember the segment number.
                MovingSegment = segment_number;

                // Remember the offset from the mouse to the segment's first point.
                OffsetX = Pt1[segment_number].X - e.X;
                OffsetY = Pt1[segment_number].Y - e.Y;
            }
            else
            {
                // Start drawing a new segment.
                pictureBox1.MouseMove -= pictureBox1_MouseMove_NotDown;
                pictureBox1.MouseMove += pictureBox1_MouseMove_Drawing;
                pictureBox1.MouseUp += pictureBox1_MouseUp_Drawing;

                isDrawing = true;
                newPt1 = new Point(e.X, e.Y);
                newPt2 = new Point(e.X, e.Y);
            }
        }

        #region "Drawing"

        // We're drawing a new segment.
        private void pictureBox1_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            // Save the new point.
            newPt2 = new Point(e.X, e.Y);

            // Redraw.
            pictureBox1.Invalidate();
        }

        // Stop drawing.
        private void pictureBox1_MouseUp_Drawing(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            // Reset the event handlers.
            pictureBox1.MouseMove -= pictureBox1_MouseMove_Drawing;
            pictureBox1.MouseMove += pictureBox1_MouseMove_NotDown;
            pictureBox1.MouseUp -= pictureBox1_MouseUp_Drawing;

            // Create the new segment.
            Pt1.Add(newPt1);
            Pt2.Add(newPt2);

            // Redraw.
            pictureBox1.Invalidate();
        }

        #endregion // Drawing

        #region "Moving End Point"

        // The segment we're moving or the segment whose end point we're moving.
        private int MovingSegment = -1;

        // The end point we're moving.
        private bool MovingStartEndPoint = false;

        // The offset from the mouse to the object being moved.
        private int OffsetX, OffsetY;

        // We're moving an end point.
        private void pictureBox1_MouseMove_MovingEndPoint(object sender, MouseEventArgs e)
        {
            // Move the point to its new location.
            if (MovingStartEndPoint)
                Pt1[MovingSegment] =
                    new Point(e.X + OffsetX, e.Y + OffsetY);
            else
                Pt2[MovingSegment] =
                    new Point(e.X + OffsetX, e.Y + OffsetY);

            // Redraw.
            pictureBox1.Invalidate();
        }

        // Stop moving the end point.
        private void pictureBox1_MouseUp_MovingEndPoint(object sender, MouseEventArgs e)
        {
            // Reset the event handlers.
            pictureBox1.MouseMove += pictureBox1_MouseMove_NotDown;
            pictureBox1.MouseMove -= pictureBox1_MouseMove_MovingEndPoint;
            pictureBox1.MouseUp -= pictureBox1_MouseUp_MovingEndPoint;

            // Redraw.
            pictureBox1.Invalidate();
        }

        #endregion // Moving End Point

        #region "Moving Segment"

        // We're moving a segment.
        private void pictureBox1_MouseMove_MovingSegment(object sender, MouseEventArgs e)
        {
            // See how far the first point will move.
            int new_x1 = e.X + OffsetX;
            int new_y1 = e.Y + OffsetY;

            int dx = new_x1 - Pt1[MovingSegment].X;
            int dy = new_y1 - Pt1[MovingSegment].Y;

            if (dx == 0 && dy == 0) return;

            // Move the segment to its new location.
            Pt1[MovingSegment] = new Point(new_x1, new_y1);
            Pt2[MovingSegment] = new Point(
                Pt2[MovingSegment].X + dx,
                Pt2[MovingSegment].Y + dy);

            // Redraw.
            pictureBox1.Invalidate();
        }

        // Stop moving the segment.
        private void pictureBox1_MouseUp_MovingSegment(object sender, MouseEventArgs e)
        {
            // Reset the event handlers.
            pictureBox1.MouseMove += pictureBox1_MouseMove_NotDown;
            pictureBox1.MouseMove -= pictureBox1_MouseMove_MovingSegment;
            pictureBox1.MouseUp -= pictureBox1_MouseUp_MovingSegment;

            // Redraw.
            pictureBox1.Invalidate();
        }

        #endregion // Moving End Point

        // See if the mouse is over an end point.
        private bool MouseIsOverEndpoint(Point mouse_pt, out int segment_number, out Point hit_pt)
        {
            for (int i = 0; i < Pt1.Count; i++)
            {
                // Check the starting point.
                if (FindDistanceToPointSquared(mouse_pt, Pt1[i]) < over_dist_squared)
                {
                    // We're over this point.
                    segment_number = i;
                    hit_pt = Pt1[i];
                    return true;
                }

                // Check the end point.
                if (FindDistanceToPointSquared(mouse_pt, Pt2[i]) < over_dist_squared)
                {
                    // We're over this point.
                    segment_number = i;
                    hit_pt = Pt2[i];
                    return true;
                }
            }

            segment_number = -1;
            hit_pt = new Point(-1, -1);
            return false;
        }

        // See if the mouse is over a line segment.
        private bool MouseIsOverSegment(Point mouse_pt, out int segment_number)
        {
            for (int i = 0; i < Pt1.Count; i++)
            {
                // See if we're over the segment.
                PointF closest;
                if (FindDistanceToSegmentSquared(
                    mouse_pt, Pt1[i], Pt2[i], out closest)
                        < over_dist_squared)
                {
                    // We're over this segment.
                    segment_number = i;
                    return true;
                }
            }

            segment_number = -1;
            return false;
        }

        // Calculate the distance squared between two points.
        private int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        // Calculate the distance squared between
        // point pt and the segment p1 --> p2.
        private double FindDistanceToSegmentSquared(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return dx * dx + dy * dy;
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the segments.
            for (int i = 0; i < Pt1.Count; i++)
            {
                // Draw the segment.
                e.Graphics.DrawLine(Pens.Blue, Pt1[i], Pt2[i]);
            }

            // Draw the end points.
            foreach (Point pt in Pt1)
            {
                Rectangle rect = new Rectangle(
                    pt.X - object_radius, pt.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);
            }
            foreach (Point pt in Pt2)
            {
                Rectangle rect = new Rectangle(
                    pt.X - object_radius, pt.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);
            }

            // If there's a new segment under constructions, draw it.
            if (isDrawing)
            {
                e.Graphics.DrawLine(Pens.Red, newPt1, newPt2);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // MouseEventArgs mouse = (MouseEventArgs)e;
            if (pictureBox1.Image != null)
            {
                //List<int> dis = graph_.Dijkstra(ImageMatrix, 1, 2);

            }
        }

    }
}