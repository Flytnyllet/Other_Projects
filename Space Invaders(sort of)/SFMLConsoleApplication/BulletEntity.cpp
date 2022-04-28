#include "BulletEntity.h"

using namespace sf;
using namespace std;

BulletEntity::BulletEntity(Sprite bulletSprite, Vector2f velocity) : Entity(bulletSprite), velocity(velocity)
{
	sprite.setScale(0.5, 0.5);
}


BulletEntity::~BulletEntity()
{
}

void BulletEntity::Move(float deltaTime)
{
	sprite.move(velocity*deltaTime);
}
