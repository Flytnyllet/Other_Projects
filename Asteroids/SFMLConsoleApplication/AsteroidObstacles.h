#pragma once

#ifndef INCLUDED_ASTEROIDS
#define INCLUDED_ASTEROIDS

#include <vector>
#include <string>
#include <SFML/Graphics.hpp>

using namespace sf;
using namespace std;

class Asteroid {
	public:
		Asteroid(RenderWindow *window, Sprite mySprite);
		void DrawAsteroids();
		void updateAsteroids();
		Vector2f getPosition();
		float getAsteroidRadius();

	private:
		RenderWindow *window;
		Sprite asteroidSprite;
		int asteroidSpeed;
};

#endif
