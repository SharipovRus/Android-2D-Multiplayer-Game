# Android-2D-Multiplayer-Game
Android-2D-Multiplayer-Game made in Unity

# Created mechanics:

## Multiplayer/menu:
- Multiplayer is connected via Photon
- The player is uploaded to the server when connected to the server (up to 4 players)

## UI/Game Management:
- Loading, Lobby, Game screens
- Players can choose name
- Players can name the game rooms and connects to named rooms
- Added spawn button before entering the arena 
- Disconnect Manager (if player absent for more than 10 second, there will be a screen with disconnect popup) If the timer runs out - player will be back in the main menu
- Pop up screen if you lost the connection to internet(also allows to reconnect or go back to main menu)
- PingRate at the top left corner of the screen 

## Gameplay:
- Player controls (moving, jumping, shooting) after the connection to a server for Android(virtual joystick) and PC(keyboard controls)
- Animation of walking and shooting
- Players can shoot in the direction they are looking(Sprite flips as well) 
- A battle arena has been created
- Respawn of player if they are killed
- Health bars for players(decreases when you take damage) 
- If your player be hit - color of model will change color(red) for 1 second as in indicator of a hit

## Screenshots
- Create name menu
![Без имени](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/9eecc5d6-72cd-422d-b303-4e4dc98fe7fd)

- Room menu (can create room or join one)
![Room](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/e4c6e4db-ff9e-4065-9f24-eb43f88fdbc5)

- Spawn Button
![Spawn](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/1d5f0682-c072-480e-a77e-5455648ea95f)

- Arena with 2 players
![Lob](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/1339ebc8-6b2d-48f5-8849-ea8414c98507)

- Hit effect
![Hit](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/bfe0454c-e0e6-4df7-968a-400868085c70)

- Respawn screen 
![Respawn](https://github.com/SharipovRus/2D-Android-Multiplayer-Game/assets/106979924/32cf9a54-d945-40d8-9e38-aa437e427d7f)

