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

This project is gonna be based on my experience at a Game Jam over the weekend with Arielle, Ethan, Owen, and Hubert!
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
  >   Antler charge -> Glow white then attack vertically
  >   Back leg kick -> Melee attack
  >   Jump down and slam -> Small circle indicator
  >   Summon horse? At half health

- Based on the things here, we did not have time to create many assets or animations for them
- There were also physical constraints such as the jumping one, where if in theory it jumps from the top of the screen, it might get blocked by the wall and softlock the game.
- Same thing with antler charge where it originally attacked vertically, which doesn't make much sense if the player is on the sides.
- Instead I made it antler charge into the player's current location.
  - There were a lot of bugs where the deer would pause, then teleport at max speed to the player, killing them instantly. Other bugs made it get stuck from trying to follow the player and the position it was supposed to go towards. 


- Unfortunately the deer boss was not the biggest issue. At this point in time, it was the end of the day, and we still did not design the dragon boss.
  - It was 1 am by this point, and I was busy putting in the animations for the Deer and player, and I kept wondering what kind of boss attack we can do with the most minimal amount of assets, as the artists still need to design the dragon with less than 12 hours left.
  - And so It was critical to find a way to make the battle engaging, without making the dragon move (so we don't have to make an animation for it)


  > --- Dragon Attacks ---
    >   Dice roll at half health (random attack from every single option in the game (D6) + > maybe auto kills you on contact but is avoidable)
    >   Breathing fire (AOE) -> Chain reaction
    >   Moving Whirlwinds -> Chain reaction


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

# Exploration Prototype 3 | 08-02-2026

I was feeling a little burned out from programming from the game jam, so I will be mainly hand drawn prototypes this week.

As such, let's try to think up some big fun ideas.

## Exploration Prototype 3: Day 1 | 08-02-2026

I really liked the Golden Sun growing up. I played it before I could even read, and what really hooked me in were the
"psynergy" mechanics. These were essentially the HMs from pokemon that you can use outside of battle to manipulate objects in the overworld, except those skills can be used for different situations.

![GoldenSunGames](/Process/Images/golden_sun_games.jpeg)

![Psynergy](/Process/Images/Ankohl.gif)

So I was thinking, what if I "made" a puzzle game using similar mechanics like psynergy.

### Bootleg Golden Sun Prototype
- Here is a **Role Prototype** of the kind of psynergy that I want to implement
- The first one is the skill to shrink and grow object. 
  - The idea is that growing the cube will let you access a path on the elevated level, and shrinking things could unblock paths
- The middle one is just the Move psynergy from the game, but I believe that if I made a game it would be essential to be able to move things
- The last one is the ability to cut things
  - This would be useful to cut marked walls to create stairs and cut vines blocking the way.

![CustomPsynergy](/Process/Images/psynergy_prototype.png)


- After this point, I was feeling a bit overwhelmed, and did not feel like coding this. It would be fun for a different time, but not today.


## Exploration Prototype 3: Day 2 | 09-02-2026

I was still on the fence about programming, so I continued to explore some big ideas on paper and incorporate the singleton spawning stuff we saw in class.
This next one is inspired by a lot of Korean Webtoons where it's the apocalypse and there's a "system" that gives people powers to fend off monster invasions.

### Gamer Apocalypse Idea
- The idea is that you are a gamer that lives alone and have not left your house for over a month because you were busy playing games. You eventually run out of cup noodles, and decide to go out to the local convenient store to buy more.
- You eventually reach the store, notice that the cashier is a monster, scream, then someone saves you and explains the situation to you that an apocalypse happened and a "Game System" suddenly appeared 
- You notice the "pop up screen" in the corner of your eye (screen) that you have been ignoring because you thought it was a symptom from playing too much games
- It tells you the world is ending, but you have been bestowed a power to help save the world
- The power lets you use abilities of the games you played recently
  - The games you played in the intro will serve as a tutorial of the abilities
  - These games are
    - Minecraft
    - League of Legends
    - Valorant
  - More games can be collected in the world and for you to use
  - Eventually you can find a Game Developer Kit and be able to mix and match abilities from different games for a custom character


### The Prototypes
I made some Look Feel / Role Prototypes for each of the games forms that can be switched at any time using TAB

*Ignore the big ass white space in the images


#### Minecraft
The purpose of the Minecraft Form is to offer the player base building and crafting abilities, while also providing the iconic combat gameplay

![MinecraftUI](/Process/Images/mine_ui.png)

Abilities
- 1: Switch to Sword
- 2: Switch to Bow and Arrow
- 3: Switch to Place Block Mode
- 4: Switch to Place TNT
- Q: Shield Block
- E: Open Inventory
  - Can craft stuff inside

Special Mechanics
- Breaking buildings drops Blocks that the user can place down to build walls
- Killing enemies drop crafting material
- Inventory can carry the materials and craft weapon upgrades, furniture, other game objects
- Crafting Items, Armor, Weapon Upgrades (Sword -> Diamond Sword/ Gun -> Vandal)
- Arrows can curve over walls
- Monsters from the minecraft world will spawn in
  - Creepers 
  - Skeletons
  - Zombies


#### Valorant
The purpose of this Valorant Form is to offer players gun gameplay, where they can aim to shoot the enemies.
This form is mostly focused on killing 

![ValorantUI](/Process/Images/val_ui.png)

Abilities
- 1: Switch to Main Gun
- 2: Switch to Pistol
- 3: Switch to Knife
- 4: Plant the spike (Detonate after a certain amount of time)
- Q: Smoke
  - Create a sphere that drops enemy aggro and vision when entering inside
- E: Ultimate Ability (Throw knives that pierces enemies for a certain period of time) requires 5 Charges
- Shift: Quick Dash, requires 2 Charges


Special Mechanics
- Charges: Earn a Charge on enemy kill
- Ammo: Each gun has a certain amount of ammo that needs to reload once depleted
- Headshots: Enemies have points on their body that guarantees critical damage when hit


#### League of Legends
The purpose of the LOL Form is to offer players the baby difficulty and mage fantasy.
This form offers a lot of utility and crowd control.


![LOLUI](/Process/Images/lol_ui.png)

Abilities
- 1: Root
  - Throw a ball in straight line that damages and immobilize the first 2 enemies hit
- 2: Barrier
  - Create a shield for all party members that offer bonus hp
- 3: Slow Ball
  - Create a ball of light that slows enemies movement speed in an area for a certain duration. Press it again or after expiring detonates and deal damage,
- 4: Final Spark
  - Deal big damage and a straight line to all enemies in the area
- Q: Potion
  - Gives health regeneration over time
- E: Ward (explained below)


Special Mechanics
- Auto Attack (Left click): The basic attack will home in on on the enemies, but they can still be blocked
- Shields carry over when switching forms
- Wards: Creates a zone illuminated and prevents enemies from spawning in that area (they can still walk into it), 
  - They can also act as waypoints to teleport to
  - Intended to be used to set up a temporary shelter
- Monsters from the LOL world spawn
  - Melee Minions
  - Caster Minions



### Afterthought
After doing these prototypes, I do think it would be a good idea and the mechanics kind of play into each other, 
however one major issue is the keybindings. For the LOL form, the main buttons used are 1,2,3,4, while the other forms don't rely on it. This can cause players to struggle from switching to different forms

The other obvious issue is the implementation time, having to create all of these mechanics that interacts with eachother and offer the possibility of future Games affecting them will be hard to plan.

If I were to actually work on this idea, I think after creating everything listed, I would only need to focus on the narrative aspects which would make things much smoother.


---- --------

# Exploration Prototype 4 | 12-02-2026

For this week's exploration, I wanted to try to make an idea that I had after my game jam was over. 
It is called Redact Those Philes! and basically you are given a list of words to redact, and you get penalties for redacting incorrect words and not redacting the requested word.
But all of this is happening in a happy vibe workplace where you can put some fun stickers on the files.
At the end of the day, you get money for not making mistakes and pay bills.

## Exploration Prototype 4: Day 1 | 12-02-2026

![Table](/Process/Images/table.png)
![EndOfDay](/Process/Images/end_of_day.png)

- These are some Look/Feel Prototypes that I drew so that I know the layout that I need to make.

![RTPPrototype](/Process/Images/rtp_prototype.png)

- And this is a Implementation prototype that I quickly made to reflect the layout that I want.


### End Of Day
- There really wasn't much stuff that I worked on that day (mainly cause I did it during class time), but I am hoping to be able to make a working game by next thursday.



## Exploration Prototype 4: Day 2 | 17-02-2026
I continued working a bit on it in between my class. Unfortunately, Unity HUB wanted me to download a new version of Unity, so an Hour was spent waiting for it.

But today I wanted to try to use text as an object and not as a UI element. I had to do a little research to get it to work as apparently a text object is considered a 3D object and not 2D.

I also wanted to make the redaction, where a separate object that is hidden above the word will be enabled after getting clicked.
I struggled a bit because I was trying to use OnMouseDown, but it wasn't registering it.

Unfortunately I was not able to solve it that day.

## Exploration Prototype 4: Day 3 | 18-02-2026
My goal for today is to get the redaction feature working.

- I first did some more research about how to detect mouse clicks and ended up by finding out about Raycasting which is basically having an Object project an invisible line towards a point, and the first object with a collider will be returned.
  - In my case, the mouse location would be the object that does the raycasting towards the words.
  - I did take a lot of time going back and forth between debugging and thinking about how to implement stuff as well.
- The next thing was to connect the click with enabling the black bar, which was quickly done since the code was ready from day 2.

![redaction](/Process/Images/redaction.gif)

- After this, I started working on getting words from a txt file so that I can randomly generate them and changing the text when the word is spawned
  - To do this, I created a Singleton game manager instance like in class, and have that read a txt file and store the words.
  - I made a function to generate words that gets called by the words on Start


and that was basically it for today, hopefully I can grind it all tmr.

# Exploration Prototype 4: Day 4 | 19-02-2026
My plan for today is to create a spawner that will fill up the page with words.

- The first thing I thought about was the brick breaker game and how we can generate rows and columns of objects. So I stole that.
- One issue that popped up was that the size of my words were waaaay bigger than the prefab. (I did not take a picture of this)
  - After ChatGPTing my problem, It turns out that the issue is that the scale of the prefab was inheriting the parent (where it spawns inside)
  - To solve this, I simply had to remove the parent after initializing it by doing `newWord.transform.SetParent(null);`, then setting it back to whatever parent object that I want it stored in.

![redactionPage](/Process/Images/redacted_file.png)

- I then ADHD my way into setting up stuff for the future such as
  - Creating a list of words to redact, which we can compare the clicked word 
    - And while writting this I just realized that I needed a way to check if a player did not redact a redaction word.
      - The way I would probably do this to add the spawned word into a list, then loop through the list and find all the word that contains the banned word, then loop through to see if a isRedacted boolean was triggered.
      - I will probably have to optimize this later
    - I created a function that will generate an index that correspond to a word so that it can be used to compare words quickly
    - I created a function that will convert the index into the word
    - I created a function that will generate a redaction word
      - Which I then created a function that will clear out the list of redaction words and generate 3 of them to use at the start of the game.

- Essentially that was it at this time (before submission).

### Afterthought

I have grown attached to this idea so I would like to continue this project for the rest of whatever. And I won't take no for an answer :)

I do promise to polish it up and add more features and do whatever requirements we need.

-------------- 

# Game Analysis | 23-02-2026

The game that I will talk about is an old MMORPG loosely based on Celtic and Irish mythology by Nexon and Devcat is called Mabinogi.

![Mabinogi](/Process/Images/mabinogi.jpg)

## What I find Interesting

Mabinogi is not like other MMOs. Most MMOs focuses on combat or questing, while mabinogi has a lot more emphasis on "Slice of Life" gameplay. It still has combat, main story quest and there are classes called "Talents", but you can switch between them at anytime by equiping different gear. This becomes pertinant because you can have Life Talents as your "main" class such as Blacksmithing, Tailoring, Cooking, Adventuring, etc. This means that you don't need to engage in the combat aspect to play the game or you can find other people to accompany you to get materials.

![MabinogiTalents](/Process/Images/mabinogi_talents.jpg)

On top of that, there are other features of the game that promote doing stuff outside of combat. One thing is the Bard Bulletin Board, where players can use their Music Composition skill and compose Music using Music Macro Language (MML) for other players to perform. Players can then start a jam and have other players join in with their own instrument and create a symphony. With this feature, every 3-4 months there is a group of volunteer Bards that organize a whole concert featuring popular game / anime music with "cosplay" and props to support it.

![MabinogiComposition](/Process/Images/mabinogi_composition.webp)
![MabinogiBardBoard](/Process/Images/mabinogi_bard_board.png)
![MabinogiJam](/Process/Images/mabinogi_jam.gif)

There is also a whole MabiNovel feature that few people use, but essentially you can collect NPC books and create a visual novel book inside the game for other players to read using the NPCs collected as the performing characters. 

![MabinogiMabiNovel](/Process/Images/mabinogi_mabinovel.jpeg)

## Where They Have Failed

Mabinogi is in the process of going through a game engine update to Unreal Engine 5, as such there are some changes that are attempting to modernize the game. While some of the changes are good, they strip away the identity of the game.

![MabinogiEternity](/Process/Images/mabinogi_eternity.gif)

There was a mid-game dungeon that was pretty difficult to clear, but it was sort of a benchmark dungeon that beginner players used to determine how strong they were, and it was satisfying to be able to clear it. As they started rolling out changes to the dungeon system to modernize it, they made it very easy to clear, cutting content from 20-ish minutes to clear to less than 2 minutes. This is good as it allowed players with less time to play, but the difficulty became very unsatisfying. 

The major downside is that the next hardest content is too hard and has limited entry. As such, if you try to learn the dungeon and fail, you will get locked out of an attempt. This also disuades stronger player from playing with beginners as they will be punished for it.

## What Ideas / Techniques I Should Borrow

The main idea that I like from mabinogi, is that it tries to cater to every type of gameplay. Whether you like fashion, crafting, home-building, combat, questing, gathering, trading, exploring, music, socializing, roleplaying, etc, there is content that you can do and it doesn't feel like a waste of time as you are rewarded for doing these activities. 
- So for my future games, I would try to avoid having only 1 fixed solution way to play a game. For a combat-focused game, I think it would be important to have different types of combat-styles like ranged, melee, defense, support, poisons, etc.

The idea that I will try to avoid is having a low difficulty. While I like the idea of having a stress-free environment, people will end up getting unsatisfied if they can clear the content easily. I have seen this happen with my game jam, where we lowered the difficulty out of a fear that the judges won't be able to clear the game in the allocated time, which resulted in them clearing too quickly and running out of content.

- So I will be trying to encorporate more levels of progressively higher difficulty to leave the player satified and have a challenge that they can strive for.

### Bonus Mabinogi Propaganda
The mabinogi devs working on Project Eternity (Unreal Engine Update) are also making some dev blogs where they write out what they are working on, what works and doesn't work and stuff, similar to what we are doing :D

If you are interested feel free to read some entries!
https://www.nexon.com/mabinogi/micro-site/eternity

If you are feeling overwhelmed, here is one of the blogs that I find really interesting!
https://www.nexon.com/mabinogi/micro-site/eternity/news/18146

-----------

# Iterative Prototyping 1 | 19-02-2026

I know that the instructions say to talk about 3 ideas discussed during the ideation process, but in this week's post, I will focus on the one that stood out to me.

## Play My Tune

I have decided to name this game idea "Play My Tune" and this idea came about when I was sharing ideas with Bianca Gauthier. Our words were Instruments and Bills. The first idea was about busking for money, but then I started thinking about opening up an instrument store. Then with our previous ideas about Island Dating (Traveling from island to island, finding out what they like and trying to match them together), what if we make it a whore house?

It was originally a joke but instruments fit so well into all the innuendos about sex work. We kept coming up with instruments and dirty wording about it, and it kept making more sense. Considering that HuniePop / HunieCam got really popular because it was very controversial, our idea would probably do really well on the market.

## Core Idea

Similar to HuniePop or HunieCam, you are essentially operating a brothel, but for instruments. Your goal is to pay back some debt, and then make as much money as possible. The instruments are humanized(?) and there is a lot of inuendos and double entendres, but the game should be somewhat PG. There are different sections for your Instruments to work for, and you eventually build up your "shop".

## Characters

The goal of the characters are to represent an instrument in the most perverse way and to show the sterotypical tropes that one might find online. You may find this to be very controversial, but if we objectify everybody equally it should be fine (probably).

- Drums: Masochist (Likes to get hit)
- Piano: Likes to be played by multiple people at the same time (Duets)
- Frog Percussion: Abs + pole dancer (The abs represent the frogs back thingees)
- Cello: Curvy
- Church Organ: Nun
- Recorder: Lolita (Everone's first instrument) She can only work as a server*
- Mandolin: Chinese Aesthetic
- Harmonica: American
- Banjo: YeeHaw American
- Bagpipes: Scottish and speaks giberish
- Violin: Classy / Seductive
- Guitar: Likes to get fingered
- Harp: Greek Goddess vibes
- Base: Dominant
- French Horn: Larger and French
- Flute: Likes to be blown
- Maracas: Twins
- Ukelele: Innocent
- Melodica: **Legal** Japanese School Girl
- Cymbals: Loud
- Triangle: Tsundere
- Clarinet: Nerd
- Theremin: Kuudere / Does not like to be touched
- Saxaphone: In charge of the Phone Sax department
- Otamatone: Yandere
- Chimes: Elegant
- Pan Flute: Is fine with anybody
- Marimba: Office worker with a phone
- Keyboard: Roleplay (Can immitate other instruments)
- Bowed / Instruments with a stick: Pole Dancers

![Cello](/Process/Images/playmytune_cello.png)
![Frog](/Process/Images/playmytune_frog.png)

You can eventually hire characters to work at your shop, and view information about them. You can choose who you hire based on what station or attribute you need. These attribute affect where you can place the and bonus rewards when they match the required attribute.

![Hire](/Process/Images/playmytune_hire.png)
![CelloInfo](/Process/Images/playmytune_info_cello.png)
![KeyboardInfo](/Process/Images/playmytune_info_piano.png)

## Game Mechanics

Your shop is a mix of a bar / instrument rental shop. Eventually you will unlock the Operation rooms where you can host different services that will provide you with more money. Here are some events that are mapped to some lewd counterparts.

![BarRoom](/Process/Images/playmytune_room_main.png)
![OperationRoom](/Process/Images/playmytune_room_operation.png)

- Live Performances: Cam shows / Livestream of the instruments playing their respective instrument
- Instrument Rental: Escorting, Some customers might want to rent out some instruments with certain preferences
- Phone Sax: Phone Sex, People will call the characters in these posts and make passive income.
- Entertainers: People that perform on stage or entertain patrons at their seats
- Host / Hostest: People that serve food / drinks
- Orchestras: Orgies but professional, you can select multiple characters to participate and get rewards back later.

![CamShow](/Process/Images/playmytune_liveperformance.png)
![Rent](/Process/Images/playmytune_rent.png)
![Orchestra](/Process/Images/playmytune_orchestra.png)

## Conclusion

This idea is lowkey very good but I don't think it will be executed anytime soon. I might one day come back to this idea.

------------------------------
# Iterative Prototype 2 | 26-02-2026 - 12-03-2026

So for this week, I decided that I wanted to continue working on my idea from 2 weeks ago (Redact Those Philes).

## What I did a while ago

Right after I wrote out the journal for that week, I worked on 
- Displaying the Banned Words
- Difficulty scaling off of Levels
- Pages / Rounds
- Days (Levels)
- Summary Screen (No Data connected)

![endScreen1](/Process/Images/end_screen_v1.png)

## Day 1

### End / Summary Screen

Now, I need to link the actual values to the screen and I should also display the Total at the end.
All I need to do is to put all the text boxes as Attributes of a script and get the values from the Singleton GameManager Object.
The total is slightly different as I needed to create a new function to calculate the amount to add, 
as I don't want to directly add it to the balance right away or else it will override the "Balance" field.

![endScreen2](/Process/Images/end_screen_v2.png)
*the data is still not connected in this picture, but imagine it*

After this, I needed to be able to open this screen at the end of a Day, but then that means that I need to connect the next Day button to call the `NewDay()` function


### ReFormatting
The words felt wayy to small to see, which makes it a bit uncomfortable to play the game. I originally kept it that way because it felt like a word document. 
But as I kept play testing, it was annoying to put my face up to the screen, so I decided I should make it bigger as a quality of life change.

![oldVersion](/Process/Images/redacted_file.png)

The issues with making the font bigger is that the longer words would overlap or be split on 2 lines. My solution to this is to get rid of the bigger words, which was only "Strawberry".
With the words bigger, I needed to change the spawners to place them properly. 
Fortunately, I set attributes for that code to have a width_between, height_between, and margins to be easily modifiable.
I also needed to change the File size so that it can properly fit the bigger words. 
At the same time, I decreased the amount of maximum rows so that it gets less overwhelming.

![day1](/Process/Images/day1.png)

### World Events

Since my game is based on how the Epstein files are getting handled, I thought that it would be pretty funny to include some events that affect gameplay to be based on real life stuff.
The events that I thought of are:
- "The DOW is over 50000!": No Penalties for Mistakes
- "Getting Tariff Dividends": Double Pay
- "Government Shutdown": No Pay
- "We are getting investigated": Double Penalties for Mistakes
- "Deadline to release files is approaching!": Longer Time Limit per Pages

Some other ones that I am thinking about rn but haven't implemented
- "Foreign Interference": Get Donation Money
- "Rent Freeze": No Bills 
- "Flu Season": More Bills


The hardest part was thinking about how to implement this system. 
I originally set it up as a bunch of booleans so that I could have 2 effects at the same time if I wanted to.
The issue is that I have to go through a bunch of if statements to check if an event is set up as True.

I then switched over to an array of booleans so I can quickly loop through them. 
Then I realized that I was stupid and just needed a integer variable and use a switch case to set up the events.
Since I would still have to write unique code for new events, putting them all in a switch case means that I can just add a new Case for a new event.
While this is similar to just writing a bunch of if statements with the boolean array, I don't need to check if a boolean is true or not, I just need to compare the integer I get from randomly generating it.

Then I wrote a function to help reset all the stats changed from the events. This gets called before a world event is set up so that they don't get affected by the previous day.

![doublePay](/Process/Images/doublePay.png)
![doublePenalty](/Process/Images/doublePenalty.png)

### The next step

The next thing that I plan to do is to have a black screen that fades in, then displays the end of day summary, and fade back to black.
While its black a second time, this gives me the opportunity to make a text dialogue pop up for Bribing, then after that I can pop up a newspaper with world events.

## BONUS 
During the reading week, I had to do a challenge for an interview. The challenge was to create a game / prototype about the human digestive system.

I tried listening to YouTube videos about this subject, and I did not understand a single word. So instead, I decided to focus on the coding part.

### Ideation

Every video that I watched was about food going through the mouth, then the oesophagus, the stomach, and finally the intestines.
The first thing that I thought about was a Tower Defense Game. Since we want to break down the food, using organs and things in our body that disolves the food as the Towers was genius.

### Process

I created a google docs and started listing out all the Objects, Functions, and Attributes that I might need to implement. 
I also wanted to try to impress the people by incorporating inheritance and design patterns like the singleton instance, decorator, strategy, etc.
For example, There is a base "Entity" script, which both the Towers and Enemies inherit from, giving them the same attributes and functions.

![Doc1](/Process/Images/hdtd_doc1.png)
![Doc2](/Process/Images/hdtd_doc2.png)

I also included some moodboard images to help with the layout of the UI.

![HDTD_MoodBoard](/Process/Images/hdtd_moodboard.png)

Then I started implementing stuff.

### Implementation

The hardest things during implementation, is to create the attacking script for the towers. 
I needed it to be Generic enough so that I can reuse existing code as well as making a variety of attacks with a singular script.
To do so, I created a base abstract Attack script, which all attacks / action that affects enemies will inherit.
This Attack script will hold the information of the tower that is attacking, this allows the attack to use stats of the Attacking entity, 
making it very easy to slap it on another Tower or even Enemy. Any child can also override the functions and attributes built-in to create special effects like slowing or reducing armor.
And these scripts are also reusable and can be added on top of an attack, giving them even more effects.

An example is the speedbump which has a ReduceSpeed Module, making it deal its regular attack damage (1) and slowing enemies by a specified amount and duration.

An example of the modularity of the code is that the Spit Spitter and Acid Sprayer uses the exact same code, but different settings. The acid sprayer does have an additional Defense Reduction module tho.

### Conclusion
Since this isn't really the main topic, il keep it short, and you can try out the prototype here
https://piploop.itch.io/human-digestion-tower-defense 
