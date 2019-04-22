using System;

class StopNumEpochs : StopCondition
{
    int epochs;
    public StopNumEpochs(int epochs)
    {
        this.epochs = epochs;
    }
    public override bool Stop()
    {
        if (a.CurrentEpoch >= epochs)
        {
            return true;
        }
        return false;
    }
}
