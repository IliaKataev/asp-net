
namespace Kino_Pesochnisa_16._06._25
{
    public class EvenConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value) && int.TryParse(value?.ToString(), out int number))
            {
                return number % 2 == 0;
            }
            return false;
        }
    }
}
