namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;

public class FromPathAttribute : ParameterLocationAttribute
{
    public FromPathAttribute() : base(ParameterLocation.Path)
    {

    }
}
