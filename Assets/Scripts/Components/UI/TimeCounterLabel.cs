using Escapa.Core.Managers;

namespace Escapa.Components.UI
{
    public sealed class TimeCounterLabel : Label
    {
        private readonly char[] currentRecordBuffer = { '0', '0', '0', '0', '.', '0', '\0' };

        private new void Start() => TextMesh.color = StyleManager.Colors.TextAlfa;

        private void FixedUpdate()
        {
            UpdateCurrentRecordBuffer(ScoreManager.CurrentTime);
            TextMesh.SetText(new string(currentRecordBuffer));
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
