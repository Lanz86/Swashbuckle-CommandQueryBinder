namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Common;
public static class IsNullableExtensions
{
    public static bool IsNullable(this object obj)
    {
        if (obj == null) return true;
        Type type = obj.GetType();
        if (!type.IsValueType) return true;
        if (Nullable.GetUnderlyingType(type) != null) return true;
        return false;
    }

    public static bool IsNullable<T>(this T obj)
    {
        if (obj == null) return true; 
        Type type = typeof(T);
        if (!type.IsValueType) return true; 
        if (Nullable.GetUnderlyingType(type) != null) return true; 
        return false; 
    }

    public static bool IsNullable<T>()
    {
        Type type = typeof(T);
        if (!type.IsValueType) return true; 
        if (Nullable.GetUnderlyingType(type) != null) return true; 
        return false; 
    }
}
