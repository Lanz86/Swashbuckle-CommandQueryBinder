namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;

public class FromQueryAttribute : ParameterLocationAttribute
{
    public FromQueryAttribute() : base(ParameterLocation.Query)
    {

    }
}
