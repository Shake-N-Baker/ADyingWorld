using UnityEngine;
using UnityEngine.UI;
using System;
using Code.Controllers;

namespace Code.Models
{
	/// <summary>
	/// The inventory class holds data and controls the inventory.
	/// </summary>
	public class Inventory
	{
		/// <summary>
		/// Reference to the game.
		/// </summary>
		private Game game;

		/// <summary>
		/// Reference to the UI graphics.
		/// </summary>
		private UIGraphics graphics;

		/// <summary>
		/// Flag of whether the inventory is open.
		/// </summary>
		private bool _open;
		public bool open
		{
			get { return _open; }
			set
			{
				_open = value;
				if (!_open)
				{
					x = 0;
					y = 1;
					updateUICloseInventory();
				}
				else
				{
					updateUIOpenInventory();
				}
			}
		}

		/// <summary>
		/// The X and Y coordinates of the current selection.
		/// </summary>
		private int x;
		private int y;

		/// <summary>
		/// Initializes a new instance of the <see cref="Inventory"/> class.
		/// </summary>
		/// <param name="game">Reference to the Game.</param>
		public Inventory(Game game)
		{
			this.game = game;
			graphics = game.graphics;
			_open = false;
			x = 0;
			y = 1;
			updateUICloseInventory();
		}

		/// <summary>
		/// Handles the input when the inventory is open.
		/// </summary>
		public void handleInput()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				// Cancel, Exit Inventory etc.
				if (open)
				{
					open = false;
					return;
				}
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				// Confirm etc.
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				// Move selection up
				y -= 1;
				if (y < 0)
				{
					y = 0;
				}
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				// Move selection down
				y += 1;
				if (y > 5)
				{
					y = 5;
				}
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				// Move selection left
				x -= 1;
				if (x < 0)
				{
					x = 0;
				}
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				// Move selection right
				x += 1;
				if (x > 4)
				{
					x = 4;
				}
			}
			if (y == 0)
			{
				graphics.hudInventoryHighlight.transform.position = new Vector3((36 * x) + 18, 218, 0);
			}
			else
			{
				graphics.hudInventoryHighlight.transform.position = new Vector3((36 * x) + 18, 187 - (27 * (y - 1)), 0);
			}
		}

		/// <summary>
		/// Updates the user interface for opening the inventory.
		/// </summary>
		private void updateUIOpenInventory()
		{
			graphics.hudInventoryHighlight.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudInventory.GetComponent<SpriteRenderer>().enabled = true;
			graphics.healthInventoryText.GetComponent<Text>().enabled = true;
			graphics.goldInventoryText.GetComponent<Text>().enabled = true;
			graphics.attackInventoryText.GetComponent<Text>().enabled = true;
			graphics.armorInventoryText.GetComponent<Text>().enabled = true;
			graphics.instructionsInventoryText.GetComponent<Text>().enabled = true;
			graphics.descriptionInventoryText.GetComponent<Text>().enabled = true;
			graphics.hudTopLeftSmallPanel.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudSmallPanelHeart.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudSmallPanelCoin.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudHotkeySlotOnePanel.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudHotkeySlotTwoPanel.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudHotkeySlotThreePanel.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudHotkeyIconChest.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudHotkeyInventoryPanel.GetComponent<SpriteRenderer>().enabled = false;
			graphics.healthSmallPanelText.GetComponent<Text>().enabled = false;
			graphics.goldSmallPanelText.GetComponent<Text>().enabled = false;
			graphics.hotkeySlotOneText.GetComponent<Text>().enabled = false;
			graphics.hotkeySlotTwoText.GetComponent<Text>().enabled = false;
			graphics.hotkeySlotThreeText.GetComponent<Text>().enabled = false;
			graphics.hotkeyInventoryText.GetComponent<Text>().enabled = false;
		}
		
		/// <summary>
		/// Updates the user interface for closing the inventory.
		/// </summary>
		private void updateUICloseInventory()
		{
			graphics.hudInventoryHighlight.GetComponent<SpriteRenderer>().enabled = false;
			graphics.hudInventory.GetComponent<SpriteRenderer>().enabled = false;
			graphics.healthInventoryText.GetComponent<Text>().enabled = false;
			graphics.goldInventoryText.GetComponent<Text>().enabled = false;
			graphics.attackInventoryText.GetComponent<Text>().enabled = false;
			graphics.armorInventoryText.GetComponent<Text>().enabled = false;
			graphics.instructionsInventoryText.GetComponent<Text>().enabled = false;
			graphics.descriptionInventoryText.GetComponent<Text>().enabled = false;
			graphics.hudSmallPanelHeart.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudSmallPanelCoin.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudTopLeftSmallPanel.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudHotkeyIconChest.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudHotkeySlotOnePanel.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudHotkeySlotTwoPanel.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudHotkeySlotThreePanel.GetComponent<SpriteRenderer>().enabled = true;
			graphics.hudHotkeyInventoryPanel.GetComponent<SpriteRenderer>().enabled = true;
			graphics.healthSmallPanelText.GetComponent<Text>().enabled = true;
			graphics.goldSmallPanelText.GetComponent<Text>().enabled = true;
			graphics.hotkeySlotOneText.GetComponent<Text>().enabled = true;
			graphics.hotkeySlotTwoText.GetComponent<Text>().enabled = true;
			graphics.hotkeySlotThreeText.GetComponent<Text>().enabled = true;
			graphics.hotkeyInventoryText.GetComponent<Text>().enabled = true;
		}
	}
}