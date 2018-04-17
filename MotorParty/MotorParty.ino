// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>
#include <AccelStepper.h>

// Stepper motor on M3+M4 48 steps per revolution
AF_Stepper motor1(48, 2);

// Wrapper functions
void forwardstep1(){
  motor1.onestep(FORWARD, SINGLE);
}
void backwardstep1(){
  motor1.onestep(BACKWARD, SINGLE);
}

AccelStepper stepper1(forwardstep1, backwardstep1);

void setup() {
  Serial.begin(9600);           // set up Serial library at 9600 bps
  Serial.println("Motor party!");

  stepper1.setMaxSpeed(800.0);
  stepper1.setAcceleration(400.0);
  stepper1.moveTo(500);

}

// Test the DC motor, stepper and servo ALL AT ONCE!
void loop() {
  if (stepper1.distanceToGo() == 0)
    stepper1.moveTo(-stepper1.currentPosition());
  stepper1.run();
}
