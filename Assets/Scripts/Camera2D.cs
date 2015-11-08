using System;

/// <summary>
/// The camera 2d class holds data for the current viewport
/// of the world shown in focus.
/// </summary>
public class Camera2D
{
	// Constants
	public const int VIEW_TILES_WIDE = 20;
	public const int VIEW_TILES_HIGH = 18;
	public const int CENTER_TILE_X = 10;
	public const int CENTER_TILE_Y = 8;

	// Reference to the world the camera is attached to
	public World world;

	// The current camera focus (hero position)
	public int focusX;
	public int focusY;

	// The new camera focus (hero position) after transitioning
	private int newFocusX;
	private int newFocusY;

	// Getters and setters for the various coordinates
	public int xMin
	{
		get { return Math.Max(Math.Min(focusX - CENTER_TILE_X, world.tilesWide - VIEW_TILES_WIDE), 0); }
	}
	public int yMin
	{
		get { return Math.Max(Math.Min(focusY - CENTER_TILE_Y, world.tilesHigh - VIEW_TILES_HIGH), 0); }
	}
	public int xMax
	{
		get { return Math.Max(Math.Min(focusX + VIEW_TILES_WIDE - CENTER_TILE_X - 1, world.tilesWide - 1), Math.Min(VIEW_TILES_WIDE - 1, world.tilesWide)); }
	}
	public int yMax
	{
		get { return Math.Max(Math.Min(focusY + VIEW_TILES_HIGH - CENTER_TILE_Y - 1, world.tilesHigh - 1), Math.Min(VIEW_TILES_HIGH - 1, world.tilesHigh)); }
	}
	public int newXMin
	{
		get { return Math.Max(Math.Min(newFocusX - CENTER_TILE_X, world.tilesWide - VIEW_TILES_WIDE), 0); }
	}
	public int newYMin
	{
		get { return Math.Max(Math.Min(newFocusY - CENTER_TILE_Y, world.tilesHigh - VIEW_TILES_HIGH), 0); }
	}
	public int newXMax
	{
		get { return Math.Max(Math.Min(newFocusX + VIEW_TILES_WIDE - CENTER_TILE_X - 1, world.tilesWide - 1), Math.Min(VIEW_TILES_WIDE - 1, world.tilesWide)); }
	}
	public int newYMax
	{
		get { return Math.Max(Math.Min(newFocusY + VIEW_TILES_HIGH - CENTER_TILE_Y - 1, world.tilesHigh - 1), Math.Min(VIEW_TILES_HIGH - 1, world.tilesHigh)); }
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Camera"/> data class.
	/// </summary>
	/// <param name="world">The world the camera is focusing on.</param>
	/// <param name="x">The x coordinate in focus.</param>
	/// <param name="y">The y coordinate in focus.</param>
	public Camera2D(World world, int x, int y)
	{
		this.world = world;
		this.focusX = x;
		this.focusY = y;
	}

	/// <summary>
	/// Transition the camera in the specified direction.
	/// </summary>
	/// <param name="direction">The direction.</param>
	public void transitioning(Direction direction)
	{
		this.newFocusX = this.focusX;
		this.newFocusY = this.focusY;
		switch (direction)
		{
			case Direction.UP:
				if ((this.newFocusY + 1) < world.tilesHigh)
				{
					this.newFocusY += 1;
				}
				break;
			case Direction.DOWN:
				if ((this.newFocusY - 1) >= 0)
				{
					this.newFocusY -= 1;
				}
				break;
			case Direction.LEFT:
				if ((this.newFocusX - 1) >= 0)
				{
					this.newFocusX -= 1;
				}
				break;
			case Direction.RIGHT:
				if ((this.newFocusX + 1) < world.tilesWide)
				{
					this.newFocusX += 1;
				}
				break;
		}
	}

	/// <summary>
	/// Finish the transition updating the current coordinates
	/// of the camera.
	/// </summary>
	public void finishTransition()
	{
		this.focusX = this.newFocusX;
		this.focusY = this.newFocusY;
	}
}