using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
		double result = double.NaN;
		foreach (var point in data) {
			result = double.IsNaN(result)?point.OriginalY: alpha*point.OriginalY + result *(1-alpha);
			yield return point.WithExpSmoothedY(result);
		}
	}
}