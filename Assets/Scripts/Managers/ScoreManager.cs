using Escapa.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Managers
{
    public static class ScoreManager
    {
        /// <summary>
        /// Current game time.
        /// </summary>
        public static float CurrentRecord => (_finishTime.HasValue && _startTime.HasValue) ? _finishTime.Value - _startTime.Value : 0f;

        /// <summary>
        /// Is current time is new high score.
        /// </summary>
        public static bool IsHighScore { get; private set; }

        private const int _recordsCount = 4;

        private static float? _startTime;
        private static float? _finishTime;

        public static void Start()
        {
            IsHighScore = false;
            _finishTime = null;

            _startTime = Time.realtimeSinceStartup;
        }

        public static void Finish()
        {
            _finishTime = Time.realtimeSinceStartup;

            IsHighScore = CurrentRecord > CurrentTop;
        }

        /// <summary>
        /// Current high score.
        /// </summary>
        public static float CurrentTop
        {
            get => Records[DifficultyManager.Level];
            private set => Records[DifficultyManager.Level] = value;
        }

        private static List<float> _records = null;
        private static List<float> Records
        {
            get
            {
                if (_records == null)
                {
                    _records = new List<float>(_recordsCount);

                    for (var i = 0; i < _recordsCount; i++)
                        _records.Add(PlayerPrefs.GetFloat($"{PlayerPrefKeys.Record}{i}", 0f));
                }

                return _records;
            }
        }

        /// <summary>
        /// Save all records in memory.
        /// </summary>
        public static void SaveRecords()
        {
            for (var i = 0; i < _recordsCount; i++)
                PlayerPrefs.SetFloat($"{PlayerPrefKeys.Record}{i}", Records[i]);
        }
    }
}
