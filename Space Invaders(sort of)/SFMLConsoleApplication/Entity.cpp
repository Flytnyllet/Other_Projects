#include "Entity.h"

using namespace sf;
using namespace std;

Entity::Entity(Sprite sprite) : sprite(sprite)
{
}


Entity::~Entity()
{
}

void Entity::drawEntity(RenderWindow& window)
{
	window.draw(sprite);
}

bool Entity::collidesWith(const Entity& entity) const
{
	return sprite.getGlobalBounds().intersects(entity.sprite.getGlobalBounds());
}

sf::Vector2f Entity::getSize()
{
	auto bounds = sprite.getLocalBounds();

	return Vector2f(bounds.width, bounds.height);
}

sf::Vector2f Entity::getPosition()
{
	return sprite.getPosition();
}

