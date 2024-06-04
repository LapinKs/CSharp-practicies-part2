using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public class GreedyPathFinder : IPathFinder
{
	public List<Point> FindPathToCompleteGoal(State state)
	{
        if (state.Goal == 0)
            return new List<Point>();
        var pathFinder = new DijkstraPathFinder();
		var result = new List<Point>();
		var def = state.Chests.Count - state.Goal;
		while (state.Chests.Count >def)
		{
			var path = pathFinder.GetPathsByDijkstra(state, state.Position, state.Chests).FirstOrDefault();
			if (path == default || state.Energy < path.Cost)
				return new List<Point> { };

				result.AddRange(path.Path.Skip(1));
				state.Energy -= path.Cost;
				state.Position = path.End;
 				state.Chests.Remove(path.End);
		}
		return result;
	}
}