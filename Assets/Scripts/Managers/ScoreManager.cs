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
        public static float CurrentRecord => _finishTime - _startTime;

        /// <summary>
        /// Is current time is new high score.
        /// </summary>
        public static bool IsHighScore { get; private set; }

        private const int _recordsCount = 4;

        private static float _startTime;
        private static float _finishTime;

        public static void Start()
        {
            IsHighScore = false;
            _finishTime = 0f;

            _startTime = Time.realtimeSinceStartup;
        }

        public static void Finish()
        {
            _finishTime = Time.realtimeSinceStartup;

            if (CurrentRecord > CurrentTop)
            {
                IsHighScore = true;
                Records[DifficultyManager.Level] = CurrentRecord;
            }
        }

        /// <summary>
        /// Current high score.
        /// </summary>
        public static float CurrentTop => Records[DifficultyManager.Level];

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
