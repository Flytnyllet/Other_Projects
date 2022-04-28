#include "AsteroidObstacles.h"

Asteroid::Asteroid(RenderWindow *window, Sprite mySprite)
{
	Asteroid::window = window;
	asteroidSprite = mySprite;
	asteroidSprite.setPosition(Vector2f(rand() % window->getSize().x, -(rand() % 200 + 100)));

	asteroidSpeed = rand() % 8 + 1;
	asteroidSprite.setScale(0.4f, 0.4f);
}

void Asteroid::DrawAsteroids()
{ 
	window->draw(asteroidSprite);
}

void Asteroid::updateAsteroids()
{
	asteroidSprite.move(0, asteroidSpeed);
}

Vector2f Asteroid::getPosition()
{
	return asteroidSprite.getPosition();
}

float Asteroid::getAsteroidRadius()
{
	return asteroidSprite.getGlobalBounds().width / 3;
}
