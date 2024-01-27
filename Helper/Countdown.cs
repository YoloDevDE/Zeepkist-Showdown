using System;
using System.Threading;
using System.Threading.Tasks;

namespace Showdown3.Helper;

public class Countdown
{
    private CancellationTokenSource _cancellationTokenSource;
    private int _seconds;

    public Countdown(int seconds)
    {
        _seconds = seconds;
    }

    public event Action<int> OnTick;
    public event Action OnCountdownEnd;

    public void Start()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => RunCountdown(_cancellationTokenSource.Token));
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }

    private async Task RunCountdown(CancellationToken cancellationToken)
    {
        while (_seconds > 0 && !cancellationToken.IsCancellationRequested)
        {
            OnTick?.Invoke(_seconds);
            await Task.Delay(1000, cancellationToken);
            _seconds--;
        }

        if (!cancellationToken.IsCancellationRequested) OnCountdownEnd?.Invoke();
    }
}