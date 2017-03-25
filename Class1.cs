using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{
    class Graph_m :IGraph // graph represented by matrix of adjacency
    {
        int vertices, edges;
        string[] label;
        bool[,] adj_matrix;
        bool directed;

        public Graph_m(int v,bool if_directed)
        {
            vertices = v;
            edges = 0;
            label = new string[v];
            adj_matrix = new bool[v, v];
            if (if_directed) directed = true;
            else directed = false;

            for (int i =0;i<v;i++) //initializing default labels
            {
                label[i] = "label" + i.ToString(); //default label : label1,label2 etc...
            }

            for (int i = 0; i < v; i++)
                for (int j = 0; j < v; j++)
                    adj_matrix[i, j] = false;
        }
     
        public void insert_edge(int v1,int v2)
        {
            if (adj_matrix[v1,v2]==false)
            {
                adj_matrix[v1, v2] = true;
                if (directed == false)
                    adj_matrix[v2, v1] = true;
                edges++;
            }
        }

        public void remove_edge(int v1, int v2)
        {
            if (adj_matrix[v1, v2] == true)
            {
                adj_matrix[v1, v2] = false;
                if (directed == false)
                    adj_matrix[v2, v1] = false;
                edges--;
            }
        }

        public int find_vertex_by_label(string l) // this function enables operations on graph using string labels instead of numbers
        {
            for (int i=0; i<vertices;i++)       //in bigger graphs efficiency here could be improven by using dictionary
            {
                if (label[i] == l) return i;
            }

            return 0;
        }

        public void rename_label(int n ,string new_label)
        {
            label[n] = new_label;
        }

        public Graph_m create_random_graph (int v,int e, bool if_directed) // random graph with v vertices and e edges,can be directed
        {
            Random r = new Random();
            Graph_m random_graph = new Graph_m(v, if_directed);
            random_graph.vertices = v;
            random_graph.edges = e;
            random_graph.directed = if_directed;

            for(int i=0;i<e;i++)
            {
                int v1 = r.Next(0, v);
                int v2 = r.Next(0, v);
                if (adj_matrix[v1, v2] == false)
                {
                    adj_matrix[v1, v2] = true;
                    if (if_directed == false)
                        adj_matrix[v2, v1] = true;
                }
                else i--;                
            }
            return random_graph;
        }

        public int[] shortest_path(int v1,int v2) //Using BSF-algorithm
        {
            Queue<int> q = new Queue<int>();
            bool found = false;
            int current_v;
            bool[] visited = new bool[vertices];
            int[] path = new int[vertices];
            int[] path_length = new int[vertices];
            for (int i = 0; i < vertices; i++)
            {
                visited[i] = false;
                path_length[i] = -1;
            }

            path_length[v1] = 0;
            path[v1] = -1;
            visited[v1] = true;
            q.Enqueue(v1);

            while(q.Any())
            {
                current_v = q.Dequeue();
                if(current_v==v2)
                {
                    found = true;
                    break;
                }

                for(int i=0; i<vertices; i++)
                {
                    if(adj_matrix[current_v,i]==true)
                    {
                        if (visited[i] == true) continue;
                        else
                        {
                            path[i] = current_v;
                            path_length[i] = path_length[current_v] + 1;
                            q.Enqueue(i);
                            visited[i] = true;
                        }
                    }
                }

                
            }

            if (!found) return null; 
            else   
            {
                int[] path_representation = new int[path_length[v2]+1]; ///formating output array so it contains only values representing path in the right order
                int x = v2;                                                        
                for (int i=0;i<path_length[v2]+1;i++)
                {
                    path_representation[i] = x;
                    x = path[x];
                }


             //   Console.Write(path_length[v2]);
                return path_representation; 
            }

            


        }
        

        

    }
}
