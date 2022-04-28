
#include "Coin.h"

Coin::Coin(RenderWindow *window, Sprite mySprite)
{
	Coin::window = window;
	coinSprite = mySprite;

	coinSprite.setScale(0.1f, 0.1f);
	coinSprite.setPosition(Vector2f(rand() % (window->getSize().x - (int)coinSprite.getGlobalBounds().width), -100));
}

Coin::~Coin()
{

}

void Coin::DrawCoin()
{
	window->draw(coinSprite);
}

void Coin::updateCoin()
{
	coinSprite.move(0, 3.5);
}

Vector2f Coin::getPosition()
{
	return coinSprite.getPosition();
}

float Coin::getCoinRadius()
{
	return coinSprite.getGlobalBounds().width/1.5;
}

