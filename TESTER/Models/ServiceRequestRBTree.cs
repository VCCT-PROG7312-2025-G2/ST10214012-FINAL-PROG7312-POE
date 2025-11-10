using System;
using System.Collections.Generic;

namespace TESTER.Models
{
    // --------------------------------------------------------------
    // Red-Black Tree Node
    // --------------------------------------------------------------
    internal class RBNode
    {
        public ServiceRequest Data;
        public RBNode Left;
        public RBNode Right;
        public RBNode Parent;
        public bool IsRed;               // true = RED, false = BLACK

        public RBNode(ServiceRequest data)
        {
            Data = data;
            IsRed = true;                // new nodes are red
        }
    }

    // --------------------------------------------------------------
    // Red-Black Tree (public API)
    // --------------------------------------------------------------
    public class ServiceRequestRBTree
    {
        private RBNode _root;

        // ----------------------------------------------------------
        // PUBLIC METHODS
        // ----------------------------------------------------------
        public void Insert(ServiceRequest request)
        {
            RBNode newNode = new RBNode(request);
            _root = Insert(_root, newNode, null);
            FixInsert(newNode);
        }

        public ServiceRequest Search(int id)
        {
            RBNode node = Search(_root, id);
            return node?.Data;
        }

        public List<ServiceRequest> InOrder()
        {
            var result = new List<ServiceRequest>();
            InOrder(_root, result);
            return result;
        }

        // ----------------------------------------------------------
        // PRIVATE HELPERS
        // ----------------------------------------------------------
        private RBNode Insert(RBNode root, RBNode newNode, RBNode parent)
        {
            if (root == null)
            {
                newNode.Parent = parent;
                return newNode;
            }

            if (newNode.Data.Id < root.Data.Id)
                root.Left = Insert(root.Left, newNode, root);
            else
                root.Right = Insert(root.Right, newNode, root);

            return root;
        }

        private RBNode Search(RBNode node, int id)
        {
            while (node != null)
            {
                if (id == node.Data.Id) return node;
                node = id < node.Data.Id ? node.Left : node.Right;
            }
            return null;
        }

        private void InOrder(RBNode node, List<ServiceRequest> list)
        {
            if (node == null) return;
            InOrder(node.Left, list);
            list.Add(node.Data);
            InOrder(node.Right, list);
        }

        // ----------------------------------------------------------
        // RED-BLACK FIX-UP AFTER INSERT
        // ----------------------------------------------------------
        private void FixInsert(RBNode node)
        {
            while (node != _root && node.Parent.IsRed)
            {
                RBNode parent = node.Parent;
                RBNode grandParent = parent.Parent;

                // Parent is LEFT child of grandparent
                if (parent == grandParent.Left)
                {
                    RBNode uncle = grandParent.Right;

                    if (uncle?.IsRed == true)                     // Case 1
                    {
                        parent.IsRed = false;
                        uncle.IsRed = false;
                        grandParent.IsRed = true;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Right)                 // Case 2 (triangle)
                        {
                            RotateLeft(parent);
                            node = parent;
                            parent = node.Parent;
                        }
                        // Case 3 (line)
                        RotateRight(grandParent);
                        parent.IsRed = false;
                        grandParent.IsRed = true;
                        node = parent;
                    }
                }
                // Parent is RIGHT child of grandparent
                else
                {
                    RBNode uncle = grandParent.Left;

                    if (uncle?.IsRed == true)                     // Case 1
                    {
                        parent.IsRed = false;
                        uncle.IsRed = false;
                        grandParent.IsRed = true;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Left)                  // Case 2
                        {
                            RotateRight(parent);
                            node = parent;
                            parent = node.Parent;
                        }
                        // Case 3
                        RotateLeft(grandParent);
                        parent.IsRed = false;
                        grandParent.IsRed = true;
                        node = parent;
                    }
                }
            }
            _root.IsRed = false;        // root always black
        }

        // ----------------------------------------------------------
        // ROTATIONS
        // ----------------------------------------------------------
        private void RotateLeft(RBNode x)
        {
            RBNode y = x.Right;
            x.Right = y.Left;

            if (y.Left != null)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == null)
                _root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        private void RotateRight(RBNode y)
        {
            RBNode x = y.Left;
            y.Left = x.Right;

            if (x.Right != null)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == null)
                _root = x;
            else if (y == y.Parent.Right)
                y.Parent.Right = x;
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }
    }
}