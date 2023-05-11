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
![Без имени](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/15dcb36a-1a54-485a-94f5-fe23547b8e0a)

- Room menu (can create room or join one)
![Room](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/839376ac-fd6c-4aab-a36c-7e42184cb85b)

- Spawn Button
![Spawn](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/649e7282-e7d2-4cf7-95fb-00e71f49d26c)

- Arena with 2 players
![Lob](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/08d6eafb-f875-4988-8f69-cba70f218678)

- Hit effect
![Hit](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/748b61db-5c82-4e16-8450-360247dad1c6)


- Respawn screen 
![Respawn](https://github.com/SharipovRus/Android-2D-Multiplayer-Game/assets/106979924/c98ecc9f-22e9-4f11-8ec8-4017105d1dc5)

