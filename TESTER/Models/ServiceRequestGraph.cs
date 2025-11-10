using System.Collections.Generic;

namespace TESTER.Models
{
    public class ServiceRequestGraph
    {
        private Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

        public void AddDependency(int requestId, int dependsOnId)
        {
            if (!adjList.ContainsKey(requestId)) adjList[requestId] = new List<int>();
            adjList[requestId].Add(dependsOnId);
        }

        public List<int> GetDependencies(int requestId)
        {
            return adjList.ContainsKey(requestId) ? adjList[requestId] : new List<int>();
        }

        public List<int> DFS(int startId)
        {
            var visited = new HashSet<int>();
            var result = new List<int>();
            DFSUtil(startId, visited, result);
            return result;
        }

        private void DFSUtil(int node, HashSet<int> visited, List<int> result)
        {
            if (visited.Contains(node)) return;
            visited.Add(node);
            result.Add(node);
            foreach (var neighbor in GetDependencies(node))
            {
                DFSUtil(neighbor, visited, result);
            }
        }
    }
}
