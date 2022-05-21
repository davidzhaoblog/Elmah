namespace Elmah.ModelContracts
{
    public interface IElmahError: IElmahErrorIdentifier
    {
        string Application { get; set; }

        string Host { get; set; }

        string Type { get; set; }

        string Source { get; set; }

        string Message { get; set; }

        string User { get; set; }

        int StatusCode { get; set; }

        System.DateTime TimeUtc { get; set; }

        int Sequence { get; set; }

        string AllXml { get; set; }

    }
}

