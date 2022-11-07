namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;

public class FromHeaderAttribute : ParameterLocationAttribute
{
    public FromHeaderAttribute() : base(ParameterLocation.Header)
    {

    }
}
