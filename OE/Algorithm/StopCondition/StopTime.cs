using System;

class StopTime : StopCondition
{
    int time;
    public StopTime(int time)
    {
        this.time = time;
    }
    public override bool Stop()
    {
        if (a.stats.ElapsedMilliseconds > time)
        {
            return true;
        }
        return false;
    }
}
