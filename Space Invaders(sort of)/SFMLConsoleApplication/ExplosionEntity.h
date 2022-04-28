#pragma once

#include "Entity.h"

class ExplosionEntity : public Entity
{
public:
	ExplosionEntity(sf::Sprite sprite);
	~ExplosionEntity();

	bool shouldRemove(float deltaTime);

private:
	float timeLeft;

};

