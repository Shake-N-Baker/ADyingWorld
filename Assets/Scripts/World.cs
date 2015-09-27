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
		ground = new int[MAP_TILES_WIDE * MAP_TILES_HIGH];
		overGround = new int[MAP_TILES_WIDE * MAP_TILES_HIGH];
		int x = 0;
		int y = 0;
		while (y < MAP_TILES_HIGH)
		{
			x = 0;
			while (x < MAP_TILES_WIDE)
			{
				ground[convertIndex(x, y)] = 1;
				overGround[convertIndex(x, y)] = 1;
				x++;
			}
			y++;
		}
	}

	/// <summary>
	/// Converts the width and height to an array index.
	/// </summary>
	/// <returns>The index inside the world array.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	private int convertIndex(int x, int y)
	{
		return (y * MAP_TILES_WIDE) + x;
	}
}