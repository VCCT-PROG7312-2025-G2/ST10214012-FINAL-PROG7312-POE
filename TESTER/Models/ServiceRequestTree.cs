using System;
using System.Collections.Generic;

namespace TESTER.Models
{
    //Service Request Tree
    public class ServiceRequestNode
    {
        public ServiceRequest Data;
        public ServiceRequestNode Left;
        public ServiceRequestNode Right;
        public int Height;

        public ServiceRequestNode(ServiceRequest data)
        {
            Data = data;
            Height = 1;
        }
    }

    public class ServiceRequestBST
    {
        public ServiceRequestNode Root;

        public void Insert(ServiceRequest request)
        {
            Root = Insert(Root, request);
        }

        private ServiceRequestNode Insert(ServiceRequestNode node, ServiceRequest request)
        {
            if (node == null) return new ServiceRequestNode(request);

            if (request.Id < node.Data.Id)
                node.Left = Insert(node.Left, request);
            else
                node.Right = Insert(node.Right, request);

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

          
            if (balance > 1 && request.Id < node.Left.Data.Id) return RightRotate(node);
            if (balance < -1 && request.Id > node.Right.Data.Id) return LeftRotate(node);
            if (balance > 1 && request.Id > node.Left.Data.Id)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
            if (balance < -1 && request.Id < node.Right.Data.Id)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public ServiceRequest Search(int id)
        {
            var node = Root;
            while (node != null)
            {
                if (id == node.Data.Id) return node.Data;
                node = id < node.Data.Id ? node.Left : node.Right;
            }
            return null;
        }

        public List<ServiceRequest> InOrder()
        {
            var list = new List<ServiceRequest>();
            InOrder(Root, list);
            return list;
        }

        private void InOrder(ServiceRequestNode node, List<ServiceRequest> list)
        {
            if (node == null) return;
            InOrder(node.Left, list);
            list.Add(node.Data);
            InOrder(node.Right, list);
        }

        private int GetHeight(ServiceRequestNode node) => node?.Height ?? 0;
        private int GetBalance(ServiceRequestNode node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        private ServiceRequestNode RightRotate(ServiceRequestNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        private ServiceRequestNode LeftRotate(ServiceRequestNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }
    }
}
