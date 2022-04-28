#include "EnemyEntity.h"

using namespace sf;
using namespace std;

EnemyEntity::EnemyEntity(sf::Sprite sprite, Vector2u windowSize, Vector2f enemyVelocity, Texture& bulletTexture, Texture& explosionTexture)
	: Entity(sprite), windowSize(windowSize), enemyVelocity(enemyVelocity), bulletTexture(bulletTexture), explosionTexture(explosionTexture), timeLeftToShoot(0)
{
	
}

EnemyEntity::~EnemyEntity()
{
}

ExplosionEntity * EnemyEntity::createExplosion()
{
	Sprite explosionSprite(explosionTexture);
	Vector2f explosionPosition = sprite.getPosition();
	explosionPosition.y -= 60.0f;

	explosionSprite.setPosition(explosionPosition);
	ExplosionEntity* explosion = new ExplosionEntity(explosionSprite);

	return explosion;
}

void EnemyEntity::Move(float deltaTime)
{
	sprite.move(enemyVelocity*deltaTime);
	ConstrainMove();
}

void EnemyEntity::ConstrainMove()
{
	auto enemyBounds = sprite.getGlobalBounds();

	if (enemyBounds.left < 0)
	{
		sprite.setPosition(0, sprite.getPosition().y);
		enemyVelocity.x = -enemyVelocity.x;
	}
	else if (enemyBounds.left + enemyBounds.width > windowSize.x)
	{
		sprite.setPosition(windowSize.x - enemyBounds.width, sprite.getPosition().y);
		enemyVelocity.x = -enemyVelocity.x;
	}
}

BulletEntity* EnemyEntity::Shoot(float deltaTime)
{
	timeLeftToShoot -= deltaTime;
	if (timeLeftToShoot <= 0)
	{
		timeLeftToShoot = 1;
		Sprite bulletSprite(bulletTexture);
		Vector2f bulletSpawnPos = sprite.getPosition();
		bulletSpawnPos.x += sprite.getGlobalBounds().width / 2;

		auto bulletBounds = bulletSprite.getGlobalBounds();
		bulletSpawnPos.x -= bulletBounds.width / 4;
		bulletSpawnPos.y -= bulletBounds.height;

		bulletSprite.setPosition(bulletSpawnPos);		

		return new BulletEntity(bulletSprite, Vector2f(0, 300));
	}

	return nullptr;
}


