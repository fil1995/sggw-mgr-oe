using System;

abstract class StopCondition : IStopCondition
{
    protected Algorithm a;
    public string StopType => this.GetType().Name;

    public void Initialize(Algorithm a)
    {
        this.a = a;
        
    }

    public virtual bool Stop()
    {
        return true;
    }
}
