#include "ConcreteGame.h"
#include "SFML/Graphics.hpp"
#include <vector>
#include <chrono>

using namespace sf;
using namespace std;

namespace {
	const std::string windowTitle = "SFML Console Application";
	const sf::VideoMode videoMode = sf::VideoMode(768, 850);
	const sf::Color backgroundColor = sf::Color::Blue;
	const unsigned int FRAMERATE_LIMIT = 60;
}


ConcreteGame::ConcreteGame() : window(videoMode, windowTitle, Style::Titlebar | Style::Close), timePassed(0), enemySpawnTimer(0)
{
	auto now = std::chrono::system_clock::now();
	auto ms = std::chrono::time_point_cast<std::chrono::milliseconds>(now);
	long value = std::chrono::duration_cast<std::chrono::milliseconds>(ms.time_since_epoch()).count();
	srand((unsigned int)value);

	window.setFramerateLimit(FRAMERATE_LIMIT);
	playerTexture.loadFromFile("Sprites/Ship.png");
	enemyTexture.loadFromFile("Sprites/EnemyShip.png");
	bulletTexture.loadFromFile("Sprites/Bullet.png");
	explosionTexture.loadFromFile("Sprites/Explosion.png");

	createPlayer();

}


ConcreteGame::~ConcreteGame()
{
	delete playerEntity;
	for (auto* enemy : enemyEntities) {
		delete enemy;
	}

	for (auto* bullet : playerBullets) {
		delete bullet;
	}

	for (auto* bullet : enemyBullets) {
		delete bullet;
	}
}

void ConcreteGame::runGame()
{
	Clock clock;

	while (window.isOpen())
	{
		float deltaTime = clock.restart().asSeconds();
		timePassed += deltaTime;

		processWindowEvents();

		spawnEnemies(deltaTime);

		updateEntity(deltaTime);

		if (doesPlayerCollidesWithEnemy())
			return;

		if (doesPlayerCollideWithEnemyBullets())
			return;

		KillEnemiesCollidingWithBullets();

		removeOffScreenEntities();


		window.clear(backgroundColor);

		drawEntities();


		window.display();

	}
}

void ConcreteGame::createPlayer()
{
	Sprite playerSprite(playerTexture);

	Vector2u windowSize = window.getSize();

	playerSprite.setPosition(windowSize.x / 2, windowSize.y / 2);
	playerSprite.setScale(0.1, 0.1);

	playerEntity = new PlayerEntity(playerSprite, windowSize, bulletTexture);
}

void ConcreteGame::spawnEnemies(float deltaTime)
{
	enemySpawnTimer -= deltaTime;

	if (enemySpawnTimer <= 0)
	{
		enemySpawnTimer = 0.5 + std::max(2 - std::floor(timePassed/10) * 0.2f, 0.f);

		Sprite enemySprite(enemyTexture);

		Vector2u windowSize = window.getSize();

		auto enemyBounds = enemySprite.getGlobalBounds();
		enemySprite.setScale(0.3, -0.3);
		enemySprite.setPosition(rand() % (int)(windowSize.x - enemyBounds.width), -enemyBounds.height);

		Vector2f spawnVelocity(200, 200);
		if (rand() % 2 == 0) {
			spawnVelocity.x = -spawnVelocity.x;
		}
		EnemyEntity* enemy = new EnemyEntity(enemySprite, windowSize, spawnVelocity, bulletTexture, explosionTexture);
		enemyEntities.push_back(enemy);
	}
}


void ConcreteGame::processWindowEvents()
{
	Event event;
	while (window.pollEvent(event))
	{
		if (event.type == Event::Closed)
		{
			window.close();
		}
	}
}

void ConcreteGame::updateEntity(float deltaTime)
{
	updatePlayer(deltaTime);
	updateEnemies(deltaTime);
	updateExplosions(deltaTime);
}

bool ConcreteGame::doesPlayerCollidesWithEnemy()
{
	for (EnemyEntity* enemy : enemyEntities) {
		if (playerEntity->collidesWith(*enemy))
			return true;
	}
	return false;
}


void ConcreteGame::KillEnemiesCollidingWithBullets()
{
	for (int i = playerBullets.size() - 1; i >= 0; i--)
	{
		auto* bullet = playerBullets[i];
		for (int j = enemyEntities.size() - 1; j >= 0; j--) {

			auto* enemy = enemyEntities[j];

			if (bullet->collidesWith(*enemy))
			{
				auto* explosion = enemy->createExplosion();
				explosionEntities.push_back(explosion);

				enemyEntities.erase(enemyEntities.begin() + j);
				delete enemy;

				delete bullet;
				playerBullets.erase(playerBullets.begin() + i);

				break;
			}
		}
	}
}

bool ConcreteGame::doesPlayerCollideWithEnemyBullets()
{
	for (BulletEntity* enemyBullet : enemyBullets)
	{
		if (playerEntity->collidesWith(*enemyBullet))
			return true;
	}
	return false;
}

void ConcreteGame::drawEntities()
{
	for (ExplosionEntity* explosion : explosionEntities) {
		explosion->drawEntity(window);
	}

	for (EnemyEntity* enemy : enemyEntities) {
		enemy->drawEntity(window);
	}

	for (BulletEntity* bullet : playerBullets) {
		bullet->drawEntity(window);
	}

	for (BulletEntity* bullet : enemyBullets) {
		bullet->drawEntity(window);
	}
	
	playerEntity->drawEntity(window);
	
}

void ConcreteGame::updatePlayer(float deltaTime)
{
	playerEntity->Move(deltaTime);

	for (BulletEntity* bullet : playerBullets)
	{
		bullet->Move(deltaTime);
	}

	auto* newPlayerBullets = playerEntity->Shoot(deltaTime);

	if (newPlayerBullets != nullptr) {
		for (BulletEntity* bullet : *newPlayerBullets) {
			playerBullets.push_back(bullet);
		}
		delete newPlayerBullets;
	}
}

void ConcreteGame::updateEnemies(float deltaTime)
{
	for (BulletEntity* bullet : enemyBullets)
	{
		bullet->Move(deltaTime);
	}

	for (EnemyEntity* enemyEntity : enemyEntities)
	{
		enemyEntity->Move(deltaTime);
	}

	for (EnemyEntity* enemyEntity : enemyEntities)
	{
		BulletEntity* newEnemyBullet = enemyEntity->Shoot(deltaTime);
		if (newEnemyBullet != nullptr) {
			enemyBullets.push_back(newEnemyBullet);
		}
	}
}

void ConcreteGame::updateExplosions(float deltaTime)
{
	for (int i = explosionEntities.size() - 1; i >= 0; i--)
	{
		auto* explosionEntity = explosionEntities[i];

		if (explosionEntity->shouldRemove(deltaTime)) 
		{
			//ta bort explosionen (delete) och från listan
			explosionEntities.erase(explosionEntities.begin() + i);
			delete explosionEntity;
		}
	}
}

bool ConcreteGame::isBelowWindow(Entity & entity)
{
	return entity.getPosition().y > window.getSize().y;
}

bool ConcreteGame::isAboveWindow(Entity & entity)
{
	return entity.getPosition().y + entity.getSize().y < 0;
}

void ConcreteGame::removeOffScreenEntities()
{
	for(int i = playerBullets.size() - 1; i >= 0; i--)
	{
		if (isAboveWindow(*playerBullets[i])) {

			delete playerBullets[i];
			playerBullets.erase(playerBullets.begin() + i);
		}
	}
	for (int i = enemyBullets.size() - 1; i >= 0; i--)
	{
		if (isBelowWindow(*enemyBullets[i])) {

			delete enemyBullets[i];
			enemyBullets.erase(enemyBullets.begin() + i);
		}
	}
	for (int i = enemyEntities.size() - 1; i >= 0; i--)
	{
		if (isBelowWindow(*enemyEntities[i])) {

			delete enemyEntities[i];
			enemyEntities.erase(enemyEntities.begin() + i);
		}
	}
}
