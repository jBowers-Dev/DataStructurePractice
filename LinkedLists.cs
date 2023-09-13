using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructurePractice
{
    #region Nodes

    public class SinglyLinkedListNode<T>
    {
        public T Data { get; set; }            // Data stored in the node
        public SinglyLinkedListNode<T> Next { get; set; }  // Reference to the next node

        public SinglyLinkedListNode(T data)
        {
            Data = data;
            Next = null; // By default, the next node is null (end of the list)
        }
    }

    public class DoublyLinkedListNode<T>
    {
        public T Data { get; set; }                   // Data stored in the node
        public DoublyLinkedListNode<T> Next { get; set; }  // Reference to the next node
        public DoublyLinkedListNode<T> Previous { get; set; }  // Reference to the previous node

        public DoublyLinkedListNode(T data)
        {
            Data = data;
            Next = null;     // By default, the next node is null (end of the list)
            Previous = null; // By default, the previous node is null (beginning of the list)
        }
    }

    #endregion Nodes

    #region Linked Lists

    public class SinglyLinkedList<T>
    {
        private SinglyLinkedListNode<T> _head;

        public void InsertBefore(T data)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);
            newNode.Next = _head;
            _head = newNode;
        }

        public void InsertAfter(T data)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

            if (_head == null)
                _head = newNode;
            else
            {
                SinglyLinkedListNode<T> current = _head;
                while (current != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        /// <summary>
        /// Insert data at a specific index point
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">The data object used as an index.</param>
        public void InsertAfter(T data, T index)
        {
            SinglyLinkedListNode<T> newNode = new SinglyLinkedListNode<T>(data);

            if (_head == null)
                _head = newNode;
            else
            {
                SinglyLinkedListNode<T> current = _head;
                while (current != null)
                {
                    if (current.Data.Equals(index))
                    {
                        newNode.Next = current.Next;
                        current.Next = newNode;
                        return; // Break out, we found our first instance
                    }

                    current = current.Next;
                }
            }
        }

        public void Delete(T data)
        {
            Debug.Assert((_head != null), "List has not been instantiated.");

            // Check to see if it's the head
            if (_head.Data.Equals(data))
            {
                _head = _head.Next;
                return;
            }

            // Delete a node in the middle of the list
            SinglyLinkedListNode<T> current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    current.Next = current.Next.Next;
                    return;
                }

                current = current.Next;
            }

            // Null the node at the end of the list if it matches
            if (current.Data.Equals(data))
            {
                current = null; // No more in the list, set it to null
                return;
            }
        }

        /// <summary>
        /// Search the linked list for specific data
        /// </summary>
        /// <param name="data">The data search parameter</param>
        /// <returns></returns>
        public T Search(T data)
        {
            SinglyLinkedListNode<T> current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return current.Data; // Found within the list, returning value

                current = current.Next;
            }
            return default(T); // No data found, return default value
        }

        /// <summary>
        /// Return a list of the data
        /// </summary>
        /// <returns></returns>
        public List<T> Traverse()
        {
            List<T> result = new List<T>();

            SinglyLinkedListNode<T> current = _head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }

            return result;
        }
    }

    public class DoublyLinkedList<T> : SinglyLinkedList<T>
    {
        private DoublyLinkedListNode<T> _head;

        public void Prepend(T data)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }

        public void InsertBefore(T data, T index)
        {
            DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(data);
            DoublyLinkedListNode<T> current = _head;

            while (current != null)
            {
                if (current.Data.Equals(index))
                {
                    newNode.Previous = current.Previous;
                    newNode.Next = current;

                    if (current.Previous != null)
                    {
                        current.Previous.Next = newNode;
                    }
                    else
                    {
                        // If the index is the head node, update the head.
                        _head = newNode;
                    }

                    current.Previous = newNode;
                    return;
                }
                current = current.Next;
            }
        }

        public void DeleteAt(int position)
        {
            if (position < 0)
            {
                // Invalid position, do nothing or throw an exception
                return;
            }

            DoublyLinkedListNode<T> current = _head;
            int counter = 0;

            while (current != null)
            {
                if (counter == position)
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        // Deleting the head node, update the head.
                        _head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }

                    return;
                }

                counter++;
                current = current.Next;
            }
        }

        public void Reverse()
        {
            DoublyLinkedListNode<T> current = _head;
            DoublyLinkedListNode<T> temp = null;

            while (current != null)
            {
                // Swap Next and Previous pointers
                temp = current.Next;
                current.Next = current.Previous;
                current.Previous = temp;

                // Move to the next node
                current = current.Previous; // Note: Previous becomes the new Next
            }

            // Update the head to the last node (which was the original head)
            _head = temp?.Previous;
        }

        public void PrintReverse()
        {
            DoublyLinkedListNode<T> current = _head;

            // Traverse to the last node (tail)
            while (current != null && current.Next != null)
            {
                current = current.Next;
            }

            // Traverse backward and print the data
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Previous;
            }

            Console.WriteLine(); // Print a newline for formatting
        }
    }

    #endregion Linked Lists
}