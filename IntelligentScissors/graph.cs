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
            double[,] energy = new double[1000, 1000];
            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);

            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    Vector2D e;
                    e = ImageOperations.CalculatePixelEnergies(x, y, ImageMatrix);
                    if (e.X == 0)
                        energy[y + 1, x] = double.MaxValue;
                    else
                        energy[y + 1, x] = 1 / e.X;

                    if (e.Y == 0)
                        energy[y, x + 1] = double.MaxValue;
                    else
                        energy[y, x + 1] = 1 / e.Y;
                }
            }
            return energy;
        }

        const int N = (1 << 22), M = (1 << 18), OO = 0x3f3f3f3f;

        List<Pair<int, int>> adj = new List<Pair<int, int>>(N);
        public static bool valid ( int x, int y)
        {
            if (x >= 0 && y >= 0 && x < 5 && y < 5) return true;
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
            PriorityQueue pq = new PriorityQueue(x,y,0.0);
             dis[x,y] = 0;
             while (!pq.Empty())
             {
                 double d = pq.Top().weight;
                 int xx = pq.Top().qx.Peek();
                 int yy = pq.Top().qy.Peek();
                 pq.Pop();
                
                if (d > dis[xx, yy]) continue;

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
