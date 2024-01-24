using System;
using System.Threading;

namespace Showdown3;

public class CountDown
{
    private readonly int _interval;
    private int _countDownValue;
    private bool _isCounting;

    public CountDown(int countDownStart, int interval = 1000)
    {
        _countDownValue = countDownStart;
        _interval = interval;
        _isCounting = true;

        // Starten Sie den Countdown im Konstruktor
        StartCountdown();
    }

    public event Action<int> Tick;
    public event Action CountdownEnded;

    private void StartCountdown()
    {
        var countdownThread = new Thread(() =>
        {
            OnTick(_countDownValue);
            while (_isCounting)
            {
                Thread.Sleep(_interval);
                _countDownValue--;
                // Event auslösen für jeden Takt
                OnTick(_countDownValue);
                if (_countDownValue <= 0)
                {
                    // Countdown beenden und Event auslösen
                    _isCounting = false;
                    OnCountdownEnded();
                }
            }
        });

        countdownThread.Start();
    }

    private void OnTick(int currentValue)
    {
        Tick?.Invoke(currentValue);
    }

    private void OnCountdownEnded()
    {
        CountdownEnded?.Invoke();
    }

    public int Update()
    {
        // Hier können Sie zusätzliche Logik hinzufügen, wenn der Countdown aktualisiert wird
        return _countDownValue;
    }

    public void End()
    {
        // Hier können Sie zusätzliche Logik hinzufügen, wenn der Countdown endet
        _isCounting = false;
        OnCountdownEnded();
    }
}