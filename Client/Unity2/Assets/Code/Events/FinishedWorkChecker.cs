namespace KS.PizzaEmpire.Unity
{
    using Common.GameLogic;
    using Common.BusinessObjects;
    using System;
    using UnityEngine;

    /// <summary>
    /// Represents an item that checks with the sever to see if work has been completed
    /// </summary>
    public class FinishedWorkChecker
    {
        protected float checkWorkTimer { get; set; }
        protected bool areCheckingWork { get; set; }
        protected GamePlayer player { get; set; }
		protected Action<ServerCommunication> OnError { get; set; }

        /// <summary>
        /// The time to wait before testing if any work items are done
        /// </summary>
        public int CheckWorkSeconds { get; set; }

        /// <summary>
        /// Creates a new instance of the FinishedWorkChecker class.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="seconds"></param>
		public FinishedWorkChecker(GamePlayer player, int seconds, Action<ServerCommunication> onError)
        {
            this.player = player;
            CheckWorkSeconds = seconds;
            OnError = onError;
        }

        /// <summary>
        /// Checks if work has been done
        /// </summary>
        void CheckIfWorkDone()
        {
            if (areCheckingWork)
            {
                return;
            }

            bool contactServer = false;

            for(int i = 0;i < player.WorkItems.Count;i++)
            {
                WorkItem workItem = player.WorkItems[i];
                if (workItem.FinishTime <= DateTime.UtcNow)
                {
                    contactServer = true;
                    break;
                }
            }

            if (contactServer)
            {
                areCheckingWork = true;

                ServerCommunicator.Instance.Communicate(
                    ServerActionEnum.FinishWork,
                    (ServerCommunication com) =>
                    {
                        DateTime[] now = ServerCommunicator.Instance.ParseResponse<DateTime[]>(com);
                        now[0] = now[0].ToUniversalTime();
                        GamePlayerLogic.Instance.FinishWork(player, now[0]);

                        areCheckingWork = false;
				}, OnError);
            }
        }

        /// <summary>
        /// Updates the work checker
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            checkWorkTimer += dt;
            if (checkWorkTimer > CheckWorkSeconds)
            {
                checkWorkTimer = 0;
                CheckIfWorkDone();
            }
        }
    }
}
