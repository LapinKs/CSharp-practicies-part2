using System;
using System.Collections.Generic;
using System.Linq;


namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var paths = new Dictionary<Point, SinglyLinkedList<Point>>() ;
		var visited = new HashSet<Point>() { start};
		var queue = new Queue<Point>();

		paths.Add(start, new SinglyLinkedList<Point>(start));
		queue.Enqueue(start);
		while (queue.Count != 0)
		{
			var way = queue.Dequeue();
			
			if (way.X < 0|| way.Y<0||way.X>=map.Dungeon.GetLength(0)
				||way.Y>=map.Dungeon.GetLength(1)||map.Dungeon[way.X, way.Y] != MapCell.Empty ) continue;
            for(int x = -1; x < 2; x++)
			{
				for(int y = -1; y < 2; y++)
				{
					if (x == 0 || y == 0){
						var point = new Point{X = way.X + x,Y = way.Y + y};
						if (visited.Contains(point)) continue;
						queue.Enqueue(point);
						visited.Add(point);
						paths.Add(point, new SinglyLinkedList<Point>(point, paths[way]));
					}
					else continue;
				}
			}
        }
		foreach (var e in chests)
            if (paths.ContainsKey(e)) yield return paths[e];
            
    }
}