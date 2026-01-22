## Game Journal Thing

### Idea: Rock Stacker 

- **Description:** Stack various size rocks on top of each other and try to go as high as possible. If one of your rocks 
and touch the floor, you lose and must restart.

  
### Parts

- Floor
- Initial Block
- Rocks
- Rock Spawner
- Camera



### Thoughts
My goal with this project was to try to make it as invisible as possible. Its hard to identity good game design sometimes.
But it is very easy to notice when something is missing or a mechanic feels janky.

So, my plan is to incorporate as much user feedback as possible and reduce user frustrations.

I also wanted to make it somewhat replayable, so RNG is a must.


#### Floor

- The floor should be wide enough so that the player can place all their rocks on screen and have a longer edge so they can play on the borders as well
- The floor should not be too initially high as to give players area on their screen to fill up
- The floor should however be tall enough so that when users scroll down, it won't be a void
- The floor should be able to differentiate between the base rock and regular rocks to determine if the game ends
- The floor should be able to support / not move when a rock is placed on it

#### Base Rock

- The base rock should be wide enough for users to place rocks
  - I initially made it too big, where you were able to put all the different types of rocks easily, making the game boring. I reduced it so that at most 2 differnt rocks can confortably sit on it
- The base rock should be noticibly different from regular rocks
- The base rock should not trigger an end game when hitting the floor
- The hitbox of the base rock should be more forgiving to make stacking less frustrating
- The base rock should allow rocks to rest on it

#### Rocks

- Rocks should come in different shapes 
  - I settled with 3 variations: Large, Medium, Small, and gave them all slightly different properties (height and color)
- Rocks should be able to differentiate the floor and other rocks including base rocks
- Rocks should have physics that allow them to fall, rotate and collide with eachother
- The rocks should have a slightly difficult to stack, but not impossible
  - I made the rocks capsules so there is a curve on the edge

#### Rock Spawner
- It should be clear how to spawn rocks
  - I originally thought about have a UI Rock Pocket and dragging the rock out of it, but I settled to having it spawn on click
- There should be clear where the rocks will spawn from
  - I made the spawner follow the mouse
- It should be clear which rock is spawning next
  - I made the rock spawner copy the sprite renderer of the rock to spawn next
- It should be easy to add more rocks to spawn as options to facilitate future rocks
  - I use an array and I use it to mess with the probability of rocks spawning (more medium and small rocks)
- Only 1 rock should spawn at a time until one lands
  - There was originally an "error" where it kept spawning rocks when the mouse button is held down, i decided to add a cooldown
    - To make the cooldown visible, i make the opacity of the spawner to change based on if its available or not
  - I also didn't want to restrict how people play completely so you can technically go really high up and spawn rocks off cooldown before they land


#### Camera 

- The camera should leave enough room for players to not feel constricted
- The camera should be adjustable to where players are at
  - A slight frustrating thing is that the camera can't exactly keep up at the same pace as the player, since some player might stack it vertically all the way, and others might try to create a good foundation.
So to solve this, you can manually control the camera with W/S or UP/DOWN arrows, or scroll wheel, at your own pace
  - I also made it so that when you add a rock the camera slightly jumps up, this can help to indicate that the camera can move, and you won't get stuck too quickly.
    - It does however feel very janky with the sudden jump.
- The camera should not expose underground
  - I added a limit to how far down it can go
- The camera should not move left or right so that it restricts players into building vertically


#### Other
- For feedback, 
  - I added in sounds for spawning and landing rocks (except for the base rock, cause i didnt write a script for it)
  - I added a sound for game over
  - I added an indicator for the spawner
  - I changed the opacity of the spawner to represent the cooldown

- For comfortability (things players intuitively expect from a game)
  - I added a play again button
  - I added an exit button 
    - There lowkey should be a pause menu, but like what are you even pausing
  - When its game over, the game stops being controllable
- For addiction
  - I added in a rock counter so people can try to see how many rocks they can place 
    - Originally i also wanted a height meter, but did not have time to implement it

### Struggles
- Most struggles comes from not knowing the syntax. I mostly used AI as a way of googling this quickly.
- Choosing the proper way of using inputs. There are many ways to do it and i kepts mix and matching
  them.
- Audio sounds are slightly delayed
- I am working on a VM so i don't have direct access to files I create on the actual computer
- I accidentally made the large rock and small rock a prefab variant of the medium rock, so when i change stuff for the medium rock, it changes it for the others too. 
This became kind of convienent sometimes.

### Resources

- Everything is made by me*
  - The rocks are technically colored unity sprites
  - The sounds are made with MuseScore Studio and trimmed with Audacity
  - The game engine is Unity 6.3.4f1
  - The scripts were written in JetBrain's Rider