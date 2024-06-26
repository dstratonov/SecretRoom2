using System.Collections.Generic;

namespace Common.UI.Root
{
    public class CanvasProvider
    {
        private readonly Stack<CanvasRoot> _roots = new();
        
        public CanvasRoot GetActiveCanvas()
        {
            return _roots.Peek();
        }

        public void AddRoot(CanvasRoot root)
        {
            _roots.Push(root);
        }

        public void RemoveTopRoot()
        {
            _roots.Pop();
        }
    }
}