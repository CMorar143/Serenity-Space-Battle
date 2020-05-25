# Serenity Space Battle

## Premise/Description of the Assignment
This project will focus on recreating a space battle from the sci-fi movie Serenity (2005). The space battle will be recreated using the Unity3D game engine. The space battle recreated for this project can be found below:

[![Video](https://img.youtube.com/vi/X_VSJfHiNPA/0.jpg)](https://www.youtube.com/watch?v=X_VSJfHiNPA)

The scene will start off with the antagonists moving up towards a fog/cloud wall. There is a main enemy ship, and the others use offset pursue to follow the main enemy into position. After the enemies are all in position, their behaviours are turned off and their dampness is increased to give them a natural look when they are slowing down and stopping.

At this point the protagonist ships are all on the other side of the cloud. The main protagonist (the Serenity ship) is given the arrive behaviour so it will arrive through the cloud. Just before the serenity ship starts its journey however, the other protagonist ships are given offset pursue (to pursue the serenity) but the offset pursue is then disabled. Once the serenity ship has assroved at it's destination, the offset pursue is enabled in the other protagonists. This was done so the Serenity is the first to arrive through the clouds, only to have more allies show up (as a surprise for the antagonists and the audience).

After the standoff has occurred all of the enemy ships will randomly select one (by using tags) of the protagonist ships and then pursue them. The protagonists are all trying to get behind the enemy blockade. However, whenever one of the enemy ships gets too close to them, they will flee from the enemy ship until they are out of range. Once they are out of range they will go back to trying to get behind the blockade.

### Main scripts that I wrote
- BattleBehaviour.cs
  - This is for after the standoff occurs
- cameraBehaviour.cs
  - This is for moving the camera durring the scene
  - It also controls a lot of the voice acting lines
- Laser.cs
  - This is for the lasers that the enemies shoot at the protagonists
- Mothership.cs
  - This is for the enemy ships behaviour
  - This also controls some of the voice acting lines
- Protagonists.cs
  - This is for controlling the protagonists behaviour during the standoff
  
### What I am most proud of
I am definitely most proud of the standoff. The way the enemy ships arrive in formation, and the fact that the one single serenity ship shows up. The serenity ship appears a lot smaller, which gives off an appearance of how ill-equiped and desperate they are compared to the antagonists of the scene. And then the supporting protagonists show up by the Serenity's side only to end up in a cinematic shot showing the good vs evil. 


## Story Synopsis
The basic idea of the story is based heavily off the movie. It is set in the 26th century, by then humans have become an interstellar species, trekking to another solar system. The government known as the Allegience now rules the newly colonised solar system. A woman known as River Tam has uncovered well-kept secrets held by top government officials. She finds herself in the company of two smugglers, captains of the Serenity, who have been tasked with helping River Tam disseminate these secrets. They are on their way to meet with a hacker who can help broadcast this message. The broadcast could bring down the Allegience, putting an end to their tyranny. Being fully aware of this, the Allegience creates a blockade with the hope of destroying the Serenity and protecting their incriminating secrets. An epic space battle ensues.

## Storyboard
The scene begins with an opening title card along with a brief description of what's going on. This will help set up the scene.

**The Storyboard is as Follows:**

![Storyboard 1](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/1.jpg)
![Storyboard 2](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/2.jpg)
![Storyboard 3](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/3.jpg)
![Storyboard 4](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/4.jpg)
![Storyboard 5](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/5.jpg)
![Storyboard 6](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/6.jpg)
![Storyboard 7](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Drawn/7.jpg)



**If the incredible art above does not fully convey the idea too well, here is the same storyboard recreated with unity and shots from the movie itself:**

![Storyboard 1](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/1.JPG)
![Storyboard 2](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/2.JPG)
![Storyboard 3](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/3.JPG)
![Storyboard 4](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/4.JPG)
![Storyboard 5](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/5.JPG)
![Storyboard 6](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/6.JPG)
![Storyboard 7](https://github.com/CMorar143/Serenity-Space-Battle/blob/master/StoryBoard/Final/Good/7.JPG)



