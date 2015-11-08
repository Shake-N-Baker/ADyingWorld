using UnityEngine;
using System;

/// <summary>
/// The character class holds data for and displays an instance
/// of a character.
/// </summary>
public class Character
{
	// Display Components
	public GameObject container;
	public SpriteRenderer baseComponent;
	public SpriteRenderer hairComponent;
	public SpriteRenderer legsComponent;
	public SpriteRenderer torsoComponent;
	public SpriteRenderer headComponent;
	public SpriteRenderer shieldComponent;
	public SpriteRenderer weaponComponent;
	public bool visible;

	// Data Components
	private int baseType;
	private int hairType;
	private int legsType;
	private int torsoType;
	private int headType;
	private int shieldType;
	private int weaponType;

	// World coordinates
	public World world;
	public int x;
	public int y;

	// New coordinates after transitioning
	public int newX;
	public int newY;

	/// <summary>
	/// Initializes a new instance of the <see cref="Character"/> class.
	/// </summary>
	/// <param name="world">World character currently resides in.</param>
	/// <param name="x">The tile x position.</param>
	/// <param name="y">The tile y position.</param>
	public Character(World world, int x, int y)
	{
		this.world = world;
		this.x = this.newX = x;
		this.y = this.newY = y;
		baseType = -1;
		hairType = -1;
		legsType = -1;
		torsoType = -1;
		headType = -1;
		shieldType = -1;
		weaponType = -1;
		display();
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
			this.baseType = baseComponent;
		}
		if (hairComponent != -2)
		{
			this.hairType = hairComponent;
		}
		if (legsComponent != -2)
		{
			this.legsType = legsComponent;
		}
		if (torsoComponent != -2)
		{
			this.torsoType = torsoComponent;
		}
		if (headComponent != -2)
		{
			this.headType = headComponent;
		}
		if (shieldComponent != -2)
		{
			this.shieldType = shieldComponent;
		}
		if (weaponComponent != -2)
		{
			this.weaponType = weaponComponent;
		}
	}

	/// <summary>
	/// Finishs the transition updating the current coordinates
	/// of the character.
	/// </summary>
	public void finishTransition()
	{
		this.x = this.newX;
		this.y = this.newY;
	}

	private void display()
	{
		visible = true;
		container = new GameObject();
		container.name = "CharContainer";
		GameObject charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharWeapon";
		charComponent.transform.parent = container.transform;
		weaponComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharShield";
		charComponent.transform.parent = container.transform;
		shieldComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHead";
		charComponent.transform.parent = container.transform;
		headComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharTorso";
		charComponent.transform.parent = container.transform;
		torsoComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharLegs";
		charComponent.transform.parent = container.transform;
		legsComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHair";
		charComponent.transform.parent = container.transform;
		hairComponent = charComponent.GetComponent<SpriteRenderer>();
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = null;
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharBase";
		charComponent.transform.parent = container.transform;
		baseComponent = charComponent.GetComponent<SpriteRenderer>();
	}
}