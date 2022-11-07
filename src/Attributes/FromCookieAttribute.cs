namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Attributes;

public class FromCookieAttribute : ParameterLocationAttribute
{
	public FromCookieAttribute() : base(ParameterLocation.Cookie)
	{

	}
}
