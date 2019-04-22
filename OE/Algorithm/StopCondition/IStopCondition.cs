interface IStopCondition
{
    bool Stop();
    string StopType { get; }
}
