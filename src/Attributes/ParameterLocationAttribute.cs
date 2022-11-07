namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;

public class ParameterLocationAttribute : Attribute
{
    public ParameterLocationAttribute(ParameterLocation parameterLocation)
    {
        ParameterLocation = parameterLocation;
    }

    public ParameterLocation ParameterLocation { get; set; }
}
