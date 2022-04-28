#pragma once
#ifndef INCLUDED_GAME
#define INCLUDED_GAME
#include "Game.h"
#include "Coin.h"
#include "Ship.h"
#include "AsteroidObstacles.h"
#include "SFML/Graphics.hpp"
#include <iostream>

using namespace sf;
using namespace std;

class Game 
{
	public:
		Game();
	//	~Game();
		void run();

	private:
		RenderWindow window;
		Texture shipTexture;
		Texture coinTexture;
		int Level;
		bool mGameOver;
		bool Collision(Vector2f position0, float Radius0, Vector2f position1, float Radius1);
		bool CoinCollision(Ship *ship, Coin *coin);
		Clock AsteroidSpawnTimer; 
		int SpawnCountAsteroids;
		bool AsteroidCollisions(Asteroid *asteroid, Ship *ship);
		void DestroyAsteroids(vector<Asteroid*> &AsteroidList);

};

#endif