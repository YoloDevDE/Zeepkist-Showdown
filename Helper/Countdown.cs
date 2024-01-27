using System;
using System.Threading;
using System.Threading.Tasks;

namespace Showdown3.Helper;

public class Countdown
{
    private CancellationTokenSource _cancellationTokenSource;
    private readonly int _originalSeconds;
    private int _remainingSeconds;

    public Countdown(int seconds)
    {
        _originalSeconds = seconds;
        _remainingSeconds = seconds;
    }

    public event Action<int> OnTick;
    public event Action OnCountdownEnd;
    public event Action OnCountdownReset;

    public void Start()
    {
        Stop(); // Stelle sicher, dass der aktuelle Countdown gestoppt wird
        _remainingSeconds = _originalSeconds; // Setze die Zeit zurück
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => RunCountdown(_cancellationTokenSource.Token));
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }

    public string GetFormattedRemainingTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_remainingSeconds);
        string formattedTime = timeSpan.TotalHours >= 1
            ? timeSpan.ToString(@"hh\:mm\:ss")
            : timeSpan.ToString(@"mm\:ss");
        return formattedTime;
    }

    public void Reset()
    {
        OnCountdownReset?.Invoke();
        Start(); // Stoppt und startet den Countdown neu
    }

    private async Task RunCountdown(CancellationToken cancellationToken)
    {
        while (_remainingSeconds > 0 && !cancellationToken.IsCancellationRequested)
        {
            OnTick?.Invoke(_remainingSeconds);
            await Task.Delay(1000, cancellationToken);
            _remainingSeconds--;
        }

        if (!cancellationToken.IsCancellationRequested) OnCountdownEnd?.Invoke();
    }
}