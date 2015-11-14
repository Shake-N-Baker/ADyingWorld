using System;
using System.Collections.Generic;

public enum BehaviorType
{
	None,
	Villager
}

/// <summary>
/// The character behavior class controls how the character
/// interacts with the world and characters based on certain
/// behaviors.
/// </summary>
public class CharacterBehavior
{
	// Static variable to generate random numbers
	private static Random rand;

	// Character that this behavior is attached to
	private Character character;

	// The type of behavior
	private BehaviorType behavior;

	/// <summary>
	/// Initializes a new instance of the <see cref="CharacterBehavior"/> class.
	/// </summary>
	/// <param name="character">Character that this behavior is attached to.</param>
	/// <param name="behavior">The behavior type.</param>
	public CharacterBehavior(Character character, BehaviorType behavior)
	{
		if (rand == null)
		{
			rand = new Random();
		}
		this.character = character;
		this.behavior = behavior;
	}

	/// <summary>
	/// Update the character this is attached to based on the world and characters.
	/// </summary>
	/// <param name="characters">The list of characters.</param>
	public void Update(List<Character> characters)
	{
		int r, destinationX = character.x, destinationY = character.y;
		switch (behavior)
		{
			case BehaviorType.Villager:
				r = rand.Next(0, 100);
				if (r < 15)
				{
					destinationX += 1;
				}
				else if (r < 30)
				{
					destinationX -= 1;
				}
				else if (r < 45)
				{
					destinationY += 1;
				}
				else if (r < 60)
				{
					destinationY -= 1;
				}
				foreach (Character otherCharacter in characters)
				{
					if (character == otherCharacter)
					{
						continue;
					}
					if (otherCharacter.newX == destinationX && otherCharacter.newY == destinationY)
					{
						destinationX = character.x;
						destinationY = character.y;
						break;
					}
				}
				if (character.world.pathBlocked(destinationX, destinationY))
				{
					destinationX = character.x;
					destinationY = character.y;
				}
				break;
			case BehaviorType.None:
			default:
				return;
		}
		character.newX = destinationX;
		character.newY = destinationY;
	}
}