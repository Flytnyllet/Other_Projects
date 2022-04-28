#include "Game.h"

namespace
{

	const string windowTitle = "Asteroids";
	const VideoMode videoMode = VideoMode(768, 1024);
	const Color backgroundColor = Color::Black;
	const int FRAMERATE_LIMIT = 60;
	RenderWindow window(videoMode, windowTitle, Style::Titlebar | Style::Close);
	const int START_LEVEL = 1;
	float Asteroid_Spawn_Base = 1.0f;
	const float Asteroid_Spawn_Increase = 0.2f;

}

Game::Game() :
	window(videoMode, windowTitle, Style::Titlebar | Style::Close),
	Level(START_LEVEL),
	mGameOver(false) {
	window.setFramerateLimit(FRAMERATE_LIMIT);
}

	bool Game::Collision(Vector2f position0, float Radius0, Vector2f position1, float Radius1)
	{
		float deltaX = position0.x - position1.x;
		float deltaY = position0.y - position1.y;
		float RadiusSummary = Radius0 + Radius1;

		return deltaX * deltaX + deltaY * deltaY < RadiusSummary * RadiusSummary;
	}

	bool Game::CoinCollision(Ship *ship, Coin *coin_obj)
	{
		Vector2f shipPosition = ship->getPosition();
		float shipRadius = ship->getShipRadius();
		Vector2f coinPosition = coin_obj->getPosition();
		float coinRadius = coin_obj->getCoinRadius();

		return Collision(shipPosition, shipRadius, coinPosition, coinRadius);
	}

	bool Game::AsteroidCollisions(Asteroid *asteroid, Ship *ship)
	{
		Vector2f shipPosition = ship->getPosition();
		float shipRadius = ship->getShipRadius();
		Vector2f asteroidPosition = asteroid->getPosition();
		float asteroidRadius = asteroid->getAsteroidRadius();

		return Collision(asteroidPosition, asteroidRadius, shipPosition, shipRadius);
	}
	void Game::DestroyAsteroids(vector<Asteroid*> &AsteroidList)
	{
		for (int i = 0; i < AsteroidList.size(); i++)
		{
			if (window.getSize().y + AsteroidList[i]->getAsteroidRadius() < AsteroidList[i]->getPosition().y)
			{
				delete AsteroidList[i];
				AsteroidList.erase(AsteroidList.begin() + i);
			}
		}
	}
	
	void Game::run()
	{
		srand(time(NULL));

		Texture shipTexture;
		shipTexture.loadFromFile("Sprites/Ship.png");
		Texture coinTexture;
		coinTexture.loadFromFile("Sprites/Coin.png");
		Texture asteroidTexture;
		asteroidTexture.loadFromFile("Sprites/Asteroid.png");

		Sprite ShipSprite;
		ShipSprite.setTexture(shipTexture);
		Sprite CoinSprite;
		CoinSprite.setTexture(coinTexture);
		Sprite AsteroidSprite;
		AsteroidSprite.setTexture(asteroidTexture);

		Ship *ship = new Ship(&window, ShipSprite, Vector2f(334, 512)); 

		std::vector <Coin*> myCoins;
		myCoins.push_back(new Coin(&window, CoinSprite));

		std::vector <Asteroid*> myAsteroids;
		myAsteroids.push_back(new Asteroid(&window, AsteroidSprite));

		Clock AsteroidClock;

		while (window.isOpen() && !mGameOver)
		{
			Event event;

			while (window.pollEvent(event))
			{
				if (event.type == Event::Closed)
				{
					window.close();
				}
			}

			ship->MovementShip();
			ship->constrainShipPosition();

			window.clear(backgroundColor);

			ship->DrawShip();

			if (Asteroid_Spawn_Base < AsteroidClock.getElapsedTime().asSeconds())
			{
				int SpawnCountAsteroids = (Asteroid_Spawn_Base + Level * Asteroid_Spawn_Increase);
				for (int i = 0; i < SpawnCountAsteroids; i++)
				{
					Asteroid *asteroid_obst = new Asteroid(&window, AsteroidSprite);
					myAsteroids.push_back(asteroid_obst);
				}
				AsteroidClock.restart();
			}

			for (int i = 0; i < myAsteroids.size(); i++)
			{
				myAsteroids[i]->DrawAsteroids();
				myAsteroids[i]->updateAsteroids();

				if (AsteroidCollisions(myAsteroids[i], ship) == true)
				{
					mGameOver = true;
				}
			}

			DestroyAsteroids(myAsteroids);

			for (int i = 0; i < myCoins.size(); i++) // går igenom myCoins vector
			{
				myCoins[i]->updateCoin();
				myCoins[i]->DrawCoin();
				
				if (window.getSize().y + myCoins[i]->getCoinRadius() < myCoins[i]->getPosition().y)
				{
					mGameOver = true;
				}

				if (CoinCollision(ship, myCoins[i]))
				{
					Level++;
					delete myCoins[i];
					myCoins.erase(myCoins.begin() + i);
					myCoins.push_back(new Coin(&window, CoinSprite));
				}
			}

			window.display();
		}
	}