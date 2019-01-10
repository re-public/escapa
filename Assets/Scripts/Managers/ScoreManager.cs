using Escapa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Managers
{
    public static class ScoreManager
    {
        public const float CounterInterval = 0.02f;
        public const int RecordsCount = 5;

        public static bool isCountStarted = false;
        public static bool IsHighScore { get; private set; }

        public static float CurrentRecord { get; private set; }
        public static IEnumerator Count()
        {
            CurrentRecord = 0f;
            IsHighScore = false;
            isCountStarted = true;

            while (isCountStarted)
            {
                CurrentRecord += CounterInterval;
                yield return new WaitForSeconds(CounterInterval);
            }

            if (CurrentRecord > CurrentTop)
            {
                CurrentTop = CurrentRecord;
                IsHighScore = true;
            }
        }

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
                    _records = new List<float>(RecordsCount);

                    for (var i = 0; i < RecordsCount; i++)
                        _records.Add(PlayerPrefs.GetFloat($"{PlayerPrefKeys.Record}{i}", 0f));
                }

                return _records;
            }
        }

        public static void SaveRecords()
        {
            for (var i = 0; i < RecordsCount; i++)
                PlayerPrefs.SetFloat($"{PlayerPrefKeys.Record}{i}", Records[i]);
        }
    }
}
