using System.Threading;

namespace Showdown3;

using System;
using System.Threading;

public class CountDown
{
    private int countDownValue;
    private readonly int interval;
    private bool isCounting;

    public event EventHandler<int> Tick;
    public event EventHandler CountdownEnded;

    public CountDown(int countDownStart, int interval = 1000)
    {
        this.countDownValue = countDownStart;
        this.interval = interval;
        this.isCounting = true;

        // Starten Sie den Countdown im Konstruktor
        StartCountdown();
    }

    private void StartCountdown()
    {
        Thread countdownThread = new Thread(() =>
        {
            while (isCounting)
            {
                Thread.Sleep(interval);
                countDownValue--;

                // Event auslösen für jeden Takt
                OnTick(countDownValue);

                if (countDownValue < 0)
                {
                    // Countdown beenden und Event auslösen
                    isCounting = false;
                    OnCountdownEnded();
                }
            }
        });

        countdownThread.Start();
    }

    private void OnTick(int currentValue)
    {
        Tick?.Invoke(this, currentValue);
    }

    private void OnCountdownEnded()
    {
        CountdownEnded?.Invoke(this, EventArgs.Empty);
    }

    public int Update()
    {
        // Hier können Sie zusätzliche Logik hinzufügen, wenn der Countdown aktualisiert wird
        return countDownValue;
    }

    public void End()
    {
        // Hier können Sie zusätzliche Logik hinzufügen, wenn der Countdown endet
        isCounting = false;
        OnCountdownEnded();
    }
}
