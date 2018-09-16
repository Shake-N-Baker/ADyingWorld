using System;

namespace Code.Models
{
	/// <summary>
	/// The tile class holds data for an instance of a tile.
	/// </summary>
	public class Tile
	{
		// Tile sprite types
		public int ground;
		public int overGround;
		public int wall;
		public int decorationBase;
		public int wallTableDecoration;
		public int roof;
		public int decorationOverhead;

		// Tile variables
		public bool pathingBlocked;
		public int lightLevel;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tile"/> class.
		/// </summary>
		/// <param name="ground">Sprite index of ground layer.</param>
		/// <param name="overGround">Sprite index of over ground layer.</param>
		/// <param name="wall">Sprite index of wall layer.</param>
		/// <param name="decorationBase">Sprite index of decoration base layer.</param>
		/// <param name="wallTableDecoration">Sprite index of wall table decoration layer.</param>
		/// <param name="roof">Sprite index of roof layer.</param>
		/// <param name="decorationOverhead">Sprite index of decoration overhead layer.</param>
		/// <param name="pathingBlocked">If set to <c>true</c> pathing blocked.</param>
		/// <param name="lightLevel">Light level.</param>
		public Tile(int ground = -1,
					int overGround = -1,
					int wall = -1,
					int decorationBase = -1,
					int wallTableDecoration = -1,
					int roof = -1,
					int decorationOverhead = -1,
					bool pathingBlocked = false,
					int lightLevel = 0)
		{
			this.ground = ground;
			this.overGround = overGround;
			this.wall = wall;
			this.decorationBase = decorationBase;
			this.wallTableDecoration = wallTableDecoration;
			this.roof = roof;
			this.decorationOverhead = decorationOverhead;
			this.pathingBlocked = pathingBlocked;
			this.lightLevel = lightLevel;
		}

		/// <summary>
		/// Update the tile to the specified parameters, or does not change if -2 is used.
		/// </summary>
		/// <param name="ground">Ground to change to or -2.</param>
		/// <param name="overGround">Over ground to change to or -2.</param>
		/// <param name="wall">Wall to change to or -2.</param>
		/// <param name="decorationBase">Decoration base to change to or -2.</param>
		/// <param name="wallTableDecoration">Wall table decoration to change to or -2.</param>
		/// <param name="roof">Roof to change to or -2.</param>
		/// <param name="decorationOverhead">Decoration overhead to change to or -2.</param>
		/// <param name="pathingBlocked">Pathing blocked if 1, no change if -2, not blocked for anything else.</param>
		/// <param name="lightLevel">Light level to change to or -2.</param>
		public void Update(int ground = -2,
						int overGround = -2,
						int wall = -2,
						int decorationBase = -2,
						int wallTableDecoration = -2,
						int roof = -2,
						int decorationOverhead = -2,
						int pathingBlocked = -2,
						int lightLevel = -2)
		{
			if (ground != -2)
			{
				this.ground = ground;
			}
			if (overGround != -2)
			{
				this.overGround = overGround;
			}
			if (wall != -2)
			{
				this.wall = wall;
			}
			if (decorationBase != -2)
			{
				this.decorationBase = decorationBase;
			}
			if (wallTableDecoration != -2)
			{
				this.wallTableDecoration = wallTableDecoration;
			}
			if (roof != -2)
			{
				this.roof = roof;
			}
			if (decorationOverhead != -2)
			{
				this.decorationOverhead = decorationOverhead;
			}
			if (pathingBlocked != -2)
			{
				if (pathingBlocked == 1)
				{
					this.pathingBlocked = true;
				}
				else
				{
					this.pathingBlocked = false;
				}
			}
			if (lightLevel != -2)
			{
				this.lightLevel = lightLevel;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Tile"/> is empty of anything but
		/// ground and over ground tile data.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool isEmpty
		{
			get { return wall == -1 && decorationBase == -1 && wallTableDecoration == -1 && roof == -1 && decorationOverhead == -1 && !pathingBlocked; }
		}
	}
}