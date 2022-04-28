#include "Ship.h"

Ship::Ship(RenderWindow *window, Sprite mySprite_temp, Vector2f position)
{
	Ship::window = window;
	mySprite = mySprite_temp;
	mySprite.setPosition(position);

	mySprite.setScale(0.1f, 0.1f);
}

void Ship::DrawShip()
{
	window->draw(mySprite);
}

void Ship::MovementShip() 
{

	if (Keyboard::isKeyPressed(Keyboard::W) )
	{
		mySprite.move(0,-speed);
	}
	if (Keyboard::isKeyPressed(Keyboard::S))
	{
		mySprite.move(0, speed);
	}
	if (Keyboard::isKeyPressed(Keyboard::A))
	{
		mySprite.move(-speed, 0);
	}
	if (Keyboard::isKeyPressed(Keyboard::D))
	{
		mySprite.move(speed, 0);
	}

}

void Ship::constrainShipPosition() {

	float MaxX = window->getSize().x;
	float MaxY = window->getSize().y;

	float shipX = mySprite.getPosition().x;
	float shipY = mySprite.getPosition().y;

	if (shipX + mySprite.getGlobalBounds().width > MaxX) 
	{
		shipX = MaxX - mySprite.getGlobalBounds().width;
	}
	if(shipX < 0)
	{
		shipX = 0;
	}
	if (shipY + mySprite.getGlobalBounds().height > MaxY)
	{
		shipY = MaxY - mySprite.getGlobalBounds().height;
	}
	if (shipY < 0)
	{
		shipY = 0;
	}
	mySprite.setPosition(shipX, shipY);
}

Vector2f Ship::getPosition()
{
	return mySprite.getPosition();
}

float Ship::getShipRadius()
{
	return mySprite.getGlobalBounds().width / 2;
}
