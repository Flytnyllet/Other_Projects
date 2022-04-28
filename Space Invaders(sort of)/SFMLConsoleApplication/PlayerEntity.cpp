#include "PlayerEntity.h"
#include "SFML/Graphics.hpp"

using namespace sf;
using namespace std;

PlayerEntity::PlayerEntity(Sprite sprite, Vector2u windowSize, Texture& bulletTexture) 
	: Entity(sprite), windowSize(windowSize), bulletTexture(bulletTexture), timeLeftToShoot(0)
{

}


PlayerEntity::~PlayerEntity()
{
}

void PlayerEntity::Move(float deltaTime)
{
	Vector2f moveDelta(0, 0);

	if (Keyboard::isKeyPressed(Keyboard::W))
	{
		moveDelta.y -= 230;
	}
	if (Keyboard::isKeyPressed(Keyboard::A))
	{
		moveDelta.x -= 230;
	}
	if (Keyboard::isKeyPressed(Keyboard::S))
	{
		moveDelta.y += 230;
	}
	if (Keyboard::isKeyPressed(Keyboard::D))
	{
		moveDelta.x += 230;
	}
	sprite.move(moveDelta*deltaTime);

	ConstrainMove();
}

std::vector<BulletEntity*>* PlayerEntity::Shoot(float deltaTime)
{
	timeLeftToShoot -= deltaTime;

	if (Keyboard::isKeyPressed(Keyboard::Space) && timeLeftToShoot <= 0)
	{
		timeLeftToShoot = 0.6;

		Sprite bulletSprite(bulletTexture);
		Vector2f bulletSpawnPos = sprite.getPosition();
		bulletSpawnPos.x += sprite.getGlobalBounds().width/2;

		vector<BulletEntity*>* bullets = new vector<BulletEntity*>();

		//calculate position on first bullet
		auto bulletBounds = bulletSprite.getGlobalBounds();
		bulletSpawnPos.x -= bulletBounds.width / 2;
		bulletSpawnPos.y -= bulletBounds.height / 2;

		//set position for first bullet
		bulletSprite.setPosition(bulletSpawnPos);

		//spawn first bullet to list
		bullets->push_back(new BulletEntity(bulletSprite, sf::Vector2f(0, -340)));

		//move to 2nd bullets position
		bulletSprite.move(-sprite.getGlobalBounds().width / 4, sprite.getGlobalBounds().height / 2);

		//spawn 2nd bullet to list
		bullets->push_back(new BulletEntity(bulletSprite, Vector2f(-340, -340)));

		//move to 3rd bullets position
		bulletSprite.move(sprite.getGlobalBounds().width / 2, 0);

		//spawn 3rd bullet to list
		bullets->push_back(new BulletEntity(bulletSprite, Vector2f(340, -340)));

		return bullets;
	}

	return nullptr;
}

void PlayerEntity::ConstrainMove()
{
	sf::FloatRect pBounds = sprite.getGlobalBounds();

	if (pBounds.left + pBounds.width > windowSize.x)
	{
		sprite.setPosition(windowSize.x - pBounds.width, sprite.getPosition().y);

		/*auto diff = window.getSize().x - pBounds.left - pBounds.width;
		Player.move(diff, 0);*/

	}
	else if (pBounds.left < 0)
	{
		sprite.setPosition(0, sprite.getPosition().y);
		//Player.move(-pBounds.left, 0);
	}

	if (pBounds.top < 0)
	{
		sprite.setPosition(sprite.getPosition().x, 0);

	}
	else if (pBounds.top + pBounds.height > windowSize.y)
	{
		sprite.setPosition(sprite.getPosition().x, windowSize.y - pBounds.height);
	}
}
