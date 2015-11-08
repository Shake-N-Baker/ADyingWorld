using UnityEngine;
using System;

/// <summary>
/// The character class holds data for and displays an instance
/// of a character.
/// </summary>
public class Character
{
	// Character Sprite Sheets
	public static Sprite[] charBase;
	public static Sprite[] charHair;
	public static Sprite[] charHead;
	public static Sprite[] charLegs;
	public static Sprite[] charShield;
	public static Sprite[] charTorso;
	public static Sprite[] charWeapon;

	// Display Components
	public GameObject container;
	public SpriteRenderer baseComponent;
	public SpriteRenderer hairComponent;
	public SpriteRenderer legsComponent;
	public SpriteRenderer torsoComponent;
	public SpriteRenderer headComponent;
	public SpriteRenderer shieldComponent;
	public SpriteRenderer weaponComponent;
	private bool _visible;
	public bool visible
	{
		get
		{
			return _visible;
		}
		set
		{
			if (value)
			{
				if (!_visible)
				{
					_visible = true;
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
					if (baseType != -1)
					{
						baseComponent.sprite = charBase[baseType];
					}
					else
					{
						baseComponent.sprite = null;
					}
				}
			}
			else
			{
				if (_visible)
				{
					_visible = false;
					Game.RemoveObject(container);
				}
			}
		}
	}

	// Data Components
	public int baseType;
	public int hairType;
	public int legsType;
	public int torsoType;
	public int headType;
	public int shieldType;
	public int weaponType;

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
	}

	/// <summary>
	/// Update the character to the specified parameters, or does not change if -2 is used.
	/// </summary>
	/// <param name="baseType">Base type to change to or -2.</param>
	/// <param name="hairType">Hair type to change to or -2.</param>
	/// <param name="legsType">Legs type to change to or -2.</param>
	/// <param name="torsoType">Torso type to change to or -2.</param>
	/// <param name="headType">Head type to change to or -2.</param>
	/// <param name="shieldType">Shield type to change to or -2.</param>
	/// <param name="weaponType">Weapon type to change to or -2.</param>
	public void Update(int baseType = -2,
	                   int hairType = -2,
	                   int legsType = -2,
	                   int torsoType = -2,
	                   int headType = -2,
	                   int shieldType = -2,
	                   int weaponType = -2)
	{
		if (baseType != -2)
		{
			this.baseType = baseType;
		}
		if (hairType != -2)
		{
			this.hairType = hairType;
		}
		if (legsType != -2)
		{
			this.legsType = legsType;
		}
		if (torsoType != -2)
		{
			this.torsoType = torsoType;
		}
		if (headType != -2)
		{
			this.headType = headType;
		}
		if (shieldType != -2)
		{
			this.shieldType = shieldType;
		}
		if (weaponType != -2)
		{
			this.weaponType = weaponType;
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
}