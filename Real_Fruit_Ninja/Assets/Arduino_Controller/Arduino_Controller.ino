//Libraries
#include <CapacitiveSensor.h>//https://github.com/PaulStoffregen/CapacitiveSensor

//Parameters
bool autocal  = 0;
const int sensitivity  = 1000;
const int thresh  = 200;
const int csStep  = 10000;
CapacitiveSensor cs1  = CapacitiveSensor(2, 3);
CapacitiveSensor cs2  = CapacitiveSensor(4, 5);
CapacitiveSensor cs3  = CapacitiveSensor(6, 7);
CapacitiveSensor cs4  = CapacitiveSensor(8, 9);
CapacitiveSensor cs5  = CapacitiveSensor(10, 11);
CapacitiveSensor cs6  = CapacitiveSensor(12, 13);



static bool button_flag1 = false;
static bool button_flag2 = false;
static bool button_flag3 = false;
static bool button_flag4 = false;
static bool button_flag5 = false;
static bool button_flag6 = false;



void setup() {
  Serial.begin(115200);
  if (autocal == 0) {
    {
      cs1.set_CS_AutocaL_Millis(0xFFFFFFFF);
      cs2.set_CS_AutocaL_Millis(0xFFFFFFFF);
      cs3.set_CS_AutocaL_Millis(0xFFFFFFFF);
      cs4.set_CS_AutocaL_Millis(0xFFFFFFFF);
      cs5.set_CS_AutocaL_Millis(0xFFFFFFFF);
      cs6.set_CS_AutocaL_Millis(0xFFFFFFFF);
    }
  }
}

void loop() {
  long value1 = reader(1);
  long value2 = reader(2);
  long value3 = reader(3);
  long value4 = reader(4);
  long value5 = reader(5);
  long value6 = reader(6);
  
  if (value1 > 1000 && button_flag1 == false){
    button_flag1 = true;
    Serial.println("1");
  }
  if (value1 < 1000 && button_flag1 == true) {
    button_flag1 = false;
  }
  
  if (value2 > 1000 && button_flag2 == false){
    button_flag2 = true;
    Serial.println("2");
  }
  if (value2 < 1000 && button_flag2 == true) {
    button_flag2 = false;
  }
  
  if (value3 > 1000 && button_flag3 == false){
    button_flag3 = true;
    Serial.println("3");
  }
  if (value3 < 1000 && button_flag3 == true) {
    button_flag3 = false;
  }

  if (value4 > 1000 && button_flag4 == false){
    button_flag4 = true;
    Serial.println("4");
  }
  if (value4 < 1000 && button_flag4 == true) {
    button_flag4 = false;
  }

  if (value5 > 1000 && button_flag5 == false){
    button_flag5 = true;
    Serial.println("5");
  }
  if (value5 < 1000 && button_flag5 == true) {
    button_flag5 = false;
  }

  if (value6 > 1000 && button_flag6 == false){
    button_flag6 = true;
    Serial.println("6");
  }
  if (value6 < 1000 && button_flag6 == true) {
    button_flag6 = false;
  }
}



long reader(int index) { 
  ////read sensor value
  long reading;
  // read the right one
  switch (index) {
  case 1:
    reading = cs1.capacitiveSensor(sensitivity);
    break;
  case 2:
    reading = cs2.capacitiveSensor(sensitivity);
    break;
  case 3:
    reading = cs3.capacitiveSensor(sensitivity);
    break;
  case 4:
    reading = cs4.capacitiveSensor(sensitivity);
    break;
  case 5:
    reading = cs5.capacitiveSensor(sensitivity);
    break;
  case 6:
    reading = cs6.capacitiveSensor(sensitivity);
    break;
}
  
  return reading;
}
