#include <ArduinoWebsockets.h>
#include <WiFi.h>
#include <ESP32Servo.h>


const char* ssid = "********"; //Enter SSID
const char* password = "********"; //Enter Password
const char* websockets_server_host = "***.***.***.***"; //Enter server adress
const uint16_t websockets_server_port = ****; // Enter server port

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
class StepEmulServo {
private:
    const float vl = 0.30769; //l 135 0.30769  =12*180/7020; 
    const float vr = 0.30527;//r 45 0.30527  =17*180/10024;
    const int xl = 80;
    const int xr = 30;
    Servo servo;
    int currentAngle = 0;
    void rotate(bool left, int delta) {
        int vect, x; float v;
        if (left)
        {
            vect = 135; v = vl; x = xl;
        }
        else
        {
            vect = 45; v = vr; x = xr;
        }
        int timeSpan = ((int)round((delta / v))) + x;
        servo.write(vect); delay(timeSpan); servo.write(90);
    }
public:
    StepEmulServo() {}
    void attach(int PIN) {
        servo.attach(PIN); servo.write(90); delay(50);
    }
    void write(int angle) {
        if (angle > 360 || angle < 0) { angle = 0; }
        int delta = abs(currentAngle - angle), vecDelta = 0;
        if (delta > 180)
            vecDelta = 360 - delta;
        else
            vecDelta = delta;
        bool isLeft = ((bool)(delta > 180)) ^ ((bool)(angle > currentAngle));
        rotate(isLeft, vecDelta);
        currentAngle = angle;
    }
};
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
class GeneralServo {
private:
    int begin;
    int end;
    Servo servo;
public:
    GeneralServo(int _begin, int _end) {
        begin = _begin;
        end = _end;
    }
    void attach(int PIN) { servo.attach(PIN); }

    void write(int angle) {
        int delta = end - begin;
        int native = (int)(round(begin + (((float)angle) / ((float)100) * ((float)delta))));
        servo.write(native);
    }
};
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

StepEmulServo servo1;
GeneralServo servo2(0, 134),
servo3(105, 180), servo4(21, 119);//  134  46  118
//Servo servo2, servo3, servo4;
void ServoInit()
{
    servo1.attach(17);
    servo2.attach(5);
    servo3.attach(18);
    servo4.attach(19);

    //servo1.write(180);
    servo2.write(50);
    servo3.write(50);
    servo4.write(50);
}

using namespace websockets;
WebsocketsClient client;
bool TryRestoreConnection() {
    servo2.write(100);
    if (WiFi.status() != WL_CONNECTED) {
        WiFi.begin(ssid, password);
        for (int i = 0; i < 10 && WiFi.status() != WL_CONNECTED; i++) { Serial.print("."); delay(1000); }
    }
    if (WiFi.status() != WL_CONNECTED) {
        Serial.println("No Wifi!");
        return false;
    }
    Serial.println("Connected to Wifi, Connecting to server.");
    // try to connect to Websockets server
    bool connected = client.connect(websockets_server_host, websockets_server_port, "/");
    if (connected) {
        delay(200);
        connected = client.send("IAMGLADOSBODY");
        if(connected){
        Serial.println("Connected!");
        servo2.write(50);
        return true;
        }
    }
    Serial.println("Not Connected!");
    return false;
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#define LIGHT 4
#define EYE 16 


void setup() {
    Serial.begin(9600);

    ServoInit(); // Инициализация стерв

    pinMode(LIGHT, OUTPUT);
    pinMode(EYE, OUTPUT);
    digitalWrite(LIGHT, LOW);
    digitalWrite(EYE, HIGH);

    while (!TryRestoreConnection()) {}
    // run callback when messages are received
    client.onMessage([&](WebsocketsMessage message) {
        Serial.print("Got Message: ");
        String receivedString = message.data();
        Serial.println(receivedString);
        if (receivedString.length() == 7)
        {
            if (receivedString[0] == '{' && receivedString[6] == '}')
                RunCommand(receivedString);
        }
        else
            Serial.println("not a command.");
        });
}

void loop() {
    //ping
   
    // let the websockets client check for incoming messages
    if (client.available()) {
        client.poll();
    }
    delay(500);
}