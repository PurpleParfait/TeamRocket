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

// Potentiometer control
int val = 0;
int previous = 0;
int long newval = 0;
// END Pot. Control

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);           // set up Serial library at 9600 bps
  Serial.println("Motor party!");

  //stepper1.setMaxSpeed(400.0);  // Actual max speed of small Nema17 stepper
  
  stepper1.setMaxSpeed(200.0);  // More torque
  stepper1.setAcceleration(48000.0);
  //stepper1.setAcceleration(1000.0);
  
  stepper1.moveTo(200);

}

// Test the DC motor, stepper and servo ALL AT ONCE!
void loop() {
  /*
  // Potentiometer control
  val = analogRead(A4);

  if ((val > previous + 12) || (val < previous - 12)){
    newval = map(val, 0, 1023, 0, 200);
    stepper1.runToNewPosition(newval);
    previous = val;
  }
  if (val == 0){
    digitalWrite(13, HIGH);
  }
  else{
    digitalWrite(13, LOW);
  }
  */
  
  if (stepper1.distanceToGo() == 0)
    stepper1.moveTo(-stepper1.currentPosition());
  stepper1.run();
  
}
