using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntelligentScissors
{
    class Node
    {
        public double weight { get; set; }

        public int count;
        public Queue<int> qx, qy;
        public Node left, right;
        public bool nullflag;
        public Node()
        {
            count = 0;
            nullflag = false;
            qx = new Queue<int>();
            qy = new Queue<int>();

        }
        public Node(int xx, int yy, double w)
        {
            this.weight = w;
            count = 1;
            nullflag = false;
            qx = new Queue<int>();
            qy = new Queue<int>();
            qx.Enqueue(xx);
            qy.Enqueue(yy);
        }

    }
<<<<<<< HEAD
    class elPriorityQueuebta3khadiga
=======
    class PriorityQueue
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
    {
        private Node root;
        private int Count;

<<<<<<< HEAD
        public elPriorityQueuebta3khadiga()
=======
        public PriorityQueue()
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        {
            root = new Node();

        }
<<<<<<< HEAD
        public elPriorityQueuebta3khadiga(int xx, int yy, double w)
=======
        public PriorityQueue(int xx, int yy, double w)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        {
            root = new Node(xx, yy, w);
            Count++;
        }
        public void push(int xx, int yy, double w)
        {
            if (root == null)
            {
                root = new Node(xx, yy, w);
                Count++;
                return;
            }
            Node tempNode = new Node();
            tempNode = root;
            while (true)
            {
                if (tempNode.weight < w)
                {
                    if (tempNode.weight == w)
                    {
                        tempNode.count++;
                        tempNode.qx.Enqueue(xx); tempNode.qy.Enqueue(yy);
                        break;
                    }
                    else if (tempNode.right == null)
                    {
                        tempNode.right = new Node(xx, yy, w);
                        break;
                    }
                    else
                    {
                        tempNode = tempNode.right;
                    }
                }
                else
                {
                    if (tempNode.weight == w)
                    {
                        tempNode.count++;
                        tempNode.qx.Enqueue(xx); tempNode.qy.Enqueue(yy);
                        break;
                    }
                    else if (tempNode.left == null)
                    {
                        tempNode.left = new Node(xx, yy, w);
                        break;
                    }
                    else
                    {
                        tempNode = tempNode.left;
                    }
                }
            }
            Count++;
        }
        public bool Empty()
        {
            return Count == 0;
        }
<<<<<<< HEAD
        private void e3redly_kol_7aga_yabnlkalb(Node dod)
=======
        private void e3redly_kol_7aga(Node dod)
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        {
            if (dod == null) return;
            //if(dod.nullflag==false)
            Console.WriteLine(dod.weight + " " + dod.nullflag + " ");
<<<<<<< HEAD
            e3redly_kol_7aga_yabnlkalb(dod.left);
            e3redly_kol_7aga_yabnlkalb(dod.right);
=======
            e3redly_kol_7aga(dod.left);
            e3redly_kol_7aga(dod.right);
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685

        }
        public void show()
        {
<<<<<<< HEAD
            e3redly_kol_7aga_yabnlkalb(root);
=======
            e3redly_kol_7aga(root);
>>>>>>> 85698e9a01fdada32ad0384f8855c35581873685
        }
        public Node Top()
        {
            Node minNode = root;

            while (minNode.left != null)
            {
                minNode = minNode.left;
            }
            return minNode;
        }
        public void Pop()
        {
            //get the minimum node
            Node minNode = root;
            Node parent = root;
            while (minNode.left != null)
            {
                if (minNode.left.nullflag == false)
                {
                    parent = minNode;
                    minNode = minNode.left;
                }
                else break;

            }
            if (minNode.count > 1)
            {
                minNode.count--;
                Count--;
                minNode.qx.Dequeue();
                minNode.qy.Dequeue();

                return;
            }

            Node n = new Node();
            n = minNode;


            if ((n.left == null) && (n.right == null)) //deleting a leaf node
            {
                if (n == root)
                    root = null;
                else
                {

                    if (minNode.weight < parent.weight)
                    { parent.left = null; }
                    else
                        parent.right = null;
                }
                minNode.nullflag = true;
            }
            else if ((n.left == null) && (n.right != null))
            {
                if (n == root)
                    root = n.right;
                else
                {
                    if (minNode.weight < parent.weight)
                        parent.left = n.right;
                    else
                        parent.right = n.right;
                }

            }
            else if ((n.left != null) && (n.right == null))
            {
                if (n == root)
                    root = n.left;
                else
                {
                    if (minNode.weight < parent.weight)
                        parent.left = n.left;
                    else
                        parent.right = n.left;
                }

            }
            else
            {
                n.weight = minNode.weight;

                if (parent == n)
                    parent.right = minNode.right;
                else
                    parent.left = minNode.right;

            }
            Count--;
        }


    }
}



