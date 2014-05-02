using System;
using System.IO;


var ORIGINALS_DIR_NAME = "Originals";
var NEW_IMAGE_WIDTH    = 800;
var RESIZED_DIR_NAME   = "Resized";


Console.WriteLine("\nStarting ...\n");


// ensure originals directory exists
if(Directory.Exists(ORIGINALS_DIR_NAME)) {
    Console.WriteLine("Directory exists: " + ORIGINALS_DIR_NAME);
} else {
    Console.WriteLine("Creating directory: " + ORIGINALS_DIR_NAME);
    Directory.CreateDirectory(ORIGINALS_DIR_NAME);
}


// check if image magick installed
System.Diagnostics.Process process = new System.Diagnostics.Process();
System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
startInfo.UseShellExecute = false;
startInfo.RedirectStandardOutput = true;
startInfo.FileName = "which";
startInfo.Arguments = "convert";
process.StartInfo = startInfo;
process.Start();
string processOutput = process.StandardOutput.ReadToEnd();
process.WaitForExit();
Console.WriteLine("Process Output type: " + processOutput.GetType());
Console.WriteLine("Process Output length: " + processOutput.Length);
Console.WriteLine("Process Output: " + processOutput);

if(processOutput.Length == 0) {
    Console.WriteLine("Cannot find image magick to process files.\n");
    Environment.Exit(0);
}


// get file names
string[] image_files1 = Directory.GetFiles(".", "*.jpg");
Console.WriteLine("image_files1 length = " + image_files1.Length);
string[] image_files2 = Directory.GetFiles(".", "*.JPG");
Console.WriteLine("image_files2 length = " + image_files2.Length);
List<String> imageFilesList = new List<String>();
imageFilesList.AddRange(image_files1);
imageFilesList.AddRange(image_files2);
Console.WriteLine("image files list = " + imageFilesList);
Console.WriteLine("image files list length = " + imageFilesList.Count);


string[] imageFiles = new string[imageFilesList.Count];
for(int i=0; i < imageFilesList.Count; i++) {
    Console.WriteLine("Image: " + imageFilesList[i]);
    imageFiles[i] = imageFilesList[i].Substring(2);
}


// make copy of images
System.Diagnostics.Process processCopies = null;
System.Diagnostics.ProcessStartInfo startInfo2 = null;
for(int i=0; i < imageFiles.Length; i++) {
    processCopies = new System.Diagnostics.Process();
    startInfo2 = new System.Diagnostics.ProcessStartInfo();
    startInfo2.UseShellExecute = false;
    startInfo2.RedirectStandardOutput = true;
    startInfo2.FileName = "cp";
    startInfo2.Arguments = imageFiles[i] + " ./" + ORIGINALS_DIR_NAME + "/" + imageFiles[i];
    processCopies.StartInfo = startInfo2;
    processCopies.Start();
    string processOutput = processCopies.StandardOutput.ReadToEnd();
    processCopies.WaitForExit();
    Console.WriteLine("Process Output type: " + processOutput.GetType());
    Console.WriteLine("Process Output length: " + processOutput.Length);
    Console.WriteLine("Process Output: " + processOutput);

    if(processOutput.Length == 0) {
        // Console.WriteLine("Process output length is 0.\n");
    } else {
        Console.WriteLine("Problem making copies.\n");
        Environment.Exit(0);
    }
}


// ensure resize directory exists
if(Directory.Exists(RESIZED_DIR_NAME)) {
    Console.WriteLine("Directory exists: " + RESIZED_DIR_NAME);
} else {
    Console.WriteLine("Creating directory: " + RESIZED_DIR_NAME);
    Directory.CreateDirectory(RESIZED_DIR_NAME);
}


// make small images
System.Diagnostics.Process processSmallImages = null;
System.Diagnostics.ProcessStartInfo startInfoSmallImages = null;
for(int i=0; i < imageFiles.Length; i++) {
    processSmallImages = new System.Diagnostics.Process();
    startInfoSmallImages = new System.Diagnostics.ProcessStartInfo();
    startInfoSmallImages.UseShellExecute = false;
    startInfoSmallImages.RedirectStandardOutput = true;
    startInfoSmallImages.FileName = "convert";
    // startInfoSmallImages.Arguments = imageFiles[i] + " ./" + ORIGINALS_DIR_NAME + "/" + imageFiles[i];
    string[] filename_parts = imageFiles[i].Split(new string[] {"."}, StringSplitOptions.None);
    string new_filename = "./" + RESIZED_DIR_NAME + "/" + filename_parts[0] + "_w" + NEW_IMAGE_WIDTH + "." + filename_parts[1];
    startInfoSmallImages.Arguments = imageFiles[i] + " -resize " + NEW_IMAGE_WIDTH + " " + new_filename;
    processSmallImages.StartInfo = startInfoSmallImages;
    processSmallImages.Start();
    string processOutput = processSmallImages.StandardOutput.ReadToEnd();
    processSmallImages.WaitForExit();
    Console.WriteLine("Process Output type: " + processOutput.GetType());
    Console.WriteLine("Process Output length: " + processOutput.Length);
    Console.WriteLine("Process Output: " + processOutput);

    if(processOutput.Length == 0) {
        // Console.WriteLine("Process output length is 0.\n");
    } else {
        Console.WriteLine("Problem making copies.\n");
        Environment.Exit(0);
    }
}

Console.WriteLine("\nDone.");



