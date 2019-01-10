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
        public static bool IsNewScore { get; private set; }
        public static bool IsHighScore { get; private set; }

        public static float CurrentRecord { get; private set; }
        public static IEnumerator Count()
        {
            CurrentRecord = 0f;
            IsNewScore = false;
            IsHighScore = false;
            isCountStarted = true;

            while (isCountStarted)
            {
                CurrentRecord += CounterInterval;
                yield return new WaitForSeconds(CounterInterval);
            }

            AddRecord(CurrentRecord);
        }

        private static List<float> _records = null;
        public static List<float> Records
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

        private static void AddRecord(float record)
        {
            for (var i = 0; i < RecordsCount; i++)
                if (record > Records[i])
                {
                    for (var j = RecordsCount - 1; j > i; j--)
                        Records[j] = Records[j - 1];

                    Records[i] = record;

                    IsNewScore = true;
                    IsHighScore = i == 0;

                    return;
                }
        }
    }
}
