using System;

/// <summary>
/// The zone class handles generation of the zones in a world.
/// For instance a 256-by-256 tile world may be broken into 8-by-8
/// zones consisting of 32-by-32 tiles each. Each zone is self
/// contained.
/// </summary>
public class Zone
{
	// Constants
	public const string TOWN = "town";
	public const string FOREST = "forest";

	// Instance variables
	public World world;
	public string name;
	public int offsetX;
	public int offsetY;
	public int tilesWide;
	public int tilesHigh;
	private Tile[] zoneTiles;

	/// <summary>
	/// Initializes a new instance of the <see cref="Zone"/> class.
	/// </summary>
	/// <param name="name">The name of the zone.</param>
	/// <param name="world">Reference to the world.</param>
	/// <param name="offsetX">Zone offset x.</param>
	/// <param name="offsetY">Zone offset y.</param>
	/// <param name="zoneWidth">Zone width.</param>
	/// <param name="zoneHeight">Zone height.</param>
	public Zone(string name, World world, int offsetX, int offsetY, int zoneWidth, int zoneHeight)
	{
		this.name = name;
		this.world = world;
		this.offsetX = offsetX;
		this.offsetY = offsetY;
		this.tilesWide = zoneWidth;
		this.tilesHigh = zoneHeight;
		zoneTiles = new Tile[zoneWidth * zoneHeight];
		for (int y = 0; y < this.tilesHigh; y++)
		{
			for (int x = 0; x < this.tilesWide; x++)
			{
				zoneTiles[convertIndex(x, y)] = world.tiles[convertWorldIndex(x + offsetX, y + offsetY)];
			}
		}
		bool leftBorder = false, rightBorder = false, topBorder = false, bottomBorder = false;
		if (offsetX == 0)
		{
			leftBorder = true;
		}
		if (offsetX + zoneWidth == world.tilesWide)
		{
			rightBorder = true;
		}
		if (offsetY == 0)
		{
			bottomBorder = true;
		}
		if (offsetY + zoneHeight == world.tilesHigh)
		{
			topBorder = true;
		}
		switch (name)
		{
			case Zone.TOWN:
				buildTown(leftBorder, rightBorder, bottomBorder, topBorder);
				break;
			case Zone.FOREST:
				buildForest(leftBorder, rightBorder, bottomBorder, topBorder);
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Builds the zone as a town.
	/// </summary>
	/// <param name="leftBorder">Whether this zone is on the left border.</param>
	/// <param name="rightBorder">Whether this zone is on the right border.</param>
	/// <param name="bottomBorder">Whether this zone is on the bottom border.</param>
	/// <param name="topBorder">Whether this zone is on the top border.</param>
	private void buildTown(bool leftBorder, bool rightBorder, bool bottomBorder, bool topBorder)
	{
		Random rand = new Random();
		int r;
		for (int y = 0; y < this.tilesHigh; y++)
		{
			for (int x = 0; x < this.tilesWide; x++)
			{
				zoneTiles[convertIndex(x, y)].ground = 80;
			}
		}
		int topLeftX = -1, topLeftY = -1, topRightX = -1, topRightY = -1, botLeftX = -1, botLeftY = -1, botRightX = -1, botRightY = -1;
		// Pick a corner to build offset from
		int buildingWidth = 9;
		int buildingHeight = 7;
		string[] corners = new string[4]{"TL", "TR", "BL", "BR"};
		int cornerOffX = rand.Next(1, 5 + 1);
		int cornerOffY = rand.Next(1, 5 + 1);
		r = rand.Next(0, corners.Length);
		string corner = corners[r];
		corners[r] = corners[corners.Length - 1];
		if (corner == "TL")
		{
			cornerOffY = (tilesHigh - 1) - cornerOffY;
			topLeftX = cornerOffX + 4;
			topLeftY = cornerOffY - 7;
		}
		else if (corner == "TR")
		{
			cornerOffX = (tilesWide - 1) - buildingWidth - cornerOffX;
			cornerOffY = (tilesHigh - 1) - cornerOffY;
			topRightX = cornerOffX + 4;
			topRightY = cornerOffY - 7;
		}
		else if (corner == "BL")
		{
			cornerOffY = cornerOffY + buildingHeight;
			botLeftX = cornerOffX + 4;
			botLeftY = cornerOffY - 7;
		}
		else if (corner == "BR")
		{
			cornerOffX = (tilesWide - 1) - buildingWidth - cornerOffX;
			cornerOffY = cornerOffY + buildingHeight;
			botRightX = cornerOffX + 4;
			botRightY = cornerOffY - 7;
		}
		// Build Inn
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY)].Update(decorationOverhead: 105);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 1)].Update(decorationOverhead: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 1)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 1)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 1)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 1)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 1)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 1)].Update(decorationOverhead: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 2)].Update(roof: 24, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 2)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 2)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 2)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 2)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 2)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 2)].Update(roof: 25, decorationOverhead: 104, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 3)].Update(wall: 21, roof: 31, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 3)].Update(wall: 21, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 3)].Update(wall: 43, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 3)].Update(wall: 21, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 3)].Update(wall: 21, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 3)].Update(wall: 21, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 3)].Update(wall: 21, roof: 32, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 7, cornerOffY - 3)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 8, cornerOffY - 3)].Update(decorationOverhead: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 4)].Update(wall: 25, decorationOverhead: 54, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 4)].Update(wall: 26, decorationOverhead: 55, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 4)].Update(wall: 26, decorationOverhead: 55, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 4)].Update(wall: 26, decorationOverhead: 55, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 4)].Update(wall: 26, decorationOverhead: 55, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 4)].Update(wall: 26, decorationOverhead: 55, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 4)].Update(wall: 27, decorationOverhead: 57, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 7, cornerOffY - 4)].Update(wall: 26, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 8, cornerOffY - 4)].Update(wall: 27, roof: 32, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 5)].Update(wall: 25, decorationOverhead: 58, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 5)].Update(wall: 26, wallTableDecoration: 278, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 5)].Update(wall: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 5)].Update(wall: 26, wallTableDecoration: 279, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 5)].Update(wall: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 5)].Update(wall: 26, wallTableDecoration: 279, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 5)].Update(wall: 27, decorationOverhead: 60, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 7, cornerOffY - 5)].Update(wall: 21, wallTableDecoration: 404, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 8, cornerOffY - 5)].Update(wall: 24, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 6)].Update(wall: 22, decorationOverhead: 58, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 6)].Update(wall: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 6)].Update(wall: 21, wallTableDecoration: 529, pathingBlocked: 1);
		world.placeLight(offsetX + cornerOffX + 2, offsetY + cornerOffY - 6, 13);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 6)].Update(wall: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 6)].Update(wall: 21, wallTableDecoration: 233, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 6)].Update(wall: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 6)].Update(wall: 24, wallTableDecoration: 529, decorationOverhead: 60, pathingBlocked: 1);
		world.placeLight(offsetX + cornerOffX + 6, offsetY + cornerOffY - 6, 13);
		// Pick a remaining corner to build offset from
		buildingWidth = 7;
		buildingHeight = 6;
		cornerOffX = rand.Next(1, 5 + 1);
		cornerOffY = rand.Next(1, 5 + 1);
		r = rand.Next(0, corners.Length - 1);
		corner = corners[r];
		corners[r] = corners[corners.Length - 2];
		if (corner == "TL")
		{
			cornerOffY = tilesHigh - cornerOffY;
			topLeftX = cornerOffX + 1;
			topLeftY = cornerOffY - 6;
		}
		else if (corner == "TR")
		{
			cornerOffX = tilesWide - buildingWidth - cornerOffX;
			cornerOffY = tilesHigh - cornerOffY;
			topRightX = cornerOffX + 1;
			topRightY = cornerOffY - 6;
		}
		else if (corner == "BL")
		{
			cornerOffY = cornerOffY + buildingHeight;
			botLeftX = cornerOffX + 1;
			botLeftY = cornerOffY - 6;
		}
		else if (corner == "BR")
		{
			cornerOffX = tilesWide - buildingWidth - cornerOffX;
			cornerOffY = cornerOffY + buildingHeight;
			botRightX = cornerOffX + 1;
			botRightY = cornerOffY - 6;
		}
		// Build Blacksmith
		zoneTiles[convertIndex(cornerOffX, cornerOffY)].Update(decorationOverhead: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY)].Update(decorationOverhead: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY)].Update(roof: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY)].Update(roof: 23, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 1)].Update(roof: 24, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 1)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 1)].Update(roof: 25, decorationOverhead: 100, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 1)].Update(wall: 79, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 1)].Update(roof: 35, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 1)].Update(roof: 36, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 1)].Update(roof: 37, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 2)].Update(wall: 68, roof: 31, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 2)].Update(wall: 67, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 2)].Update(wall: 70, roof: 32, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 2)].Update(wall: 72, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 2)].Update(wall: 71, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 2)].Update(wall: 72, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 2)].Update(wall: 73, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 3)].Update(wall: 71, decorationOverhead: 54, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 3)].Update(wall: 72, decorationOverhead: 55, wallTableDecoration: 576, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 3)].Update(wall: 73, decorationOverhead: 57, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 3)].Update(wall: 67, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 3)].Update(wall: 71, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 3)].Update(wall: 72, wallTableDecoration: 359, pathingBlocked: 1);
		world.placeLight(offsetX + cornerOffX + 5, offsetY + cornerOffY - 3, 15);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 3)].Update(wall: 73, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 4)].Update(wall: 68, decorationOverhead: 58, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 4)].Update(wall: 67, wallTableDecoration: 233, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 4)].Update(wall: 70, decorationOverhead: 60, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 4)].Update(decorationBase: 119, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 4)].Update(wall: 88, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 4)].Update(wall: 67, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 4)].Update(wall: 90, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 5)].Update(overGround: 154);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 5)].Update(decorationBase: 120, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 6, cornerOffY - 5)].Update(decorationBase: 196, pathingBlocked: 1);
		// Pick a remaining corner to build offset from
		buildingWidth = 6;
		buildingHeight = 7;
		cornerOffX = rand.Next(1, 5 + 1);
		cornerOffY = rand.Next(1, 5 + 1);
		r = rand.Next(0, corners.Length - 2);
		corner = corners[r];
		corners[r] = corners[corners.Length - 3];
		if (corner == "TL")
		{
			cornerOffY = tilesHigh - cornerOffY;
			topLeftX = cornerOffX + 4;
			topLeftY = cornerOffY - 7;
		}
		else if (corner == "TR")
		{
			cornerOffX = tilesWide - buildingWidth - cornerOffX;
			cornerOffY = tilesHigh - cornerOffY;
			topRightX = cornerOffX + 4;
			topRightY = cornerOffY - 7;
		}
		else if (corner == "BL")
		{
			cornerOffY = cornerOffY + buildingHeight;
			botLeftX = cornerOffX + 4;
			botLeftY = cornerOffY - 7;
		}
		else if (corner == "BR")
		{
			cornerOffX = tilesWide - buildingWidth - cornerOffX;
			cornerOffY = cornerOffY + buildingHeight;
			botRightX = cornerOffX + 4;
			botRightY = cornerOffY - 7;
		}
		// Build Apothecary/Herbalist
		zoneTiles[convertIndex(cornerOffX, cornerOffY)].Update(decorationOverhead: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY)].Update(decorationOverhead: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 1)].Update(roof: 24, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 1)].Update(roof: 29, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 1)].Update(roof: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 2)].Update(wall: 26, roof: 31, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 2)].Update(wall: 26, roof: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 2)].Update(wall: 26, roof: 32, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 2)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 2)].Update(roof: 22, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 2)].Update(roof: 23, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 3)].Update(wall: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 3)].Update(wall: 41, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 3)].Update(wall: 27, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 3)].Update(roof: 36, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 3)].Update(roof: 36, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 3)].Update(roof: 37, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 4)].Update(wall: 25, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 4)].Update(wall: 26, wallTableDecoration: 317, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 4)].Update(wall: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 4)].Update(wall: 26, wallTableDecoration: 1, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 4)].Update(wall: 26, wallTableDecoration: 529, pathingBlocked: 1);
		world.placeLight(offsetX + cornerOffX + 4, offsetY + cornerOffY - 4, 13);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 4)].Update(wall: 27, wallTableDecoration: 3, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX, cornerOffY - 5)].Update(wall: 42, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 5)].Update(wall: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 5)].Update(wall: 21, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 5)].Update(wall: 43, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 5)].Update(wall: 21, wallTableDecoration: 233, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 5, cornerOffY - 5)].Update(wall: 24, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 1, cornerOffY - 6)].Update(decorationBase: 110, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 2, cornerOffY - 6)].Update(decorationBase: 26, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 3, cornerOffY - 6)].Update(decorationBase: 9, pathingBlocked: 1);
		zoneTiles[convertIndex(cornerOffX + 4, cornerOffY - 6)].Update(overGround: 154);
		// Place roads
		int centerX, topMinY, botMinY;
		if (topLeftY != -1 && topRightY != -1)
		{
			topMinY = Math.Min(topLeftY, topRightY);
		}
		else
		{
			topMinY = Math.Max(topLeftY, topRightY);
		}
		if (botLeftY != -1 && botRightY != -1)
		{
			botMinY = Math.Min(botLeftY, botRightY);
		}
		else
		{
			botMinY = Math.Max(botLeftY, botRightY);
		}
		centerX = World.ZONE_TILES_WIDE / 2;
		if (topLeftX != -1)
		{
			for (int roadY = topLeftY; roadY > topMinY; roadY--)
			{
				zoneTiles[convertIndex(topLeftX, roadY)].Update(overGround: 154);
			}
			zoneTiles[convertIndex(topLeftX, topMinY)].Update(overGround: 148);
			for (int roadX = topLeftX + 1; roadX < centerX; roadX++)
			{
				zoneTiles[convertIndex(roadX, topMinY)].Update(overGround: 155);
			}
		}
		if (topRightX != -1)
		{
			for (int roadY = topRightY; roadY > topMinY; roadY--)
			{
				zoneTiles[convertIndex(topRightX, roadY)].Update(overGround: 154);
			}
			zoneTiles[convertIndex(topRightX, topMinY)].Update(overGround: 149);
			for (int roadX = topRightX - 1; roadX > centerX; roadX--)
			{
				zoneTiles[convertIndex(roadX, topMinY)].Update(overGround: 155);
			}
		}
		if (botLeftX != -1)
		{
			for (int roadY = botLeftY; roadY > botMinY; roadY--)
			{
				zoneTiles[convertIndex(botLeftX, roadY)].Update(overGround: 154);
			}
			zoneTiles[convertIndex(botLeftX, botMinY)].Update(overGround: 148);
			for (int roadX = botLeftX + 1; roadX < centerX; roadX++)
			{
				zoneTiles[convertIndex(roadX, botMinY)].Update(overGround: 155);
			}
		}
		if (botRightX != -1)
		{
			for (int roadY = botRightY; roadY > botMinY; roadY--)
			{
				zoneTiles[convertIndex(botRightX, roadY)].Update(overGround: 154);
			}
			zoneTiles[convertIndex(botRightX, botMinY)].Update(overGround: 149);
			for (int roadX = botRightX - 1; roadX > centerX; roadX--)
			{
				zoneTiles[convertIndex(roadX, botMinY)].Update(overGround: 155);
			}
		}
		if (topLeftX != -1 && topRightX != -1)
		{
			zoneTiles[convertIndex(centerX, topMinY)].Update(overGround: 150);
		}
		else if (topLeftX != -1)
		{
			zoneTiles[convertIndex(centerX, topMinY)].Update(overGround: 147);
		}
		else
		{
			zoneTiles[convertIndex(centerX, topMinY)].Update(overGround: 146);
		}
		if (botLeftX != -1 && botRightX != -1)
		{
			zoneTiles[convertIndex(centerX, botMinY)].Update(overGround: 153);
		}
		else if (botLeftX != -1)
		{
			zoneTiles[convertIndex(centerX, botMinY)].Update(overGround: 149);
		}
		else
		{
			zoneTiles[convertIndex(centerX, botMinY)].Update(overGround: 148);
		}
		for (int roadY = topMinY - 1; roadY > botMinY; roadY--)
		{
			zoneTiles[convertIndex(centerX, roadY)].Update(overGround: 154);
		}
	}

	/// <summary>
	/// Builds the zone as a forest.
	/// </summary>
	/// <param name="leftBorder">Whether this zone is on the left border.</param>
	/// <param name="rightBorder">Whether this zone is on the right border.</param>
	/// <param name="bottomBorder">Whether this zone is on the bottom border.</param>
	/// <param name="topBorder">Whether this zone is on the top border.</param>
	private void buildForest(bool leftBorder, bool rightBorder, bool bottomBorder, bool topBorder)
	{
		Random rand = new Random();
		int r;
		for (int y = 0; y < this.tilesHigh; y++)
		{
			for (int x = 0; x < this.tilesWide; x++)
			{
				if (x == 0 && leftBorder)
				{
					zoneTiles[convertIndex(x, y)].decorationBase = 6;
					zoneTiles[convertIndex(x, y)].pathingBlocked = true;
				}
				else if ((x + 1) == this.tilesWide && rightBorder)
				{
					zoneTiles[convertIndex(x, y)].decorationBase = 6;
					zoneTiles[convertIndex(x, y)].pathingBlocked = true;
				}
				else if (y == 0 && bottomBorder)
				{
					zoneTiles[convertIndex(x, y)].decorationBase = 6;
					zoneTiles[convertIndex(x, y)].pathingBlocked = true;
				}
				else if ((y + 1) == this.tilesHigh && topBorder)
				{
					zoneTiles[convertIndex(x, y)].decorationBase = 6;
					zoneTiles[convertIndex(x, y)].pathingBlocked = true;
				}
				else
				{
					r = rand.Next(0, 100) + 1;
					if (r < 45)
					{
						zoneTiles[convertIndex(x, y)].ground = 80;
					}
					else if (r < 99)
					{
						zoneTiles[convertIndex(x, y)].ground = 81;
					}
					else
					{
						zoneTiles[convertIndex(x, y)].ground = 82;
					}
					r = rand.Next(0, 100) + 1;
					if (r <= 18 && y < this.tilesHigh - 1)
					{
						if (zoneTiles[convertIndex(x, y)].isEmpty)
						{
							zoneTiles[convertIndex(x, y)].decorationBase = 15;
							zoneTiles[convertIndex(x, y)].pathingBlocked = true;
							zoneTiles[convertIndex(x, y + 1)].decorationOverhead = 45;
						}
					}
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
	private int convertWorldIndex(int x, int y)
	{
		return (y * world.tilesWide) + x;
	}

	/// <summary>
	/// Converts the width and height to an array index.
	/// </summary>
	/// <returns>The index inside the zone array.</returns>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	private int convertIndex(int x, int y)
	{
		return (y * tilesWide) + x;
	}
}