const int stepPin = 12;
const int dirPin = 11;

void setup() {
  pinMode(stepPin, OUTPUT);
  pinMode(dirPin, OUTPUT);
}

void loop() {
  digitalWrite(dirPin, HIGH);
  for (int i = 0; i < 100; i++){
    motorStep();
    delay(1);
  }

  digitalWrite(dirPin, LOW);
  for (int i = 0; i < 100; i++){
    motorStep();
    delay(1);
  }
}

void motorStep(){
  digitalWrite(stepPin, HIGH);
  delay(1);
  digitalWrite(stepPin, LOW);
}

