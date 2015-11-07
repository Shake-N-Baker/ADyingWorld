using System;

/// <summary>
/// The character class holds data for an instance of a character.
/// </summary>
public class Character
{
	// Components
	private int baseComponent;
	private int hairComponent;
	private int legsComponent;
	private int torsoComponent;
	private int headComponent;
	private int shieldComponent;
	private int weaponComponent;

	// World coordinates
	public World world;
	public int x;
	public int y;

	/// <summary>
	/// Initializes a new instance of the <see cref="Character"/> class.
	/// </summary>
	/// <param name="world">World character currently resides in.</param>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	public Character(World world, int x, int y)
	{
		this.world = world;
		this.x = x;
		this.y = y;
		baseComponent = -1;
		hairComponent = -1;
		legsComponent = -1;
		torsoComponent = -1;
		headComponent = -1;
		shieldComponent = -1;
		weaponComponent = -1;
	}

	/// <summary>
	/// Update the character to the specified parameters, or does not change if -2 is used.
	/// </summary>
	/// <param name="baseComponent">Base component to change to or -2.</param>
	/// <param name="hairComponent">Hair component to change to or -2.</param>
	/// <param name="legsComponent">Legs component to change to or -2.</param>
	/// <param name="torsoComponent">Torso component to change to or -2.</param>
	/// <param name="headComponent">Head component to change to or -2.</param>
	/// <param name="shieldComponent">Shield component to change to or -2.</param>
	/// <param name="weaponComponent">Weapon component to change to or -2.</param>
	public void Update(int baseComponent = -2,
	                   int hairComponent = -2,
	                   int legsComponent = -2,
	                   int torsoComponent = -2,
	                   int headComponent = -2,
	                   int shieldComponent = -2,
	                   int weaponComponent = -2)
	{
		if (baseComponent != -2)
		{
			this.baseComponent = baseComponent;
		}
		if (hairComponent != -2)
		{
			this.hairComponent = hairComponent;
		}
		if (legsComponent != -2)
		{
			this.legsComponent = legsComponent;
		}
		if (torsoComponent != -2)
		{
			this.torsoComponent = torsoComponent;
		}
		if (headComponent != -2)
		{
			this.headComponent = headComponent;
		}
		if (shieldComponent != -2)
		{
			this.shieldComponent = shieldComponent;
		}
		if (weaponComponent != -2)
		{
			this.weaponComponent = weaponComponent;
		}
	}
}