#pragma once

#include "SFML/Graphics/Sprite.hpp"
#include "SFML/Graphics/RenderWindow.hpp"

#include "Entity.h"
#include "BulletEntity.h"
#include "ExplosionEntity.h"

class EnemyEntity : public Entity
{
public:
	EnemyEntity(sf::Sprite sprite, sf::Vector2u windowSize, sf::Vector2f enemyVelocity, sf::Texture& bulletTexture, sf::Texture& explosionTexture);
	~EnemyEntity();

	ExplosionEntity* createExplosion();
	void Move(float deltaTime);
	BulletEntity* Shoot(float deltaTime);

private:
	void ConstrainMove();
	sf::Vector2f enemyVelocity;
	sf::Vector2u windowSize;
	sf::Texture& bulletTexture;
	sf::Texture& explosionTexture;
	float timeLeftToShoot;

};

