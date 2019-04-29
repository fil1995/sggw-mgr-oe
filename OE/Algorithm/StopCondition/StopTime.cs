using System;

class StopTime : StopCondition
{
    int time;
    public StopTime(int timeSeconds)
    {
        this.time = timeSeconds;
    }
    public override bool Stop()
    {
        if (a.stats.ElapsedSeconds > time)
        {
            return true;
        }
        return false;
    }
}
