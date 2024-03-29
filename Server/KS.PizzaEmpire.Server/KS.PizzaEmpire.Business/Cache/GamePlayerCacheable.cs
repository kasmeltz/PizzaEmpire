﻿namespace KS.PizzaEmpire.Business.Cache
{
    using Common.BusinessObjects;
    using KS.PizzaEmpire.Business.ProtoSerializable;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the state for a player of the game as stored in the cache.
    /// </summary>
    [ProtoContract]
    [Serializable]
    public class GamePlayerCacheable : ICacheEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerCacheable class.
        /// </summary>
        public GamePlayerCacheable() { }

        /// <summary>
        /// The number of coins owned by the player
        /// </summary>
        [ProtoMember(1)]
        public int Coins { get; set; }

        /// <summary>
        /// The number of coupons owned by the player
        /// </summary>
        [ProtoMember(2)]
        public int Coupons { get; set; }

        /// <summary>
        /// The current experience of the player
        /// </summary>
        [ProtoMember(3)]
        public int Experience { get; set; }

        /// <summary>
        /// The players current level
        /// </summary>
        [ProtoMember(4)]
        public int Level { get; set; }

        /// <summary>
        /// The player's current tutorial stage
        /// </summary>
        [ProtoMember(5)]
        public int TutorialStage { get; set; }

        /// <summary>
        /// Whether the state has changed
        /// </summary>
        [ProtoMember(6)]
        public bool StateChanged { get; set; }

        /// <summary>
        /// The locations (restuarants or head office) associated with the player
        /// </summary>
        [ProtoMember(7)] 
        public List<BusinessLocationProtoSerializable> Locations { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        [ProtoMember(8)]
        public List<WorkInProgressProtoSerializable> WorkInProgress { get; set; }
    }
}
