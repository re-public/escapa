﻿using Escapa.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Managers
{
    public static class ScoreManager
    {
        private static float _startTime;

        /// <summary>
        /// Current game time.
        /// </summary>
        public static float CurrentRecord => _startTime > float.Epsilon ? Time.realtimeSinceStartup - _startTime : 0f;

        public static float LastTime { get; private set; }

        /// <summary>
        /// Is current time is new high score.
        /// </summary>
        public static bool IsHighScore { get; private set; }

        public static void StartCount()
        {
            IsHighScore = false;
            _startTime = Time.realtimeSinceStartup;
        }

        public static void StopCount()
        {
            LastTime = CurrentRecord;
            _startTime = 0f;

            if (LastTime > CurrentTop)
            {
                IsHighScore = true;
                Records[(int) DifficultyManager.Level] = LastTime;
            }
        }

        /// <summary>
        /// Current high score.
        /// </summary>
        public static float CurrentTop => Records[(int) DifficultyManager.Level];

        private static List<float> _records;
        private static List<float> Records
        {
            get
            {
                if (_records == null)
                {
                    _records = new List<float>();

                    for (var i = Difficulties.Easy; i <= Difficulties.Insane; i++)
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
            for (var i = Difficulties.Easy; i <= Difficulties.Insane; i++)
            {
                var index = (int) i;
                PlayerPrefs.SetFloat($"{PlayerPrefKeys.Record}{index}", Records[index]);
            }
        }
    }
}
