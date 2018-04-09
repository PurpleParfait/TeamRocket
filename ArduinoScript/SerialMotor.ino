const int stepPin = 12;
const int dirPin = 11;
const int Max = 1001;
int pos = 0;

void setup()
{
  Serial.begin(9600);
  pinMode(stepPin, OUTPUT);
  pinMode(dirPin, OUTPUT);
}

void loop()
{
    // Send some message when I receive an 'A' or a 'Z'.
    switch (Serial.read())
    {
        case 'A':
            digitalWrite(dirPin, HIGH);
            motorStep();
            pos += 1;
            Serial.println(pos);
            delay(1);
            break;
            
        case 'D':
            digitalWrite(dirPin, LOW);
            motorStep();
            pos -= 1;
            Serial.println(pos);
            delay(1);
            break;

        case 'B':
            digitalWrite(dirPin, HIGH);
            for (int i = 0; i<100; i++){
              pos += 1;
              motorStep();
              delay(1);
            }
            Serial.println(pos);
            break;

        case 'C':
            digitalWrite(dirPin, HIGH);
            while(pos < Max){
              pos++;
              motorStep();
              delay(1);
            }
            Serial.println(pos);
            break;

        case 'O':
            digitalWrite(dirPin, LOW);
            for(int i = 0; i < 100; i++){
              pos -= 1;
              motorStep();
              delay(1);
            }
            Serial.println(pos);
            break;

        case 'P':
            digitalWrite(dirPin, LOW);
            while(pos>0){
              pos -= 1;
              motorStep();
              delay(1);
            }
            Serial.println(pos);
            break;

        case 'X':
            if (pos >= 1){
              digitalWrite(dirPin, LOW);
              while (pos >=1){
                pos -= 1;
                motorStep();
                delay(1);
              }
            }
            if (pos <= -1){
              digitalWrite(dirPin, HIGH);
              while (pos <= -1){
                pos += 1;
                motorStep();
                delay(1);
              }
            }
            Serial.println("Return to Start");
            Serial.println(pos);
            break;
    }
}

void motorStep(){
  digitalWrite(stepPin, HIGH);
  delay(1);
  digitalWrite(stepPin, LOW);
}
