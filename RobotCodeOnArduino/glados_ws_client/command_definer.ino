void RunServoCommand(int num, int angle)
{
    if (angle < 0)
    {
        angle = 0;
        Serial.println("angle <0.");
    }
    else if (angle > 360)
    {
        angle = 360;
        Serial.println("angle >180.");
    }
    if (num == 1)
    {
        servo1.write(angle);
        Serial.println("Servo 1 rotate.");
    }
    else if (num == 2)
    {
        servo2.write(angle);
        Serial.println("Servo 2 rotate.");
    }
    else if (num == 3)
    {
        servo3.write(angle);
        Serial.println("Servo 3 rotate.");
    }
    else if (num == 4)
    {
        servo4.write(angle);
        Serial.println("Servo 4 rotate.");
    }
    else
    {
        Serial.println("Servo undefinded.");
    }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void RunAnimation_undefined_0(){
  Serial.println("Animation undefined ran.");

}

void RunAnimation_dance_5(){
  for( int i =0; i<250;i++){
          digitalWrite(LIGHT, HIGH);
          delay(30);
          digitalWrite(LIGHT, LOW);
          delay(30);
  }
}

void Wakeup(){
  Serial.println(" Wakeup ran.");
 // is_Wake=true;

  }


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void RunAnimationCommand(int num,int pow)
{
  if(pow==0){
  if(num==0){
        RunAnimation_undefined_0();
     }else if(num==1){
      RunAnimation_bitls_1();
    }else if(num==2){
        RunAnimation_show_2();
    }else if(num==3){
      RunAnimation_joke_3();
    } else if(num==4){
      RunAnimation_fact_4();
    }else if(num==5){
      RunAnimation_dance_5();
    }
    else{ Serial.println("Animation not found.");}
  }else{
     RunTimeSpanAnimation(1000*num+pow);
  }
}
void RunLumen(int num, int pow){
if(num==1){
  if(pow<50){
      Serial.println("Light 1 off.");
      digitalWrite(LIGHT, LOW);
    }else{
      Serial.println("Light 1 on.");
      digitalWrite(LIGHT, HIGH);
    }
  }else if(num==2){
    if(pow<50){
      Serial.println("Light 2 off.");
      digitalWrite(EYE, LOW);
    }else{
      Serial.println("Light 2 on.");
      digitalWrite(EYE, HIGH);
    }
  }else{ Serial.println("Light not found.");}
}


void RunCommand(String command)
{
    char deviceType = command[1];
    int deviceNumber = command[2] - 48;
    int deviceParam = (100 * (command[3] - 48)) + (10 * (command[4] - 48)) + ((command[5] - 48));
    if (deviceType == 'L')
      RunLumen(deviceNumber,deviceParam);
    else if (deviceType == 'S')
      RunServoCommand(deviceNumber, deviceParam);
    else if (deviceType == 'A')
      RunAnimationCommand(deviceNumber,deviceParam);
    else
      Serial.println("device type undefined.");
}