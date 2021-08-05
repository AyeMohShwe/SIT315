int PIR = 2; //Motion Sensor Input pin
int LED = 13; //Output pin
int value = 0;

const int btn = 3; //Push button input pin
const int LED2 = 12;

const uint8_t buttonState_UP = LOW;
const uint8_t buttonState_DOWN = HIGH;

const uint8_t LED2State_OFF = LOW;
const uint8_t LED2State_ON = HIGH;

// GLOBAL VARIABLES
uint8_t buttonPrevState;
uint8_t LED2State;

void setup()
{
  pinMode(PIR, INPUT);
  pinMode(LED, OUTPUT);
  
  pinMode(btn, INPUT_PULLUP);
  pinMode(LED2, OUTPUT);
  
  buttonPrevState = buttonState_UP;
  LED2State = LED2State_OFF;
  
  Serial.begin(9600);
  
  //Attch interrupts 
  attachInterrupt(digitalPinToInterrupt(PIR), detect_motion, CHANGE);
  attachInterrupt(digitalPinToInterrupt(btn), led2_toggle, CHANGE);
}

void loop()
{
  delay(1500); // Delay 1.5 seconds to improve simulation performance
}

void detect_motion()
{
  value = digitalRead(PIR);
  if(value == HIGH){
    digitalWrite(LED, HIGH);
    Serial.println("Motion detected!"); //Display Data on screen
    delay(300);
  }else {
    digitalWrite(LED, LOW);
    Serial.println("Motion stopped!");
    delay(500);
  }  
}
void led2_toggle()
{
  // check if there was a transition of button state
  uint8_t buttonState = digitalRead(btn);
  
  //display the values of led2 state to the screen(serial monitor)
  Serial.println(LED2State);
  
  if ( (buttonState == buttonState_DOWN) && (buttonPrevState == buttonState_UP) )
  {
    LED2State = !LED2State;
    digitalWrite(LED2, LED2State);
  }
  //Save the current state as the previous state, for the next time through the loop
  buttonPrevState = buttonState;
    
  //delay half a second to avoid debouncing
  delay(500);
}
