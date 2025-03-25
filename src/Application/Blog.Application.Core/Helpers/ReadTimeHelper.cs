using ReadTimeEstimator.Implementations.Estimators;

namespace Blog.Application.Core.Helpers;

public static class ReadTimeHelper
{
    public static int GetReadTimeMinutes(string value)
    {
        var estimator = new HtmlEstimator();
        return (int)Math.Ceiling(estimator.ReadTimeInMinutes(value));
    }
}