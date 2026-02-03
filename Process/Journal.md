# Game Journal Thing

# Make-A-Thing | 19-01-2026

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

# Design Journal: Exploration Prototype 1 | 27-01-2026

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
    Using UnityEngine.InputSystem; 
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

# Exploration Prototype 2 | The Fellmonger | 30-01-2026

This project is gonna be based on my experience at a Game Jam over the weekend!
https://sh4rpsteel.itch.io/the-fellmonger


## The Fellmonger: Day 1 | 30-01-2026

Considering that the game jam officially started at 8:00 pm and most of us had to go home early to catch the bus/train, we decided that the first day will be focused on exploration and prototyping.

The theme of this project was **The Hunt**


### Initial Ideas
- We considered a couple of ideas such as 
  - Platformer
  - Grocery store / food hunt
  - Boss Rush (We ended up with this one)


- We then tried to make a mood board to get an idea of what we want
  - we were inspired by the binding of issac type gameplay and visuals (Insert Picture Here)
  - We had some forest theme for like hunters
  - We wanted a deer boss


### Prototyping
- I started working on a small lowfi implementation prototype that included
  - Character movement
  - Character attack
  - Attack "movement"
  - Enemies taking damage from attacks
  - Field and borders

![PrototypeGif](/Process/Images/Prototype.gif)
*My laptop was on life support recording this


### Thoughts 
- It went very well, we got a good idea of what we wanted and that the programming side would also be possible (little did I know).


## The Felmonger: Day 2 | 31-01-2026

I don't really remember what happened, but things seemed to be going smoothly.
The artists are working hard to create the background and characters, sound designer was making some music and sound effects, and our game designer was coming up with ideas and thematics of the game.

I believe that I was writing out the scripts for the attacks.
- The first attack was the skill which had 2 ideas
  - either a Volley (Ashe w, iykyk)
  - triple shot (We decided on this one)

- The next thing was deciding enemy attacks. We had so many aspirations, but it was not possible for me to do most of them

> --- Deer Attacks ---
> Antler charge -> Glow white then attack vertically
> Back leg kick -> Melee attack
> Jump down and slam -> Small circle indicator
> Summon horse? At half health

- Based on the things here, we did not have time to create many assets or animations for them
- There were also physical constraints such as the jumping one, where if in theory it jumps from the top of the screen, it might get blocked by the wall and softlock the game.
- Same thing with antler charge where it originally attacked vertically, which doesn't make much sense if the player is on the sides.
- Instead I made it antler charge into the player's current location.
  - There were a lot of bugs where the deer would pause, then teleport at max speed to the player, killing them instantly. Other bugs made it get stuck from trying to follow the player and the position it was supposed to go towards. 


- Unfortunately the deer boss was not the biggest issue. At this point in time, it was the end of the day, and we still did not design the dragon boss.
  - It was 1 am by this point, and I was busy putting in the animations for the Deer and player, and I kept wondering what kind of boss attack we can do with the most minimal amount of assets, as the artists still need to design the dragon with less than 12 hours left.
  - And so It was critical to find a way to make the battle engaging, without making the dragon move (so we don't have to make an animation for it)


  > --- Dragon Attacks ---
  > Dice roll at half health (random attack from every single option in the game (D6) + > maybe auto kills you on contact but is avoidable)
  > Breathing fire (AOE) -> Chain reaction
  > Moving Whirlwinds -> Chain reaction


- These were the original idea, we had to incomporate dice decisions as a challenge, so I had to figure out how to make a dice attack.
- Again, since there is no guarantee that the artists can draw and animate assets in time
- I started prototyping some ideas (on paper)

- ![dragon_prototype](/Process/Images/dragon_prototype.jpg)

- The original idea was to have different effects for the dragon rolling a nat 1 and a nat 6, where 6 does a big damage attack and 1 healing the player.

- What I basically did before passing out crying is make a square shoot a bunch of triangle based on a random number roll (Imagine the image below was in programming)

![dragon_programming_prototype](/Process/Images/dragon_prototype_2.png) 


### Afterthoughts
- I ended the day at 2 am before passing out in the corner and catching a cold, but when I managed to get the programming done for one of the dragon attack, I had a glimmer of hope.

- A really cool that that happened was the attacks from the "dragon" blocked and destroyed the arrows, making the game slightly more challenging by requiring the player to time their attacks or reposition better.

- Some struggles that I had were Coroutines, since while the `WaitForSeconds()` function was still happening, it could be called again during that time, messing up some timing stuff. But I think I understand it better now.


## The Felmonger: Day 3 - End | 01-02-2026

This is the last day of the jam, I woke up at like 5 am shivering to the bone and bloodshot eyes. But I had to continue pushing.

- I started to make the common game stuff like game over screen which allowed the player to restart, the logic to open the right wall after defeating a boss, creating a portal that leads to the next level.

- I also wanted to create a special attack to give some variety to the dragon. We needed a strong "fireball" aoe attack, so for now, when the dragon rolls a 6, a big square appears and does 2 damage to the player, and slows them (we had an idea for a slowing ice breath at some point)

![dragon_aoe](/Process/Images/dragon_aoe_attack.png)


### Merge Conflicts

- After my team started to come back on site, we manage to get a lot of work done, and they helped to integrate the sound and UI. However, errors kept popping up over and over again.

 > Merge Conflicts Merge Conflicts Merge ConflictsMerge Conflicts Merge Conflicts Merge ConflictsMerge ConflictsMerge ConflictsMerge ConflictsMerge ConflictsMerge ConflictsMerge ConflictsMerge ConflictsMergeConflictsMergeConflictsMergeConflicts

- The .Unity file of a scene kept changing everytime someone made a change (duh), but that kept getting conflicted from git. **To fix this, we used Rider to go through the file, and start accepting all the changes until it wasn't angry anymore.** The good news is that we can fix things quickly. The bad news is that sometimes, some people changes didn't go through because of it. 
- This nearly costed us our audio, but fortunately there was an extension to the submission time and we managed to submit something pretty cool


### Afterthoughts: The Finale
I know that I yapped a lot about stuff that isn't just prototyping and exploring stuff, but since I did the gamejam, I wanted to use that opportunity to prototype in a realistic setting, even though the time to work on this was very short.

It was very fun, Prototyping on the first day did help us a shit ton, since the artist team got to imagine what aesthetics we were going for, the game designer knew what kind of attacks and gameplay to do, and programming let me know what was possible and not possible. This lead to a very smooth-ish game development experience.

### The Game :D
https://sh4rpsteel.itch.io/the-fellmonger 

![dear](/Process/Images/deer_final.png)
![dragon1](/Process/Images/dragon_projectile.png)
![dragon2](/Process/Images/dragon_horse.png)
**oh yeah, we also had to include a horse in the game

### Credits to my team
- Game Designer: Ethan Armstrong
- Environmmental Artist / UI Programmer: Arielle Wong
- Character Design / 2D Artist: Owen Yang
- Sound Designer: Hubert Sia
- Lead Programmer: Jimmy Le (Me)
![thumbsUp](/Process/Images/Emote.png)
----- ---------
