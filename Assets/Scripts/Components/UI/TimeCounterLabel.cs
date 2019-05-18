using Escapa.Core.Managers;

namespace Escapa.Components.UI
{
    public sealed class TimeCounterLabel : Label
    {
        private readonly char[] _currentRecordBuffer = { '0', '0', '0', '0', '.', '0', '0', '\0' };

        private new void Start() => TextMesh.color = StyleManager.Current.textAlfa;

        private void FixedUpdate()
        {
            UpdateCurrentRecordBuffer(ScoreManager.CurrentTime);
            TextMesh.SetText(new string(_currentRecordBuffer));
        }

        private void UpdateCurrentRecordBuffer(float value)
        {
            int firstPart = (int)value;
            int secondPart = (int)((value - firstPart) * 100);
            if (secondPart < 10)
                secondPart *= 10;

            int i = _currentRecordBuffer.Length - 1;

            _currentRecordBuffer[i] = '\0';
            --i;

            if (secondPart == 0)
            {
                _currentRecordBuffer[i] = '0';
                _currentRecordBuffer[i - 1] = '0';
                _currentRecordBuffer[i - 2] = '.';
                i -= 3;
            }
            else
            {
                do
                {
                    _currentRecordBuffer[i] = (char)(secondPart % 10 + '0');
                    secondPart /= 10;
                    --i;
                }
                while (secondPart > 0);

                _currentRecordBuffer[i] = '.';
                --i;
            }

            do
            {
                _currentRecordBuffer[i] = (char)(firstPart % 10 + '0');
                firstPart /= 10;
                --i;
            }
            while (firstPart > 0);

            for (int j = 0; j < _currentRecordBuffer.Length - i - 1; ++j)
                _currentRecordBuffer[j] = _currentRecordBuffer[i + j + 1];
        }
    }
}