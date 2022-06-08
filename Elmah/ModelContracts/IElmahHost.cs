namespace Elmah.ModelContracts
{
    public interface IElmahHost: IElmahHostIdentifier
    {
        Microsoft.Spatial.Geography SpatialLocation { get; set; }
    }
}

