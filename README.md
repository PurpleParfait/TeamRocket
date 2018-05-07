<iframe width="560" height="315" src="https://www.youtube.com/embed/VEwjRkFCsCg" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe> 

## Project Description:

As the video game industry grows, developers seek to provide more and more immersive experiences to their players. Many companies, such as Nintendo, have attempted to bring gameplay experiences into the physical realm through devices such as the Rumble Pak and Nintendo Labo. Few developers, however, have tried to bring the experience of flight to their players. This project provides haptic feedback for a flight simulation game. While strapped to a wooden frame, the player uses a Wii balance board to control the motion of a hang glider in a Unity game. As the hang glider turns to the left or right, stepper motors attached to string and Velcro straps pull on the player’s arms to simulate wind resistance. 

## Materials used:
* Unity 
* [SerialCommUnity](https://github.com/dwilches/SerialCommUnity)
* Arduino Duemilanove
* Adafruit Motor Shield
* [Wantai Stepper Motor (2)](https://www.sparkfun.com/products/13656)
* Wii Balance Board
* [Wii Balance Walker Driver](http://www.greycube.com/site/download.php?view.68)


## Team members:
* Kyle Gronberg (Initial Project Build, Unity Code Tweaks) 
* Cade Haley (Final Build Tweaks, Arduino Code) 
* Madison Razook (Final Build Tweaks, Unity Code) 


## Design Considerations and Reflections

We started this project with the goal of creating a device that provided haptic feedback that simulated wind-resistance for a flight-based games. With the intent that the game would be controlled similar to how a child pretends to be an airplane (arms outstretched, leaning to turn), it was decided to focus the feedback on the arms. We started by exploring in what ways we could pull on or restrict a human arm. Being inexperienced designers, we decided on using rope for its simplicity. By tying rope to points on an arm, its possible to restrict or force movement, similar to a puppet string. 
For automating interaction, we needed some sort of motor. By attaching the rope to a motor and spinning it, the rope will spool and pull towards the motor. Both stepper motors and servos had their benefits. Ultimately we chose stepper motors for our purposes, as precise angles were unnecessary except for resetting the motor, and high-torque, back-drivable steppers were cheaper than servo counterparts. 
For proof-of-concept, we focused mainly on one axis of movement: backwards. Because these axises are relative to one's position, the device has to always be directly behind the user. This was another decision point: restrict the user's movements, or make the device move with the user. We decided the device should move with the user for portability reasons. Towards this end, we came up with the current backpack-esque shape of the device. To ensure the user's arms were pulled backwards and not in towards their back, we built the backpack such that the motor was far enough away from the user's back and added a crossbeam with eyelets that could act as a pulley system. 
The demo was designed in Unity. We needed a way to communicate to the Arduino which controls the stepper motors. To do this, we used a serial port, utilizing the [SerialCommUnity](https://github.com/dwilches/SerialCommUnity) library by "dwilches". This allows us to send strings that are placed into a queue then interpreted by Arduino. To minimize lag, it was necessary to prevent this queue from becoming to large. We attempted to minimize the signals sent by Unity to avoid this.
