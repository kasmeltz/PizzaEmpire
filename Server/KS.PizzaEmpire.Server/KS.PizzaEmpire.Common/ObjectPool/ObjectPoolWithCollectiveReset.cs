namespace KS.PizzaEmpire.Common.ObjectPool
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An object pool whose items will all be stored back in the pool and reset
    /// together. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolWithCollectiveReset<T> where T : class, new()
    {
        private List<T> objectList;
        private int nextAvailableIndex = 0;
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
        public ObjectPoolWithCollectiveReset(int initialBufferSize, Action<T>
            ResetAction, Action<T> OnetimeInitAction)
        {
            objectList = new List<T>(initialBufferSize);
            resetAction = ResetAction;
            onetimeInitAction = OnetimeInitAction;
        }

        /// <summary>
        /// Creates a new object pool with the specified initial buffer size,
        /// global reset method and global one time init method
        /// </summary>
        /// <param name="initialBufferSize">The initial size of the stack.</param>
        /// <param name="ResetAction">A global action to be performed every time 
        /// a previously used object is retreived from the pool.</param>
        /// <param name="isResetAction">Whether the provided action is for reset or onetime.</param>
        public ObjectPoolWithCollectiveReset(int initialBufferSize, Action<T> Action,
            bool isResetAction)
        {
            objectList = new List<T>(initialBufferSize);

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
        public ObjectPoolWithCollectiveReset(int initialBufferSize)
        {
            objectList = new List<T>(initialBufferSize);
        }

        /// <summary>
        /// Returns an instance from the pool
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            if (nextAvailableIndex < objectList.Count)
            {
                // an allocated object is already available; just reset it
                T t = objectList[nextAvailableIndex];
                nextAvailableIndex++;

                if (resetAction != null)
                    resetAction(t);

                return t;
            }
            else
            {
                // no allocated object is available
                T t = new T();
                objectList.Add(t);
                nextAvailableIndex++;

                if (onetimeInitAction != null)
                    onetimeInitAction(t);

                return t;
            }
        }

        /// <summary>
        /// Returns all of the items back to the pool in one shot
        /// </summary>
        public void StoreAll()
        {
            nextAvailableIndex = 0;
        }
    }
}