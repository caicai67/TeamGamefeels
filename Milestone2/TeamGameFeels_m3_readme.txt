Team GameFeels

i. Team Info
	Caitlin Morris - cmorris40@gatech.edu - cmorris40
	Ambrose Cheung - acheung30@gatech.edu - acheung30
	Chris Donlan - chris.donlan87@gmail.com - cdonlan3
	Karan Pratap Singh - kps@gatech.edu - ksingh75
	Justin - jthornburgh3@gatech.edu - jthornburgh3
	Charlie - charlie.jolman@gmail.com - cjolman3

ii. Requirements
	
	1) Nav mesh rig is complete; all AI NPC's travel across the RAIN nav mesh (Called Navigation Mesh in the Scene)
	2) There are 4 Navigation targets used in 1 behavior tree
	3) Waypoint network rig with 20+ waypoints, multiple waypoints connected to branches
	4) Waypoint Route rig with 4 waypoints, used in Zombie Cop behavior tree
	5) 3 of 4 NPCs use the Mechanim motor control; one uses custom motor control
	6) The WhiteClown goes into attack mode when the player gets close; also, time permitting, the Demon launches a spell
	7) The Demon NPC targets a leading position calculated by adding a velocity vector * estimated time to collide
	8) Each of the 4 NPCs has a unique personality, moves about the world without colliding, and animates appropriately
	9) 
 

iii. Resources
	
	InControl: for arbitrary controller setup
	RAIN AI system. 
	Mixamo Characters and animations
	Standard Assets animations
	http://rivaltheory.com/forums/topic/refusing-waypoint-network-fail-parallel/


iv. Special Install Instructions

	Developed using both Mac OS X 10.11 and Windows 10. 

v. Game Testing Steps
	
	1) Observe that the "Navigation Mesh" object in the scene is a RAIN navigation mesh containing the whole terrain square
	2) Observe the vampire character patrolling through the 4 navigation targets in the middle of the set of NPCs
		a) if you approach the vampire with the Player Character, the vampire will pursue the character
		b) this was an earlier character (before better tutorial videos) and uses two custom action node scripts to calculate the path and update the animator controller
	3) Observe the purple Waypoint Network on the terrain and the object (same name) in the hierarchy
	4) Observe the red Waypoint Route Rig on the terrain to the right in the scene view, where the zombie cop is patrolling a loop route
	5) The Demon, Cop, and WhiteClown are all recieving animator controller instructions from the mechanim motor.   The vampire is receiving animator controller updates from a custom node script, not a motor
	6) The clown has an animator controller with states for his teleportation, spell casting, and turning animations. His behavior tree decides if he sees the player, if he needs to move closer, and when to cast a spell.
	7) run in large circles and right angles to the Demon to see it hone in on a point ahead of Dreyar. The white clown has a custom AI element attached that allows it to acquire Dreyar’s coordinates and teleport to a point behind him.
	8) Observe the crazy clown spin in circles (smaller visual sensor), and teleport behind Dreyar when he sees him; the demon pursues the player right away (he has a 20 meter 360 visual sensor (telepathy?)); the vampire pursues the character within a similar sight distance, or patrols the 4 navigation points at a sprint.  The Cop patrols the waypoint. 
                As the player approaches the clown, he goes into "fight mode."
	9) The characters are all forming this semi cohesive theme of some sort of zombie/vampire/demon/ghost uprising in some sort of interdimensional time travel vacuum. 
vi. Main Scene File

	Assets/Scenes/temp2




