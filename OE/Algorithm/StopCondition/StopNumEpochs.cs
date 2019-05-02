using System;

class StopNumEpochs : StopCondition
{
    int epochs;
    DateTime dateEnd;
    public StopNumEpochs(int epochs, DateTime dateEnd)
    {
        this.epochs = epochs;
        this.dateEnd = dateEnd;
    }
    public override bool Stop()
    {
        if (a.CurrentEpoch >= epochs || DateTime.Now > dateEnd)
        {
            return true;
        }
        return false;
    }
}