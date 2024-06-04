using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism;

public class LevenshteinCalculator
{
	public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
	{
		var result = new List<ComparisonResult>();
		for (int i = 0;i < documents.Count;i++) {
			for(int j = i+1;j < documents.Count; j++)
			{
				result.Add(CompareDocuments(documents[j], documents[i]));
			}
		}
		return result;
	}
	public static ComparisonResult CompareDocuments(DocumentTokens one, DocumentTokens two)
	{
		var curr = new double[one.Count + 1];
		for(int i = 0;i < curr.Length;i++) {
			curr[i] = i;
		}
		for(int i = 1;i <= two.Count; i++)
		{
			var prev = curr;
			curr = new double[one.Count + 1];
			curr[0] = i;
			for(int j = 1;j <= one.Count; j++)
			{
				if (one[j - 1] == two[i-1]) {
					curr[j] = prev[j - 1];
				}
				else curr[j] = Math.Min(TokenDistanceCalculator.GetTokenDistance(one[j - 1], two[i - 1]) + prev[j - 1],
					Math.Min(prev[j], curr[j-1])+1);
			}
		}
		return new ComparisonResult(one, two, curr[one.Count]);
	}
}