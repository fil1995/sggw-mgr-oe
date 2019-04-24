using System;

abstract class StopCondition : IStopCondition
{
    protected Algorithm a;
    public string StopType => this.GetType().Name;

    public void Initialize(Algorithm a)
    {
        if (this.a!=null)
        {
            throw new Exception("Warunek stopu wykorzystywany kolejny raz!!!");
        }
        this.a = a;
        
    }

    public virtual bool Stop()
    {
        throw new Exception("brak implementacji stopu");
    }
    public StopCondition Clone()
    {
        return (StopCondition)this.MemberwiseClone();
    }

}
