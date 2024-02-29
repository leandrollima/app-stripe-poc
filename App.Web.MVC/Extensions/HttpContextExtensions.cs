namespace App.Web.MVC.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUrl(this HttpContext context, string path)
        {
            return $"{context.Request.Scheme}://{context.Request.Host.Value}/{path}";
        }
    }
}
