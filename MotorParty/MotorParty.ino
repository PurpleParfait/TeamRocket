// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>

// Stepper motor on M3+M4 48 steps per revolution
AF_Stepper stepper(48, 2);

void setup() {
  Serial.begin(9600);           // set up Serial library at 9600 bps
  Serial.println("Motor party!");

}

int i;

// Test the DC motor, stepper and servo ALL AT ONCE!
void loop() {
  for (i=0; i<255; i++) {  
    stepper.step(1, FORWARD, INTERLEAVE);
    delay(3);
 }
 
  for (i=255; i!=0; i--) {  
    stepper.step(1, BACKWARD, INTERLEAVE);
    delay(3);
 }
 
  for (i=0; i<255; i++) {
  
    delay(3);
    stepper.step(1, FORWARD, DOUBLE);
 }
 
  for (i=255; i!=0; i--) {
    stepper.step(1, BACKWARD, DOUBLE);
    delay(3);
 }
}
