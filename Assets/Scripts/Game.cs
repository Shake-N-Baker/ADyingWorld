using UnityEngine;
using System.Collections;

/// <summary>
/// The class for running the game.
/// </summary>
public class Game : MonoBehaviour
{
	// Sprite sheets
	public Sprite[] tileGround;
	public Sprite[] tileOverGround;
	public Sprite[] tileDecorationBase;
	public Sprite[] charBase;
	public Sprite[] charHair;
	public Sprite[] charHead;
	public Sprite[] charLegs;
	public Sprite[] charShield;
	public Sprite[] charTorso;
	public Sprite[] charWeapon;

	public World world;
	public GameObject board;
	public GameObject character;

	/// <summary>
	/// Awake this instance, resources are loaded at this point.
	/// </summary>
	void Awake()
	{
		tileGround = Resources.LoadAll<Sprite>("tileGround");
		tileOverGround = Resources.LoadAll<Sprite>("tileOverGround");
		tileDecorationBase = Resources.LoadAll<Sprite>("tileDecorationBase");
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

		character = new GameObject();
		character.name = "Character";
		GameObject charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charWeapon[Random.Range(0, charWeapon.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharWeapon";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charShield[Random.Range(0, charShield.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharShield";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charHead[Random.Range(0, charHead.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHead";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charTorso[Random.Range(0, charTorso.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharTorso";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charLegs[Random.Range(0, charLegs.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharLegs";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charHair[Random.Range(0, charHair.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharHair";
		charComponent.transform.parent = character.transform;
		charComponent = new GameObject();
		charComponent.AddComponent<SpriteRenderer>();
		charComponent.GetComponent<SpriteRenderer>().sprite = charBase[Random.Range(0, charBase.Length)];
		charComponent.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
		charComponent.transform.position = new Vector3(0, 16, 0);
		charComponent.name = "CharBase";
		charComponent.transform.parent = character.transform;

		board = new GameObject();
		board.name = "Board";
		for (int x = 0; x < 20; x++)
		{
			for (int y = 0; y < 18; y++)
			{
				GameObject tile = new GameObject();
				tile.AddComponent<SpriteRenderer>();
				tile.GetComponent<SpriteRenderer>().sprite = tileGround[Random.Range(0, tileGround.Length)];
				tile.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
				tile.transform.position = new Vector3(x * 16, (y + 1) * 16, 0);
				tile.name = "TileX-" + x + "Y-" + y;
				tile.transform.parent = board.transform;
			}
		}
	}

	/// <summary>
	/// Update the game, this called once per frame.
	/// </summary>
	void Update()
	{
		if (Input.GetKey(KeyCode.W))
		{
			character.transform.position += Vector3.up * 1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			character.transform.position += Vector3.down * 1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			character.transform.position += Vector3.left * 1f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			character.transform.position += Vector3.right * 1f;
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
}