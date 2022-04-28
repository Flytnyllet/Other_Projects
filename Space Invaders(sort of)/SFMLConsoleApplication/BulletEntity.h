#pragma once

#include "Entity.h"
#include "SFML/Graphics/Sprite.hpp"

class BulletEntity : public Entity
{
public:
	BulletEntity(sf::Sprite bulletSprite, sf::Vector2f velocity);
	~BulletEntity();

	void Move(float deltaTime);

private:
	sf::Vector2f velocity;

};

