﻿using System;

class StopPopulationDeviation : StopCondition
{
    double deviation;
    public StopPopulationDeviation(double deviation)
    {
        this.deviation = deviation;
    }
    public override bool Stop()
    {
        if (a.CurrentEpoch > 2 && a.PopulationDeviation() < 0.01)
        {
            return true;
        }
        return false;
    }
}
