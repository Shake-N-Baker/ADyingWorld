using System;

/// <summary>
/// The world class manages the world data.
/// </summary>
public class World
{
	public const int MAP_TILES_WIDE = 256;
	public const int MAP_TILES_HIGH = 256;
	private int[] ground;
	private int[] overGround;
	private int[] wall;
	private int[] decorationBase;
	private int[] wallTableDecoration;
	private int[] roof;
	private int[] decorationOverhead;

	public int tilesWide;
	public int tilesHigh;
	public int spawnX;
	public int spawnY;

	/// <summary>
	/// Initializes a new instance of the <see cref="AssemblyCSharp.World"/> class.
	/// </summary>
	public World()
	{
		createWorld();
	}

	/// <summary>
	/// Creates the world.
	/// </summary>
	public void createWorld()
	{
		spawnX = 0;
		spawnY = 0;
		tilesWide = MAP_TILES_WIDE;
		tilesHigh = MAP_TILES_HIGH;
		ground = new int[tilesWide * tilesHigh];
		overGround = new int[tilesWide * tilesHigh];
		wall = new int[tilesWide * tilesHigh];
		decorationBase = new int[tilesWide * tilesHigh];
		wallTableDecoration = new int[tilesWide * tilesHigh];
		roof = new int[tilesWide * tilesHigh];
		decorationOverhead = new int[tilesWide * tilesHigh];
		int x = 0;
		int y = 0;
		Random random = new Random();
		while (y < tilesHigh)
		{
			x = 0;
			while (x < tilesWide)
			{
				if (random.Next(0, 2) > 0)
				{
					ground[convertIndex(x, y)] = 80;
				}
				else
				{
					ground[convertIndex(x, y)] = 82;
				}
				overGround[convertIndex(x, y)] = -1;
				wall[convertIndex(x, y)] = -1;
				decorationBase[convertIndex(x, y)] = -1;
				wallTableDecoration[convertIndex(x, y)] = -1;
				roof[convertIndex(x, y)] = -1;
				decorationOverhead[convertIndex(x, y)] = -1;
				x++;
			}
			y++;
		}
	}

	/// <summary>
	/// Returns the tile type at the coordinates on the
	/// specified sorting layer.
	/// </summary>
	/// <returns>The tile type.</returns>
	/// <param name="layer">The sorting layer.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public int getTile(String layer, int x, int y)
	{
		int tile = -1;
		if (x < 0 || y < 0 || x >= tilesWide || y >= tilesHigh)
		{
			return tile;
		}
		switch (layer)
		{
			case "Ground":
				tile = ground[convertIndex(x, y)];
				break;
			case "OverGround":
				tile = overGround[convertIndex(x, y)];
				break;
			case "Wall":
				tile = wall[convertIndex(x, y)];
				break;
			case "DecorationBase":
				tile = decorationBase[convertIndex(x, y)];
				break;
			case "WallTableDecoration":
				tile = wallTableDecoration[convertIndex(x, y)];
				break;
			case "Roof":
				tile = roof[convertIndex(x, y)];
				break;
			case "DecorationOverhead":
				tile = decorationOverhead[convertIndex(x, y)];
				break;
		}
		return tile;
	}

	/// <summary>
	/// Converts the width and height to an array index.
	/// </summary>
	/// <returns>The index inside the world array.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	private int convertIndex(int x, int y)
	{
		return (y * tilesWide) + x;
	}
}