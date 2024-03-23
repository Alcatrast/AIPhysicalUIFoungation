/*void RunTimeSpanAnimation(int timeFor50ms)
{
  Serial.println("Time: "+timeFor50ms); 
    int msInOne=50;
    int minMove = 15;
    int staticStep=random(500/msInOne,1500/msInOne+1);
    int count = timeFor50ms/staticStep;
 if (count <= 1 ) {  Serial.println("count: "+count); return; }
    timeFor50ms = staticStep * (count);
    staticStep = timeFor50ms / (count);

    if(count<=1 || staticStep<=1){ Serial.println("stsp: "+staticStep);return;}
    int len = count + 1;
    int moveRange = 100 - 2 * minMove;
    int te = 0, tp = 0;
    int prevPos2 = 50, pe2 = 0, pos2 = 0;// pe<50
    int prevPos3 = 50, pe3 = 0, pos3 = 0;// pe<50
    int prevPos4 = 50, pe4 = 0, pos4 = 0;// pe<50


    int* timesFor50ms = new int[len];
    timesFor50ms[0] = 0;
    int* positons2 = new int[len];
    int* positons3 = new int[len];
    int* positons4 = new int[len];
int a=0;

    for (int i = 0; i < count; i++) {
        te = random((-staticStep / 2), (staticStep / 2)+1);
        tp = staticStep * (i + 1) + te;

        pe2 = random(0, moveRange+1) - moveRange / 2;
        pe2==0 ? pe2=1 : a=0;
        pos2 = (100 + prevPos2 + ((minMove * pe2 / abs(pe2)) + pe2)) % 100;

        pe3 = random(0, moveRange+1) - moveRange / 2;
        pe3==0 ? pe3=1 : a=0;
        pos3 = (100 + prevPos3 + ((minMove * pe3 / abs(pe3)) + pe3)) % 100;
        
        pe4= random(0, moveRange+1) - moveRange / 2;
        pe4==0 ? pe4=1 : a=0;
        pos4 = (100 + prevPos4 + ((minMove * pe4 / abs(pe4)) + pe4)) % 100;

        prevPos2 = pos2;
        positons2[i] = pos2;

        prevPos3 = pos3;
        positons3[i] = pos3;
        
        prevPos4 = pos4;
        positons4[i] = pos4;
        
        timesFor50ms[i + 1] = tp;
    }
    positons2[len - 1] = 50;
    positons3[len - 1] = 50;
    positons4[len - 1] = 50;

    timesFor50ms[len - 1] = timeFor50ms;


    servo2.write(positons2[0]);
    Serial.println((positons2[0]));
    servo3.write(positons3[0]);
    Serial.println((positons3[0]));
    servo4.write(positons4[0]);
    Serial.println((positons4[0]));
    for (int i = 1; i < len; i++) {
        delay(((timesFor50ms[i] - timesFor50ms[i - 1])) * msInOne);
        servo2.write(positons2[i]);
        Serial.println((positons2[i]));
        servo3.write(positons3[i]);
        Serial.println((positons3[i]));
        servo4.write(positons4[i]);
        Serial.println((positons4[i]));
    }
    delete[] positons2;
    delete[] positons3;
    delete[] positons4;
    delete[] timesFor50ms;
}*/

int** GenerateTimeSpanAnimation(int timeFor50ms)
{
    // Serial.println("Time: " + timeFor50ms);
    int msInOne = 50;
    int minMove = 15;
    int staticStep = random(500 / msInOne, 1500 / msInOne + 1);
    int count = timeFor50ms / staticStep;
    if (count <= 1) { return new int* [0]; }
    timeFor50ms = staticStep * (count);
    staticStep = timeFor50ms / (count);

    if (count <= 1 || staticStep <= 1) { return new int* [0]; }
    int len = count + 1;
    int moveRange = 100 - 2 * minMove;
    int te = 0, tp = 0;
    int prevPos2 = 50, pe2 = 0, pos2 = 0;// pe<50

    int* timesFor50ms = new int[len];
    timesFor50ms[0] = 0;
    int* positons2 = new int[len];
    int a = 0;

    for (int i = 0; i < count; i++) {
        te = random((-staticStep / 2), (staticStep / 2) + 1);
        tp = staticStep * (i + 1) + te;

        pe2 = random(0, moveRange + 1) - moveRange / 2;
        pe2 == 0 ? pe2 = 1 : a = 0;
        pos2 = (100 + prevPos2 + ((minMove * pe2 / abs(pe2)) + pe2)) % 100;

        if (pos2 == 50) pos2 = 51;
        prevPos2 = pos2;
        positons2[i] = pos2;
        Serial.println(pos2);

        timesFor50ms[i + 1] = tp;
    }
    positons2[len - 1] = 50;

    timesFor50ms[len - 1] = timeFor50ms;

    int** matrix = new int* [2];
    matrix[0] = timesFor50ms;
    matrix[1] = positons2;
    return matrix;
}

void smooth(int timeFor50ms, int* timesFor50ms2, int* positons2, int* timesFor50ms3, int* positons3, int* timesFor50ms4, int* positons4) {
    int msInOne = 50;
    int countFrames = 5;

    int frameDuration = msInOne / countFrames;
    int ms = msInOne * timeFor50ms;
    int iterarion = 0;

    bool awaitNext2 = true;
    int startPosition2, startTime2, endTime2, endPosition2, writed_pos2;
    float Ax2, Ay2, pos2;
    int timeIterator2 = 0, p2 = 0;

     bool awaitNext3 = true;
    int startPosition3, startTime3, endTime3, endPosition3, writed_pos3;
    float Ax3, Ay3, pos3;
    int timeIterator3 = 0, p3 = 0;

     bool awaitNext4 = true;
    int startPosition4, startTime4, endTime4, endPosition4, writed_pos4;
    float Ax4, Ay4, pos4;
    int timeIterator4 = 0, p4 = 0;

    while (iterarion * msInOne < ms) {

        if (timesFor50ms2[p2] * msInOne < ms && awaitNext2) {

            startTime2 = timesFor50ms2[p2];
            startPosition2 = positons2[p2];
            if (startPosition2 == 50) { break; }
            endTime2 = timesFor50ms2[p2 + 1];
            endPosition2 = positons2[p2 + 1];

            float dx2 = endTime2 - startTime2;
            float dy2 = endPosition2 - startPosition2;
            float dxa2 = 1;
            float dya2 = Foo(1) - Foo(0);
            Ax2 = dx2 / dxa2;
            Ay2 = dy2 / dya2;

            timeIterator2 = 0;
            awaitNext2 = false;
        }
        
        if (timesFor50ms3[p3] * msInOne < ms && awaitNext3) {

            startTime3 = timesFor50ms3[p3];
            startPosition3 = positons3[p3];
            if (startPosition3 == 50) { break; }
            endTime3 = timesFor50ms3[p3 + 1];
            endPosition3 = positons3[p3 + 1];

            float dx3 = endTime3 - startTime3;
            float dy3 = endPosition3 - startPosition3;
            float dxa3 = 1;
            float dya3 = Foo(1) - Foo(0);
            Ax3 = dx3 / dxa3;
            Ay3 = dy3 / dya3;

            timeIterator3 = 0;
            awaitNext3 = false;
        }
        if (timesFor50ms4[p4] * msInOne < ms && awaitNext4) {

            startTime4 = timesFor50ms4[p4];
            startPosition4 = positons4[p4];
            if (startPosition4 == 50) { break; }
            endTime4 = timesFor50ms4[p4 + 1];
            endPosition4 = positons4[p4 + 1];

            float dx4 = endTime4 - startTime4;
            float dy4 = endPosition4 - startPosition4;
            float dxa4 = 1;
            float dya4 = Foo(1) - Foo(0);
            Ax4 = dx4 / dxa4;
            Ay4 = dy4 / dya4;

            timeIterator4 = 0;
            awaitNext4 = false;
        }

        for (int i = countFrames - 1; i >= 0; i--) {
            pos2 = Ay2 * Foo((timeIterator2 - ((1.0 / (countFrames)*i))) / Ax2) + endPosition2;
            writed_pos2 = (int)round(pos2);

            pos3 = Ay3 * Foo((timeIterator3 - ((1.0 / (countFrames)*i))) / Ax3) + endPosition3;
            writed_pos3 = (int)round(pos3);
            
            pos4 = Ay4 * Foo((timeIterator4 - ((1.0 / (countFrames)*i))) / Ax4) + endPosition4;
            writed_pos4 = (int)round(pos4);

            servo2.write(writed_pos2);
            Serial.println(writed_pos2);
             servo3.write(writed_pos3);
            Serial.println(writed_pos3);
             servo4.write(writed_pos4);
            Serial.println(writed_pos4);

            delay(frameDuration);
        }

        iterarion += 1;

        if (endTime2 - startTime2 - timeIterator2 <= 1) { p2 += 1; awaitNext2 = true; }
        if (endTime3 - startTime3 - timeIterator3 <= 1) { p3 += 1; awaitNext3 = true; }
        if (endTime4 - startTime4 - timeIterator4 <= 1) { p4 += 1; awaitNext4 = true; }

        timeIterator2 += 1;
        timeIterator3 += 1;
        timeIterator4 += 1;

    }
    servo2.write(50);
    servo3.write(50);
    servo4.write(50);
}

float Foo(float x) {
    return pow(M_E, -1*2 * x) - pow(M_E, -1*3);
}

void RunTimeSpanAnimation(int timeFor50ms){
    auto matrix2 = GenerateTimeSpanAnimation(timeFor50ms);
    auto matrix3 = GenerateTimeSpanAnimation(timeFor50ms);
    auto matrix4 = GenerateTimeSpanAnimation(timeFor50ms);

    Serial.println("///////////////////////////");    
    
    smooth(200, matrix2[0], matrix2[1],matrix3[0], matrix3[1],matrix4[0], matrix4[1]);
   
    delete[] matrix2[0];
    delete[] matrix2[1];
    delete[] matrix2;

     delete[] matrix3[0];
    delete[] matrix3[1];
    delete[] matrix3;

    delete[] matrix4[0];
    delete[] matrix4[1];
    delete[] matrix4;
}
