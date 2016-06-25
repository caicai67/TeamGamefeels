Caitlins_readme.txt

i. Team Info
	Caitlin Morris - cmorris40@gatech.edu - cmorris40
	Ambrose Cheung - acheung30@gatech.edu - acheung30
	Chris Donlan - chris.donlan87@gmail.com - cdonlan3
	Karan Pratap Singh - kps@gatech.edu - ksingh75
	Justin - jthornburgh3@gatech.edu - jthornburgh3
	Charlie

ii. Requirements

	Team Requirements:
		
		Basic Physics Interactions - Objects can be collided with through the use of a collider
		Collider Animation - Player character collider changes when jumping and rolling to better fit the animation
		Ragdoll Simulation
		Game Feel - Realtime responsive control
		Collider Animation: Character's collider animates in various animations

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
			Bugs: Hanging animations clip through objects

		Chris:
			completed requirements;
			Rigidbodies: Stack of boxes, Small spheres, giant spinning spheres, saddle box, spinning axes, pendulum axes
			Compound Objects and Joints: Spinning axes, swinging axes
			Variable Height Terrain: Metal/concrete checkerboard area with ramps, cliff-like area, steep mountain
			Sounds: Three different walking sounds on different terrains - concrete, dirt, metal
			Game Feel: Dying scream when hit by giant spheres, pendulums, spinning axes, as well as reaction of axes to character
			Bugs: Character configurations did not work on everyone's computers, character colliders may conflict (should be fixed), and joints fall apart easily (despite forces being set to infinity)

		KP:- 
			I have completed all of the requirements.

			I also animated the collider for regular jump animations and created the jump blend tree.
			
			Input: Press X on PS4 Controller and Space on keyboard to jump. Use direction and jump together to do a forward or backward running jump.
			
			Theme: Futuristic enclosed space dimension
			
			Compound Objects and Joints: hinged restaurant doors and hinged ramp. Created stairs and their collider myself.
			
			Terrain: Multiple terrains and stairs and decks.
		 
			Physics: objects create different sound when they fall or roll, player can knock over all moveable objects, 
			player can push hinged doors and go down a platform via a hinged ramp. 
		
			Game Feel: wind zone with decreasing sound dopplar effect and swaying palm trees for immersion, Everything is textured. 
		
			Sounds: different floors cause different footstep sounds.
		
			Bug: Please note that all the hinges in my level were lubricated recently so they don't creak.

			
		
		Justin: 
			Rigidbodies: yellow ball, wire mesh cube, metal ball on concrete pillar, metal ball on mountain side, falling green cylinder
			Compound components: breakable wall (made of 7 cubes with fixed joints)
				small house (various rectangular prisms, hanging door)
			Variable terrain: varied gound, concrete ramp
			Sounds: different sounds for dirt metal and concrete
			Game Feel: all 3D objects not embedded in the ground are interactable
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
		Free footsteps sound effect from Unity Asset Store
		Mountain Terrain Package
		Mocap and Standard Asset Animations
		Mixamo
		Freesounds.org
		Unity Standard Assets (Terrain, animations`)

	KP:	Free footsteps sound effect from Unity Asset Store
		Mocap and Standard Asset Animations
		Mixamo
		Freesounds.org


	Justin:
		Texture file from Stone Texture Pack from the Asset store
		Some additional textures from the other imported assets

	Charlie:

iv. Special Install Instructions

	Developed using both Mac OS X 10.11 and Windows 10. 

v. Game Testing Steps

	Caitlin's Scene ("3" Key):
		The camera can be turned using the arrow keys. Turning the camera to the right and using WASD to move, approaching the gate should activate it to open. Walking across using the moving sidewalk, the character is moved in tandem with the sidewalk. Once the player is on the other tower, they have the option of running toward the table and knocking it off the platform (who doesn't like flipping tables?). It will make a crashing sound and, if enough force is applied, the legs may break off. 
		From here the player can walk/run onto the cloth canopy, which will bend and stretch as the player falls through. To the right, there is a collection of blocks. They can give and wobble (especially the smaller ones), but eventually settle back due to gravity. The player can go back up to the top using the blocks and jump key (space), but this is a little tricky. Turning back toward the space between the towers, the player can go through the double doors. If the player hits them with enough force they should squeak. Moving away from all the other objects, the player will notice the sound of the escalator drops off.

		Sneaking:
			Sneaking is toggled using the "/" key. Once in sneak mode, the controls are similar to those of run mode (WASD to move, left-right-up-down to move the camera). The differences are that in sneak mode the character is slower and can no longer jump.

	Ambrose's Scene ("4" Key) [Keyboard (WASD)]:
		Run forward through the boxes, up the hill, up the stairs, and through the double doors.
		Run forward until touching the wall, but under the ledge jutting out from it.
		Press 'E' to climb and hang on the wall
		Hold 'D' to shimmy to the other side of the valley
		Upon reaching the end of the ledge, press 'E' to fall onto the ground.

	Chris' Scene ("1" Key):
		Run toward boxes, bust through them
		Run around spheres, walk on top of them, roll into them (circle button, keyboard 'r')
		Run at the green saddle, hold R1 (left shift) and press jump (space bar) to hurdle the saddle
		Roll under the saddle. 
		Run up to axes, let them hit you. press options (backspace to reload the level). 
		Run up to pendulums, let them hit you.  press options (backspace)
		Run up to giant spheres in next room, let them hit. (backspace)
		Run past giant spheres on both the metal and concrete squares. run up side ramps. 
		Run into next area, through far hallway.  Unity generated terrain, olive trees, painted terrain textures
		Run past mountains to giant mountain in rear. climb up mountain. 
		Meshes were custom (besides character, terrain, and small spheres)

	KP's Scene ("2" Key):
		Run on the concrete area for a bit.
		Finally, run, roll or jump up the stairs and get on top of the metal deck.
		Roll through or jump over the crates on top of the metal deck.
		Right behind the crates is a hinged metal ramp that will let you come down from the ramp or you can jump off the ramp.
		Roll or bump into the metal barrels outside the beach dining area.
		The dining area can be entered through the 2 swinging doors.
		You can roll through the dining table and chairs.
		Knock over the stacked barrels for fun and hear them rolling away.
		Look at the palm trees swaying in the ocean breeze.
		Hear the ocean breeze get stronger as you reach the dining area.
		Notice the sounds your big feet make whenever you run over a new surface.
		
		

	Justin's Scene ("5" Key):
		Stand still for 10 seconds, you can see the cylinder fall onto the house and sphere roll down the side of the mountain to eventually knock over some of the boxes. 
		There is a ball and wire mesh cube to interact with on the spawn platform. 
		The last ball can be reached by going up the concrete ramp to the top of the concrete pillar. 
		The two compound objects are the breakable wall which can be broken by running through it, and 
			the door on the house which can be opened by running through it. 
		Three different footfall sounds occur in the following locations: 
			Metal on spawn platform
			Concrete on the ramp and pillar and inside the house
			Dirt everywhere else. 
		Gamefeel: many interactable objects. 
		
	Charlie's Scene: No work done

vi. Main Scene File

	Chris





