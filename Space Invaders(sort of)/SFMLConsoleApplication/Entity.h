#pragma once

#include "SFML/Graphics/Sprite.hpp"
#include "SFML/Graphics/RenderWindow.hpp"

class Entity
{
public:
	Entity(sf::Sprite sprite);
	~Entity();

	void drawEntity(sf::RenderWindow& window);
	bool collidesWith(const Entity& entity) const;
	sf::Vector2f getSize(); 
	sf::Vector2f getPosition();

protected:
	sf::Sprite sprite;

private:

};

