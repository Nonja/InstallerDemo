using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class SetupPresenter
    {
        ISetupView mview;
        public SetupPresenter(ISetupView view)
        {
            mview = view;
        }

        public void StartInstall()
        {
            try
            {
                SetupModel model = new SetupModel();
                DirectoryInfo source = new DirectoryInfo(model.sourceDirectory);
                DirectoryInfo di = new DirectoryInfo(mview.destinationDirectory);

                //Check if chosen destination directory path exists 
                if (!Directory.Exists(mview.destinationDirectory))
                {
                    mview.Message("Directory doesn't exists, choose another");
                    return;
                }

                //Create root directory
                DirectoryInfo dest = di.CreateSubdirectory("InstallDemo");

                //Copy contents from source to destination
                CopyDirectory(source, dest);

            }

            catch (UnauthorizedAccessException ex)
            {
                mview.Message(ex.Message);
                return;
            }
            catch (ArgumentException ex)
            {
                mview.Message(ex.Message);
                return;
            }

        }


        //Count files in folder and subfolders
        private string CountFiles(string folder)
        {
            int count = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Length;
            return count.ToString();
        }


        //Copy files of source folder to destination folder
        private async void CopyDirectory(DirectoryInfo sourceFolder, DirectoryInfo destFolder)
        {

            foreach (string filename in Directory.EnumerateFiles(sourceFolder.FullName))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(destFolder + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        await SourceStream.CopyToAsync(DestinationStream);
                        UpdateProgress();
                    }
                }
            }

            //Copy each subdirectory using recursion.
            foreach (DirectoryInfo sourceSubDir in sourceFolder.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = destFolder.CreateSubdirectory(sourceSubDir.Name);
                CopyDirectory(sourceSubDir, nextTargetSubDir);
            }
        }


        //Check and update progress of copying
        private void UpdateProgress()
        {
            DirectoryInfo dest = new DirectoryInfo(mview.destinationDirectory);
            DirectoryInfo di = dest.GetDirectories("InstallDemo").First();
            string progress = CountFiles(dest.FullName);
            mview.progressOfFiles = progress;
            string files = mview.numberOfFiles;

            if (files == progress)
            {
                mview.InstallDone();
                return;
            }
           
        }


        //Count and set the number of files to copy
        internal void sourceInfo()
        {
            SetupModel model = new SetupModel();
            DirectoryInfo source = new DirectoryInfo(model.sourceDirectory);
            string count = CountFiles(source.FullName);
            mview.numberOfFiles = (count);
        }
    }
}
