// Adafruit Motor shield library
// copyright Adafruit Industries LLC, 2009
// this code is public domain, enjoy!

#include <AFMotor.h>
#include <AccelStepper.h>

// Stepper motor on M3+M4 48 steps per revolution
AF_Stepper motor1(48, 2);
AF_Stepper motor2(48, 1);


// Wrapper functions
void forwardstep1(){
  motor1.onestep(FORWARD, SINGLE);
}
void backwardstep1(){
  motor1.onestep(BACKWARD, SINGLE);
}
void forwardstep2(){
  motor2.onestep(FORWARD, SINGLE);
}
void backwardstep2(){
  motor2.onestep(BACKWARD, SINGLE);
}

AccelStepper stepper1(forwardstep1, backwardstep1);
AccelStepper stepper2(forwardstep2, backwardstep2);

// Potentiometer control
int previous = 0;
int long newval_L = 0;
int long newval_R = 0;
int number_in_L = 0;
int number_in_R = 0;
String final_msg_L = "";
String final_msg_R = "";
// END Pot. Control

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);           // set up Serial library at 9600 bps

  //stepper1.setMaxSpeed(400.0);  // Actual max speed of small Nema17 stepper
  stepper1.setMinPulseWidth(100);
  stepper2.setMinPulseWidth(100);
  float maxspeed = 200.0;  // More torque
  stepper1.setMaxSpeed(maxspeed);
  stepper1.setSpeed(maxspeed);
  stepper2.setMaxSpeed(maxspeed);
  stepper2.setSpeed(maxspeed);
  //stepper1.setAcceleration(48000.0);
  //float acceleration = 200;
  float acceleration = 48000;
  stepper1.setAcceleration(acceleration);
  stepper2.setAcceleration(acceleration);
  
  stepper1.moveTo(200);
  stepper2.moveTo(200);

}

// Test the DC motor, stepper and servo ALL AT ONCE!
void loop() {
  

  stepper1.runSpeedToPosition();
  stepper2.runSpeedToPosition();

 
  //numChars = Serial.readBytes(message_in,4);
  //if (Serial.readBytes(message_in,4) == 4){
  if (Serial.available() > 0){
    if (Serial.peek() == 'L'){
      Serial.read();
      number_in_L = Serial.parseInt();
      Serial.read();
    }
    if (Serial.peek() == 'R'){
      Serial.read();
      number_in_R = Serial.parseInt();
      Serial.read();
    }
    Serial.println("Input num: ");
    Serial.println(number_in_L);
    final_msg_L = String(number_in_L);
    final_msg_R = String(number_in_R);
    
    newval_L = final_msg_L.toInt();
    newval_R = final_msg_R.toInt();
    stepper1.setSpeed(stepper1.distanceToGo());
    stepper1.moveTo(newval_L);
    stepper2.setSpeed(stepper2.distanceToGo());
    stepper2.moveTo(newval_R); 

    // Flush serial buffer
    while(Serial.available() > 0){
      char t = Serial.read();
    }
  }
  

 
  /*
  // Potentiometer control
  val = analogRead(A4);

  if ((val > previous + 50) || (val < previous - 50)){
    newval = map(val, 0, 1023, 0, 200);
    stepper1.moveTo(newval);
    stepper2.moveTo(newval);
    previous = val;
  }
  if (val == 0){
    digitalWrite(13, HIGH);
  }
  else{
    digitalWrite(13, LOW);
  }
  */
  /*
  if (stepper1.distanceToGo() == 0)
    stepper1.moveTo(-stepper1.currentPosition());
  stepper1.runSpeedToPosition();

  if (stepper2.distanceToGo() == 0)
    stepper2.moveTo(-stepper2.currentPosition());
  stepper2.runSpeedToPosition();
  */
}
