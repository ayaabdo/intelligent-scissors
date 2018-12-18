using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace IntelligentScissors
{
    public  class graph_
    {       
        public static double [,] calculateWeights(RGBPixel[,]  ImageMatrix)
        {
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);
            double[,] energy = new double[10000, 10000];

            for (int y = 0; y < width ; y++)
            {
                for (int x = 0; x < height ; x++)
                {
                    var e = ImageOperations.CalculatePixelEnergies(y, x, ImageMatrix);
                    if (e.X == 0)
                        energy[y + 1, x] = 10000000000000000;
                    else
                        energy[y + 1, x] = 1 / e.X;

                    if (e.Y == 0)
                        energy[y, x + 1] = 10000000000000000;
                    else
                        energy[y, x + 1] = 1 / e.Y;

                    //MessageBox.Show(e.X.ToString() + " " + e.Y.ToString() + "\n");
                    //MessageBox.Show(energy[y + 1, x].ToString() + " " + energy[y, x + 1].ToString());
                }
            }
            return energy;
        }

        const int N = (1 << 22), M = (1 << 18), OO = 0x3f3f3f3f;

        List<Pair<int, int>> adj = new List<Pair<int, int>>(N);
        public static bool valid ( int x, int y)
        {
            RGBPixel[,] ImageMatrix = new RGBPixel[10000, 10000];
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);

            if (x >= 0 && y >= 0 && x < height && y < width) return true;
            return false;
        }
         public static double[,] Dijkstra(double [,] graph,int x , int y)
         {
            RGBPixel[,] ImageMatrix = new RGBPixel[10000,10000];
            int width = ImageOperations.GetHeight(ImageMatrix);
            int height = ImageOperations.GetWidth(ImageMatrix);
             double [,] dis = new double[height,width];
            for (int i = 0;i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                    dis[i, j] = int.MaxValue;
            }
            PriorityQueue pq = new PriorityQueue(1,2,0.0);
             dis[x,y] = 0;
             while (!pq.Empty())
             {
                 double d = pq.Top().weight;
                 int xx = pq.Top().qx.Peek();
                 int yy = pq.Top().qy.Peek();
                 pq.Pop();
                
                if (d > dis[xx, yy]) continue;

                //get neighbours
                 if (valid(xx+1,yy)&&dis[xx+1,yy] > d + graph[xx+1,yy] )
                 {
                     dis[xx + 1, yy] = d + graph[xx + 1, yy];
                        pq.push(xx+1,yy,dis[xx+1,yy]);
                 }

                 if (valid(xx,yy+1)&&dis[xx, yy+1] > d + graph[xx, yy+1])
                 {
                     dis[xx, yy + 1] = d + graph[xx, yy + 1];
                        pq.push(xx, yy+1, dis[xx , yy+1]);
                }
                 if (valid(xx-1, yy) && dis[xx - 1, yy] > d + graph[xx - 1, yy])
                 {
                     dis[xx - 1, yy] = d + graph[xx - 1, yy];
                        pq.push(xx - 1, yy, dis[xx - 1, yy]);
                }
                 if (valid(xx, yy-1) && dis[xx, yy - 1] > d + graph[xx, yy - 1])
                 {
                     dis[xx, yy - 1] = d + graph[xx, yy - 1];
                        pq.push(xx, yy-1, dis[xx, yy-1]);
                }
             }
                return dis;
         }
      
    }
}
