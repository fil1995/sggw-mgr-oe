using System;

class StopTime : StopCondition
{
    int time;
    DateTime dateEnd;
    public StopTime(int timeSeconds,DateTime dateEnd)
    {
        this.time = timeSeconds;
        this.dateEnd = dateEnd;
    }
    public override bool Stop()
    {
        if (a.stats.ElapsedSeconds > time || DateTime.Now>dateEnd)
        {
            return true;
        }
        return false;
    }
}
