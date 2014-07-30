namespace KS.PizzaEmpire.Common.ObjectPool
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An object pool that contains items that know how to reset themselves
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolWithReset<T> where T : class, IResetable, new()
    {
        private Stack<T> objectStack;
        private Action<T> resetAction;
        private Action<T> onetimeInitAction;

        /// <summary>
        /// Creates a new object pool with the specified initial buffer size,
        /// global reset method and global one time init method
        /// </summary>
        /// <param name="initialBufferSize">The initial size of the stack.</param>
        /// <param name="ResetAction">A global action to be performed every time 
        /// a previously used object is retreived from the pool.</param>
        /// <param name="OnetimeInitAction">An initial method that will only be
        /// performed when a new object is first instantiated.</param>
        public ObjectPoolWithReset(int initialBufferSize, Action<T> ResetAction, 
            Action<T> OnetimeInitAction)
        {
            objectStack = new Stack<T>(initialBufferSize);
            resetAction = ResetAction;
            onetimeInitAction = OnetimeInitAction;
        }

        /// <summary>
        /// Creates a new object pool with the specified initial buffer size,
        /// global reset method and global one time init method
        /// </summary>
        /// <param name="initialBufferSize">The initial size of the stack.</param>
        /// <param name="Action">A global action to be performed every time 
        /// a previously used object is retreived from the pool or one time when
        /// a new object is instantiated.</param>
        /// <param name="isResetAction">Whether the provided action is for reset or onetime.</param>
        public ObjectPoolWithReset(int initialBufferSize, Action<T> Action,
            bool isResetAction)
        {
            objectStack = new Stack<T>(initialBufferSize);

            if (isResetAction)
            {
                resetAction = Action;
            }
            else
            {
                onetimeInitAction = Action;
            }            
        }

        /// <summary>
        /// Creates a new object pool with the specified initial buffer size,
        /// global reset method and global one time init method
        /// </summary>
        /// <param name="initialBufferSize">The initial size of the stack.</param>
        public ObjectPoolWithReset(int initialBufferSize)
        {
            objectStack = new Stack<T>(initialBufferSize);
        }

        /// <summary>
        /// Returns an instance from the pool
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            if (objectStack.Count > 0)
            {
                T t = objectStack.Pop();

                t.Reset();

                if (resetAction != null)
                {
                    resetAction(t);
                }

                return t;
            }
            else
            {
                T t = new T();

                if (onetimeInitAction != null)
                { 
                    onetimeInitAction(t);
                }

                return t;
            }
        }

        /// <summary>
        /// Adds an item back to the object pool when no longer required
        /// </summary>
        /// <param name="obj"></param>
        public void Store(T obj)
        {
            objectStack.Push(obj);
        }
    }
}