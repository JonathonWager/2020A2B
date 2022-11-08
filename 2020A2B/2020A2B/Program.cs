using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020A2B
{
    internal class Program
    {
        public class PriorityQueue
        {
            Node[] pq;        // Linear array of items (Generic)
            private int capacity;  // Maximum number of items in a priority queue
            private int count;
            public PriorityQueue()
            {
                pq = new Node[5];
                capacity = 4;
                BuildHeap();
                MakeEmpty();
            }
            public void MakeEmpty()
            {
                count = 0;
            }
            public bool Empty()
            {
                return count == 0;
            }
            public int Size()
            {
                
                return count;
            }
            private void DoubleCapacity()
            {
                
                Node[] oldPq = pq;
                capacity = capacity * 2;
                pq = new Node[capacity+1];
                //Console.WriteLine(capacity);
                //Console.ReadLine();
                for (int i = 0; i < count + 1 ; i++)
                {
                    pq[i] = oldPq[i];
                }
            }
            private void PercolateUp(int i)
            {
                int child = i, parent;

                // As long as child is not the root (i.e. has a parent)
                while (child > 1)
                {
                    parent = child / 2;
                   // Console.WriteLine(child + " " + parent);
                   // Console.ReadLine();
                    if (pq[child].Frequency < pq[parent].Frequency)
                    // If child has a higher priority than parent
                    {
                        // Swap parent and child
                        Node item = pq[child];
                        pq[child] = pq[parent];
                        pq[parent] = item;
                        child = parent;  // Move up child index to parent index
                    }
                    else
                        // Item is in its proper position
                        return;
                }
            }
            private void PercolateDown(int i)
            {
                int parent = i, child;
                // while parent has at least one child
                while (2 * parent <= count)
                {
                    // Select the child with the highest priority
                    child = 2 * parent;    // Left child index
                    if (child < count)  // Right child also exists
                        if (pq[child + 1].Frequency < pq[child].Frequency)
                            // Right child has a higher priority than left child
                            child++;

                    // If child has a higher priority than parent
                    if (pq[child].Frequency < pq[parent].Frequency)
                    {
                        // Swap parent and child
                        Node item = pq[child];
                        pq[child] = pq[parent];
                        pq[parent] = item;
                        parent = child;  // Move down parent index to child index
                    }
                    else
                        // Item is in its proper place
                        return;
                }
            }
            private void TestSort()
            {
                Node store;
                int sortCount = Size();
                int compareCount = 1;
               
                //Console.ReadLine();
                while (sortCount > 1)
                {
                    if (compareCount == pq.Length - 1 || compareCount >= sortCount)
                    {
                        sortCount--;
                        compareCount = 1;
                    }
                    //Console.WriteLine(sortCount + " " + compareCount);
                   // Console.ReadLine();
                    if (pq[sortCount].Frequency < pq[compareCount].Frequency)
                    {
                        store = pq[sortCount];
                        pq[sortCount] = pq[compareCount];
                        pq[compareCount] = store;
                    }
                    compareCount++;
                }
            }
            public void Insert(Node item)
            {
                if (count == capacity)
                {
                    DoubleCapacity();
                }
                pq[++count] = item;      // Place item at the next available position
                //Console.WriteLine(count + " cap " + capacity + " pq " + pq.Length + " item " + item.Frequency);
                //Console.ReadLine();
                PercolateUp(count);
            }
            public Node Remove()
            {
                if (Empty())
                    throw new InvalidOperationException("Priority queue is empty");
                else
                {
                    // Remove item with highest priority (root) and
                    // replace it with the last item
                    Node rNode = pq[1];
                    //Console.WriteLine("size " + count);
                    pq[1] = pq[count--];
                    

                    // Percolate down the new root item
                    //PercolateDown(1);
                    TestSort();
                    return rNode;
                }
            }
            public Node Front()
            {
                if (Empty())
                    throw new InvalidOperationException("Priority queue is empty");
                else
                    return pq[1];  // Return the root item (highest priority)
            }
            private void BuildHeap()
            {
                int i;

                // Percolate down from the last parent to the root (first parent)
                for (i = count / 2; i >= 1; i--)
                {
                    PercolateDown(i);
                }
            }
            public int Count()
            {
                return count;
            }
            public void PrintOrder(Node root)
            {
                PrintOrder(root, 0);
            }

            // Private PrintOrder
            // Recursively implements the public Print

            private void PrintOrder(Node root, int indent)
            {
                if (root != null)
                {
                    PrintOrder(root.Right, indent + 3);
                    if(root.Character != (char)0)
                    {
                        Console.WriteLine(new String(' ', indent) + root.Frequency + root.Character);
                    }
                    else
                    {
                        Console.WriteLine(new String(' ', indent) + root.Frequency);
                    }
                   
                    PrintOrder(root.Left, indent + 3);
                }
            }
        }
        public class Node : IComparable
        {
            public char Character { get; set; }
            public int Frequency { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(char character, int frequency, Node left, Node right) 
            {
                Character = character;
                Frequency = frequency;
                Left = left;
                Right = right;
            }
            // 3 marks
            public int CompareTo(Object obj) 
            {
                return 1;
            }
        }
        class Huffman
        {
            private Node HT; // Huffman tree to create codes and decode text
            private Dictionary<char, string> D; // Dictionary to encode text
                                                // Constructor
            public Huffman(string S) 
            {
                Build(AnalyzeText(S));
            }
            // 8 marks
            // Return the frequency of each character in the given text (invoked by Huffman)
            public int[] AnalyzeText(string S) 
            {
                int[] freqStorage = new int[53];
                for (int i = 0; i < S.Length; i++)
                {
                    if ((int)S[i] == 32)
                    {
                        freqStorage[52]++;
                    }
                    else
                    {
                        if ((int)S[i] >= 97)
                        {
                            freqStorage[(int)S[i] - 71]++;
                        }
                        else
                        {
                            freqStorage[(int)S[i] - 65]++;
                        }
                    }
                }
                return freqStorage;
            }
            // 16 marks
            // Build a Huffman tree based on the character frequencies greater than 0 (invoked by Huffman)
            private void Build(int[] F)
            {
                PriorityQueue PQ = new PriorityQueue();
             
                for (int i = 0; i < F.Length; i++)
                {
                    //Console.WriteLine(PQ.Count());
                    if (F[i]!= 0)
                    {
                        if (i == 52)
                        {
                            //Console.WriteLine(F[52]);
                            PQ.Insert(new Node((char)(32), F[i], null, null));
                        }
                        else
                        {
                            if (i < 26)
                            {
                                //Console.WriteLine("trig");
                                PQ.Insert(new Node((char)(i + 65), F[i], null, null));
                               // Console.WriteLine((char)(i + 65) + " " + F[i]);
                            }
                            else
                            {
                                PQ.Insert(new Node((char)(i + 71), F[i], null, null));
                            }
                        }
                    }
                    
                }
                //Console.WriteLine(PQ.Size());
                //Console.WriteLine(PQ.Remove().Frequency);
                Node store;
                int test = PQ.Size();
                Node test1 ;
                Node test2;
                while (PQ.Count() > 1)
                {
                    
                    test1 = PQ.Remove();
                    test2 = PQ.Remove();
                    //Console.WriteLine(test1.Frequency +" + " + test2.Frequency);
                    PQ.Insert(new Node((char)0, test1.Frequency + test2.Frequency, test1, test2));
                }
                //Console.WriteLine(PQ.Front().Left.Frequency);
                //PQ.Remove();
                PQ.PrintOrder(PQ.Front());

            }
            // 12 marks
            // Create the code of 0s and 1s for each character by traversing the Huffman tree (invoked by Huffman)
            // Store the codes in Dictionary D using the char as the key
            private void CreateCodes() 
            { 
            
            }
            // 8 marks
            // Encode the given text and return a string of 0s and 1s
            public string Encode(string S) 
            {
                return "";
            }
            // 8 marks
            // Decode the given string of 0s and 1s and return the original text
            public string Decode(string S) 
            {
                return "";
            }
        }
        static void Main(string[] args)
        {
            Huffman test = new Huffman("for each character by traversing the Huffman tree and return the original text  Decode the given string of Return the frequency of each character in the given text");
            
            //Console.WriteLine((int)'A');
            Console.ReadLine();
        }
    }
}
