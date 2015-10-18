using UnityEngine;
using System.Collections;

/// <summary>
/// The class for running the game.
/// </summary>
public class Game : MonoBehaviour
{
	// Constants
	public const int VIEW_TILES_WIDE = 20;
	public const int VIEW_TILES_HIGH = 18;
	public const int CENTER_TILE_X = 10;
	public const int CENTER_TILE_Y = 8;
	public const int TURNS_PER_DAY = 360;

	// Tile Sprite Sheets
	public Sprite[] tileGround;
	public Sprite[] tileOverGround;
	public Sprite[] tileWall;
	public Sprite[] tileDecorationBase;
	public Sprite[] tileWallTableDecoration;
	public Sprite[] tileRoof;
	public Sprite[] tileDecorationOverhead;
	public Sprite[][] tileLayer;

	// Character Sprite Sheets
	public Sprite[] charBase;
	public Sprite[] charHair;
	public Sprite[] charHead;
	public Sprite[] charLegs;
	public Sprite[] charShield;
	public Sprite[] charTorso;
	public Sprite[] charWeapon;

	// Game Objects
	public World world;
	public GameObject board;
	public GameObject[,,] tiles;
	public GameObject hero;

	// Game Variables
	public string[] layers;
	public int heroX;
	public int heroY;
	public int turnsTaken;

	// Debug Variables
	public bool debugOn = false;
	public Color debugPathBlockColor = new Color(1, 0.6f, 0.6f, 1);

	/// <summary>
	/// Awake this instance, resources are loaded at this point.
	/// </summary>
	void Awake()
	{
		tileGround = Resources.LoadAll<Sprite>("tileGround");
		tileOverGround = Resources.LoadAll<Sprite>("tileOverGround");
		tileWall = Resources.LoadAll<Sprite>("tileWall");
		tileDecorationBase = Resources.LoadAll<Sprite>("tileDecorationBase");
		tileWallTableDecoration = Resources.LoadAll<Sprite>("tileWallTableDecoration");
		tileRoof = Resources.LoadAll<Sprite>("tileRoof");
		tileDecorationOverhead = Resources.LoadAll<Sprite>("tileDecorationOverhead");
		tileLayer = new Sprite[7][];
		tileLayer[0] = tileGround;
		tileLayer[1] = tileOverGround;
		tileLayer[2] = tileWall;
		tileLayer[3] = tileDecorationBase;
		tileLayer[4] = tileWallTableDecoration;
		tileLayer[5] = tileRoof;
		tileLayer[6] = tileDecorationOverhead;
		charBase = Resources.LoadAll<Sprite>("charBase");
		charHair = Resources.LoadAll<Sprite>("charHair");
		charHead = Resources.LoadAll<Sprite>("charHead");
		charLegs = Resources.LoadAll<Sprite>("charLegs");
		charShield = Resources.LoadAll<Sprite>("charShield");
		charTorso = Resources.LoadAll<Sprite>("charTorso");
		charWeapon = Resources.LoadAll<Sprite>("charWeapon");
	}

	/// <summary>
	/// Start the game, this is used for initialization.
	/// </summary>
	void Start()
	{
		world = new World();
		turnsTaken = 0;

		hero = new GameObject();
		heroX = world.spawnX;
		heroY = world.spawnY;
		hero.name = "Hero";
		GameObject charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charWeapon[Random.Range(0, charWeapon.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharWeapon";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charShield[Random.Range(0, charShield.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharShield";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charHead[Random.Range(0, charHead.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHead";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charTorso[Random.Range(0, charTorso.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharTorso";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charLegs[Random.Range(0, charLegs.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharLegs";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charHair[Random.Range(0, charHair.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHair";
		charComponent.transform.parent = hero.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charBase[Random.Range(0, charBase.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharBase";
		charComponent.transform.parent = hero.transform;

		layers = new string[]{
			"Ground",
			"OverGround",
			"Wall",
			"DecorationBase",
			"WallTableDecoration",
			"Roof",
			"DecorationOverhead"
		};

		// Setup the board
		board = new GameObject();
		board.name = "Board";
		tiles = new GameObject[layers.Length, VIEW_TILES_WIDE + 2, VIEW_TILES_HIGH + 2];

		// Add tiles for each layer on the main screen and just off screen for transitions
		for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
		{
			string layer = layers[layerIndex];
			for (int x = -1; x <= VIEW_TILES_WIDE; x++)
			{
				for (int y = -1; y <= VIEW_TILES_HIGH; y++)
				{
					// Don't tile the corners off screen
					if ((x == -1 || x == VIEW_TILES_WIDE) && (y == -1 || y == VIEW_TILES_HIGH))
					{
						continue;
					}
					GameObject tile = new GameObject();
					tile.AddComponent<SpriteRenderer>();
					tile.GetComponent<SpriteRenderer>().sortingLayerName = layer;
					tile.transform.position = new Vector3(x * 16, (y + 1) * 16, 0);
					tile.name = layer + "X-" + (x + 1) + "Y-" + (y + 1);
					tile.transform.parent = board.transform;
					tiles[layerIndex, x + 1, y + 1] = tile;
				}
			}
		}

		updateHeroPosition();
		drawWorld();
	}

	/// <summary>
	/// Update the game, this called once per frame.
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			debugOn = !debugOn;
			drawWorld();
		}

		if (Input.GetKey(KeyCode.W))
		{
			int newHeroY = Mathf.Min(world.tilesHigh - 1, heroY + 1);
			if (!world.pathBlocked(heroX, newHeroY))
			{
				heroY = newHeroY;
				updateHeroPosition();
				drawWorld();
			}
		}
		if (Input.GetKey(KeyCode.S))
		{
			int newHeroY = Mathf.Max(0, heroY - 1);
			if (!world.pathBlocked(heroX, newHeroY))
			{
				heroY = newHeroY;
				updateHeroPosition();
				drawWorld();
			}
		}
		if (Input.GetKey(KeyCode.A))
		{
			int newHeroX = Mathf.Max(0, heroX - 1);
			if (!world.pathBlocked(newHeroX, heroY))
			{
				heroX = newHeroX;
				updateHeroPosition();
				drawWorld();
			}
		}
		if (Input.GetKey(KeyCode.D))
		{
			int newHeroX = Mathf.Min(world.tilesWide - 1, heroX + 1);
			if (!world.pathBlocked(newHeroX, heroY))
			{
				heroX = newHeroX;
				updateHeroPosition();
				drawWorld();
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			GameObject component = GameObject.Find("CharBase");
			component.GetComponent<SpriteRenderer>().sprite = charBase[Random.Range(0, charBase.Length)];
			component = GameObject.Find("CharHair");
			component.GetComponent<SpriteRenderer>().sprite = charHair[Random.Range(0, charHair.Length)];
			component = GameObject.Find("CharLegs");
			component.GetComponent<SpriteRenderer>().sprite = charLegs[Random.Range(0, charLegs.Length)];
			component = GameObject.Find("CharTorso");
			component.GetComponent<SpriteRenderer>().sprite = charTorso[Random.Range(0, charTorso.Length)];
			component = GameObject.Find("CharHead");
			component.GetComponent<SpriteRenderer>().sprite = charHead[Random.Range(0, charHead.Length)];
			component = GameObject.Find("CharShield");
			component.GetComponent<SpriteRenderer>().sprite = (Random.Range(0, 2) == 1 ? charShield[Random.Range(0, charShield.Length)] : null);
			component = GameObject.Find("CharWeapon");
			component.GetComponent<SpriteRenderer>().sprite = (Random.Range(0, 2) == 1 ? charWeapon[Random.Range(0, charWeapon.Length)] : null);
		}
	}

	/// <summary>
	/// Updates the hero position.
	/// </summary>
	void updateHeroPosition()
	{
		turnsTaken++;
		int x = CENTER_TILE_X;
		int y = CENTER_TILE_Y;
		if (world.tilesWide >= VIEW_TILES_WIDE)
		{
			if (heroX < CENTER_TILE_X)
			{
				x = heroX;
			}
			else if (heroX > ((world.tilesWide - VIEW_TILES_WIDE) + CENTER_TILE_X))
			{
				x = CENTER_TILE_X + (heroX - ((world.tilesWide - VIEW_TILES_WIDE) + CENTER_TILE_X));
			}
		}
		else
		{
			x = ((VIEW_TILES_WIDE - world.tilesWide) / 2) + heroX;
		}
		if (world.tilesHigh >= VIEW_TILES_HIGH)
		{
			if (heroY < CENTER_TILE_Y)
			{
				y = heroY;
			}
			else if (heroY > ((world.tilesHigh - VIEW_TILES_HIGH) + CENTER_TILE_Y))
			{
				y = CENTER_TILE_Y + (heroY - ((world.tilesHigh - VIEW_TILES_HIGH) + CENTER_TILE_Y));
			}
		}
		else
		{
			y = ((VIEW_TILES_HIGH - world.tilesHigh) / 2) + heroY;
		}
		hero.transform.position = new Vector3(16 * x, 16 * y, 0);
	}

	/// <summary>
	/// Draws the world.
	/// </summary>
	void drawWorld()
	{
		float tint = world.getDayTimeTint(turnsTaken, TURNS_PER_DAY);
		Color tintColor = new Color(tint, tint, tint, 1);
		int cameraOffX, cameraOffY;
		if (world.tilesWide >= VIEW_TILES_WIDE)
		{
			cameraOffX = Mathf.Min(Mathf.Max(heroX - CENTER_TILE_X, 0), world.tilesWide - VIEW_TILES_WIDE);
		}
		else
		{
			cameraOffX = -(VIEW_TILES_WIDE - world.tilesWide) / 2;
		}
		if (world.tilesHigh >= VIEW_TILES_HIGH)
		{
			cameraOffY = Mathf.Min(Mathf.Max(heroY - CENTER_TILE_Y, 0), world.tilesHigh - VIEW_TILES_HIGH);
		}
		else
		{
			cameraOffY = -(VIEW_TILES_HIGH - world.tilesHigh) / 2;
		}
		for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
		{
			string layer = layers[layerIndex];
			for (int x = -1; x <= VIEW_TILES_WIDE; x++)
			{
				for (int y = -1; y <= VIEW_TILES_HIGH; y++)
				{
					// Skip the corners off screen
					if ((x == -1 || x == VIEW_TILES_WIDE) && (y == -1 || y == VIEW_TILES_HIGH))
					{
						continue;
					}
					int tileType = world.getTile(layer, cameraOffX + x, cameraOffY + y);
					GameObject tile = tiles[layerIndex, x + 1, y + 1];
					if (tileType != -1)
					{
						int lightLevel = world.getLightLevel(cameraOffX + x, cameraOffY + y);
						tile.GetComponent<SpriteRenderer>().sprite = tileLayer[layerIndex][tileType];

						bool pathBlock = world.pathBlocked(cameraOffX + x, cameraOffY + y);
						if (pathBlock && debugOn)
						{
							tile.GetComponent<SpriteRenderer>().material.color = debugPathBlockColor;
						}
						else
						{
							if (lightLevel > 0 && world.getLightTint(lightLevel) > tint)
							{
								tile.GetComponent<SpriteRenderer>().material.color = world.getLightTintColor(lightLevel);
							}
							else
							{
								tile.GetComponent<SpriteRenderer>().material.color = tintColor;
							}
						}
					}
					else
					{
						tile.GetComponent<SpriteRenderer>().sprite = null;
					}
				}
			}
		}
	}
}