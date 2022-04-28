#pragma once

#ifndef INCLUDED_SHIP
#define INCLUDED_SHIP

#include <vector>
#include <string>
#include <SFML/Graphics.hpp>

using namespace std;
using namespace sf;

class Ship
{
public:
	void DrawShip();
	Ship(RenderWindow *window, Sprite mySprite, Vector2f position);
	void MovementShip();
	void constrainShipPosition();
	Vector2f getPosition();
	float getShipRadius();

private:
	RenderWindow *window;
	Sprite mySprite;
	int speed = 6;
};

#endif
