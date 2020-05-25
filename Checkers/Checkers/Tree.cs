using System.Collections.Generic;

namespace Checkers
{
    //Drzewo, zawarte w niej beda mozliwe rozegrania gry
    //delegate void TreeVisitor<T>(T nodeData);

    class Tree<T>
    {
        private T data;
        private LinkedList<Tree<T>> children;

        public Tree(T data)
        {
            this.Data = data;
            children = new LinkedList<Tree<T>>();
        }

        public Tree<T> AddChild(T data)
        {
            Tree<T> child = new Tree<T>(data);
            children.AddFirst(child);
            return child;
        }

        public Tree<T> GetChild(int i)
        {
            foreach (Tree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }
        public LinkedList<Tree<T>> Children
        {
            get { return this.children; }
        }
        public T Data { get => data; set => data = value; }

        public void Traverse(System.Action<T> visitor) //skip first
        { 
            foreach (Tree<T> kid in children)
                kid.traverse(visitor);
        }
        public void traverse(System.Action<T> visitor) //kazdy node odwiedzony
        {
            visitor(this.Data);
            foreach (Tree<T> kid in children)
                kid.Traverse(visitor);
        }
    }
}
