
#pragma once

#ifndef INCLUDED_COIN
#define INCLUDED_COIN

#include <vector>
#include <string>
#include <SFML/Graphics.hpp>

using namespace std;
using namespace sf;

class Coin 
{
public:
	Coin(RenderWindow *window, Sprite mySprite);
	~Coin();
	void DrawCoin();
	void updateCoin();
	bool coinSpawn = true;
	Vector2f getPosition();
	float getCoinRadius();

private:
	RenderWindow *window;
	Sprite coinSprite;

};

#endif

//sfml - graphics - d.lib