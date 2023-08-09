using BarbarianSim.Events;

namespace BarbarianSim;

public class SimLogger
{
    public virtual void Info(string msg) => throw new NotImplementedException();
    public virtual void Verbose(string msg) => ActiveEvent?.AddVerboseLog(msg);
    public virtual void Warning(string msg) => throw new NotImplementedException();
    public virtual void LogError(string msg) => throw new NotImplementedException();

    public EventInfo ActiveEvent { get; set; }
}
