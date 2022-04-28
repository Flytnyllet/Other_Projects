#include "ExplosionEntity.h"

using namespace sf;
using namespace std;

ExplosionEntity::ExplosionEntity(Sprite sprite) : Entity(sprite), timeLeft(0.6)
{
	
}


ExplosionEntity::~ExplosionEntity()
{
}

bool ExplosionEntity::shouldRemove(float deltaTime)
{
	timeLeft -= deltaTime;

	return timeLeft <= 0;
}
