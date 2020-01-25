namespace Vici8145Lib
{
    public enum NonRespondingCommands
    {
        AutoRange = 0,
        Range,
        SecondView,
        EndHold,
        Hold,
        EndRel,
        Rel,
        EndMinMax,
        MinMax,
        DisableInterface,
        Timer,
        Select
    }

    public enum RespondingCommands
    {
        MainDisplayValue = 0,
        AnalogeBarValue,
        SecondDisplayValue
    }

    public enum EntitiesEnum
    {
        Voltage,
        Resistance,
        Capacity,
        Frequency,
        Diode,
        Temp,
        // ReSharper disable once UnusedMember.Global
        Conductance,
        Current,
        Generator
    }

    public enum PrefixEnum
    {
        Pica,
        Nano,
        Micro,
        Milli,
        Kilo,
        Mega,
        None
    }

}