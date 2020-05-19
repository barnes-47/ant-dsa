namespace Ds.Generic
{
    public class QueueUsingTwoStacks<T>
    {
        #region Private Variables
        private Stack<T> _primary;
        private Stack<T> _secondary;
        #endregion

        #region Public Properties
        public int Count { get; private set; }
        #endregion

        #region Ctors
        public QueueUsingTwoStacks(int capacity)
        {
            _primary = new Stack<T>(capacity);
            _secondary = new Stack<T>(capacity);
        }
        #endregion

        #region Public Methods
        public T Dequeue()
        {
            var item = _primary.Pop();
            --Count;

            return item;
        }

        public void Enqueue(T item)
        {
            while (_primary.IsNotEmpty)
            {
                _secondary.Push(_primary.Pop());
            }
            _primary.Push(item);
            while (_secondary.IsNotEmpty)
            {
                _primary.Push(_secondary.Pop());
            }

            ++Count;
        }

        public T Peek()
            => _primary.Peek();
        #endregion
    }
}
