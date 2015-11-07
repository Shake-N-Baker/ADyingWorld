using UnityEngine;
using System.Collections;

public class CharacterDisplay
{
	// Components
	public GameObject container;
	public SpriteRenderer baseComponent;
	public SpriteRenderer hairComponent;
	public SpriteRenderer legsComponent;
	public SpriteRenderer torsoComponent;
	public SpriteRenderer headComponent;
	public SpriteRenderer shieldComponent;
	public SpriteRenderer weaponComponent;

	/// <summary>
	/// Initializes a new instance of the <see cref="CharacterDisplay"/> class.
	/// </summary>
	/// <param name="parent">The parent transform to place this character under.</param>
	public CharacterDisplay(Transform parent)
	{
		container = new GameObject();
		container.transform.parent = parent;
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