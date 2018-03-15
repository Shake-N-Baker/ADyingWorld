using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// User interface graphics class handles the sprites involved in
/// menus inventory etc.
/// </summary>
public class UIGraphics
{
	/// <summary>
	/// Reference to the game.
	/// </summary>
	private Game game;

	// UI Fonts
	public static Font customFont;

	// UI Sprites
	public static Sprite uiLongPanel;
	public static Sprite uiPanel;
	public static Sprite uiPanelHighlight;
	public static Sprite uiHeart;
	public static Sprite uiCoin;
	public static Sprite uiSword;
	public static Sprite uiChest;
	public static Sprite uiInventory;

	// UI GameObjects to display the sprites
	public GameObject hudTopLeftSmallPanel;
	public GameObject hudSmallPanelHeart;
	public GameObject hudSmallPanelCoin;
	public GameObject hudHotkeyIconChest;
	public GameObject hudHotkeySlotOnePanel;
	public GameObject hudHotkeySlotTwoPanel;
	public GameObject hudHotkeySlotThreePanel;
	public GameObject hudHotkeyInventoryPanel;
	public GameObject hudInventory;
	public GameObject hudInventoryHighlight;

	// UI GameObjects to display text
	public GameObject healthSmallPanelText;
	public GameObject goldSmallPanelText;
	public GameObject healthInventoryText;
	public GameObject goldInventoryText;
	public GameObject attackInventoryText;
	public GameObject armorInventoryText;
	public GameObject instructionsInventoryText;
	public GameObject descriptionInventoryText;
	public GameObject hotkeySlotOneText;
	public GameObject hotkeySlotTwoText;
	public GameObject hotkeySlotThreeText;
	public GameObject hotkeyInventoryText;

	/// <summary>
	/// Initializes a new instance of the <see cref="UIGraphics"/> class.
	/// </summary>
	/// <param name="game">Game.</param>
	public UIGraphics(Game game)
	{
		this.game = game;
		
		hudInventory = new GameObject("hudInventory");
		hudInventory.transform.position = new Vector3(8, 273, 0);
        SpriteRenderer hudSprite = hudInventory.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiInventory;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		hudInventory.GetComponent<SpriteRenderer>().enabled = false;
		hudInventoryHighlight = new GameObject("hudInventoryHighlight");
		hudInventoryHighlight.transform.position = new Vector3(18, 218, 0);
		hudSprite = hudInventoryHighlight.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiPanelHighlight;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 2;
		
		hudSmallPanelHeart = new GameObject("hudHeart");
		hudSmallPanelHeart.transform.position = new Vector3(7, 282, 0);
		hudSprite = hudSmallPanelHeart.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiHeart;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 2;
		hudSmallPanelCoin = new GameObject("hudCoin");
		hudSmallPanelCoin.transform.position = new Vector3(55, 282, 0);
		hudSprite = hudSmallPanelCoin.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiCoin;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 2;
		hudHotkeyIconChest = new GameObject("hudChest");
		hudHotkeyIconChest.transform.position = new Vector3(289, 24, 0);
		hudSprite = hudHotkeyIconChest.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiChest;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 2;
		hudTopLeftSmallPanel = new GameObject("hudDarkPanel");
		hudTopLeftSmallPanel.transform.position = new Vector3(3, 286, 0);
		hudSprite = hudTopLeftSmallPanel.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiLongPanel;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		hudHotkeySlotOnePanel = new GameObject("hudHotkeySlotOnePanel");
		hudHotkeySlotOnePanel.transform.position = new Vector3(176, 27, 0);
		hudSprite = hudHotkeySlotOnePanel.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiPanel;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		hudHotkeySlotTwoPanel = new GameObject("hudHotkeySlotTwoPanel");
		hudHotkeySlotTwoPanel.transform.position = new Vector3(212, 27, 0);
		hudSprite = hudHotkeySlotTwoPanel.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiPanel;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		hudHotkeySlotThreePanel = new GameObject("hudHotkeySlotThreePanel");
		hudHotkeySlotThreePanel.transform.position = new Vector3(248, 27, 0);
		hudSprite = hudHotkeySlotThreePanel.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiPanel;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		hudHotkeyInventoryPanel = new GameObject("hudHotkeyInventoryPanel");
		hudHotkeyInventoryPanel.transform.position = new Vector3(284, 27, 0);
		hudSprite = hudHotkeyInventoryPanel.AddComponent<SpriteRenderer>();
		hudSprite.sprite = uiPanel;
		hudSprite.sortingLayerName = "UI";
		hudSprite.sortingOrder = 1;
		
		healthSmallPanelText = CreateTextObject("HealthUIText", game.hero.health.ToString(), 0.5f, 27, 180, 100, 100);
		goldSmallPanelText = CreateTextObject("GoldUIText", "0", 0.5f, 75, 180, 100, 100);
		hotkeySlotOneText = CreateTextObject("HotkeyOneUIText", "1", 0.5f, 199, 0, 100, 23);
		hotkeySlotTwoText = CreateTextObject("HotkeyTwoUIText", "2", 0.5f, 235, 0, 100, 23);
		hotkeySlotThreeText = CreateTextObject("HotkeyThreeUIText", "3", 0.5f, 271, 0, 100, 23);
		hotkeyInventoryText = CreateTextObject("hotkeyInventoryText", "E", 0.5f, 307, 0, 100, 23);
		healthInventoryText = CreateTextObject("HealthInvUIText", game.hero.health.ToString(), 0.5f, 55, 160, 100, 100);
		goldInventoryText = CreateTextObject("GoldInvUIText", "0", 0.5f, 135, 160, 100, 100);
		attackInventoryText = CreateTextObject("AttackInvUIText", "1", 0.5f, 55, 140, 100, 100);
		armorInventoryText = CreateTextObject("ArmorInvUIText", "0", 0.5f, 135, 140, 100, 100);
		instructionsInventoryText = CreateTextObject("InstrInvUIText", "Q to cancel/close, E to confirm", 0.5f, 25, 0, 175, 50);
		descriptionInventoryText = CreateTextObject("DescInvUIText", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse", 0.5f, 206, 38, 100, 230);
	}

	/// <summary>
	/// Creates a text object and returns a reference to it.
	/// </summary>
	/// <returns>The text object reference.</returns>
	/// <param name="name">Name of the object.</param>
	/// <param name="text">Text to display.</param>
	/// <param name="scale">Scale of the text.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="width">The text rectangle width.</param>
	/// <param name="height">The text rectangle height.</param>
	private GameObject CreateTextObject(string name, string text, float scale, float x, float y, int width = 100, int height = 100)
	{
		GameObject textGO = new GameObject(name);
		textGO.transform.parent = GameObject.Find("Canvas").transform;
		textGO.AddComponent<CanvasRenderer>();
		Text txt = textGO.AddComponent<Text>();
		txt.text = text;
		txt.font = customFont;
		txt.fontSize = 16;
		txt.color = new Color(251f / 255f, 239f / 255f, 215f / 255f);
		RectTransform rectTrans = txt.GetComponent<RectTransform>();
		rectTrans.anchorMin = new Vector2(0, 0);
		rectTrans.anchorMax = new Vector2(0, 0);
		rectTrans.pivot = new Vector2(0, 0);
		rectTrans.anchoredPosition = new Vector2(x, y);
		rectTrans.sizeDelta = new Vector2(width / scale, height / scale);
		textGO.transform.localScale = new Vector3(scale, scale, scale);
		return textGO;
	}
}