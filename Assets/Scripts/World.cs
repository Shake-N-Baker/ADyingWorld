using UnityEngine;
using System;

/// <summary>
/// The world class manages the world data.
/// </summary>
public class World
{
	// Constants
	public const int MAP_TILES_WIDE = 256;
	public const int MAP_TILES_HIGH = 256;
	public const int ZONE_TILES_WIDE = 32;
	public const int ZONE_TILES_HIGH = 32;

	// Instance variables
	public int tilesWide;
	public int tilesHigh;
	public Tile[] tiles;
	public Zone[] zones;
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
		tilesWide = MAP_TILES_WIDE;
		tilesHigh = MAP_TILES_HIGH;
		tiles = new Tile[tilesWide * tilesHigh];
		for (int y = 0; y < tilesHigh; y++)
		{
			for (int x = 0; x < tilesWide; x++)
			{
				tiles[convertIndex(x, y)] = new Tile(80);
			}
		}

		int zonesWide = MAP_TILES_WIDE / ZONE_TILES_WIDE;
		int zonesHigh = MAP_TILES_HIGH / ZONE_TILES_HIGH;
		zones = new Zone[zonesWide * zonesHigh];
		System.Random rand = new System.Random();
		int townZoneX = rand.Next(0, zonesWide / 2) + (zonesWide / 4);
		int townZoneY = rand.Next(0, zonesHigh / 2) + (zonesHigh / 4);
		spawnX = (townZoneX * ZONE_TILES_WIDE) + (ZONE_TILES_WIDE / 2);
		spawnY = (townZoneY * ZONE_TILES_HIGH) + (ZONE_TILES_HIGH / 2);
		for (int y = 0; y < zonesHigh; y++)
		{
			for (int x = 0; x < zonesWide; x++)
			{
				if (x == townZoneX && y == townZoneY)
				{
					zones[(y * zonesWide) + x] = new Zone(Zone.TOWN, this, x * ZONE_TILES_WIDE, y * ZONE_TILES_HIGH, ZONE_TILES_WIDE, ZONE_TILES_HIGH);
				}
				else
				{
					zones[(y * zonesWide) + x] = new Zone(Zone.FOREST, this, x * ZONE_TILES_WIDE, y * ZONE_TILES_HIGH, ZONE_TILES_WIDE, ZONE_TILES_HIGH);
				}
			}
		}
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
				tile = tiles[convertIndex(x, y)].ground;
				break;
			case "OverGround":
				tile = tiles[convertIndex(x, y)].overGround;
				break;
			case "Wall":
				tile = tiles[convertIndex(x, y)].wall;
				break;
			case "DecorationBase":
				tile = tiles[convertIndex(x, y)].decorationBase;
				break;
			case "WallTableDecoration":
				tile = tiles[convertIndex(x, y)].wallTableDecoration;
				break;
			case "Roof":
				tile = tiles[convertIndex(x, y)].roof;
				break;
			case "DecorationOverhead":
				tile = tiles[convertIndex(x, y)].decorationOverhead;
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
		return tiles[convertIndex(x, y)].pathingBlocked;
	}

	/// <summary>
	/// Returns the light level at the specified coordinates.
	/// </summary>
	/// <returns>The light level.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	public int getLightLevel(int x, int y)
	{
		return tiles[convertIndex(x, y)].lightLevel;
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
	public void placeLight(int x, int y, int brightness)
	{
		if (tiles[convertIndex(x, y)].lightLevel < brightness)
		{
			tiles[convertIndex(x, y)].lightLevel = brightness;
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
	/// Changes the sprite for animation tiles.
	/// </summary>
	public void changeTileAnimations()
	{
		for (int i = 0; i < tiles.Length; i++)
		{
			switch (tiles[i].wallTableDecoration)
			{
				case 359:
					tiles[i].wallTableDecoration = 360;
					break;
				case 360:
					tiles[i].wallTableDecoration = 359;
					break;
				case 529:
					tiles[i].wallTableDecoration = 530;
					break;
				case 530:
					tiles[i].wallTableDecoration = 529;
					break;
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