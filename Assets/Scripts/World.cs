using UnityEngine;
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
	private bool[] pathingBlocked;
	private int[] lightLevel;

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
		pathingBlocked = new bool[tilesWide * tilesHigh];
		lightLevel = new int[tilesWide * tilesHigh];
		int x = 0;
		int y = 0;
		while (y < tilesHigh)
		{
			x = 0;
			while (x < tilesWide)
			{
				ground[convertIndex(x, y)] = 80;
				overGround[convertIndex(x, y)] = -1;
				wall[convertIndex(x, y)] = -1;
				decorationBase[convertIndex(x, y)] = -1;
				wallTableDecoration[convertIndex(x, y)] = -1;
				roof[convertIndex(x, y)] = -1;
				decorationOverhead[convertIndex(x, y)] = -1;
				pathingBlocked[convertIndex(x, y)] = false;
				lightLevel[convertIndex(x, y)] = 0;
				x++;
			}
			y++;
		}
		wall[convertIndex(5, 5)] = 30;
		wallTableDecoration[convertIndex(5, 5)] = 529;
		pathingBlocked[convertIndex(5, 5)] = true;
		placeLight(5, 5, 13);
	}

	/// <summary>
	/// Returns the tile type at the coordinates on the
	/// specified sorting layer.
	/// </summary>
	/// <returns>The tile type.</returns>
	/// <param name="layer">The sorting layer.</param>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
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
	/// Gets the tint of tiles based on time of day.
	/// </summary>
	/// <returns>The tile tint.</returns>
	/// <param name="turn">The current turn.</param>
	/// <param name="turnsPerDay">Turns per day.</param>
	public float getDayTimeTint(int turn, int turnsPerDay)
	{
		int time = (turn + (turnsPerDay / 4)) % turnsPerDay;
		if ((turnsPerDay / 4) <= time && time <= (3 * (turnsPerDay / 4)))
		{
			return 1f;
		}
		else if (time < (turnsPerDay / 4))
		{
			if (time < (turnsPerDay / 8))
			{
				return 0.4f;
			}
			else
			{
				time = time - (turnsPerDay / 8);
				return 0.4f + (0.6f * (time / (turnsPerDay / 8f)));
			}
		}
		else
		{
			if (time > (7 * (turnsPerDay / 8)))
			{
				return 0.4f;
			}
			else
			{
				time = time - (3 * (turnsPerDay / 4));
				return 1.0f - (0.6f * (time / (turnsPerDay / 8f)));
			}
		}
	}

	/// <summary>
	/// Returns whether the path is blocked at the specified coordinates.
	/// </summary>
	/// <returns><c>true</c>, if path is blocked <c>false</c> otherwise.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	public bool pathBlocked(int x, int y)
	{
		return pathingBlocked[convertIndex(x, y)];
	}

	/// <summary>
	/// Returns the light level at the specified coordinates.
	/// </summary>
	/// <returns>The light level.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	public int getLightLevel(int x, int y)
	{
		return lightLevel[convertIndex(x, y)];
	}

	/// <summary>
	/// Gets the tint of the light of specified brightness.
	/// </summary>
	/// <returns>The tint of the tile.</returns>
	/// <param name="brightness">The brightness level of the light.</param>
	public float getLightTint(int brightness)
	{
		if (brightness < 1)
		{
			return 0;
		}
		else if (brightness > 9)
		{
			return 1;
		}
		else
		{
			return 0.4f + (0.06f * brightness);
		}
	}

	/// <summary>
	/// Gets the tint color of the light of specified brightness.
	/// </summary>
	/// <returns>The tint color of the tile.</returns>
	/// <param name="brightness">The brightness level of the light.</param>
	public Color getLightTintColor(int brightness)
	{
		return new Color(getLightTint(brightness), getLightTint(brightness), getLightTint(brightness), 1);
	}

	/// <summary>
	/// Places a light at the designated coordinates and spreads it.
	/// </summary>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	/// <param name="brightness">The brightness of the light.</param>
	private void placeLight(int x, int y, int brightness)
	{
		if (lightLevel[convertIndex(x, y)] < brightness)
		{
			lightLevel[convertIndex(x, y)] = brightness;
			if (brightness > 1)
			{
				if ((x - 1) >= 0)
				{
					placeLight(x - 1, y, brightness - 1);
				}
				if ((x + 1) < tilesWide)
				{
					placeLight(x + 1, y, brightness - 1);
				}
				if ((y - 1) >= 0)
				{
					placeLight(x, y - 1, brightness - 1);
				}
				if ((y + 1) < tilesHigh)
				{
					placeLight(x, y + 1, brightness - 1);
				}
			}
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
		return (y * tilesWide) + x;
	}
}