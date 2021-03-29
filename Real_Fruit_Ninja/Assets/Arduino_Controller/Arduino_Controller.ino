//Libraries
#include <CapacitiveSensor.h>//https://github.com/PaulStoffregen/CapacitiveSensor

//Parameters
bool autocal  = 0;
const int numReadings  = 10;
long readings [numReadings];
int readIndex  = 0;
long total  = 0;
const int sensitivity  = 1000;
const int thresh  = 200;
const int csStep  = 10000;
CapacitiveSensor cs  = CapacitiveSensor(2, 3);



// The flag signals to the rest of the program an interrupt occured
static bool button_flag = false;
// Remember the state the river in the Unity program is in
static bool river_state = false;

// Interrupt handler, sets the flag for later processing
void buttonPress() {
  button_flag = true;
}


void setup() {
  //Init Serial USB
  Serial.begin(115200);
  //Init cs
  if (autocal == 0) {
    {
      cs.set_CS_AutocaL_Millis(0xFFFFFFFF);
    }
  }
}

void loop() {
  int value = smooth();
  if (value > 1000 && button_flag == false){
    button_flag = true;
    Serial.println("Oignon");
  }
  if (value < 1000 && button_flag == true) {
    button_flag = false;
  }
}

long smooth() { /* function smooth */
  ////Perform average on sensor readings
  long average;
  // subtract the last reading:
  total = total - readings[readIndex];
  // read the sensor:
  readings[readIndex] = cs.capacitiveSensor(sensitivity);
  // add value to total:
  total = total + readings[readIndex];
  // handle index
  readIndex = readIndex + 1;
  if (readIndex >= numReadings) {
    readIndex = 0;
  }
  // calculate the average:
  average = total / numReadings;

  return average;
}
