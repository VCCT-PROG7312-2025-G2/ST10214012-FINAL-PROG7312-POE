using System;
using System.Collections.Generic;
using System.Linq;

namespace TESTER.Models
{
    //MST
    public class Edge
    {
        public ServiceRequest A { get; set; }
        public ServiceRequest B { get; set; }
        public double Weight { get; set; }
    }

    public class ServiceRequestMST
    {
        private class DisjointSet
        {
            private Dictionary<int, int> parent = new Dictionary<int, int>();
            private Dictionary<int, int> rank = new Dictionary<int, int>();

            public void MakeSet(int id)
            {
                parent[id] = id;
                rank[id] = 0;
            }

            public int Find(int id)
            {
                if (parent[id] != id)
                    parent[id] = Find(parent[id]);
                return parent[id];
            }

            public void Union(int x, int y)
            {
                int xRoot = Find(x), yRoot = Find(y);
                if (xRoot == yRoot) return;

                if (rank[xRoot] < rank[yRoot])
                    parent[xRoot] = yRoot;
                else if (rank[xRoot] > rank[yRoot])
                    parent[yRoot] = xRoot;
                else
                {
                    parent[yRoot] = xRoot;
                    rank[xRoot]++;
                }
            }
        }

        public List<List<ServiceRequest>> BuildBatches(List<ServiceRequest> requests, int maxPerBatch = 3)
        {
            if (!requests.Any() || requests.Count <= maxPerBatch)
                return new List<List<ServiceRequest>> { requests };

            var edges = new List<Edge>();
            var locationGroups = requests.GroupBy(r => r.Location);

            foreach (var group in locationGroups)
            {
                var list = group.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        double weight = list[i].Priority + list[j].Priority;
                        edges.Add(new Edge { A = list[i], B = list[j], Weight = weight });
                    }
                }
            }

            edges = edges.OrderBy(e => e.Weight).ToList();

            var ds = new DisjointSet();
            foreach (var r in requests) ds.MakeSet(r.Id);

            var batches = new List<List<ServiceRequest>>();
            var currentBatch = new List<ServiceRequest>();

            foreach (var edge in edges)
            {
                if (ds.Find(edge.A.Id) != ds.Find(edge.B.Id))
                {
                    ds.Union(edge.A.Id, edge.B.Id);
                    currentBatch.Add(edge.A);
                    currentBatch.Add(edge.B);

                    if (currentBatch.Count >= maxPerBatch)
                    {
                        batches.Add(currentBatch.Distinct().ToList());
                        currentBatch = new List<ServiceRequest>();
                    }
                }
            }

            if (currentBatch.Any())
                batches.Add(currentBatch.Distinct().ToList());

            var usedIds = batches.SelectMany(b => b.Select(r => r.Id)).ToHashSet();
            var leftovers = requests.Where(r => !usedIds.Contains(r.Id)).ToList();
            foreach (var r in leftovers)
                batches.Add(new List<ServiceRequest> { r });

            return batches;
        }
    }
}