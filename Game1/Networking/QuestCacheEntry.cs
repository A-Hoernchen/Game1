using System;
using Game1.Model;

namespace Game1.Networking
{
    /// <summary>
    /// Cache-Item für Quests
    /// </summary>
    internal class QuestCacheEntry
    {
        /// <summary>
        /// Name der Quest
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Progress-Stage der Quest
        /// </summary>
        public string Progress { get; set; }

        /// <summary>
        /// Status der Quest
        /// </summary>
        public QuestState State  { get; set; }
    }
}

