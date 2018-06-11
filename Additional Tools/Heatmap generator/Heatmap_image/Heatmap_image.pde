/*
VR Heatmap 0.3 by Johannes Ambrosch
 
 This tool processes head rotation data from a VR headset (e.g. HTC Vive) over the time of viewing a video.
 The data is to be provided as a CSV sheet, named "Data.csv", in the application's Data folder.
 
 The tool will analyze the data and generate a visual heatmap of the viewer's gaze direction.
 The heatmap will be saved in the program folder.
 
 */


//Name of CSV Datasheet
String filename = "Data.csv";

//Index of columns in the CSV, which contain x and y axis rotation
int xcol = 2;
int ycol = 3;

//Number of header rows (column titles etc.), which should be discarded 
int startRow = 1;

//Set true if you want a colored heatmap, false for black & white
boolean useColor = false;

int resX = 1920;
int resY = 960;
int gazeRadius = 50;

size(1920, 1080);  

String[] lines = loadStrings(filename);





//Create Heatmap array and set values to 0
float[][] heatmap = new float[resX][resY];
for (int i=0; i<resX; i++) {
  for (int j=0; j<resY; j++) {
    heatmap[i][j] = 0f;
  }
}

//Iterate through datasets and fill heatmap
for (int i=startRow; i<lines.length; i++) {
  String[] detail = split(lines[i], ";");

  float x = float(detail[xcol]);
  float y = float(detail[ycol]);

  //Catch case where head is tilted more than 90° upwards
  if (x>90&&x<270) {
    y+=180;
    y = y%360;
    x+=180;
    x = x%360;
  }

  //TODO: Maybe normalize rotation here (0° in the middle or edge?)
  x = (x+180)%360;
  y = (y+180)%360;

  int gazeY = int(map(x, 90, 270, 0, resY));
  int gazeX = int(map(y, 0, 360, 0, resX));

  //Increase Heatmap value 
  for (int u = gazeX-gazeRadius; u<= gazeX+gazeRadius; u++) {
    for (int v = gazeY-gazeRadius; v<=gazeY+gazeRadius; v++) {
      //TODO: Make gazeRadius circular
      if (u>=0&&u<resX &&v>=0&&v<resY) {
        if (sqrt(pow((u-gazeX), 2)+pow((v-gazeY), 2)) <=gazeRadius) {
          heatmap[u][v]+=1f;
        }
      }
    }
  }
}


//find most gazed point
float max = 0f;
for (int i=0; i<resX; i++) {
  for (int j=0; j<resY; j++) {
    if (heatmap[i][j] >max) {
      max=heatmap[i][j];
    }
  }
}
println(max);

//equalize Heatmap to 0-1
for (int i=0; i<resX; i++) {
  for (int j=0; j<resY; j++) {
    heatmap[i][j] = heatmap[i][j]/max;
  }
}

PImage bar = loadImage("colors.png");
bar.loadPixels();

//Create Image
PImage out = createImage(resX, resY, RGB);
out.loadPixels();
for (int i=0; i<resY; i++) {
  for (int j=0; j<resX; j++) {
    if (useColor) {
      int c = int(heatmap[j][i] * 999f);
      out.pixels[(i*resX)+j] = bar.pixels[c];
    } else {
      int c = int(heatmap[j][i] * 235f);
      if(c>0f)c+=20f;
      out.pixels[(i*resX)+j] = color(c, c, c);
    }
  }
}

out.updatePixels();
image(out, 0, 0, 1920, 960);

String savename = (useColor?"heatmap_color.png":"heatmap_bw.png");
out.save(savename);
exit();
