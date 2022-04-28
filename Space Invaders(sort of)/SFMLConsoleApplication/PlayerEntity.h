#pragma once

#include "SFML/Graphics/Sprite.hpp"
#include "SFML/Graphics/RenderWindow.hpp"

#include "BulletEntity.h"
#include "Entity.h"
#include "SFML/Graphics/Texture.hpp"

class PlayerEntity : public Entity
{
public:
	PlayerEntity(sf::Sprite sprite, sf::Vector2u windowSize, sf::Texture& bulletTexture);
	~PlayerEntity();

	void Move(float deltaTime);
	std::vector<BulletEntity*>* Shoot(float deltaTime);

private:
	void ConstrainMove();
	sf::Vector2u windowSize;
	sf::Texture& bulletTexture;
	float timeLeftToShoot;
};

