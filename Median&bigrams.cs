using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
	/// <summary>
	/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
	/// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
	/// </summary>
	/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
	public static double Median(this IEnumerable<double> items)
	{
		var sorted = items.OrderBy(item => item).ToArray();
        if (sorted.Length  == 0)
		throw new InvalidOperationException();
		return sorted.Length %2 ==1? sorted[sorted.Length/2]:
			(sorted[sorted.Length/2]+sorted[sorted.Length/2-1])/2;
	}

	/// <returns>
	/// Возвращает последовательность, состоящую из пар соседних элементов.
	/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
	/// </returns>
	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{
		var flag = true;
		var temp = default(T);
		foreach ( var item in items)
		{
			if (flag)
				flag = false;
			else 
				yield return new ( temp, item);
			temp = item;
		}
	}
}