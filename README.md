# TeamGamefeels

#### Current State of the Code

Both Dreyar and Vanguard are on the General Scene;  Mocap and the basic (tested) mixamo animations are available for Dreyar and Vanguard. 
Mixamo animations are character specific and are located at: Assets/Character/[Dreyar/Vanguard]/...

Controls, Scripts for Dreyar:
- Character/Controller/Keymapping
- Use this and input settings for managing the controls.  

Currently, WASD maps to camera and Arrows map to movement. 
- There are two cameras in the scene at the moment (each is an audio listener, so that is where the error is coming from)
- Delete one of the cameras to focus in on one character. 

##### In order to swap between scenes...

 - create your scene, 
 - place whichever scenes you want to swap between (0-7 for now) in your build settings (add them to build)
 - make sure the script Utility/LevelManager is on some object in each scene
 

### Current Scene Scenarios for M2:

- Chris: Theme: undecided; Physics: swinging pendulums, small pursuit boids (spheres), roll ball to destination
