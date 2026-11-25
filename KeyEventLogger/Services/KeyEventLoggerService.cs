using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>Kayıt durumunu ve bellekteki klavye kayıtlarını yönetir.</summary>
public class KeyEventLoggerService
{
    private readonly List<KeyEventRecord> _records = new();

    /// <summary>Listeye yeni kayıt eklendiğinde tetiklenir.</summary>
    public event EventHandler<KeyEventRecord>? RecordAdded;

    public bool IsRecording { get; private set; }

    public IReadOnlyList<KeyEventRecord> Records => _records;

    public void Start() => IsRecording = true;

    public void Stop() => IsRecording = false;

    public void Clear() => _records.Clear();

    /// <summary>Kayıt açıksa kaydı ekler. Kayıt kapalıyken hiçbir şey yapmaz.</summary>
    public void Add(KeyEventRecord kayit)
    {
        if (!IsRecording)
        {
            return;
        }

        _records.Add(kayit);
        RecordAdded?.Invoke(this, kayit);
    }
}
