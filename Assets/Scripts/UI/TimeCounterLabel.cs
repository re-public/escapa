using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(Label))]
    public sealed class TimeCounterLabel : MonoBehaviour
    {
        private readonly char[] currentRecordBuffer = { '0', '0', '0', '0', ',', '0', '\0' };
        private Label label;
        private IScoreController _score;

        private void Awake()
        {
            label = GetComponent<Label>();
            _score = GameObject.FindWithTag(Tags.ScoreController).GetComponent<IScoreController>();
        }

        private void FixedUpdate()
        {
            UpdateCurrentRecordBuffer(_score.CurrentTime);
            label.SetText(new string(currentRecordBuffer));
        }

        private void UpdateCurrentRecordBuffer(float value)
        {
            int firstPart = (int)value;
            int secondPart = (int)((value - firstPart) * 10);

            int i = currentRecordBuffer.Length - 1;

            currentRecordBuffer[i] = '\0';
            --i;

            if (secondPart == 0)
            {
                currentRecordBuffer[i] = '0';
                currentRecordBuffer[i - 1] = '.';
                i -= 2;
            }
            else
            {
                do
                {
                    currentRecordBuffer[i] = (char)(secondPart % 10 + '0');
                    secondPart /= 10;
                    --i;
                }
                while (secondPart > 0);

                currentRecordBuffer[i] = '.';
                --i;
            }

            do
            {
                currentRecordBuffer[i] = (char)(firstPart % 10 + '0');
                firstPart /= 10;
                --i;
            }
            while (firstPart > 0);

            for (int j = 0; j < currentRecordBuffer.Length - i - 1; ++j)
                currentRecordBuffer[j] = currentRecordBuffer[i + j + 1];
        }
    }
}
