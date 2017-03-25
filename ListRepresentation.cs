using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{

    class Graph_l: IGraph // graph represented by lists of adjacency
    {
        int vertices, edges;
        string[] label;
        List<int>[] adj_list;
        bool directed;

        public Graph_l(int v, bool if_directed)
        {
            vertices = v;
            edges = 0;
            label = new string[v];
            adj_list = new List<int>[v];
            if (if_directed) directed = true;
            else directed = false;

            for (int i = 0; i < v; i++) //initializing default labels
            {
                label[i] = "label" + i.ToString(); //default label : label1,label2 etc...
            }

            for(int i=0;i<v;i++)
            {
                adj_list[i] = new List<int>();
            }

        }

        public void insert_edge(int v1, int v2)
        {
            if (adj_list[v1].Contains(v2) == false )
            {
                adj_list[v1].Add(v2);
                if (directed == false)
                    adj_list[v2].Add(v1);
                edges++;
            }
        }

        public void remove_edge(int v1, int v2)
        {
            if (adj_list[v1].Contains(v2) == true)
            {
                adj_list[v1].Remove(v2);
                if (directed == false)
                    adj_list[v2].Remove(v1);
                edges++;
            }
        }

        public int find_vertex_by_label(string l) // this function enables operations on graph using string labels instead of numbers
        {
            for (int i = 0; i < vertices; i++)       //in bigger graphs efficiency here could be improven by using dictionary
            {
                if (label[i] == l) return i;
            }

            return 0;
        }

        public void rename_label(int n, string new_label)
        {
            label[n] = new_label;
        }


        public Graph_l create_random_graph(int v, int e, bool if_directed)
        {
            Random r = new Random();
            Graph_l random_graph = new Graph_l(v, if_directed);
            random_graph.vertices = v;
            random_graph.edges = e;
            random_graph.directed = if_directed;

            for (int i = 0; i < e; i++)
            {
                int v1 = r.Next(0, v);
                int v2 = r.Next(0, v);
                if (adj_list[v1].Contains(v2) == false)
                {
                    adj_list[v1].Add(v2);
                    if (if_directed == false)
                        adj_list[v2].Add(v1);
                }
                else i--;
            }
            return random_graph;
        }

        public int[] shortest_path(int v1, int v2) //Using BSF-algorithm
        {
            Queue<int> q = new Queue<int>();
            bool found = false;
            int current_v, x;
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

            while (q.Any())
            {
                current_v = q.Dequeue();
                if (current_v == v2)
                {
                    found = true;
                    break;
                }

                for (int i = 0; i < adj_list[current_v].Count; i++)
                {
                    x = adj_list[current_v].ElementAt(i);
                    {
                        if (visited[x] == true) continue;
                        else
                        {
                            path[x] = current_v;
                            path_length[x] = path_length[current_v] + 1;
                            q.Enqueue(x);
                            visited[x] = true;
                        }
                    }
                }


            }

            if (!found) return null;
            else
            {
                int[] path_representation = new int[path_length[v2] + 1];
                int x1 = v2;
                for (int i = 0; i < path_length[v2] + 1; i++)
                {
                    path_representation[i] = x1;
                    x1 = path[x1];
                }

                return path_representation;
            }


        }
    }
}
