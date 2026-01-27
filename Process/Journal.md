# Game Journal Thing

## Make-A-Thing | 19-01-2026

### Idea: Rock Stacker 

- **Description:** Stack various-sized rocks on top of each other and try to go as high as possible. If one of your rocks touches the floor, you lose and must restart.

  
### Parts

- Floor
- Initial Block
- Rocks
- Rock Spawner
- Camera



### Thoughts
My goal with this project was to try to make it as invisible as possible. It's hard to identify good game design sometimes.
But it is very easy to notice when something is missing or a mechanic feels janky.

So, my plan is to incorporate as much user feedback as possible and reduce user frustrations.

I also wanted to make it somewhat replayable, so RNG is a must.


#### Floor

- The floor should be wide enough so that the player can place all their rocks on screen and have a longer edge so they can play on the borders as well
- The floor should not be too initially high as to give players area on their screen to fill up
- The floor should be tall enough so that when users scroll down, it won't be a void
- The floor should be able to differentiate between the base rock and regular rocks to determine if the game ends
- The floor should be able to support / not move when a rock is placed on it

#### Base Rock

- The base rock should be wide enough for users to place rocks
  - I initially made it too big, where you were able to put all the different types of rocks easily, making the game boring. I reduced it so that at most 2 different rocks can comfortably sit on it
- The base rock should be noticeably different from regular rocks
- The base rock should not trigger an end game when hitting the floor
- The hitbox of the base rock should be more forgiving to make stacking less frustrating
- The base rock should allow rocks to rest on it

#### Rocks

- Rocks should come in different shapes 
  - I settled on 3 variations: Large, Medium, and Small, and gave them all slightly different properties (height and color)
- Rocks should be able to differentiate the floor and other rocks, including base rocks
- Rocks should have physics that allow them to fall, rotate, and collide with eachother
- The rocks should be slightly difficult to stack, but not impossible
  - I made the rocks capsules so there is a curve on the edge

#### Rock Spawner
- It should be clear how to spawn rocks
  - I originally thought about having a UI Rock Pocket and dragging the rock out of it, but I settled to having it spawn on click
- It should be clear where the rocks will spawn from
  - I made the spawner follow the mouse
- It should be clear which rock is spawning next
  - I made the rock spawner copy the sprite renderer of the rock to spawn next
- It should be easy to add more rocks to spawn as options to facilitate future rocks
  - I use an array, and I use it to mess with the probability of rocks spawning (more medium and small rocks)
- Only 1 rock should spawn at a time until one lands
  - There was originally an "error" where it kept spawning rocks when the mouse button was held down
  - I decided to add a cooldown
    - To make the cooldown visible, I made the opacity of the spawner change based on whether it's available or not
  - I also didn't want to restrict how people play completely, so you can technically go really high up and spawn rocks off cooldown before they land


#### Camera 

- The camera should leave enough room for players to not feel constricted
- The camera should be adjustable to where the players are at
  - A slightly frustrating thing is that the camera can't exactly keep up at the same pace as the player, since some players might stack it vertically all the way, and others might try to create a good foundation.
So to solve this, you can manually control the camera with W/S or UP/DOWN arrows, or the scroll wheel, at your own pace
  - I also made it so that when you add a rock, the camera slightly jumps up. This can help to indicate that the camera can move, and you won't get stuck too quickly.
    - It does, however, feel very janky with the sudden jump.
- The camera should not expose underground
  - I added a limit to how far down it can go
- The camera should not move left or right, so that it restricts players from building horizontally


#### Other
- For feedback, 
  - I added in sounds for spawning and landing rocks (except for the base rock, cause i didnt write a script for it)
  - I added a sound for game over
  - I added an indicator for the spawner
  - I changed the opacity of the spawner to represent the cooldown

- For comfortability (things players intuitively expect from a game)
  - I added a play again button
  - I added an exit button 
    - There lowkey should be a pause menu, but like, what are you even pausing
  - When it's game over, the game stops being controllable
- For addiction
  - I added in a rock counter so people can try to see how many rocks they can place 
    - Originally, I also wanted a height meter, but did not have time to implement it

# Make-A-Thing | 21-01-2026

Technically I already wrote about it above, but essentially I just added audio when spawning rocks and when they collide. 
As well as the game over sounds. 

I originally wanted the collisions to make sounds, but the delay made it really uncomfortable. So having a spawning sound helps to make it less awkward.

I also made each rock have a different sound. I did think about making the sounds randomized every time, but i feel like keeping them consistant between rock types is better.



## Struggles
- Most struggles come from not knowing the syntax. I mostly used AI as a way of googling this quickly.
- Choosing the proper way of using inputs. There are many ways to do it, and I kept mix and matching
  them.
- Audio sounds are slightly delayed
- I am working on a VM, so I don't have direct access to the files I create on the actual computer
- I accidentally made the large rock and small rock a prefab variant of the medium rock, so when I change stuff for the medium rock, it changes it for the others too. 
This became kind of convenient sometimes.

## Resources

- Everything is made by me*
  - The rocks are technically colored Unity sprites
  - The sounds are made with MuseScore Studio and trimmed with Audacity
  - The game engine is Unity 6.3.4f1
  - The scripts were written in JetBrains' Rider

-----------

## Design Journal: Exploration Prototype 1 | 27-01-2026

Alright, since we didn't really have a project to do, 
I decided that I wanted to complete the Catch-A-Mall project, while using my own coding methods
and respecting the original's design.

### Movement and Input
- Since I am somewhat experienced using Unity, I wanted to use the new Input System Package.
  - It is quite easy, but again, I did struggle with the syntax on how to get data from the inputs.
  - Essentially, there is an Input Action file which you can modify through the project settings that holds all the keybindings
for a specific action. These bindings return value that you give them (left gives -1, right gives 1). You can then use these values to determine the direction
that the player is moving towards.
  - In the code, you need to find the action that you want :
    ```
    Using Unity.InputSystem; 
    ...
    
    public InputActionAsset inputAction;         // Put the InputSystem_Actions file generated in here through the editor
    private InputAction move_action;             // This will hold the input that we are getting (Left or Right)
    
    void Awake(){                                  // I think you can use Start() as well
      move_action = inputAction.FindAction("Move") // "Move" is the name of the input that we want
    }
    
    void Update(){
      if(move_action.IsPressed()){
       Debug.Log(move_action.ReadValue(Vector2).x)  // This is how you get the value of it, Vector2 is there because WASD is included in "Move" 
      }
    
    }
    ```
  
### Building Dropper Script
- I basically did the same as the professor, but instead of using Sprite[], I used GameObject[] (Arrays of Prefabs)
- This way, it would be more flexible to add different types of buildings later on 
- This also avoids hardcoding building types as we can use prefab buildings with preset tags 
- I did this to try to simplify the code and allow the possibility of updating it (I won't)


### Buildings
- Like I wrote earlier, I create prefabs for each building. It is lowkey redundant, but if we were to give different values to the buildings,
we could modify the code to use the score of the object instead of +/- 1
- And of course, the mall prefab gets the "Mall" tag and the others get the "NotaMall" tags in case we needed those in class

### Something New and Fun
- **CoRoutines**
  - I did have some experience using CoRoutines, but I didn't really understand how they worked. 
  - I previously used them for animations, since you can play / stop them.
  - Based on this example, it seems like it just plays a sequence of events, and can recursively call itself to form a loop.
  - I think this would be really good for pausing as well since CoRoutines can be stopped/destroyed
- **TextMeshPro**
  - I have used text before, but there were so many errors/changes get made in the git everytime you touch something
  - This time, I wanted to fix these issues and I found that dynamic fonts are the problem
    - To solve this, you simply need to create and use a Static font
      - (I did ask AI how to fix this)

### Today's Conclusion
I basically finished the game based on what I can see posted in the professor's GitHub, 
we'll see what we do further with this project in class.

-----------------
