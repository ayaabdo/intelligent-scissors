using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
<<<<<<< HEAD

namespace IntelligentScissors
{
    public class direction
    {
        public double up { get; set; }
        public double down { get; set; }
        public double left { get; set; }
        public double right { get; set; }

        public direction()
        {
            up = -1;
            down = -1;
            left = -1;
            right = -1;
        }
        public direction(double u, double d, double l, double r)
        {
            up = u;
            down = d;
            left = l;
            right = r;
        }
        public direction(direction dr)
        {
            up = dr.up;
            down = dr.down;
            left = dr.left;
            right = dr.right;
        }
    }
    public class graph_
    {



        public static direction[,] calculateWeights(RGBPixel[,] ImageMatrix)
        {
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            direction[,] weights = new direction[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    Vector2D ee;
                    ee = ImageOperations.CalculatePixelEnergies(x, y, ImageMatrix);
                    direction dr = new direction();
                    if (y < height - 1)
                    {


                        if (ee.Y == 0) dr.down = 1E+16;
                        else
                            dr.down = 1 / ee.Y;
                    }
                    if (x < width - 1)
                    {
                        if (ee.X == 0)
                        { dr.right = 1E+16; }
                        else
                            dr.right = 1 / ee.X;
                    }
                    if (y > 0)
                    {
                        ee = ImageOperations.CalculatePixelEnergies(x, y - 1, ImageMatrix);


                        if (ee.Y == 0) dr.up = 1E+16;
                        else
                            dr.up = 1 / ee.Y;
                    }
                    if (x > 0)
                    {
                        ee = ImageOperations.CalculatePixelEnergies(x - 1, y, ImageMatrix);

                        if (ee.X == 0) dr.left = 1E+16;
                        else
                            dr.left = 1 / ee.X;

                    }
                    weights[y, x] = new direction(dr);

                    //MessageBox.Show(weights[y, x].right.ToString());



                }
            }
            //Vector2D e;
            //e = ImageOperations.CalculatePixelEnergies(2, 6 ,ImageMatrix);
            //MessageBox.Show(e.X.ToString());
            //e = ImageOperations.CalculatePixelEnergies(1, 6, ImageMatrix);
            //MessageBox.Show(e.Y.ToString());

            return weights;
=======
namespace IntelligentScissors
{
    public  class graph_
    {       
        public static double [,] calculateWeights(RGBPixel[,]  ImageMatrix)
        {
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            double[,] energy = new double[1000, 1000];

            for (int y = 0; y < width ; y++)
            {
                for (int x = 0; x < height ; x++)
                {
                    var e = ImageOperations.CalculatePixelEnergies(y, x, ImageMatrix);
                    if (e.X == 0)
                        energy[y + 1, x] = 10000000000000000;
                    else
                    {
                        if (y + 1 < width) 
                        energy[y + 1, x] = 1 / e.X;
                    }
                    if (e.Y == 0)
                        energy[y, x + 1] = 10000000000000000;
                    else
                    {                   
                        if(x + 1 < height)
                        energy[y, x + 1] = 1 / e.Y;
                    }
                    //MessageBox.Show(e.X.ToString() + " " + e.Y.ToString() + "\n");
                    //MessageBox.Show(energy[y + 1, x].ToString() + " " + energy[y, x + 1].ToString());
                }
            }
            return energy;
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        }

        const int N = (1 << 22), M = (1 << 18), OO = 0x3f3f3f3f;

        List<Pair<int, int>> adj = new List<Pair<int, int>>(N);
<<<<<<< HEAD
        public static bool valid(int y, int x, int h, int w)
        {
            if (x >= 0 && y >= 0 && y < h && x < w) return true;
            return false;
        }
        public static double[,] Dijkstra(direction[,] graph, int x, int y, int destinationX, int destinationY, int[,] fromx, int[,] fromy, int h, int w)
        {
            double[,] dis = new double[h, w];
            // int[,] fromx = new int[1000, 1000];
            //int[,] fromy = new int[1000, 1000];

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                    dis[i, j] = 1E+17;
            }
            elPriorityQueuebta3khadiga pq = new elPriorityQueuebta3khadiga(x, y, 0.0);
            dis[y, x] = 0;

=======
        public static bool valid ( int x, int y)
        {
            RGBPixel[,] ImageMatrix = new RGBPixel[10000, 10000];
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);

            if (x >= 0 && y >= 0 && x < height && y < width) return true;
            return false;
        }
         public static double[,] Dijkstra(double [,] graph,int x , int y, int destinationX, int destinationY, int[,] fromx, int[,] fromy)
        {
            double[,] dis = new double[1000, 1000];
            // int[,] fromx = new int[1000, 1000];
            //int[,] fromy = new int[1000, 1000];
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                    dis[i, j] = int.MaxValue;
            }
            PriorityQueue pq = new PriorityQueue(x,y,0.0);
            dis[x, y] = 0;
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
            while (!pq.Empty())
            {
                double d = pq.Top().weight;
                int xx = pq.Top().qx.Peek();
                int yy = pq.Top().qy.Peek();
                pq.Pop();

<<<<<<< HEAD
                if (d > dis[yy, xx]) continue;
                if (valid(yy + 1, xx, h, w) && dis[yy + 1, xx] > d + graph[yy, xx].down && graph[yy, xx].down != -1)
                {
                    dis[yy + 1, xx] = d + graph[yy, xx].down;
                    fromx[yy + 1, xx] = xx;
                    fromy[yy + 1, xx] = yy;
                    //MessageBox.Show(fromx[yy+1, xx].ToString() + " down " + fromy[yy+1, xx].ToString());
                    if (xx == destinationX && yy + 1 == destinationY)
=======
                if (d > dis[xx, yy]) continue;
                if (valid(xx + 1, yy) && dis[xx + 1, yy] > d + graph[xx + 1, yy])
                {
                    dis[xx + 1, yy] = d + graph[xx + 1, yy];
                    fromx[xx + 1, yy] = xx;
                    fromy[xx + 1, yy] = yy;
                    if (xx + 1 == destinationX && yy == destinationY)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
                    {
                        pq = null;
                        return dis;
                    }
<<<<<<< HEAD
                    pq.push(xx, yy + 1, dis[yy + 1, xx]);
                }

                if (valid(yy, xx + 1, h, w) && dis[yy, xx + 1] > d + graph[yy, xx].right && graph[yy, xx].right != -1)
                {
                    dis[yy, xx + 1] = d + graph[yy, xx].right;
                    pq.push(xx + 1, yy, dis[yy, xx + 1]);
                    fromx[yy, xx + 1] = xx;
                    fromy[yy, xx + 1] = yy;
                    //MessageBox.Show(fromx[yy, xx+1].ToString() + " right " + fromy[yy, xx+1].ToString());
                    if (xx + 1 == destinationX && yy == destinationY)
=======
                    pq.push(xx + 1, yy, dis[xx + 1, yy]);
                }

                if (valid(xx, yy + 1) && dis[xx, yy + 1] > d + graph[xx, yy + 1])
                {
                    dis[xx, yy + 1] = d + graph[xx, yy + 1];
                    pq.push(xx, yy + 1, dis[xx, yy + 1]);
                    fromx[xx, yy + 1] = xx;
                    fromy[xx, yy + 1] = yy;
                    if (xx == destinationX && yy + 1 == destinationY)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
                    {
                        pq = null;
                        return dis;
                    }
                }
<<<<<<< HEAD
                if (valid(yy - 1, xx, h, w) && dis[yy - 1, xx] > d + graph[yy, xx].up && graph[yy, xx].up != -1)
                {
                    dis[yy - 1, xx] = d + graph[yy, xx].up;
                    pq.push(xx, yy - 1, dis[yy - 1, xx]);
                    fromx[yy - 1, xx] = xx;
                    fromy[yy - 1, xx] = yy;
                    //MessageBox.Show(fromx[yy-1, xx].ToString() + " up " + fromy[yy-1, xx].ToString());
                    if (xx == destinationX && yy - 1 == destinationY)
=======
                if (valid(xx - 1, yy) && dis[xx - 1, yy] > d + graph[xx - 1, yy])
                {
                    dis[xx - 1, yy] = d + graph[xx - 1, yy];
                    pq.push(xx - 1, yy, dis[xx - 1, yy]);
                    fromx[xx - 1, yy] = xx;
                    fromy[xx - 1, yy] = yy;
                    if (xx - 1 == destinationX && yy == destinationY)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
                    {
                        pq = null;
                        return dis;
                    }
                }
<<<<<<< HEAD
                if (valid(yy, xx - 1, h, w) && dis[yy, xx - 1] > d + graph[yy, xx].left && graph[yy, xx].left != -1)
                {
                    dis[yy, xx - 1] = d + graph[yy, xx].left;
                    pq.push(xx - 1, yy, dis[yy, xx - 1]);
                    fromx[yy, xx - 1] = xx;
                    fromy[yy, xx - 1] = yy;
                    //MessageBox.Show(fromx[yy, xx-1].ToString() + " left " + fromy[yy, xx-1].ToString());
                    if (xx - 1 == destinationX && yy == destinationY)
=======
                if (valid(xx, yy - 1) && dis[xx, yy - 1] > d + graph[xx, yy - 1])
                {
                    dis[xx, yy - 1] = d + graph[xx, yy - 1];
                    pq.push(xx, yy - 1, dis[xx, yy - 1]);
                    fromx[xx, yy - 1] = xx;
                    fromy[xx, yy - 1] = yy;
                    if (xx == destinationX && yy - 1 == destinationY)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
                    {
                        pq = null;
                        return dis;
                    }
                }
            }
<<<<<<< HEAD
            /**using (StreamWriter writer = new StreamWriter("output1.txt"))
            {
               //MessageBox.Show("L");
                for (int i = 0; i < 100; ++i)
                {
                    for (int j = 0; j < 100; ++j)
                    {
                        writer.WriteLine("x y "+i+" "+j+" "+fromx[i, j]+" "+ fromy[i, j]);
                        
                     
                    }
                }
               
            }*/
            return dis;
        }

        public static void printpath(int x, int y, int srcx, int srcy, int[,] fromx, int[,] fromy, double[,] dis, RGBPixel[,] imageMatrix, List<Point> lop)
        {
            int xx = x, yy = y;

            while ((xx != srcx || yy != srcy) && (xx != 0 && yy != 0))
            {
                //MessageBox.Show(fromx[yy,xx].ToString()+" "+ fromy[yy, xx].ToString());
                lop.Add(new Point(xx, yy));
                if (lop.Count == 50)
                {
                    savepath(lop);
                }

                xx = fromx[yy, xx];
                yy = fromy[yy, xx];
                ///imageMatrix[yy, xx].blue = 0; imageMatrix[yy, xx].red = 255; imageMatrix[yy, xx].green = 0;


            }

        }
        public static void savepath(List<Point> lop)
        {
            using (StreamWriter writer = new StreamWriter("path.txt"))
            {
                for (int i = 0; i < lop.Count; i++)
                {
                    writer.WriteLine("x " + lop[i].X + "    y " + lop[i].Y);
                }
            }
        }
        public static void color(RGBPixel[,] imageMatrix, List<Point> lop)
        {
            for (int i = 0; i < lop.Count; i++)
            {
                imageMatrix[lop[i].Y, lop[i].X].blue = 255; imageMatrix[lop[i].Y, lop[i].X].red = 255; imageMatrix[lop[i].Y, lop[i].X].green = 250;
            }

=======
            return dis;
        }
        public static void printpath(int x, int y, int srcx, int srcy, int[,] fromx, int[,] fromy, double[,] dis)
        {
            if (x == srcx && y == srcy) return;
            MessageBox.Show(x + " " + y);
            printpath(fromx[x, y], fromy[x, y], srcx, srcy, fromx, fromy, dis);
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        }

    }
}
