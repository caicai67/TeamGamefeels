Caitlins_readme.txt

i. Team Info
	Caitlin Morris - cmorris40@gatech.edu - cmorris40
	Ambrose Cheung - acheung30@gatech.edu - acheung30
	Chris
	KP
	Justin
	Charlie

ii. Requirements

	Team Requirements:
		
		Basic Physics Interactions - Objects can be collided with through the use of a collider
		Collider Animation - Player character collider changes when jumping and rolling to better fit the animation
		Ragdoll Simulation
		Game Feel - Realtime responsive control

	Individual Requirements:

		Caitlin:
			Rigidbodies: The climbing blocks, table, and swinging doors are all made up of rigidbodies. There is also a cloth canopy.
			Compound Objects and Joints: The Swinging doors (lower level) contain hinge joints and the table legs are attached using fixed joints. The swinging doors, table, moving sidewalk, and automatic gate are all created using hierarchical folders. 
			Variable Height Terrain: Blocks on the side of the structure allow the character to move up and down.
			Sounds: When the character passes through the doors at a high speed they squeak. The table makes a crashing noise when gets pushed off the tower (this can be buggy sometimes). The automatic gate senses when the character is close and makes an opening and closing sound (bug: sometimes closes twice). The escalator sound is not dependant on the diegetic movement of the character, but varies with the distance the character rig is from the moving sidewalk.
			Game Feel: The table can break if the player applies enough force. Additionally, the climbing blocks wobble a little. The moving sidewalk actually changes the transform of the character, in addition to being a platform. The swinging doors react to the force imparted by the character (but they don't always swing back properly). The gate senses the character using a trigger and opens automatically. The cloth canopy is interactive.

		Ambrose:
			Rigidbodies: Boxes and doors
			Compound Objects and Joints: The double doors
			Variable Height Terrain: Hill and stone platform
			Sounds: Boxes, footsteps, and doors
			Game Feel: Player can push open the doors

		Chris:

		KP:

		Justin:

		Charlie:

iii. Resources
	
	Caitlin:
		I used freesound.org for my sound files. Their URLs are as follows

		AutomaticClose ("Trash Can (Automatic Light-sensing; I made edits to original)" by 
		ultradust): 
		https://www.freesound.org/people/ultradust/sounds/167177/

		AutomaticDoorOpen ("Trash Can (Automatic Light-sensing)" by ultradust; I made edits to original): 
		https://www.freesound.org/people/ultradust/sounds/167177/

		Cloth ("foley cloth rustle.wav" by martian): 
		https://www.freesound.org/people/martian/sounds/19291/

		escalator: ("Escalator 2.WAV" by Sandermotions):
		https://www.freesound.org/people/Sandermotions/sounds/328952/

		SlidingTable ("Sliding Chair or Table.wav" by RutgerMuller): 
		https://www.freesound.org/people/RutgerMuller/sounds/51165/

		swinging-door ("Swinging Door.wav" by andrewweathers): 
		https://www.freesound.org/people/andrewweathers/sounds/25425/

		TableCrash ("WoodDebrisFall1.wav" by zimbot): 
		https://www.freesound.org/people/zimbot/sounds/89386/

	Ambrose:
		Used freesound.org for sounds and Unity Standard Asset textures

	Chris:

	KP:

	Justin:

	Charlie:

iv. Special Install Instructions

	Developed using both Mac OS X 10.11 and Windows 10. 

v. Game Testing Steps

	Caitlin's Scene:
		The camera can be turned using the arrow keys. Turning the camera to the right and using WASD to move, approaching the gate should activate it to open. Walking across using the moving sidewalk, the character is moved in tandem with the sidewalk. Once the player is on the other tower, they have the option of running toward the table and knocking it off the platform (who doesn't like flipping tables?). It will make a crashing sound and, if enough force is applied, the legs may break off. 
		From here the player can walk/run onto the cloth canopy, which will bend and stretch as the player falls through. To the right, there is a collection of blocks. They can give and wobble (especially the smaller ones), but eventually settle back due to gravity. The player can go back up to the top using the blocks and jump key (space), but this is a little tricky. Turning back toward the space between the towers, the player can go through the double doors. If the player hits them with enough force they should squeak. Moving away from all the other objects, the player will notice the sound of the escalator drops off.

		Sneaking:
			Sneaking is toggled using the "/" key. Once in sneak mode, the controls are similar to those of run mode (WASD to move, left-right-up-down to move the camera). The differences are that in sneak mode the character is slower and can no longer jump.

	Ambrose's Scene (Using Keyboard (WASD)):
		Run forward through the boxes, up the hill, up the stairs, and through the double doors.
		Run forward until touching the wall, but under the ledge jutting out from it.
		Press 'E' to climb and hang on the wall
		Hold 'D' to shimmy to the other side of the valley
		Upon reaching the end of the ledge, press 'E' to fall onto the ground.

vi. Main Scene File

	Chris





