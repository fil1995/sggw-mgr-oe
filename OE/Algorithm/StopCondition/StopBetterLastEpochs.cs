using System;

class StopBetterLastEpochs : StopCondition
{
    double percentage;
    public StopBetterLastEpochs(double percentage)
    {
        this.percentage = percentage;
    }
    public override bool Stop()
    {
        // jeśli poprawa w porównaniu do wczesniejszego pokolenia wyniosła mało, to przerywamy
        if (a.CurrentEpoch > 50 && (a.CurrentEpoch - a.stats.NumEpochFromBest(0.8)) > 50) // czyli jeśli przez ostatnie 50 epok wynik jest nie lepszy niż 20% to koniec
        {
            return true;
        }
        return false;
    }
}
