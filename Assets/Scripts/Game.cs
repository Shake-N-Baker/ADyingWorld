using UnityEngine;
using System.Collections;

public enum Direction
{
	NONE,
	UP,
	RIGHT,
	DOWN,
	LEFT
}

/// <summary>
/// The class for running the game.
/// </summary>
public class Game : MonoBehaviour
{
	// Constants
	public const int TURNS_PER_DAY = 360;
	public const float MOVING_TRANSITION_TIME = 0.2f;
	public const float ANIMATE_TILE_TIME = 0.25f;

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
	public CharacterDisplay heroChar;

	// Game Variables
	public string[] layers;
	public int heroX;
	public int heroY;
	public Camera camera;
	public int smallWorldDrawOffsetX;
	public int smallWorldDrawOffsetY;
	public int turnsTaken;
	public float changeTileAnimateTimeLeft;
	// Moving transition variables
	public bool transitioning = false;
	public float transitionTimeLeft;
	public Direction transitionDirection;

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
		camera = new Camera(world, world.spawnX, world.spawnY);
		smallWorldDrawOffsetX = (Camera.VIEW_TILES_WIDE - world.tilesWide) / 2;
		smallWorldDrawOffsetY = (Camera.VIEW_TILES_HIGH - world.tilesHigh) / 2;
		if (smallWorldDrawOffsetX < 0)
		{
			smallWorldDrawOffsetX = 0;
		}
		if (smallWorldDrawOffsetY < 0)
		{
			smallWorldDrawOffsetY = 0;
		}
		turnsTaken = 0;

		hero = new GameObject();
		heroX = world.spawnX;
		heroY = world.spawnY;
		hero.name = "Hero";
		heroChar = new CharacterDisplay(hero.transform);
		heroChar.baseComponent.sprite = charBase[Random.Range(0, charBase.Length)];
		heroChar.hairComponent.sprite = charHair[Random.Range(0, charHair.Length)];
		heroChar.legsComponent.sprite = charLegs[Random.Range(0, charLegs.Length)];
		heroChar.torsoComponent.sprite = charTorso[Random.Range(0, charTorso.Length)];
		heroChar.headComponent.sprite = charHead[Random.Range(0, charHead.Length)];
		heroChar.shieldComponent.sprite = charShield[Random.Range(0, charShield.Length)];
		heroChar.weaponComponent.sprite = charWeapon[Random.Range(0, charWeapon.Length)];

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
		tiles = new GameObject[layers.Length, Camera.VIEW_TILES_WIDE + 2, Camera.VIEW_TILES_HIGH + 2];

		// Add tiles for each layer on the main screen and just off screen for transitions
		for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
		{
			string layer = layers[layerIndex];
			for (int x = -1; x <= Camera.VIEW_TILES_WIDE; x++)
			{
				for (int y = -1; y <= Camera.VIEW_TILES_HIGH; y++)
				{
					// Don't tile the corners off screen
					if ((x == -1 || x == Camera.VIEW_TILES_WIDE) && (y == -1 || y == Camera.VIEW_TILES_HIGH))
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

		updateCamera();
	}

	/// <summary>
	/// Update the game, this called once per frame.
	/// </summary>
	void Update()
	{
		if (transitioning)
		{
			handleTransition();
		}
		else
		{
			handleInput();
		}
		handleTileAnimation();
		drawWorld();
	}

	/// <summary>
	/// Handles the tile animations.
	/// </summary>
	private void handleTileAnimation()
	{
		changeTileAnimateTimeLeft -= Time.deltaTime;
		if (changeTileAnimateTimeLeft <= 0)
		{
			changeTileAnimateTimeLeft = ANIMATE_TILE_TIME;
			world.changeTileAnimations();
		}
	}

	/// <summary>
	/// Handles the transition between tiles when moving.
	/// </summary>
	private void handleTransition()
	{
		transitionTimeLeft -= Time.deltaTime;
		if (transitionTimeLeft <= 0)
		{
			switch (transitionDirection)
			{
				case Direction.UP:
					heroY += 1;
					break;
				case Direction.DOWN:
					heroY -= 1;
					break;
				case Direction.LEFT:
					heroX -= 1;
					break;
				case Direction.RIGHT:
					heroX += 1;
					break;
			}
			transitioning = false;
			transitionDirection = Direction.NONE;
			camera.finishTransition();
			board.transform.position = new Vector3(0, 0, 0);
		}
		updateCamera();
	}

	/// <summary>
	/// Handles the user input.
	/// </summary>
	private void handleInput()
	{
		// Below is for debug purposes
		if (Input.GetKeyDown(KeyCode.F))
		{
			debugOn = !debugOn;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			world = new World();
			turnsTaken = 0;
			heroX = world.spawnX;
			heroY = world.spawnY;
			updateCamera();
		}

		bool turnTaken = false;
		if (Input.GetKey(KeyCode.Q))
		{
			// Cancel, Open/Close Menu
		}
		if (Input.GetKey(KeyCode.E))
		{
			// Confirm
		}
		if (Input.GetKey(KeyCode.W))
		{
			// Movement up
			int newHeroY = Mathf.Min(world.tilesHigh - 1, heroY + 1);
			if (!world.pathBlocked(heroX, newHeroY) && newHeroY != heroY)
			{
				turnTaken = true;
				transitioning = true;
				transitionDirection = Direction.UP;
				transitionTimeLeft = MOVING_TRANSITION_TIME;
			}
		}
		if (Input.GetKey(KeyCode.S))
		{
			// Movement down
			int newHeroY = Mathf.Max(0, heroY - 1);
			if (!world.pathBlocked(heroX, newHeroY) && newHeroY != heroY)
			{
				turnTaken = true;
				transitioning = true;
				transitionDirection = Direction.DOWN;
				transitionTimeLeft = MOVING_TRANSITION_TIME;
			}
		}
		if (Input.GetKey(KeyCode.A))
		{
			// Movement left
			int newHeroX = Mathf.Max(0, heroX - 1);
			if (!world.pathBlocked(newHeroX, heroY) && newHeroX != heroX)
			{
				turnTaken = true;
				transitioning = true;
				transitionDirection = Direction.LEFT;
				transitionTimeLeft = MOVING_TRANSITION_TIME;
			}
		}
		if (Input.GetKey(KeyCode.D))
		{
			// Movement right
			int newHeroX = Mathf.Min(world.tilesWide - 1, heroX + 1);
			if (!world.pathBlocked(newHeroX, heroY) && newHeroX != heroX)
			{
				turnTaken = true;
				transitioning = true;
				transitionDirection = Direction.RIGHT;
				transitionTimeLeft = MOVING_TRANSITION_TIME;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			heroChar.baseComponent.sprite = charBase[Random.Range(0, charBase.Length)];
			heroChar.hairComponent.sprite = charHair[Random.Range(0, charHair.Length)];
			heroChar.legsComponent.sprite = charLegs[Random.Range(0, charLegs.Length)];
			heroChar.torsoComponent.sprite = charTorso[Random.Range(0, charTorso.Length)];
			heroChar.headComponent.sprite = charHead[Random.Range(0, charHead.Length)];
			heroChar.shieldComponent.sprite = (Random.Range(0, 2) == 1 ? charShield[Random.Range(0, charShield.Length)] : null);
			heroChar.weaponComponent.sprite = (Random.Range(0, 2) == 1 ? charWeapon[Random.Range(0, charWeapon.Length)] : null);
		}
		if (turnTaken)
		{
			turnsTaken++;
		}
		if (transitioning)
		{
			camera.transitioning(transitionDirection);
		}
	}

	/// <summary>
	/// Updates the board and hero positions on the screen.
	/// </summary>
	private void updateCamera()
	{
		int x = Camera.CENTER_TILE_X;
		int y = Camera.CENTER_TILE_Y;

		if (!transitioning)
		{
			hero.transform.position = new Vector3(16 * (camera.focusX + smallWorldDrawOffsetX - camera.xMin), 16 * (camera.focusY + smallWorldDrawOffsetY - camera.yMin), 0);
		}
		else
		{
			int newHeroX = heroX;
			int newHeroY = heroY;
			
			switch (transitionDirection)
			{
				case Direction.UP:
					newHeroY += 1;
					if (world.tilesHigh >= Camera.VIEW_TILES_HIGH)
					{
						if (heroY < Camera.CENTER_TILE_Y)
						{
							hero.transform.position += (Vector3.up * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else if (newHeroY > ((world.tilesHigh - Camera.VIEW_TILES_HIGH) + Camera.CENTER_TILE_Y))
						{
							hero.transform.position += (Vector3.up * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else
						{
							board.transform.position += (Vector3.down * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
					}
					else
					{
						hero.transform.position += (Vector3.up * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
					}
					break;
				case Direction.DOWN:
					newHeroY -= 1;
					if (world.tilesHigh >= Camera.VIEW_TILES_HIGH)
					{
						if (newHeroY < Camera.CENTER_TILE_Y)
						{
							hero.transform.position += (Vector3.down * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else if (heroY > ((world.tilesHigh - Camera.VIEW_TILES_HIGH) + Camera.CENTER_TILE_Y))
						{
							hero.transform.position += (Vector3.down * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else
						{
							board.transform.position += (Vector3.up * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
					}
					else
					{
						hero.transform.position += (Vector3.down * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
					}
					break;
				case Direction.LEFT:
					newHeroX -= 1;
					if (world.tilesWide >= Camera.VIEW_TILES_WIDE)
					{
						if (newHeroX < Camera.CENTER_TILE_X)
						{
							hero.transform.position += (Vector3.left * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else if (heroX > ((world.tilesWide - Camera.VIEW_TILES_WIDE) + Camera.CENTER_TILE_X))
						{
							hero.transform.position += (Vector3.left * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else
						{
							board.transform.position += (Vector3.right * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
					}
					else
					{
						hero.transform.position += (Vector3.left * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
					}
					break;
				case Direction.RIGHT:
					newHeroX += 1;
					if (world.tilesWide >= Camera.VIEW_TILES_WIDE)
					{
						if (heroX < Camera.CENTER_TILE_X)
						{
							hero.transform.position += (Vector3.right * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else if (newHeroX > ((world.tilesWide - Camera.VIEW_TILES_WIDE) + Camera.CENTER_TILE_X))
						{
							hero.transform.position += (Vector3.right * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
						else
						{
							board.transform.position += (Vector3.left * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
						}
					}
					else
					{
						hero.transform.position += (Vector3.right * Time.deltaTime) * 16 * (1f / MOVING_TRANSITION_TIME);
					}
					break;
			}
		}
	}

	/// <summary>
	/// Draws the world.
	/// </summary>
	private void drawWorld()
	{
		float tint = world.getDayTimeTint(turnsTaken, TURNS_PER_DAY);
		Color tintColor = new Color(tint, tint, tint, 1);
		// Loop through each layer, ground, overground etc.
		for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
		{
			string layer = layers[layerIndex];
			for (int x = -1; x <= Camera.VIEW_TILES_WIDE; x++)
			{
				for (int y = -1; y <= Camera.VIEW_TILES_HIGH; y++)
				{
					// Skip the corners off screen
					if ((x == -1 || x == Camera.VIEW_TILES_WIDE) && (y == -1 || y == Camera.VIEW_TILES_HIGH))
					{
						continue;
					}
					int tileType = world.getTile(layer, camera.xMin - smallWorldDrawOffsetX + x, camera.yMin - smallWorldDrawOffsetY + y);
					GameObject tile = tiles[layerIndex, x + 1, y + 1];
					if (tileType != -1)
					{
						int lightLevel = world.getLightLevel(camera.xMin - smallWorldDrawOffsetX + x, camera.yMin - smallWorldDrawOffsetY + y);
						tile.GetComponent<SpriteRenderer>().sprite = tileLayer[layerIndex][tileType];

						bool pathBlock = world.pathBlocked(camera.xMin - smallWorldDrawOffsetX + x, camera.yMin - smallWorldDrawOffsetY + y);
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