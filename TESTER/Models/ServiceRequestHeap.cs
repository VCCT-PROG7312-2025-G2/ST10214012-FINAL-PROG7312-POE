using System.Collections.Generic;

namespace TESTER.Models
{
    public class ServiceRequestHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        public void Add(ServiceRequest request)
        {
            heap.Add(request);
            int i = heap.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                // MIN-HEAP: smallest priority rises
                if (heap[i].Priority >= heap[parent].Priority) break;
                var temp = heap[i];
                heap[i] = heap[parent];
                heap[parent] = temp;
                i = parent;
            }
        }

        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;
            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            Heapify(0);
            return min;
        }

        private void Heapify(int i)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int smallest = i;

            // MIN-HEAP comparisons
            if (left < heap.Count && heap[left].Priority < heap[smallest].Priority) smallest = left;
            if (right < heap.Count && heap[right].Priority < heap[smallest].Priority) smallest = right;

            if (smallest != i)
            {
                var temp = heap[i];
                heap[i] = heap[smallest];
                heap[smallest] = temp;
                Heapify(smallest);
            }
        }

        public List<ServiceRequest> GetAll() => heap;
    }
}
