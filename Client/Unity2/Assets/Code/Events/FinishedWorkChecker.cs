namespace KS.PizzaEmpire.Unity
{
    using Common.GameLogic;
    using KS.PizzaEmpire.Common.BusinessObjects;
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

        /// <summary>
        /// The time to wait before testing if any work items are done
        /// </summary>
        public int CheckWorkSeconds { get; set; }

        /// <summary>
        /// Creates a new instance of the FinishedWorkChecker class.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="seconds"></param>
        public FinishedWorkChecker(GamePlayer player, int seconds)
        {
            this.player = player;
            CheckWorkSeconds = seconds;
        }

        void CheckIfWorkDone()
        {
            if (areCheckingWork)
            {
                return;
            }

            bool contactServer = false;

            foreach (WorkItem workItem in player.WorkItems)
            {
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
                    }, GUIGameObject.OnServerError);
            }
        }

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
