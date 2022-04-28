#pragma once

#include <string>
#include <iostream>
#include "SFML/Graphics/RenderWindow.hpp"
#include "SFML/Graphics/Texture.hpp"
#include "SFML/Graphics/Sprite.hpp"
#include "EnemyEntity.h"
#include "PlayerEntity.h"
#include "BulletEntity.h"
#include "ExplosionEntity.h"

class ConcreteGame
{

public:
	ConcreteGame();
	~ConcreteGame();

	void runGame();

private:
	sf::RenderWindow window;

	sf::Texture playerTexture;
	sf::Texture enemyTexture;
	sf::Texture bulletTexture;
	sf::Texture explosionTexture;

	void createPlayer();
	void spawnEnemies(float deltaTime);

	void processWindowEvents();

	bool doesPlayerCollidesWithEnemy();
	void KillEnemiesCollidingWithBullets();
	bool doesPlayerCollideWithEnemyBullets();

	void drawEntities();

	void updateEntity(float deltaTime);
	void updatePlayer(float deltaTime);
	void updateEnemies(float deltaTime);
	void updateExplosions(float deltatime);
	
	//polymorfism
	bool isBelowWindow(Entity& entity);
	bool isAboveWindow(Entity& entity);
	void removeOffScreenEntities();

	PlayerEntity* playerEntity;
	std::vector<EnemyEntity*> enemyEntities;
	std::vector<BulletEntity*> playerBullets;
	std::vector<BulletEntity*> enemyBullets;
	std::vector<ExplosionEntity*> explosionEntities;
	float enemySpawnTimer;
	float timePassed;
};

