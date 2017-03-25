using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{
    interface IGraph
    {
        void insert_edge(int v1, int v2);
        void remove_edge(int v1, int v2);
        int[] shortest_path(int v1, int v2);
        int find_vertex_by_label(string l);
    }

}
